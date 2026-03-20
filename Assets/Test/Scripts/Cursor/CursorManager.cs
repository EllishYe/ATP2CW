using UnityEngine;


public class CursorManager : MonoBehaviour
{
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

    private bool canClick;

    private void Update()
    {
        canClick = ObjectAtMousePosition();

        if (canClick&&Input.GetMouseButtonDown(0))
        {
            //检测鼠标互动状况：空点，有碰撞体，UI元素等情况
            //如果有碰撞体，执行点击事件
            ClickAction(ObjectAtMousePosition().gameObject);
            
        }
    }

    /// <summary>
    /// 若点击对象含碰撞体且有特定标签，则执行相应事件
    /// </summary>
    /// <param name="clickObject"></param>
    private void ClickAction(GameObject clickObject) {
        switch (clickObject.tag) {
            case "Teleport":
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();//如果有Teleport组件则执行传送事件
                break;
            case "Item":
                var item = clickObject.GetComponent<Item>(); 
                item?.ItemClicked();
                break;
            //case "PuzzleTrigger":
            //    var PuzzleTrigger = clickObject.GetComponent<PuzzleTrigger>();
            //    PuzzleTrigger?.OnClick();
            //    PuzzleTrigger?.Disable();
            //    break;
            //case "PuzzleItem":
            //    var Puzzleitem = clickObject.GetComponent<PuzzleItem>();
            //    Puzzleitem?.OnClick();
            //    break;
            
        }
    }


    /// <summary>
    /// 检测鼠标点击范围的碰撞体
    /// </summary>
    /// <returns></returns>
    private Collider2D ObjectAtMousePosition() {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }
}
