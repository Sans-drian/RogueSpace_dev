using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public static event Action<Enemy> OnEnemyKilled;
    [SerializeField] float health, maxHealth = 3f;
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private UnityEngine.Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        health = maxHealth;
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

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }

    void moveCharacter(UnityEngine.Vector2 direction)
    {
        rb.MovePosition((UnityEngine.Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
