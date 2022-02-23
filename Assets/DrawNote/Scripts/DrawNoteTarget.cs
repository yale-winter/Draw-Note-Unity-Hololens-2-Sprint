using UnityEngine;

public class DrawNoteTarget
{
    public int instanceID;
    public bool instanceArtExists;
    public GameObject instanceGameObject;
    public DrawNoteTarget(int setID, bool setArtExists, GameObject setGameObject)
    {
        instanceID = setID;
        instanceArtExists = setArtExists;
        instanceGameObject = setGameObject;
    }
}
