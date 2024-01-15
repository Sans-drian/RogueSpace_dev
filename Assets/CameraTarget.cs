using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    [SerializeField] float threshold;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        //getting mouse position
        UnityEngine.Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //calculating the distance of target position from player
        UnityEngine.Vector3 targetPos = (player.position + mousePos) / 2f;

        // Make constraint for camera position according to the threshold of the mouse position
        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

        this.transform.position = targetPos;

    }
}
