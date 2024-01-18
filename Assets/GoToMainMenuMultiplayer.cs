using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class GoToMainMenuMultiplayer : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(DisconnectFromServer);
    }

    void DisconnectFromServer()
    {

        PhotonNetwork.Disconnect();

    }

    public void ChangeToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
