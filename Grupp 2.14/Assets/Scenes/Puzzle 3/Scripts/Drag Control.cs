using System.Collections;
using UnityEngine;

public class DragControl : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject wordBox;
    [SerializeField] GameObject correctBlankSpace;
    [SerializeField] float fadeDuration = 0.3f;
    [SerializeField] float outlineThickness = 0.1f;

    private Vector3 ogWorldBoxPos;
    private bool isPlaced = false;
    private bool isTouchingCorrectBlank = false;
    private SpriteRenderer spriteRenderer;
    private GameObject glowOutline;
    private SpriteRenderer glowRenderer;
    private Coroutine glowCoroutine;

    void Start()
    {
        spriteRenderer = wordBox.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        ogWorldBoxPos = wordBox.transform.position;

        CreateGlowOutline();
    }

    void CreateGlowOutline()
    {
        if (spriteRenderer == null) return;

        glowOutline = new GameObject("GlowOutline");
        glowOutline.transform.SetParent(wordBox.transform);
        glowOutline.transform.localPosition = Vector3.zero;
        glowOutline.transform.localRotation = Quaternion.identity;
        glowOutline.transform.localScale = Vector3.one * (1f + outlineThickness);

        glowRenderer = glowOutline.AddComponent<SpriteRenderer>();
        glowRenderer.sprite = spriteRenderer.sprite;
        glowRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
        glowRenderer.material = spriteRenderer.material;

        glowOutline.transform.localScale = Vector3.one * (1f + outlineThickness * 2f);
        
        glowOutline.SetActive(false);
    }

    void OnMouseDrag()
    {
        if (isPlaced) return;

        rb.position = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            -Camera.main.transform.position.z));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Blank Space"))
        {
            if (collision.gameObject == correctBlankSpace)
            {
                isTouchingCorrectBlank = true;
            }
            else
            {
                isTouchingCorrectBlank = false;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Blank Space"))
        {
            if (collision.gameObject == correctBlankSpace)
            {
                isTouchingCorrectBlank = false;
            }
        }
    }

    void OnMouseUp()
    {
        if (isPlaced) return;

        if (isTouchingCorrectBlank)
        {
            rb.position = correctBlankSpace.transform.position;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.bodyType = RigidbodyType2D.Kinematic;
            isPlaced = true;
        }
        else
        {
            ShowGlow();
            rb.position = ogWorldBoxPos;
        }
    }

    void ShowGlow()
    {
        if (glowOutline == null || glowRenderer == null)
        {
            return;
        }

        if (glowCoroutine != null)
        {
            StopCoroutine(glowCoroutine);
        }

        glowCoroutine = StartCoroutine(FadeGlowOutline());
    }

    IEnumerator FadeGlowOutline()
    {
        glowOutline.SetActive(true);
        
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            float alpha = Mathf.Lerp(0f, 1f, t);
            
            Color currentColor = glowRenderer.color;
            currentColor.a = alpha;
            glowRenderer.color = currentColor;
            
            float scale = 1f + (outlineThickness * 2f) + (t * 0.1f);
            glowOutline.transform.localScale = Vector3.one * scale;
            
            yield return null;
        }

        yield return new WaitForSeconds(0.15f);

        elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            float alpha = Mathf.Lerp(1f, 0f, t);
            
            Color currentColor = glowRenderer.color;
            currentColor.a = alpha;
            glowRenderer.color = currentColor;
            
            float scale = 1f + (outlineThickness * 2f) + ((1f - t) * 0.1f);
            glowOutline.transform.localScale = Vector3.one * scale;
            
            yield return null;
        }

        glowOutline.SetActive(false);
        glowCoroutine = null;
    }
}