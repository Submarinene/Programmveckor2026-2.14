using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// A struct to hold all data for a single message
[System.Serializable]
public class ChatMessage
{
    [TextArea(3, 5)]
    public string message;
    public Sprite characterIcon;
    [Tooltip("How long to wait after this message is fully displayed before showing the next one.")]
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
    [SerializeField] private Image characterIcon;
 
    [Header("Conversation")]
    [SerializeField] private List<ChatMessage> conversation = new List<ChatMessage>();
    [SerializeField] private float typingSpeed = 0.05f;

    private bool conversationStarted = false;

    void Awake()
    {
        // Ensure the chat bubble is hidden at the start
        if (chatBubbleObject != null)
        {
            chatBubbleObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!conversationStarted && imageAnimator != null)
        {
            AnimatorStateInfo stateInfo = imageAnimator.GetCurrentAnimatorStateInfo(0);

            // Check if the specific state is playing and has finished
            if (stateInfo.IsName(fadeStateName) && stateInfo.normalizedTime >= 1.0f)
            {
                StartConversation();
            }
        }
    }

    void StartConversation()
    {
        conversationStarted = true;
        if (chatBubbleObject != null && messageText != null && characterIcon != null)
        {
            chatBubbleObject.SetActive(true);
            StartCoroutine(DisplayConversation());
        }
        else
        {
            Debug.LogError("One or more Chat UI Elements are not assigned in the Inspector!", this);
        }
    }

    IEnumerator DisplayConversation()
    {
        foreach (var chatMessage in conversation)
        {
            // Update character icon
            characterIcon.sprite = chatMessage.characterIcon;
            // Hide the icon if no sprite is provided for this message
            characterIcon.enabled = (chatMessage.characterIcon != null);

            // Type out the message
            yield return StartCoroutine(TypeText(messageText, chatMessage.message));

            // Wait for the specified delay after the message
            yield return new WaitForSeconds(chatMessage.delayAfterMessage);
        }

        // Optionally, hide the chat bubble after the conversation ends
        if (chatBubbleObject != null)
        {
            chatBubbleObject.SetActive(false);
        }
    }

    IEnumerator TypeText(TMP_Text textComp, string message)
    {
        textComp.text = "";
        foreach (char letter in message)
        {
            textComp.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}