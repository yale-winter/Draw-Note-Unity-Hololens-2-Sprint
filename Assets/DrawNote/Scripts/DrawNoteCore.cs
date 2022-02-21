using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawNoteCore : MonoBehaviour
{

    public bool drawing = false;
    private float startTime = Mathf.Infinity;

    // draw targets (create **ONE** new draw target while interacting or for color switch)
    // easily undo and redo draw last note with this list
    private Transform drawingsParent;
    public GameObject drawNoteTarget;
    [Tooltip("Current drawing and all saved drawings.")]
    private List<GameObject> drawNoteTargets = new List<GameObject>();
    [Tooltip("The index location in drawNoteTargets where you are currently or about to draw in.")]
    private int curDrawIndex = 0;
    private Transform cameraTransform;
    MixedRealityPose pose;
    public GameObject tempDebugObj;
    private GameObject instanceTempDebugObj;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        drawingsParent = new GameObject("Drawings Parent").transform;

        // disabled below for now: gives error (but works) to parent to camera in unity editor  - - - - - - -
        /*
        if (CameraCache.Main != null)
        {
            cameraTransform = CameraCache.Main.transform;
            if (cameraTransform != null)
            {
                transform.parent = cameraTransform;
            }
        }
        */


        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        instanceTempDebugObj = Instantiate(tempDebugObj);
        instanceTempDebugObj.transform.name = "Temp Debug Object";
        instanceTempDebugObj.transform.localScale = new Vector3(0.05F, 0.05F, 0.05F);
        //drawNoteTarget = new GameObject("drawNoteTarget").transform;
    }
    void Update()
    {
        if (CameraCache.Main != null)
        {
            transform.position = CameraCache.Main.transform.position;
            transform.rotation = CameraCache.Main.transform.rotation;
        }





            bool foundDrawPositon = false;
        Vector3 drawPosition = Vector3.zero;
        
        foreach (var source in CoreServices.InputSystem.DetectedInputSources)
        {
            if (source.SourceType == InputSourceType.Hand)
            {
                foreach (var p in source.Pointers)
                {
                    if (p is IMixedRealityNearPointer)
                    {
                        break;
                    }
                    if (p.Result != null)
                    {
                        var startPoint = p.Position;
                        var endPoint = p.Result.Details.Point;
                        var hitObject = p.Result.Details.Object;
                        if (hitObject && endPoint != Vector3.zero) //hitObject.transform.name == "DrawPlane"
                        {
                            // confirm any wrist is detected
                            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Any, out pose) == false)
                            {
                                break;
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
            if (!drawing && startTime < Time.time - 1.0F)
            {
                drawing = true;
            }
            if (drawing)
            {
                while (drawNoteTargets.Count <= curDrawIndex)
                {
                    GameObject newDrawTarget = Instantiate(drawNoteTarget);
                    newDrawTarget.transform.name = "NewDrawTarget(" + drawNoteTargets.Count + ")";
                    newDrawTarget.transform.parent = drawingsParent;
                    drawNoteTargets.Add(newDrawTarget);
                }
                if (drawNoteTargets[curDrawIndex] != null)
                {
                     drawNoteTargets[curDrawIndex].transform.position = drawPosition;
                    instanceTempDebugObj.transform.position = drawPosition;
                }
            }
        }
    }
}
