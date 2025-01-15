using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public Transform target; // The object to move towards
    public float moveSpeed = 1000f; // Movement speed
    public string targetTag = "Projectile";

    void Update()
    {
        // Move towards the target
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized; // Direction to the target
            transform.position += direction * moveSpeed * Time.deltaTime; // Move in that direction

            // Destroy object if it's close enough to the target
            if (Vector3.Distance(transform.position, target.position) < 3f && PlayerController.lifes > 0)
            {
                Destroy(gameObject);
                AsteroidSpawning.count--;
                PlayerController.lifes--;
                //PlayerController.lifesText.text = "Lifes :- " + PlayerController.lifesText.ToString();
                Debug.Log(PlayerController.lifes);
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            AsteroidSpawning.count--;
            PlayerController.score++;
            //PlayerController.scoreText.text = "Score :- " + PlayerController.score.ToString();
            Debug.Log(PlayerController.score);
        }
    }
}
