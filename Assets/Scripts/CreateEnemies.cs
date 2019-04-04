using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEnemies : MonoBehaviour
{
    private int frameCounter = 0;
    public GameObject enemyPrefab;
    private GameObject enemy;

    public Vector3 spawnPosition;

    public GameManagerBehaviour gameManager;

    public float spawnSpeed;

    public static GameObject[] enemyPrefabs;
    private System.Random enemyPicker;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyPrefabs = new GameObject[]
        {
            (GameObject)Resources.Load("Prefabs/Enemy", typeof(GameObject)),
            (GameObject)Resources.Load("Prefabs/Enemy1", typeof(GameObject)),
            (GameObject)Resources.Load("Prefabs/Enemy2", typeof(GameObject))
        };
        enemyPicker = new System.Random();

        gameManager = gameObject.GetComponent<GameManagerBehaviour>();

        //just left of current map start
        spawnPosition = new Vector3(-11.0f, 4.1f);

        //script starts at the same time as gameManager, so values have to be manually initalised
        gameManager.Wave = 1;
        gameManager.EnemiesLeft = 2;     

        spawnSpeed = .5f;
        StartCoroutine("spawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        frameCounter++;
    }

    /**
     * Spawns a single enemy at the spawnPosition
     * Removes one from gameManager's enemyLeft count
     */
    void spawnEnemy()
    {
        enemyPrefab = enemyPrefabs[enemyPicker.Next(enemyPrefabs.Length)];
        
        enemy = (GameObject)
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.transform.SetParent(transform);
        gameManager.EnemiesLeft--;
    }

    /**
     * Spawns enemies as long as the wave allows it, and the player isn't dead
     */
    public IEnumerator spawnEnemies()
    {
        var enemiesInWave = gameManager.EnemiesLeft;
        for (int i = 0; i < enemiesInWave ;i++ )
        {
            spawnEnemy();
            yield return new WaitForSeconds(spawnSpeed);

        }
        //player is still alive
        if (gameManager.Health > 0)
        {
            //waits until all enemies in the wave are dead
            //TODO: change this condition, as enemies per wave is now explictly defined in multiple places
            while (gameManager.EnemiesKilled < gameManager.Wave * 2)
            {
                yield return null;
            }
            nextWave();
            while (gameManager.gamePaused)
            {
                yield return new WaitForSeconds(.2f);
            }
            StartCoroutine("spawnEnemies");
        }
    }

    /**
     * Gives the gameManager the values for the next wave
     */
    private void nextWave()
    {
        Debug.Log("CONGRATULATIONS! WAVE " + gameManager.Wave + " COMPLETED!");
        gameManager.gamePaused = true;
        var button = GameObject.Find("NextWave");
        button.GetComponent<Image>().enabled = true;
        button.GetComponentInChildren<Text>().enabled = true;
        

        gameManager.Wave++;
        gameManager.EnemiesLeft = gameManager.Wave * 2;
        gameManager.EnemiesKilled = 0;
    }
}
