using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Enemy(Clone)").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (target == null || transform.position == target.position)
        {
            Destroy(gameObject);
        }
        //deleteBullet();

        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 10);
        
    }

    Vector3 somePosition()
    {
        return new Vector3(5, 2, 0);
    }

    /*
     * Unused
     * Based on bullets travelling off-map, which likely isn't a case in the final product
     * Can be changed to delete bullets on collision instead.
     * */
    void deleteBullet()
    {
        int x = (int) transform.position.x;
        int y = (int)transform.position.y;

        if (x > 10 || x < -10 || y > 10 || y < -10) {
            Destroy(this);
        }
    }
}
