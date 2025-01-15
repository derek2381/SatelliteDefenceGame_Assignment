using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifesText;
    public GameObject gameOverPanel;


    public Transform target;
    public static int score = 0;
    public static int lifes = 3;
    public float orbitRadius = 5f;
    public float orbitSpeed = 50000f;

    private float angle = 0f;
    private int flag = 1;

    public GameObject projectilePrefab;
    public float projectileSpeed = 1000f;
    public float offSet = -1f;
    // Start is called before the first frame update
    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // Show Game Over UI
        }
        scoreText.text = "Score :- " + score.ToString();
        lifesText.text = "Lifes :- " + lifes.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score :- " + score.ToString();
        lifesText.text = "Lifes :- " + lifes.ToString();
        if(lifes <= 0)
        {
            GameOver();
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && lifes > 0)
        {
            if (flag < 0)
            {
                flag = -flag;
            }
            MoveSatellite(flag); 
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && lifes > 0)
        {
            if (flag > 0)
            {
                flag = -flag;
            }
            MoveSatellite(flag);  
        }

       
        //MoveSatellite(flag);

        if (Input.GetKeyDown(KeyCode.F) && lifes > 0)
        {
            SpawnProjectile();
        }
        
    }
    private void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Show Game Over UI
        }

        Time.timeScale = 0; // Pause the game
    }

    public void RestartGame()
    {
        //Time.timeScale = 1; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    private void SpawnProjectile()
    {
        Vector3 projectileSpawnPos = transform.position + transform.right; 

        //int randomIndex = Random.Range(0, projectilePrefab.Length);

        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawnPos, Quaternion.identity);

        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        if(rb != null)
        {
            Vector3 moveDir = transform.right;
            rb.AddForce(moveDir * projectileSpeed * 5, ForceMode.Impulse);
        }
       
    }

    private void MoveSatellite(int flag)
    {
        angle += orbitSpeed * Time.deltaTime * flag;

        float x = target.position.x + orbitRadius * Mathf.Cos(angle);
        float z = target.position.z + orbitRadius * Mathf.Sin(angle);

        Vector3 newPosition = new Vector3(x, transform.position.y, z);
        transform.position = newPosition;

        Vector3 dir = (newPosition - target.position).normalized;
        Vector3 tangent = new Vector3(-dir.z, 0, dir.x);

        transform.rotation = Quaternion.LookRotation(tangent);
    }
}
