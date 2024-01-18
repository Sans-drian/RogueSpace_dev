using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Photon.Pun;
using UnityEngine;

public class EnemyMultiplayer : MonoBehaviourPun
{
    public static event Action<EnemyMultiplayer> OnEnemyKilled;
    [SerializeField] float health, maxHealth = 3f;
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private UnityEngine.Vector2 movement;

    public Health playerHealth;
    public int damage = 1;

    public float knockbackPower = 100;
    public float knockbackDuration = 1;

    [SerializeField] FloatingHealthBar healthBar;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        health = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);

        // Automatically assign the player
        player = GameObject.FindWithTag("Player").transform;
        // Automatically assign the player health
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        
    }

    void Update()
    {
        UnityEngine.Vector3 direction = player.position - transform.position;
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

    public void TakeDamage(int damageAmount, Photon.Realtime.Player attackingPlayer)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            Debug.Log("Damage taken. Attacking player: " + (attackingPlayer != null ? attackingPlayer.NickName : "null"));

            OnEnemyKilled?.Invoke(this);

            // Call the RPC to destroy the enemy on the master client
            photonView.RPC("DestroyEnemyOnMasterClient", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    void DestroyEnemyOnMasterClient()
    {
        // Destroy the enemy
        PhotonNetwork.Destroy(gameObject);
    }
    void moveCharacter(UnityEngine.Vector2 direction)
    {
        rb.MovePosition((UnityEngine.Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
