using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Terminal : ItemData
{
    public bool isUnlocked = false;
    public GameObject codes;
    public GameObject unlockedObject;

    private void Start()
    {
        unlockedObject.SetActive(false);
    }
    public void UnlockTerminal()
    {
        isUnlocked = true;
        objectsToAdd.Add(unlockedObject);
    }
    private void Update()
    {
        if (codes == null)
        {
            UnlockTerminal();
        }
    }
}
