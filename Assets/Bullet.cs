using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    // Start is called before the first frame update
   // void Start()
    //{
        
    //}

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(1);
        }

        Destroy(gameObject);
    }
    void Update()
    {
        //changes rotation of the bullet depending on the direction of the gun
        transform.right = rb.velocity;
    }


}
