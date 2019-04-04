using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [Header("Attributes")]
    public float towerRange = 8f;
    public GameObject bulletPrefab;
    public int towerWaitingPeriod;

    private GameObject bullet;
    private GameManagerBehaviour gameManager;
    private MonsterData monsterData;
    private int frameCounter;
    private Transform target;

    private float rotationAmount = 10f; 

    /* THIS IS A TEST VALUE */
    private int attackCounter = 60;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();
        monsterData = GetComponent<MonsterData>();
        System.Random rand = new System.Random();
        frameCounter = rand.Next(0, towerWaitingPeriod - 1);
    }

    private void FixedUpdate()
    {
        updateTarget();
        rotateToTarget();

        if (attackCounter >= 60)
        {
            shootBullet();
            attackCounter = 0; 
        } else
        {
            attackCounter++; 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //frameCounter++;
    }    


    /**
     * Checks if tower is in range of the Transform object
     */
    private bool isInRange(Transform t)
    {
        return Vector3.Distance(transform.position, t.position) <= towerRange;
    }

    /**
     * Tower shoots a bullet at it's current target
     */
    private void shootBullet()
    {
        //new bullet spawns on top of tower
        if (target != null)
        {
            bullet = (GameObject)
                    Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletBehaviour>().target = this.target;

            //play sound (laser)
            AudioSource audio = bullet.GetComponent<AudioSource>();
            audio.PlayOneShot(audio.clip);
        }
    }

    /**
      * Rotates the tower to its current target
      */
    private void rotateToTarget()
    {
        if (target != null)
        {
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 180;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            //Slerp or RotateTowards methods  
            transform.rotation = Quaternion.Slerp(transform.rotation, q, rotationAmount);
        }
    }

    /**
     *  Updates the current target that the tower is tracking
     * */
    private void updateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null; 

        // Finds all enemies
        foreach(GameObject enemy in enemies)
        {
            // Get distance to enemy
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            // Check if distance is shorter than any other
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null) //&& shortestDistance <= range)
        {
            target = nearestEnemy.transform; 
        } else
        {
            target = null; 
        }
    }
}
