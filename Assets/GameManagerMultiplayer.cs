using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManagerMultiplayer : MonoBehaviourPunCallbacks
{
    public static GameManagerMultiplayer instance;

    [SerializeField] TextMeshProUGUI enemiesLeftText;
    List<EnemyMultiplayer> enemies = new List<EnemyMultiplayer>();

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        // Call the RPC to update the enemies left text on all clients
        photonView.RPC("UpdateEnemiesLeftText", RpcTarget.All, enemies.Count);
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        EnemyMultiplayer.OnEnemyKilled += HandleEnemyDefeated;
    }

    private void OnDisable()
    {
        EnemyMultiplayer.OnEnemyKilled -= HandleEnemyDefeated;
    }

    void Start() //game loading
    {
        enemies = GameObject.FindObjectsOfType<EnemyMultiplayer>().ToList(); //finding out how many enemy objects exist, and puts them in a list
        photonView.RPC("UpdateEnemiesLeftText", RpcTarget.All, enemies.Count); //call update text function
    }

    [PunRPC]
    void UpdateEnemiesLeftText(int enemiesCount) //update enemy count
    {
        enemiesLeftText.text = $"Enemies Left: {enemiesCount}"; //update the enemiesleft text
    }

    public void AddEnemy(EnemyMultiplayer enemy)
    {
        enemies.Add(enemy);
        photonView.RPC("UpdateEnemiesLeftText", RpcTarget.All, enemies.Count);
    }

    void HandleEnemyDefeated(EnemyMultiplayer enemy) //subscribe to enemy event
    {
        if (enemies.Remove(enemy)) //check if the enemy is removed (?)
        {
            // Use RPC to call UpdateEnemiesLeftText on all clients
            photonView.RPC("UpdateEnemiesLeftText", RpcTarget.All, enemies.Count);
        }

        // win screen changer
        if (enemies.Count == 0) //if there are no more enemies
        {
            SceneManager.LoadScene(8); //change scene
        }
    }
}
