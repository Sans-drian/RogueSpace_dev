using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public static event Action<Enemy> OnEnemyKilled;
    [SerializeField] float health, maxHealth = 3f;
    //public Health playerHealth;
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private UnityEngine.Vector2 movement;

    public Health playerHealth;
    public int damage = 1;

    public float knockbackPower = 100;
    public float knockbackDuration = 1;


    [SerializeField] FloatingHealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        health = maxHealth;
        
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 direction = player.position - transform.position;
        //Debug.Log(direction);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);

            StartCoroutine(Movement.instance.Knockback(knockbackDuration, knockbackPower, this.transform));
            CameraShake.Instance.ShakeCamera(10f, .2f);
        }
    }

    //take damage function
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);

        //checking if health is 0
        if (health <= 0)
        {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this); //checking if enemy is killed, if yes, broadcast message (at public static event)
        }
    }

    void moveCharacter(UnityEngine.Vector2 direction)
    {
        rb.MovePosition((UnityEngine.Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
