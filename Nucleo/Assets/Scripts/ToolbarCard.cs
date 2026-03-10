using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ToolbarCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform infoPanel;
    public float animationSpeed = 0.3f;
    public float animationDelay = 0.1f;

    private Animator animator;
    private Vector2 infoHiddenPos = new Vector2(0, -60);
    private Vector2 infoVisiblePos = Vector2.zero;
    private Coroutine currentCoroutine;

    void Start()
    {
        animator = GetComponent<Animator>();
        
        if (infoPanel != null)
            infoPanel.anchoredPosition = infoHiddenPos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Stop any running coroutine
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        // Play animation to final frame
        if (animator != null)
            animator.SetTrigger("Hover");

        // Animate info panel to visible position
        currentCoroutine = StartCoroutine(AnimatePanelWithDelay(infoVisiblePos));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Stop any running coroutine
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        // Play animation back to first frame
        if (animator != null)
            animator.SetTrigger("Idle");

        // Animate info panel back to hidden position
        currentCoroutine = StartCoroutine(AnimatePanelWithDelay(infoHiddenPos));
    }

    IEnumerator AnimatePanelWithDelay(Vector2 targetPos)
    {
        yield return new WaitForSeconds(animationDelay);
        yield return StartCoroutine(AnimateInfoPanel(targetPos));
    }

    IEnumerator AnimateInfoPanel(Vector2 targetPos)
    {
        if (infoPanel == null)
            yield break;

        float elapsed = 0f;
        Vector2 startPos = infoPanel.anchoredPosition;

        while (elapsed < animationSpeed)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationSpeed);
            infoPanel.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        infoPanel.anchoredPosition = targetPos;
    }
}
