using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawNoteCore : MonoBehaviour
{
    public bool drawing = false;
    private float startTime = Mathf.Infinity;

    // draw targets (create one new draw target while drawing, or for color switch)
    // save seperately to undo and redo draw last
    private Transform drawingsParent;
    public GameObject drawNoteTarget;
    public MeshCollider drawPlane;
    [Tooltip("Current drawing and all saved drawings.")]
    private List<GameObject> drawNoteTargets = new List<GameObject>();
    [Tooltip("The index location in drawNoteTargets where you are currently or about to draw in.")]
    public int curDrawIndex = 0;

    // temp debug visual objects
    public GameObject tempDebugObj;
    private GameObject instanceTempDebugObj;

    private MixedRealityPose pose;


    public Material drawMaterial;
    public Color[] colorSwatches = new Color[5];
    public Color color = Color.white;


    void Start()
    {
        startTime = Time.time;

        // temp debug object to indicate drawing location
        instanceTempDebugObj = Instantiate(tempDebugObj);
        instanceTempDebugObj.transform.name = "Temp Debug Object";
        instanceTempDebugObj.transform.localScale = new Vector3(0.05F, 0.05F, 0.05F);
    }

    private enum DrawNoteType
    {
        /// <summary>
        /// Draw only on draw plane with hand and only if it is first object (collider) hit.
        /// </summary>
        DrawPlane,
        /// <summary>
        /// Draw only on not draw plane with hand. Includes drawing on menus.
        /// </summary>
        NotDrawPlane,
        /// <summary>
        /// Draw only on draw plane with hand even if it's behind other objects (colliders).
        /// </summary>
        ForceDrawPlane,
        /// <summary>
        /// Draw on real world meshes, and any object with a collider that is not a menu and not draw plane.
        /// </summary>
        ForceNotDrawPlaneNotMenus
    }
    void Update()
    {
        // update transform here intead of parenting gameobject to the camera which MRT gives error
        if (CameraCache.Main != null)
        {
            transform.position = CameraCache.Main.transform.position;
            transform.rotation = CameraCache.Main.transform.rotation;
        }

        if (drawing)
        {
            if (!drawPlane.enabled)
            {
                drawPlane.enabled = true;
            }
            TryDrawNote(DrawNoteType.DrawPlane);
        }
        else
        {
            if (drawPlane.enabled)
            {
                drawPlane.enabled = false;
            }
        }
    }
    private void TryDrawNote(DrawNoteType instanceType)
    {
        bool foundDrawPositon = false;
        Vector3 drawPosition = Vector3.zero;
        foreach (var source in CoreServices.InputSystem.DetectedInputSources)
        {
            if (source.SourceType == InputSourceType.Hand)
            {
                foreach (var p in source.Pointers)
                {
                    // only get far hand pointers
                    if (p is IMixedRealityNearPointer)
                    {
                        break;
                    }
                    if (p.Result != null)
                    {
                        var startPoint = p.Position;
                        var endPoint = p.Result.Details.Point;
                        var hitObject = p.Result.Details.Object;
                        if (hitObject && endPoint != Vector3.zero)
                        {
                            // confirm any wrist is detected
                            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Any, out pose) == false)
                            {
                                break;
                            }

                            // settings for draw note type
                            if (instanceType == DrawNoteType.DrawPlane)
                            {
                                if (hitObject.transform.name != "DrawPlane")
                                {
                                    break;
                                }
                            }

                            foundDrawPositon = true;
                            drawPosition = endPoint;
                        }
                    }
                }
            }
        }
        if (foundDrawPositon)
        {
            while (drawNoteTargets.Count <= curDrawIndex)
            {
                GameObject newDrawTarget = Instantiate(drawNoteTarget);
                newDrawTarget.transform.name = "NewDrawTarget(" + drawNoteTargets.Count + ")";
                newDrawTarget.transform.parent = drawingsParent;
                Material newMat = new Material(drawMaterial);
                newMat.color = color;
                newDrawTarget.transform.GetChild(0).GetComponent<TrailRenderer>().material = newMat;
                drawNoteTargets.Add(newDrawTarget);
            }
            if (drawNoteTargets[curDrawIndex] != null)
            {
                drawNoteTargets[curDrawIndex].transform.position = drawPosition;
                instanceTempDebugObj.transform.position = drawPosition;
            }
        }
    }
    public void UpdateColor(int setColor)
    {
        // set future color to be picked when making a new Draw Note Target
        color = colorSwatches[setColor];

        // update current color if already drawing
        Material newMat = new Material(drawMaterial); 
        newMat.color = color;
        if (curDrawIndex < drawNoteTargets.Count)
        {
            drawNoteTargets[curDrawIndex].transform.GetChild(0).GetComponent<TrailRenderer>().material = newMat;
        }

    }
}
