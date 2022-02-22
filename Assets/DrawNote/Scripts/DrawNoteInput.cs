using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawNoteInput : MonoBehaviour
{
    DrawNoteCore instanceDNC;
    void Awake()
    {
        instanceDNC = transform.GetComponent<DrawNoteCore>();
    }
    public void MenuButtonPressed(string buttonName)
    {
        Debug.Log($"Menu button pressed: {buttonName}");
        switch (buttonName)
        {
            case "Draw":
                instanceDNC.drawing = true;
                break;
            case "StopDraw":
                if (instanceDNC.drawing)
                {
                    instanceDNC.drawing = false;
                    instanceDNC.curDrawIndex += 1;
                }
                break;
            case "Color0":
                instanceDNC.UpdateColor(0);
                break;
            case "Color1":
                instanceDNC.UpdateColor(1);
                break;
        }
    }
}
