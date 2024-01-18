using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DisconnectButton : MonoBehaviour
{
    // Assuming the button is attached to the same GameObject as this script
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(DisconnectFromServer);
    }

    void DisconnectFromServer()
    {
        PhotonNetwork.Disconnect();

    }
}