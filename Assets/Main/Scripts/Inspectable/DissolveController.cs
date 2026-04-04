using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DissolveController : MonoBehaviour
{
    [Header("Target")]
    public Image targetImage; // Scratch Surface Image - Back

    [Header("Shader Settings")]
    public string dissolveProperty = "_DissolveAmount";

    [Header("Timing")]
    public float duration = 1.5f;

    private Material runtimeMat;


    void Awake()
    {
        // Instantiate a copy of the material
        if (targetImage != null)
        {
            runtimeMat = Instantiate(targetImage.material);
            targetImage.material = runtimeMat;
        }
    }

    #region Public API
    public void PlayDissolve()
    {
        gameObject.SetActive(true);
        StartCoroutine(DissolveRoutine());
    }
    #endregion


    IEnumerator DissolveRoutine()
    {
        float timer = 0f;

        runtimeMat.SetFloat(dissolveProperty, 0f);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            runtimeMat.SetFloat(dissolveProperty, t);

            yield return null;
        }

        runtimeMat.SetFloat(dissolveProperty, 1f);

        yield return new WaitForSeconds(0.1f);

        targetImage.gameObject.SetActive(false);

        // Notify InspectManager
        FindObjectOfType<InspectManager>().OnDissolveFinished();
    }

}
