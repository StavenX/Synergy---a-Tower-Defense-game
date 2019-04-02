using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private float HP;
    public Vector3 waypoint;

    private List<GameObject> waypoints = new List<GameObject>();
    private GameManagerBehaviour gameManager;

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
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
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
        HP = 80.0f;

        loadWaypoints();

        currentWaypoint = 0;
        waypoint = getNextWaypoint();
        
        speedCounter++;
        speedCounter = speedCounter > 8 ? 0 : speedCounter;

        //set speed = speedCounter to have some enemies get faster than their previous enemies
        //use to check that towers prioritise targets correctly
        Speed = 2;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (HP <= 0.0f)
        {
            die();
        }
        */
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
                gameManager.Health -= 10;
            }
        }
    }

    public void die ()
    {
        Destroy(gameObject);
        gameManager.Gold += 30;
    }

    public float takeDamage (float amount)
    {
        this.HP -= amount;
        return this.HP;
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
