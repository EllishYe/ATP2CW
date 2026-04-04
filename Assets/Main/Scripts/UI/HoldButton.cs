using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("UI")]
    public Image fillImage;
    public GameObject buttonRoot; // Parent object to hide button
    

    [Header("Speed")]
    public float fillSpeed = 1f;
    public float decaySpeed = 0.5f;
    

    private float progress = 0f;
    private bool isHolding = false;
    private bool isCompleted = false;

    [Header("Glow Effect")]
    public Image glowImage;
    public float glowDuration = 1f;
    private bool isPlayingCompleteEffect = false;

    void Update()
    {
        // ⭐ 如果已经完成，直接锁死
        if (isCompleted || isPlayingCompleteEffect) return;

        if (isHolding)
        {
            progress += fillSpeed * Time.deltaTime;
        }
        else
        {
            progress -= decaySpeed * Time.deltaTime;
        }

        progress = Mathf.Clamp01(progress);
        fillImage.fillAmount = progress;

        // ⭐ 完成逻辑
        if (progress >= 1f)
        {
            Complete();
        }
    }

    void Complete()
    {
        isCompleted = true;
        progress = 1f;
        fillImage.fillAmount = 1f;

        Debug.Log("ButtonClicked");

        isHolding = false;

        StartCoroutine(PlayCompleteEffect());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isCompleted) return;
        isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isCompleted) return;
        isHolding = false;
    }


    #region External API: Reset button
    public void ResetButton()
    {
        isCompleted = false;
        isHolding = false;
        progress = 0f;

        fillImage.fillAmount = 0f;

        if (buttonRoot != null)
        {
            buttonRoot.SetActive(true);
        }
        else
        {
            gameObject.SetActive(true);
        }

    }

    #endregion

    #region Complete Effect (Glow+Hide)
    System.Collections.IEnumerator PlayCompleteEffect()
    {
        isPlayingCompleteEffect = true;

        float timer = 0f;

        // Glow effect: alpha 0->1
        while (timer < glowDuration)
        {
            timer += Time.deltaTime;
            float t = timer / glowDuration;

            if (glowImage != null)
            {
                Color c = glowImage.color;
                c.a = t; // alpha 0→1
                glowImage.color = c;
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        // Hide the button after the effect
        if (buttonRoot != null)
        {
            buttonRoot.SetActive(false);
        }
        else {
            gameObject.SetActive(false);
        }
            
    }
    #endregion

}