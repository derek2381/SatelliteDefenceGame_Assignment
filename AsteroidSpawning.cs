using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AsteroidSpawning : MonoBehaviour
{
    
    public GameObject[] asteroids;
    public Transform earth;

    public float spawnRadius = 5f;
    public float spawnInterval = 5f;
    public float moveSpeed;
    public static int count = 0;
    private float spawnTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnInterval && count < 3 && PlayerController.lifes > 0)
        {
            count++;
            SpawnAsteroid();
            spawnTimer = 0f;
        }
        
    }


    void SpawnAsteroid()
    {
        Vector3 randomDir = Random.onUnitSphere;
        randomDir.y = 0f;
        Vector3 spawnPos = earth.position + randomDir.normalized * spawnRadius;

        int randomIndex = Random.Range(0, asteroids.Length);

        GameObject spawnedObject = Instantiate(asteroids[randomIndex], spawnPos, Quaternion.identity);

        ObjectMover Mover = spawnedObject.AddComponent<ObjectMover>();
        Mover.target = earth;
        moveSpeed = Random.Range(100, 140);
        Mover.moveSpeed = moveSpeed*Time.deltaTime;

        Debug.Log("Spawned Object at :- " + spawnPos);
    }

    
}
