using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemies : MonoBehaviour
{
    int counter = 0;
    public GameObject enemyPrefab;
    private GameObject enemy;
    public Vector3 spawnPosition;


    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3(-11.0f, 4.0f);
        newEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        
        if (counter % 100 == 0)
        {
            newEnemy();   
        }
    }

    void newEnemy()
    {
        enemy = (GameObject)
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.transform.parent = transform;
    }
}
