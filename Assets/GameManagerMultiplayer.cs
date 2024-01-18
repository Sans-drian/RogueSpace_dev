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
    [SerializeField] TextMeshProUGUI TimerCountdownText;
    private float countdownTime = 45f;
    private bool isCountingDown = false;
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
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(StartCountdown());
        } 
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
        if (PhotonNetwork.IsMasterClient)
        {
            if (enemies.Remove(enemy)) //check if the enemy is removed (?)
            {
                // Use RPC to call UpdateEnemiesLeftText on all clients
                photonView.RPC("UpdateEnemiesLeftText", RpcTarget.All, enemies.Count);
            }

            // win screen changer
            if (enemies.Count == 0) //if there are no more enemies
            {
                PhotonNetwork.LoadLevel(8); //change scene
            }
        }
        else
        {
            // If not the master client, send a message to the master client to handle enemy defeat
            photonView.RPC("MasterHandleEnemyDefeated", RpcTarget.MasterClient, enemy.gameObject.GetPhotonView().ViewID);
        }
    }

    [PunRPC]
    void MasterHandleEnemyDefeated(int enemyViewID)
    {
        // Find the enemy with the given view ID
        PhotonView enemyView = PhotonView.Find(enemyViewID);
        if (enemyView != null)
        {
            EnemyMultiplayer enemy = enemyView.GetComponent<EnemyMultiplayer>();
            if (enemy != null)
            {
                HandleEnemyDefeated(enemy);
            }
        }
    }
    IEnumerator StartCountdown()
    {
        isCountingDown = true;
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownTime--;
            photonView.RPC("SyncTimer", RpcTarget.All, countdownTime);
        }
        isCountingDown = false;

        // If the countdown has reached 0, switch to scene 7
        if (countdownTime <= 0)
        {
            PhotonNetwork.LoadLevel(7);
        }
    }

    [PunRPC]
    void SyncTimer(float time)
    {
        TimerCountdownText.text = time.ToString();
    }

}
