using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemies : MonoBehaviour
{
    private int frameCounter = 0;
    public GameObject enemyPrefab;
    private GameObject enemy;

    public Vector3 spawnPosition;

    public GameManagerBehaviour gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManagerBehaviour>();
        spawnPosition = new Vector3(-11.0f, 4.1f);

        //script starts at the same time as gameManager, so values have to be manually initalised
        gameManager.Wave = 1;
        gameManager.EnemiesLeft = 2;        
        spawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        frameCounter++;
        
        if (frameCounter % 25 == 0)
        {
            if (gameManager.EnemiesLeft <= 0)
            {
                nextWave();
            }
            else
            {
                spawnEnemy();
            }
        }
    }

    void spawnEnemy()
    {
        //gets a new enemy type (load is not case sensitive)
        switch (gameManager.Wave)
        {
            case 2:
            case 5:
            case 7:
            case 12:
                enemyPrefab = (GameObject)Resources.Load("Prefabs/Enemy1", typeof(GameObject));
                break;
                
            case 3:
            case 6:
            case 9:
            case 11:
            case 13:
                enemyPrefab = (GameObject)Resources.Load("Prefabs/Enemy", typeof(GameObject));
                break;

            case 4:
            case 8:
            case 10:
                enemyPrefab = (GameObject)Resources.Load("Prefabs/Enemy2", typeof(GameObject));
                break;                
        }

        enemy = (GameObject)
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.transform.SetParent(transform);
        gameManager.EnemiesLeft--;
    }

    private void nextWave()
    {
        Debug.Log("CONGRATULATIONS! WAVE " + gameManager.Wave + " COMPLETED!");
        gameManager.Wave++;
        int wave = gameManager.Wave;
        gameManager.EnemiesLeft = wave * 2;
    }
}
