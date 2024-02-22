using UnityEngine;

public class Terminal : MonoBehaviour
{
    public bool isUnlocked = false;
    public GameObject codes;
    public GameObject unlockedObject;
    public GameObject turretToDeactivate; // Reference to the turret GameObject to deactivate

    private void Start()
    {
        unlockedObject.SetActive(false);
    }

    public void UnlockTerminal()
    {
        isUnlocked = true;
        // Activate the unlocked object
        unlockedObject.SetActive(true);

        // Check if the turret GameObject reference is valid
        if (turretToDeactivate != null)
        {
            // Deactivate the turret GameObject
            turretToDeactivate.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Turret to deactivate is not assigned to the Terminal.");
        }
    }

    private void Update()
    {
        if (codes == null)
        {
            UnlockTerminal();
        }
    }
}
