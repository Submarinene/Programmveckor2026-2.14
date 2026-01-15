using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// A struct to hold all data for a single message
[System.Serializable]
public class ChatMessage
{
    [TextArea(3, 5)]
    public string message;
    public Sprite characterIcon;
    [Tooltip("How long to wait after message is fully displayed before next.")]
    public float delayAfterMessage = 2.0f;
}

public class ChatBubbles : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private Animator imageAnimator;
    [SerializeField] private string fadeStateName = "FadeOut";
 
    [Header("Chat UI Elements")]
    [Tooltip("The parent GameObject of the chat UI that will be enabled/disabled.")]
    [SerializeField] private GameObject chatBubbleObject;
    [Tooltip("The TextMeshPro component where messages will be displayed.")]
    [SerializeField] private TMP_Text messageText;
    [Tooltip("The Image component for the character's icon.")]

    [Header("Conversation")]
    [SerializeField] private List<ChatMessage> conversation = new List<ChatMessage>();
    [SerializeField] private float typingSpeed = 0.01f;

    [Header("Input")]
    [SerializeField] private KeyCode advanceKey = KeyCode.Space;
    [SerializeField] private bool requireInputToAdvance = true;

    [Header("Skip Conversation")]
    [SerializeField] private GameObject skipConfirmationObject;
    [SerializeField] private GameObject skipButton;
    [SerializeField] private TMP_Text skipConfirmationText;
    [SerializeField] private string skipConfirmationMessage = "Are you sure you want to skip? Click Skip again to confirm.";
    [SerializeField] private float skipConfirmationTimeout = 3.0f;

    [Header("Scene Transition")]
    [SerializeField] private Animator sceneTransitionAnimator;
    [SerializeField] private string endTransitionTrigger = "FadeIn";
    [SerializeField] private float transitionDuration = 1.0f;
    [SerializeField] private string nextSceneName;

    private bool conversationStarted = false;
    private bool endingSequenceStarted = false;
    private bool isTyping = false;
    private bool skipTypingRequested = false;
    private bool advanceRequested = false;
    private bool skipArmed = false;
    private float skipArmedUntil = 0.0f;
    private Coroutine conversationCoroutine;

    void Awake()
    {
        // Ensure the chat bubble is hidden at the start
        if (chatBubbleObject != null)
        {
            chatBubbleObject.SetActive(false);
        }

        if (skipButton != null)
        {
            skipButton.SetActive(false);
        }

        ResetSkipConfirmation();
    }

    void Start()
    {
        // Start conversation immediately if no animator is set up
        if (imageAnimator == null && !conversationStarted)
        {
            StartConversation();
        }
    }

    void Update()
    {
        if (!conversationStarted && !endingSequenceStarted && imageAnimator != null)
        {
            AnimatorStateInfo stateInfo = imageAnimator.GetCurrentAnimatorStateInfo(0);

            // Check if the specific state is playing and has finished
            if (stateInfo.IsName(fadeStateName) && stateInfo.normalizedTime >= 1.0f)
            {
                StartConversation();
            }
        }

        if (conversationStarted && !endingSequenceStarted)
        {
            if (Input.GetKeyDown(advanceKey))
            {
                if (isTyping)
                {
                    skipTypingRequested = true;
                }
                else
                {
                    advanceRequested = true;
                }
            }
        }

        if (skipArmed && Time.unscaledTime > skipArmedUntil)
        {
            ResetSkipConfirmation();
        }
    }

    void StartConversation()
    {
        if (endingSequenceStarted)
        {
            return;
        }

        conversationStarted = true;
        if (chatBubbleObject != null && messageText != null)
        {
            chatBubbleObject.SetActive(true);
            skipButton.SetActive(true);
            conversationCoroutine = StartCoroutine(DisplayConversation());
        }
        else
        {
            Debug.LogError("Chat bubble or message text not assigned in the Inspector", this);
        }
    }

    IEnumerator DisplayConversation()
    {
        for (int i = 0; i < conversation.Count; i++)
        {
            var chatMessage = conversation[i];

            // Type out the message
            isTyping = true;
            skipTypingRequested = false;
            yield return TypeText(messageText, chatMessage.message);
            isTyping = false;

            // Wait for the specified delay after the message
            if (requireInputToAdvance)
            {
                advanceRequested = false;
                yield return new WaitUntil(() => advanceRequested || endingSequenceStarted);
            }
            else
            {
                yield return new WaitForSeconds(chatMessage.delayAfterMessage);
            }

            if (endingSequenceStarted)
            {
                yield break;
            }
        }

        // Optionally, hide the chat bubble after the conversation ends
        // Trigger the end transition (fade to black)
        // Load the next scene
        StartEndingSequence();
    }

    IEnumerator TypeText(TMP_Text textComp, string message)
    {
        textComp.text = "";
        foreach (char letter in message)
        {
            if (skipTypingRequested)
            {
                break;
            }

            textComp.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        textComp.text = message;
    }

    public void OnSkipConversationButtonPressed()
    {
        if (endingSequenceStarted)
        {
            return;
        }

        if (!skipArmed)
        {
            skipArmed = true;
            skipArmedUntil = Time.unscaledTime + skipConfirmationTimeout;

            if (skipConfirmationObject != null)
            {
                skipConfirmationObject.SetActive(true);
            }

            if (skipConfirmationText != null)
            {
                skipConfirmationText.text = skipConfirmationMessage;
            }

            return;
        }

        ResetSkipConfirmation();

        if (conversationCoroutine != null)
        {
            StopCoroutine(conversationCoroutine);
            conversationCoroutine = null;
        }

        StartEndingSequence();
    }

    private void StartEndingSequence()
    {
        if (endingSequenceStarted)
        {
            return;
        }

        endingSequenceStarted = true;
        ResetSkipConfirmation();

        if (chatBubbleObject != null)
        {
            chatBubbleObject.SetActive(false);
        }

        if(skipButton != null)
        {
            skipButton.SetActive(false);
        }

        StartCoroutine(EndConversationSequence());
    }

    private IEnumerator EndConversationSequence()
    {
        if (sceneTransitionAnimator != null)
        {
            sceneTransitionAnimator.SetTrigger(endTransitionTrigger);
            yield return new WaitForSeconds(transitionDuration);
        }

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void ResetSkipConfirmation()
    {
        skipArmed = false;
        skipArmedUntil = 0.0f;

        if (skipConfirmationObject != null)
        {
            skipConfirmationObject.SetActive(false);
        }
    }
}