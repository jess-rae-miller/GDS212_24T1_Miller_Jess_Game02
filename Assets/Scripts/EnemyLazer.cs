using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazer : MonoBehaviour
{
    public float force;
    private Rigidbody2D rb;
    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager in the scene
        GameObject player = gameManager.GetPlayerObject(); // Get the player object from the GameManager

        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            rb.velocity = direction.normalized * force;

            float rotate = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotate + 90);
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ResetPlayerPosition();
            Destroy(gameObject);
        }
    }

    private void ResetPlayerPosition()
    {
        gameManager.ResetPlayerPosition(); // Call the GameManager function to reset player position
    }
}
