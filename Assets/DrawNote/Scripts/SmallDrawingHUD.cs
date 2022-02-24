using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmallDrawingHUD : MonoBehaviour
{
    public GameObject drawingNowMenu;
    public GameObject drawingDetails;
    private bool lastSet = true;
    public Image[] bigColorBlocks = new Image[2];
    public Image[] smallColorBlocks = new Image[4];
    public TextMeshProUGUI modeText;
    public void SetModeText(string setText)
    {
        string displayText = "Mode: " + setText;
        modeText.text = displayText;
    }
    public void SetColorBlockOptions(Color[] setColors)
    {
        for (int i = 0; i < smallColorBlocks.Length; i++)
        {
            smallColorBlocks[i].color = setColors[i];
        }
    }
    public void SetColorSelectedIndicator(Color setColor)
    {
        for (int i = 0; i < bigColorBlocks.Length; i++)
        {
            bigColorBlocks[i].color = setColor;
        }
    }
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
