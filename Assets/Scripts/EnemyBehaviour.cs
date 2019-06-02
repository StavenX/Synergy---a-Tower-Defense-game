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
        get { return this.currentWaypoint; }
        set { this.currentWaypoint = value; }
    }
    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    private static int speedCounter = 0;


    //TODO: GameManager has this list?
    /**
     * Gets the waypoints the enemy traverses through
     */
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
        
        //debugging purposes
        speedCounter+=2;
        speedCounter = speedCounter > 8 ? 0 : speedCounter;

        //set speed = speedCounter to have some enemies get faster than their previous enemies
        //use to check that towers prioritise targets correctly
        //2-3 is normal, use 80-ish for quick debugging
        Speed = 3;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        moveToWaypoint();
        if (hasReachedWaypoint())
        {
            try
            {
                waypoint = getNextWaypoint();
            }
            //enemy has no more waypoints - it has reached the end goal
            catch (System.ArgumentOutOfRangeException)
            {
                Debug.Log("Oh no! The enemy reached the castle!");
                Destroy(gameObject);
                gameManager.Health -= 10;

                //enemy has to be marked as killed here too, as the next wave only begins when all enemies have been killed
                gameManager.EnemiesKilled++;
            }
        }
    }

    /**
     * Enemy dies, passes some values to gameManager
     */
    public void die ()
    {
        Destroy(gameObject);
        gameManager.Gold += 30;
        gameManager.EnemiesKilled++;
    }

    /**
     * Reduces HP of enemy, used on bullet (etc.) trigger / collisions
     */
    public float takeDamage (float amount)
    {
        this.HP -= amount;
        return this.HP;
    }

    /**
     * Increments waypoint number by one and returns the target from the array
     * */
    private Vector3 getNextWaypoint()
    {
        currentWaypoint++;
        Vector3 target = waypoints[currentWaypoint].transform.position;
        return target;
    }

    /**
     * Moves enemy to current waypoint
     */
    private void moveToWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoint, Time.deltaTime * this.speed);
    }

    /**
     * Checks if enemy has reached its waypoint
     */
    private bool hasReachedWaypoint()
    {
        return transform.position == waypoint;
    }
}
