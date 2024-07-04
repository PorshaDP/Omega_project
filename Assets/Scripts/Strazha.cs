using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strazha : MonoBehaviour
{
    public float speed;
    public float stoppingdistance;
    public float retreatDistance;

    private Transform player;

    private float timeBtwShorts;
    public float StarttimeBtwShorts;


    public GameObject projectTile;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timeBtwShorts = StarttimeBtwShorts;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > stoppingdistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingdistance && (Vector2.Distance(transform.position, player.position) > retreatDistance))
        {
            transform.position = this.transform.position;

        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if (timeBtwShorts <= 0)
        {
            Instantiate(projectTile, transform.position, Quaternion.identity);
            timeBtwShorts = StarttimeBtwShorts;

        }
        else
        {
            timeBtwShorts -= Time.deltaTime;
        }


    }


}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Strazha : MonoBehaviour
//{
//    public float speed;
//    public Transform player;

//    // Start is called before the first frame update
//    void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // Calculate direction towards the player
//        Vector2 direction = (player.position - transform.position).normalized;

//        // Move towards the player
//        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)direction, speed * Time.deltaTime);
//    }

//    void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.tag == "Player")
//        {
//            Destroy(gameObject);
//        }
//    }
//}

