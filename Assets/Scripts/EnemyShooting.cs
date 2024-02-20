using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    //Tutorial MoreBBlakey on yt

    public GameObject lazer;
    public Transform lazerPos;

    public float timer;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);

        if (distance < 3)
        {
            timer += Time.deltaTime;

            if (timer > 0.5)
            {
                timer = 0;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(lazer, lazerPos.position, Quaternion.identity);
    }
}
