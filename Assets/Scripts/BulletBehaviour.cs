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
        bulletSpeed = 10.0f;
        totalBullets++;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (target == null)
            {
                moveToPosition(lastTargetPos);
                if (transform.position == lastTargetPos)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                lastTargetPos = target.position;
                moveToPosition(target.position);
            }            
        }
        //target already destroyed
        catch (MissingReferenceException) {
            Destroy(gameObject);
        }
        catch (System.NullReferenceException)
        {
            Destroy(gameObject);
        }
        
    }

    private void moveToPosition(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().takeDamage(damage);
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
