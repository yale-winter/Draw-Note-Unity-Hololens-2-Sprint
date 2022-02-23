using UnityEngine;

public class SmallDrawingHUD : MonoBehaviour
{
    public GameObject drawingNowMenu;
    public GameObject drawingDetails;
    private bool lastSet = true;

    public void SetVisibility(bool drawingNow, bool forceUpdate = false)
    {
        if (lastSet != drawingNow || forceUpdate)
        {
            lastSet = drawingNow;
            if (drawingNow)
            {
                drawingNowMenu.SetActive(true);
                drawingDetails.SetActive(false);
            }
            else
            {
                drawingNowMenu.SetActive(false);
                drawingDetails.SetActive(true);
            }
        }
    }
}
