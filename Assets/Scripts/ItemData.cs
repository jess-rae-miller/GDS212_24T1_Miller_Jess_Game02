using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [Header("Setup")]
    public Transform goToPoint;
    public int itemID, requiredItemID;
    public string objectName;
    public Vector2 nameTagSize = new Vector2(3, 0.65f);

    [Header("Object Removal")]
    public GameObject [] objectsToRemove;

    [Header("Fail")]
    [TextArea(3,3)]
    public string messageHint;
    public Vector2 hintBoxSize = new Vector2(4, 0.65f);
}
