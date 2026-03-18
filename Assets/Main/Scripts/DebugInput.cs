using UnityEngine;

public class DebugInput : MonoBehaviour
{
    public CodeUI codeUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            codeUI.SetCode(0, 3); // ÃÕÌâ1 ¡ú Êý×Ö3

        if (Input.GetKeyDown(KeyCode.Alpha2))
            codeUI.SetCode(1, 7);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            codeUI.SetCode(2, 1);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            codeUI.SetCode(3, 9);
    }
}