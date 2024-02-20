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
        player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[1]);
        playerWalking = true;
        TryGettingItem(item);

    }

    private void TryGettingItem(ItemData item)
    {
        bool canGetItem = item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID);
        if (canGetItem)
        {
            GameManager.collectedItems.Add(item.itemID);
        }
        StartCoroutine(UpdateSceneAfterAction(item, canGetItem));
    }

    private IEnumerator UpdateSceneAfterAction(ItemData item, bool canGetItem)
    {
        while (playerWalking)
        {
            yield return new WaitForSeconds(0.05f);
        }

        if (canGetItem)
        {
            foreach (GameObject g in item.objectsToRemove)

            Destroy(g);
            Debug.Log("item collected");
        }

            player.GetComponent<SpriteAnimator>().PlayAnimation(null);

            yield return null;
    }
}

