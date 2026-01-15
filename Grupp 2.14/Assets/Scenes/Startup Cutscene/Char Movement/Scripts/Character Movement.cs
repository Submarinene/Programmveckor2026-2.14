using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject Villager1;
    [SerializeField] private GameObject Villager2;
    [SerializeField] private GameObject Villager1TextBox;
    [SerializeField] private GameObject Villager2TextBox;
    [SerializeField] private Animator sceneTransitionAnimator;
    [SerializeField] private string endTransitionTrigger = "FadeIn";
    [SerializeField] private float transitionDuration = 1.0f;

    private Rigidbody2D rb;
    private float horizontalInput;
    private Vector3 initialScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        }

        if (horizontalInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = initialScale.x * Mathf.Sign(horizontalInput);
            transform.localScale = scale;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * walkSpeed, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Villager1"))
        {
            Villager1TextBox.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Villager2"))
        {
            Villager2TextBox.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("RightCollider"))
        {
            StartCoroutine(TransitionToCutscene());
        }
    }

    private IEnumerator TransitionToCutscene()
    {
        // Trigger the end transition (fade to black)
        if (sceneTransitionAnimator != null)
        {
            sceneTransitionAnimator.SetTrigger(endTransitionTrigger);
            yield return new WaitForSeconds(transitionDuration);
        }

        SceneManager.LoadScene("Cutscene 1");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Villager1"))
        {
            Villager1TextBox.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Villager2"))
        {
            Villager2TextBox.SetActive(false);
        }
    }
}
