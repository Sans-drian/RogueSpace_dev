using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Photon.Pun;

public class GunControllerMultiplayer : MonoBehaviour
{
    PhotonView view;
    [SerializeField] private Animator gunAnim;
    [SerializeField] private Transform gun;
    [SerializeField] private float gunDistance = 1.5f;
    private bool gunFacingRight = true;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    UnityEngine.Vector3 direction;
    

    //public UnityEngine.Vector3 bulletOffset = new UnityEngine.Vector3(1.5f, 0, 0);


    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

        if (view.IsMine)
        {
            //get the mouse postion and direction of gun
        UnityEngine.Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UnityEngine.Vector3 direction = mousePos - gun.position; 

        //gun rotation calculation bullshit
        gun.rotation = UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(0, 20, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        //gun position calculation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + UnityEngine.Quaternion.Euler(0, 0, angle) * new UnityEngine.Vector3(gunDistance, 0, 0);

        //gun shooting based on key press
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot(direction);

        //enable gun flip function
        GunFlipController(mousePos);
        }

    }


    private void GunFlipController(UnityEngine.Vector3 mousePos) //function that flips the gun based on mouse position (triggers the GunFlip() function)
    {
        if(mousePos.x < gun.position.x && gunFacingRight)
            GunFlip();
        else if(mousePos.x > gun.position.x && !gunFacingRight)
            GunFlip();
    }

    private void GunFlip()
    {
        //function that flips the gun
        gunFacingRight = !gunFacingRight;
        gun.localScale = new UnityEngine.Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);
    }

    public void Shoot(UnityEngine.Vector3 direction)
    {
        gunAnim.SetTrigger("Shoot"); //enable the trigger from the animator section for gun animation

        //UnityEngine.Vector3 bulletOffset = new UnityEngine.Vector3(3, 0, 0);
        GameObject newBullet = Instantiate(bulletPrefab, gun.position, UnityEngine.Quaternion.identity); //spawn bullet
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed; //determine bullet speed

        // Set the owner of the bullet to the local player
        Bullet bulletComponent = newBullet.GetComponent<Bullet>();
        if (bulletComponent != null)
        {
            bulletComponent.Owner = view.Owner; // Change this line
            Debug.Log("Bullet owner set to: " + PhotonNetwork.LocalPlayer.NickName);
        }
        Destroy(newBullet, 7); //remove bullet from game after a certain amount of time
    }
}
