using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Transform target { get; set; }
    public static int totalBullets = 0;
    public float damage;
    public float bulletSpeed;

    private Vector3 lastTargetPos;

    // Start is called before the first frame update
    void Start()
    {
        damage = 10.0f;
        bulletSpeed = 30.0f;
        totalBullets++;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (target == null)
            {
                //BUG: bullet randomly goes to 0,0 might be here
                moveTowardsPosition(lastTargetPos);
                if (transform.position == lastTargetPos)
                {
                    Destroy(gameObject);
                }
            }
            else //has valid target
            {                
                lastTargetPos = target.position; //value used during next Update()
                moveTowardsPosition(target.position);
            }            
        }
        //Not sure which Exceptions are correct
        //target already destroyed
        catch (MissingReferenceException) {
            Destroy(gameObject);
        }
        catch (System.NullReferenceException)
        {
            Destroy(gameObject);
        }
        
    }

    /**
     * Moves bullet towards a position
     * Use target as parameter
     * TODO: just use target in this method?
     */
    private void moveTowardsPosition(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var HP = collision.gameObject.GetComponent<EnemyBehaviour>().takeDamage(damage);
            if (HP <= 0)
            {
                collision.gameObject.GetComponent<EnemyBehaviour>().die();
            }
        }
        Destroy(gameObject);
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
