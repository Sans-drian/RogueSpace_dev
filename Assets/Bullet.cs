using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    public Photon.Realtime.Player Owner { get; set; } 
    public int Damage { get; set; } = 1;
    
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();

    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 7); //ignore the collision for bullet and player
        Physics2D.IgnoreLayerCollision(7, 7); //ignore the collision for bullet and player
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {   
            enemyComponent.TakeDamage(Damage); // Change this line
        }

        if (collision.gameObject.TryGetComponent<EnemyMultiplayer>(out EnemyMultiplayer enemyComponents))
        {   
            enemyComponents.TakeDamage(Damage, Owner); // Change this line
        }

        Destroy(gameObject);
    }

    void Update()
    {
        transform.right = rb.velocity;
    }
}
