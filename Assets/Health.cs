using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth = 3;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; //everytime game starts, reset to max health
    }


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0) //checking if health = 0
        {
            //we're dead
            //Destroy(gameObject);
            SceneManager.LoadScene(3);//show game over screen
        }
    }
    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
