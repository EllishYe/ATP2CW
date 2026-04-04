using UnityEngine;
using UnityEngine.UI;

public class ScratchCardCanvasController : MonoBehaviour
{
    public Image frontImage;
    public Image backImage;

    void Start() {
        Hide(frontImage);
        Hide(backImage);
    }

    public void ReceiveItem(ItemDetails itemdetails) {
        frontImage.sprite = itemdetails.frontSprite;
        backImage.sprite = itemdetails.backSprite;
    }
    public void Show(Image image)
    {
        image.gameObject.SetActive(true);
    }
    public void Hide(Image image)
    {
        image.gameObject.SetActive(false);
    }


}
