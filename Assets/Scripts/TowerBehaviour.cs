using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject bullet;
    private int counter;


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
        counter = 0;

        //how long the tower has to wait between each shot
        towerWaitingPeriod = 30;

        //how far the tower can shoot
        towerRange = 12.0f;

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
        counter++;

        //reduces amount of work per update to reduce strain on computer
        if (counter % towerWaitingPeriod * Time.deltaTime == 0)
        {
            //rotates towers 90 degrees
            //transform.Rotate(Vector3.forward * -90);

            
            if (target == null)
            {
                try
                {
                    target = getNewTarget();
                    rotateToTarget();
                } catch (System.NullReferenceException)
                {
                    //dont have to do anything
                }
            }
            else
            {
                float distance = Vector3.Distance(target.position, transform.position);
                //gets new target and rotates if not in range
                if (distance > towerRange) 
                {
                    target = getNewTarget();
                    rotateToTarget();
                }
                //rotates and shoots if in range of target
                else
                {
                    rotateToTarget();
                    shootBullet();
                }                
            }
        }
    }

    private bool isInRange(Transform t)
    {
        return Vector3.Distance(transform.position, t.position) <= towerRange;
    }

    Transform getNewTarget()
    {
        try
        {
            //first object with that name, is always the first enemy spawned right now
            //because tower range isn't considered
            return GameObject.Find("Enemy(Clone)").transform;

            /* //test with tower range, didn't quite work
            Transform parent = GameObject.Find("EnemySpawner").transform;
            for (int i = 0; i < parent.childCount; i++)
            {
                Debug.Log("Child " + i + ", pos " + parent.GetChild(i).position);
                Transform child = parent.GetChild(i);
                if (Vector3.Distance(child.position, transform.position) < towerRange)
                {
                    return child;
                } else
                {
                    //do something?
                }
            }
            return null;
            */
        }
        catch (System.NullReferenceException)
        {
            return null;
        }
    }

    void rotateToTarget()
    {
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 180;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        //Slerp or RotateTowards methods  
        transform.rotation = Quaternion.Slerp(transform.rotation, q, rotationAmount);
    }

    /*
     * Fires a bullet when a tower is clicked
     * Debug use mostly
     * */
    private void OnMouseUp()
    {
        shootBullet();
        
    }

    /**
     * Tower shoots a bullet
     * */
    void shootBullet()
    {
        //use v as second constructor parameter for bullet to place bullet at v
        //probably remove this
        //Vector3 v = new Vector3(transform.position.x, transform.position.y);

        //new bullet spawns on top of tower
        bullet = (GameObject)
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

}
