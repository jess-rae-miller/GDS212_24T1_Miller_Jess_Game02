using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public bool playerWalking;
    public Transform player;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void GoToItem(ItemData item)
    {
        StartCoroutine(gameManager.MoveToPoint(player, item.goToPoint.position));

        SpriteAnimator playerAnimator = player.GetComponent<SpriteAnimator>();
        if (playerAnimator != null)
        {
            StartCoroutine(playerAnimator.PlayAnimation(gameManager.playerAnimations[1]));
        }
        else
        {
            Debug.LogError("SpriteAnimator component not found on player.");
        }

        playerWalking = true;
        TryGettingItem(item);
        StartCoroutine(UpdateSceneAfterAction(item));
    }

    private void TryGettingItem(ItemData item)
    {
        if (item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID))
        {
            GameManager.collectedItems.Add(item.itemID);
        }
    }

    private IEnumerator UpdateSceneAfterAction(ItemData item)
    {
        while (playerWalking)
        {
            yield return new WaitForSeconds(0.05f);
        }

        foreach (GameObject g in item.objectsToRemove)
        {
            Destroy(g);
            yield return null;
        }
    }
}
