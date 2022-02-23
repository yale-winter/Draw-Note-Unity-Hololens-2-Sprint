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
        Undo,
        Clear
    }
    /// <summary>
    /// setActionInt param is same as DrawNoteAction enum, for reference:
    /// 0 None, 1 Draw, 2 StopDraw, 3 White, 4 Red, 5 Yellow, 6 Green, 7 Undo, 8 Clear
    /// </summary>
    /// <param name="setActionInt"></param>
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
            case DrawNoteAction.Clear:
                instanceDNC.Clear();
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
        if (Input.GetKeyUp(KeyCode.M))
        {
            DrawNoteActionTaken(7);
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            DrawNoteActionTaken(8);
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            DrawNoteActionTaken(1);
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            DrawNoteActionTaken(2);
        }
    }
}
