using UnityEngine;

public class clickAnimation : MonoBehaviour
{
    public RectTransform targetImage;   // Assign UI Image
    public float maxSize = 100f;        // Final size
    public float speed = 100f;          // Pixels per second

    void Update()
    {
        if (targetImage == null) return;

        Vector2 size = targetImage.sizeDelta;

        // Increase size continuously
        size += new Vector2(speed * Time.deltaTime, speed * Time.deltaTime);

        // If reached max size, reset to 0
        if (size.x >= maxSize)
        {
            size = Vector2.zero;
        }

        targetImage.sizeDelta = size;
    }
}
