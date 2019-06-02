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
    
    private void FixedUpdate()
    {
        try
        {
            if (target.position == new Vector3(0,0,0))
            {
                Debug.Log("annoying bug occured");
            }

            if (target == null)
            {
                //TODO: fix BUG, bullet randomly goes to 0,0 might be here
                moveTowardsPosition(lastTargetPos);
                if (transform.position == lastTargetPos)
                {
                    Destroy(gameObject);
                }
            }
            //has valid target
            else 
            {
                //keeps track of where the bullet is going, and destroys the bullet if it didn't
                //move any distance during the frame update
                var lastPos = transform.position;
                moveTowardsPosition(target.position);
                var newPos = transform.position;
                if (lastPos == newPos)
                {
                    Destroy(gameObject);
                }
            }            
        }
        //target is already destroyed, and as such the bullet is destroyed as it no longer has a target
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

    /**
     * Each enemy has a trigger collider, and a bullet does damage when it enters that collider
     * Bullet is destroyed
     * */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
            var HP = enemy.takeDamage(damage);
            if (HP <= 0)
            {
                enemy.die();
            }
            Destroy(gameObject);
        }
    }

    //Some arbitrary position, used to make debugging easier
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
