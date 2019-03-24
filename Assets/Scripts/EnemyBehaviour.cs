using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Vector3 target;

    private int currentTarget;


    // Start is called before the first frame update
    void Start()
    {
        currentTarget = 0;
        target = getNextTarget();//new Vector3(8.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime*5);
        if (transform.position == target)
        {
            try
            {
                target = getNextTarget();
            } catch (System.Exception ex)
            {
                Destroy(gameObject);
            }
            //Destroy(gameObject);
        }
    }
    

    Vector3 getNextTarget()
    {
        Transform t = GameObject.Find("Waypoints").transform;
        Vector3 target = t.GetChild(currentTarget).position;
        currentTarget++;
        return target;
    }
}
