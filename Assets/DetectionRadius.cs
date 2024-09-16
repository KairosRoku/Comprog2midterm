using UnityEngine;
using System.Collections.Generic;

public class DetectionRadiusTrigger : MonoBehaviour
{
    private PlayerController playerController;
    private HashSet<GameObject> enemiesInRange = new HashSet<GameObject>();

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        Debug.Log("DetectionRadiusTrigger initialized. Trigger is active on GameObject: " + gameObject.name);
    }

    void Update()
    {
        if (enemiesInRange.Count > 0)
        {
            playerController.ShootBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Object entered trigger: " + collision.gameObject.name); 

        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.gameObject);
            Debug.Log("Enemy entered detection radius: " + collision.gameObject.name); 
        }
        else
        {
            Debug.Log("Non-enemy object entered: " + collision.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Object exited trigger: " + collision.gameObject.name);

        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.gameObject);
            Debug.Log("Enemy exited detection radius: " + collision.gameObject.name);
        }
    }
}
