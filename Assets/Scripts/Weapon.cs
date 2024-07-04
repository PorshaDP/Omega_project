using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform FirePoint;
    public GameObject bullet;
    Animator animator;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        //if (Input.GetKey("up"))
        //{
        //    //animator.Play("fight_cast");
        //    Shoot();
        //}
        //if (Input.GetKey("down"))
        //{
        //    //animator.Play("fight_cast");
        //    Shoot();
        //}
        //if (Input.GetKey("left"))
        //{
        //    //animator.Play("fight_cast");
        //    Shoot();
        //}
        //if (Input.GetKey("right"))
        //{
        //    //animator.Play("fight_cast");
        //    Shoot();
        //}
    }

    void Shoot()
    {
        Instantiate(bullet, FirePoint.position, FirePoint.rotation);
    }
    
}

