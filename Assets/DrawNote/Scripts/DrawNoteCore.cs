using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawNoteCore : MonoBehaviour
{
    public bool drawing = true;

    // draw targets (create one new draw target while drawing, or for color switch)
    // save seperately to undo and redo draw last
    private Transform drawingsParent;
    public GameObject drawNoteTarget;

    /// <summary>
    /// Default target drawing distance is 64 centimeters away (approx average human arm length) from the User,
    /// DrawPlane GameObject's center is set to 1 meter away with a depth/thickness of 72 centimeters (to more safely catch raycast)
    /// So if the User draws directly in the middle of where their looking, that ray would hit the collider at 64 centimeters away
    /// With the User's arm in a comfortable position the drawing should display just in front of that
    /// </summary>
    public BoxCollider drawPlane;

    [SerializeField]
    [Tooltip("Current drawing and all saved drawings.")]
    private List<DrawNoteTargetModel> drawNoteTargets = new List<DrawNoteTargetModel>();

    [Tooltip("The index location in drawNoteTargets where you are currently or about to draw in.")]
    public int curDrawIndex = 0;

    /// <summary>
    /// Mode to draw notes (Normal, Mesh, etc....)
    /// </summary>
    public DrawNoteType curMode;

    private MixedRealityPose pose;


    public Material drawMaterial;
    public Color[] colorSwatches = new Color[4];
    public Color drawColor = Color.white;

    public SmallDrawingHUD instanceSmallDrawingHUD;

    private void Start()
    {
        instanceSmallDrawingHUD.SetColorBlockOptions(colorSwatches);
        instanceSmallDrawingHUD.SetColorSelectedIndicator(drawColor);
        instanceSmallDrawingHUD.SetVisibility(drawing, true);
        instanceSmallDrawingHUD.SetModeText(curMode.ToString());
    }

    public enum DrawNoteType
    {
        /// <summary>
        /// Draw only on draw plane with hand
        /// </summary>
        Normal,
        /// <summary>
        /// Draw only on meshes with hand
        /// </summary>
        Mesh,
        /// <summary>
        /// Draw from your finger
        /// </summary>
        Finger
    }
    void Update()
    {
        // update transform here intead of parenting gameobject to the camera which MRT gives error
        if (CameraCache.Main != null)
        {
            transform.position = CameraCache.Main.transform.position;
            transform.rotation = CameraCache.Main.transform.rotation;
        }

        instanceSmallDrawingHUD.SetVisibility(drawing);

        // show draw plane
        bool showDrawPlane = false;
        if (drawing && curMode == DrawNoteType.Normal)
        {
            showDrawPlane = true;
        }
        if (drawPlane.enabled != showDrawPlane)
        {
            drawPlane.enabled = showDrawPlane;
        }

        if (drawing)
        {
            TryDrawNote(curMode);
        }
        else
        {
            if (drawPlane.enabled)
            {
                drawPlane.enabled = false;
            }
        }
    }
    /// <summary>
    /// Try to Draw a Note provided The User's MRTK2 Hand pointer's are hitting the objective hit location for the DrawNoteType specified
    /// </summary>
    /// <param name="instanceType"></param>
    private void TryDrawNote(DrawNoteType instanceType)
    {
        // confirm any wrist is detected to draw
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Any, out pose) == false)
        {
            return;
        }

        bool foundDrawPositon = false;
        Vector3 drawPosition = Vector3.zero;

        // if drawing from finger just find the object
        if (curMode == DrawNoteType.Finger)
        {
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Any, out pose))
            {
                foundDrawPositon = true;
                drawPosition = pose.Position;
            }
        }
        else
        {
            // foreach loop below adapted from Julia Schwarz stack overflow
            foreach (var source in CoreServices.InputSystem.DetectedInputSources)
            {
                if (source.SourceType == InputSourceType.Hand)
                {
                    foreach (var p in source.Pointers)
                    {
                        // only get far hand pointers not close
                        if (p is IMixedRealityNearPointer)
                        {
                            continue;
                        }
                        if (p.Result != null)
                        {
                            var startPoint = p.Position;
                            var endPoint = p.Result.Details.Point;
                            var hitObject = p.Result.Details.Object;
                            if (hitObject)
                            {
                                // settings for draw note type
                                if (instanceType == DrawNoteType.Normal)
                                {
                                    if (hitObject.transform.name != "DrawPlane")
                                    {
                                        continue;
                                    }
                                    // redundancy to double check as the draw plane should not be active in the scene
                                }
                                else if (instanceType == DrawNoteType.Mesh)
                                {
                                    if (hitObject.transform.name == "DrawPlane")
                                    {
                                        continue;
                                    }
                                }
                                foundDrawPositon = true;
                                drawPosition = endPoint;
                                break;
                            }
                        }
                    }
                }
            }
        }
        if (foundDrawPositon)
        {
            while (drawNoteTargets.Count <= curDrawIndex)
            {
                GameObject instanceDrawTargetGO = Instantiate(drawNoteTarget);
                instanceDrawTargetGO.transform.name = "NewDrawTarget(" + drawNoteTargets.Count + ")";
                instanceDrawTargetGO.transform.parent = drawingsParent;
                Material newMat = new Material(drawMaterial);
                newMat.color = drawColor;
                instanceDrawTargetGO.transform.GetChild(0).GetComponent<TrailRenderer>().material = newMat;
                DrawNoteTargetModel instanceDNTM = new DrawNoteTargetModel(drawNoteTargets.Count, false, instanceDrawTargetGO);
                drawNoteTargets.Add(instanceDNTM);
            }
            if (drawNoteTargets[curDrawIndex] != null)
            {
                drawNoteTargets[curDrawIndex].instanceGameObject.transform.position = drawPosition;
            }
        }
    }

    public void UpdateColor(int setColor)
    {
        // set future color to be picked when making a new Draw Note Target
        drawColor = colorSwatches[setColor];
        // start new drawing if object instance has not been created for this color
        if (curDrawIndex < drawNoteTargets.Count)
        {
            curDrawIndex += 1;
        }
        drawing = true;
        // update seslected color on Small Drawing HUD
        instanceSmallDrawingHUD.SetColorSelectedIndicator(drawColor);
    }
    /// <summary>
    /// Deactivate the most recent drawing
    /// </summary>
    public void Undo()
    {
        if (drawing)
        {
            drawing = false;
            curDrawIndex += 1;
        }
        for (int i = drawNoteTargets.Count - 1; i >= 0; i--)
        {
            if (drawNoteTargets[i].instanceGameObject.activeSelf)
            {
                drawNoteTargets[i].instanceGameObject.SetActive(false);
                // only undo 1 object at a time
                break;
            }
        }
    }
    /// <summary>
    /// Destroy all drawings
    /// </summary>
    public void Clear()
    {
        drawing = false;
        for (int i = 0; i < drawNoteTargets.Count; i++)
        {
            Destroy(drawNoteTargets[i].instanceGameObject);
        }
        drawNoteTargets.Clear();
        curDrawIndex = 0;
    }
    public void SwitchMode()
    {
        curMode++;
        if ((int)curMode >= Enum.GetNames(typeof(DrawNoteType)).Length)
        {
            curMode = 0;
        }
        instanceSmallDrawingHUD.SetModeText(curMode.ToString());
    }
}
