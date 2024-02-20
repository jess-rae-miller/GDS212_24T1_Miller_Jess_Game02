using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    //Tutorial MoreBBlakey on yt

    public GameObject lazer;
    public Transform lazerPos;

    public float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(lazer, lazerPos.position, Quaternion.identity);
    }
}
