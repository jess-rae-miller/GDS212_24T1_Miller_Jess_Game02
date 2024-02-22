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
        // Hide hint box when moving towards an item
        gameManager.UpdateHintBox(null, false);

        // Check if interaction with this item requires another item to be collected
        if (item.requiresItem && !GameManager.collectedItems.Contains(item.requiredItemID))
        {
            Debug.Log("You need to collect a specific item first.");
            return;
        }

        // Play walking animation
        player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[1]);

        // Set playerWalking flag to true
        playerWalking = true;

        // Start coroutine to move player towards the item's position
        StartCoroutine(gameManager.MoveToPoint(player, item.goToPoint.position));

        // Try to get the item
        TryGettingItem(item);
    }

    private void TryGettingItem(ItemData item)
    {
        // Check if the required item is already collected or if no item is required
        bool canGetItem = item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID);

        // If the item can be obtained, add it to the collected items list
        if (canGetItem)
        {
            GameManager.collectedItems.Add(item.itemID);
        }

        // Start coroutine to update scene after the action
        StartCoroutine(UpdateSceneAfterAction(item, canGetItem));
    }

    private IEnumerator UpdateSceneAfterAction(ItemData item, bool canGetItem)
    {
        // Wait until player stops walking
        while (playerWalking)
        {
            yield return new WaitForSeconds(0.05f);
        }

        // If the item can be obtained, remove specified objects
        if (canGetItem)
        {
            foreach (GameObject g in item.objectsToRemove)
            {
                Destroy(g);
            }
            foreach (GameObject g in item.objectsToAdd )
            {
                g.SetActive(true);
            }
        }
        else
        {
            // Show hint box if the item cannot be obtained
            gameManager.UpdateHintBox(item, player.GetComponentInChildren<SpriteRenderer>().flipX);
            gameManager.CheckSpecialConditions(item);

        }
            // Stop walking animation
            player.GetComponent<SpriteAnimator>().PlayAnimation(null);

            yield return null;
    }
}