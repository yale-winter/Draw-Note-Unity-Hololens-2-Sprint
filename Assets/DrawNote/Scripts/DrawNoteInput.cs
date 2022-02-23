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

    public enum DrawNoteAction
    {
        None,
        Draw,
        StopDraw,
        White,
        Red,
        Yellow,
        Green,
        Undo
    }
    public void DrawNoteActionTaken(int setActionInt)
    {
        DrawNoteAction instanceAction = (DrawNoteAction)setActionInt;
        Debug.Log($"DrawNoteActionTaken: {instanceAction}");

        switch (instanceAction)
        {
            case DrawNoteAction.Draw:
                instanceDNC.drawing = true;
                break;
            case DrawNoteAction.StopDraw:
                if (instanceDNC.drawing)
                {
                    instanceDNC.drawing = false;
                    instanceDNC.curDrawIndex += 1;
                }
                break;
            case DrawNoteAction.White:
                instanceDNC.UpdateColor(0);
                break;
            case DrawNoteAction.Red:
                instanceDNC.UpdateColor(1);
                break;
            case DrawNoteAction.Yellow:
                instanceDNC.UpdateColor(2);
                break;
            case DrawNoteAction.Green:
                instanceDNC.UpdateColor(3);
                break;
            case DrawNoteAction.Undo:
                instanceDNC.Undo();
                break;
        }
    }
    public void Update()
    {
        /// temporary keyboard debug
        if (Input.GetKeyUp(KeyCode.H))
        {
            DrawNoteActionTaken(7);
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            DrawNoteActionTaken(3);
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            DrawNoteActionTaken(4);
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            DrawNoteActionTaken(2);
        }
    }
}
