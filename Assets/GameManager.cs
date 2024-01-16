using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemiesLeftText;
    List<Enemy> enemies = new List<Enemy>();

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += HandleEnemyDefeated;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= HandleEnemyDefeated;
    }


    private void Awake() //game loading
    {
        enemies = GameObject.FindObjectsOfType<Enemy>().ToList(); //finding out how many enemy objects exist, and puts them in a list
        UpdateEnemiesLeftText(); //call update text function
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleEnemyDefeated(Enemy enemy) //subscribe to enemy event
    {
        if (enemies.Remove(enemy)) //check if the enemy is removed (?)
        {
            UpdateEnemiesLeftText();
        }
    }

    void UpdateEnemiesLeftText() //update enemy count
    {
        enemiesLeftText.text = $"Enemies Left: {enemies.Count}"; //update the enemiesleft text
    }
}
