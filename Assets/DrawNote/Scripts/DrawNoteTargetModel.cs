using System;
using UnityEngine;
[Serializable]
public class DrawNoteTargetModel
{
    public int instanceID;
    public bool instanceArtExists;
    public GameObject instanceGameObject;
    public DrawNoteTargetModel(int setID, bool setArtExists, GameObject setGameObject)
    {
        instanceID = setID;
        instanceArtExists = setArtExists;
        instanceGameObject = setGameObject;
    }
}
