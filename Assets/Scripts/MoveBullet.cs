using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private Transform target;
    public static int totalBullets = 0;
    public float damage;
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        damage = 10.0f;
        bulletSpeed = 20.0f;
        totalBullets++;
        //Debug.Log(totalBullets);
        target = GameObject.Find("Enemy(Clone)").transform;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (target == null)
            {
                Destroy(gameObject);
            }
            else
            {
            }
            /*
            else if (transform.position == target.position)
            {
                Destroy(gameObject);
            }*/
            //deleteBullet();

            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * bulletSpeed);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().HP -= this.damage;
        }
        Debug.Log("bullet trigger");
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
