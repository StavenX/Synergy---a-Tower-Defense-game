using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject bullet;
    private int frameCounter;
    private LinkedList<GameObject> enemies;

    private GameManagerBehaviour gameManager;

    public bool doesRotate = false;

    public Vector3 bulletSpawnOffset;
    private Vector3 spawnLocation;


    // The target marker.
    public Transform target;

    // Angular speed in radians per sec.
    float speed;
    float rotationAmount;

    int towerWaitingPeriod;
    float towerRange;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();

        //how long the tower has to wait between each shot
        towerWaitingPeriod = 20;

        System.Random rand = new System.Random();
        frameCounter = rand.Next(0, towerWaitingPeriod - 1);
        //frameCounter = 0;


        //how far the tower can shoot
        towerRange = 8.0f;
        
        //offset set on prefab in unity editor
        Vector3 a = transform.position;
        Vector3 b = bulletSpawnOffset;
        spawnLocation = new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);


        enemies = getEnemies();

                
        //high speed means towers instantly turns to target, lower speed means they turn slowly
        //(if using Time.deltaTime * speed)
        //speed = 200.0f;

        //amount a tower rotates towards target every frame
        //1.0 = 100%, 0.5 = 50% etc.
        rotationAmount = 1.0f;

        //sets rotation target to a an enemy
        target = getNewTarget();
    }
    
    // Update is called once per frame
    void Update()
    {
        this.enemies = getEnemies();

        frameCounter++;

        //shootBullet();

        //reduces amount of work per update to reduce strain on computer
        if (frameCounter % towerWaitingPeriod * Time.deltaTime == 0)
        {
            try
            {
                if (target != null)
                {
                    //gets new target and rotates if not in range
                    if (!isInRange(target))
                    {
                        target = getNewTarget();
                        rotateToTarget();
                        shootBullet();
                    }
                    //rotates and shoots if in range of target
                    else
                    {
                        rotateToTarget();
                        shootBullet();
                    }
                }
                else
                {
                    target = getNewTarget();
                    rotateToTarget();
                    shootBullet();
                }
            } catch (NullReferenceException ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }    


    /**
     * Checks if tower is in range of the Transform object
     */
    private bool isInRange(Transform t)
    {
        return Vector3.Distance(transform.position, t.position) <= towerRange;
    }

    /**
     * Returns the enemy that is closest to their endgame
     * Returns null if no targets are available for current tower
     */
    private Transform getNewTarget()
    {
        try
        {
            //temp values, will always be overwritten unless enemies is empty
            //in which case function ends up returning null -- or throw an exception, depends what we choose to keep
            int furthestWaypoint = -1;
            Transform priorityEnemy = null;

            foreach (GameObject enemy in getEnemies())
            {
                if (isInRange(enemy.transform))
                {
                    int enemyWaypoint = enemy.GetComponent<EnemyBehaviour>().CurrentWaypoint;

                    //sets new priorityenemy if they have reached a waypoint further along the map
                    if (enemyWaypoint > furthestWaypoint)
                    {
                        if (enemy != null)
                        {
                            priorityEnemy = enemy.transform;
                            furthestWaypoint = enemyWaypoint;
                        }
                    }
                }                
            }
            if (priorityEnemy == null)
            {
                throw new NullReferenceException("There are no targets within range of this tower");
            }
            return priorityEnemy;
        }
        catch (System.NullReferenceException)
        {
            return null;
        }
        catch (System.Exception)
        {
            return null;
        }
    }

    /**
     * Rotates the tower to its current target
     */
    private void rotateToTarget()
    {
        if (!doesRotate)
        {            
            return;
        }
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
     * Fires a bullet when a tower is clicked
     * Debug use mostly
     * */
    private void OnMouseUp()
    {
        shootBullet();
        
    }

    /**
     * Tower shoots a bullet at it's current target
     * */
    void shootBullet()
    {
        //new bullet spawns on top of tower
        if (target != null)
        {
            bullet = (GameObject)
                    Instantiate(bulletPrefab, spawnLocation, Quaternion.identity);
            bullet.GetComponent<BulletBehaviour>().target = this.target;

            //play sound (laser)
            AudioSource audio = bullet.GetComponent<AudioSource>();
            audio.PlayOneShot(audio.clip);
        }
    }

    /**
     * Returns a list of all enemies
     */
    private LinkedList<GameObject> getEnemies()
    {
        return gameManager.Enemies;
        /*
        LinkedList<GameObject> list = new LinkedList<GameObject>();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            list.AddLast(enemy);
        }
        return list;
        */
    }

}
