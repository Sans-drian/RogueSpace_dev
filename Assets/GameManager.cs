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


    private void Awake()
    {
        enemies = GameObject.FindObjectsOfType<Enemy>().ToList();
        UpdateEnemiesLeftText();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleEnemyDefeated(Enemy enemy)
    {
        if (enemies.Remove(enemy))
        {
            UpdateEnemiesLeftText();
        }
    }

    void UpdateEnemiesLeftText()
    {
        enemiesLeftText.text = $"Enemies Left: {enemies.Count}";
    }
}
