using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Tutorial by PawelMakesGames on yt

    static float moveSpeed = 3.5f, moveAccuracy = 0.15f;
    public static List<int> collectedItems = new List<int>();
    public AnimationData[] playerAnimations;
    public RectTransform nameTag, hintBox;

    public IEnumerator MoveToPoint(Transform myObject, Vector2 point)
    {
        Vector2 positionDifference = point - (Vector2)myObject.position;
        while (positionDifference.magnitude > moveAccuracy)
        {
            myObject.Translate(moveSpeed * positionDifference.normalized * Time.deltaTime);
            positionDifference = point - (Vector2)myObject.position;
            yield return null;
        }

        myObject.position = point;

        if (myObject == FindObjectOfType<ClickManager>().player) 
        {
            FindObjectOfType<ClickManager>().playerWalking = false;
        }
        yield return null;
    }

    public void UpdateHintBox(ItemData item, bool playerFlipped)
    {
        if(item == null)
        {
            hintBox.gameObject.SetActive(false);
            return;
        }

        hintBox.gameObject.SetActive(true);

        hintBox.GetComponentInChildren<TextMeshProUGUI>().text = item.messageHint;
        hintBox.sizeDelta = item.hintBoxSize;

        if(playerFlipped)
        hintBox.parent.localPosition = new Vector2 (0, 2);

        else
        {
            hintBox.parent.localPosition = new Vector2 (0, 2);
        }
    }
}
