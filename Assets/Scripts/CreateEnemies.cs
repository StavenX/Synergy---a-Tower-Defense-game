using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemies : MonoBehaviour
{
    int counter = 0;
    public GameObject enemyPrefab;
    private GameObject enemy;
    public Vector3 testPos;

    // Start is called before the first frame update
    void Start()
    {
        testPos = new Vector3(-9.0f, 4.0f);
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
                Instantiate(enemyPrefab, testPos, Quaternion.identity);
    }
}
