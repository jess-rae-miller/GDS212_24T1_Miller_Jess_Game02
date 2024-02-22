using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Concurrent;

public class GameManager : MonoBehaviour
{
    //Tutorial by PawelMakesGames on yt

    static float moveSpeed = 3.5f, moveAccuracy = 0.15f;
    public static List<int> collectedItems = new List<int>();

    [Header("Setup")]
    public AnimationData[] playerAnimations;
    public RectTransform nameTag, hintBox;

    [Header("Local scenes")]
    public Image blockingImage;
    public GameObject[] localScenes;
    int activeLocalScene = 0;
    public Transform[] playerStartPositions;

    public IEnumerator MoveToPoint(Transform myObject, Vector2 point)
    {
        Vector2 positionDifference = point - (Vector2)myObject.position;

        if (myObject.GetComponentInChildren<SpriteRenderer>() && positionDifference.x != 0)
        {
            myObject.GetComponentInChildren<SpriteRenderer>().flipX = positionDifference.x < 0;
        }

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

    public GameObject GetPlayerObject()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

    public void ResetPlayerPosition()
    {
        int activeSceneIndex = activeLocalScene;
        FindObjectOfType<ClickManager>().player.position = playerStartPositions[activeSceneIndex].position;
    }

    public void UpdateNameTag(ItemData item)
    {
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = item.objectName;

        nameTag.sizeDelta = item.nameTagSize;

        nameTag.localPosition = new Vector2(item.nameTagSize.x * 0, 0f);
    }
    public void UpdateHintBox(ItemData item, bool playerFlipped)
    {
        if (item == null)
        {
            hintBox.gameObject.SetActive(false);
            return;
        }

        hintBox.gameObject.SetActive(true);

        hintBox.GetComponentInChildren<TextMeshProUGUI>().text = item.messageHint;
        hintBox.sizeDelta = item.hintBoxSize;

        if (playerFlipped)
            hintBox.parent.localPosition = new Vector2(0, 2);

        else
        {
            hintBox.parent.localPosition = new Vector2(0, 2);
        }
    }
    public void CheckSpecialConditions(ItemData item)
    {
        switch (item.itemID)
        {
            case -11:
                StartCoroutine(ChangeScene(1, 0));
                break;

            case -12:
                StartCoroutine(ChangeScene(2, 0));
                break;

            case -13:
                StartCoroutine(ChangeScene(3, 0));
                break;

            case -1:
                StartCoroutine(ChangeScene(3, 1));
                break;
        }
    }

    public IEnumerator ChangeScene(int sceneNumber, float delay)
    {
        yield return new WaitForSeconds(delay);
        Color c = blockingImage.color;

        blockingImage.enabled = true;

        while (blockingImage.color.a < 1)
        {
            c.a += Time.deltaTime;
            blockingImage.color = c;
            yield return null;
        }

        localScenes[activeLocalScene].SetActive(false);

        localScenes[sceneNumber].SetActive(true);

        activeLocalScene = sceneNumber;

        FindObjectOfType<ClickManager>().player.position = playerStartPositions[sceneNumber].position;


        while (blockingImage.color.a > 0)
        {
            c.a -= Time.deltaTime;
            blockingImage.color = c;
            yield return null;
        }

        blockingImage.enabled = false;
        yield return null;
    }
}
