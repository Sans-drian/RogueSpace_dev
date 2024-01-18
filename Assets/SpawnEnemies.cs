using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                GameObject newEnemy = PhotonNetwork.Instantiate(enemyPrefab.name, randomPosition, Quaternion.identity);
                GameManagerMultiplayer.instance.AddEnemy(newEnemy.GetComponent<EnemyMultiplayer>());
            }
        }
    }
}
