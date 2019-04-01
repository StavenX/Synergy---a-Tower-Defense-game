using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private float HP;
    public Vector3 waypoint;

    private List<GameObject> waypoints = new List<GameObject>();

    private int currentWaypoint;
    public int CurrentWaypoint
    {
        get
        {
            return this.currentWaypoint;
        }
        set
        {
            this.currentWaypoint = value;
        }
    }
    private float speed;
    private static int speedCounter = 0;


    public void loadWaypoints()
    {
        foreach (GameObject waypoint in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            waypoints.Add(waypoint);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = 30.0f;

        loadWaypoints();

        currentWaypoint = 0;
        waypoint = getNextWaypoint();
        
        speedCounter++;
        speedCounter = speedCounter > 8 ? 0 : speedCounter;

        //set speed = speedCounter to have some enemies get faster than their previous enemies
        //use to check that towers prioritise targets correctly
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0.0f)
        {
            Destroy(gameObject);
        }
        moveToWaypoint();
        if (hasReachedWaypoint())
        {
            try
            {
                waypoint = getNextWaypoint();
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Debug.Log("Oh no! The enemy reached the castle!");
                Destroy(gameObject);
            }
        }
    }

    public void takeDamage (float amount)
    {
        this.HP -= amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var name = collision.gameObject.name;
        //Debug.Log("enemy: trigger enter by " + name.ToString());

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private Vector3 getNextWaypoint()
    {
        Vector3 target = waypoints[currentWaypoint].transform.position;
        this.currentWaypoint++;
        return target;
    }

    private void moveToWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoint, Time.deltaTime * this.speed);
    }

    private bool hasReachedWaypoint()
    {
        return transform.position == waypoint;
    }
}
