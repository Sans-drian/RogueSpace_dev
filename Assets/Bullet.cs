using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 7); //ignore the collision for bullet and player
        Physics2D.IgnoreLayerCollision(7, 7); //ignore the collision for bullet and player
    }

    // Update is called once per frame

    //check collision
    private void OnCollisionEnter2D(Collision2D collision) 
    {

        
        //checking if the collision is of the enemy component
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {   
            //add damage to the enemy
            enemyComponent.TakeDamage(1);
        }

                //checking if the collision is of the enemy component
        if(collision.gameObject.TryGetComponent<EnemyMultiplayer>(out EnemyMultiplayer enemyComponents))
        {   
            //add damage to the enemy
            enemyComponents.TakeDamage(1);
        }

        //if the bullet hits anything, destroy it
        Destroy(gameObject);
    }
    void Update()
    {
        //changes rotation of the bullet depending on the direction of the gun
        transform.right = rb.velocity;
    }


}
