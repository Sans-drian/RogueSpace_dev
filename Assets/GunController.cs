using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

public class GunController : MonoBehaviour
{
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
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UnityEngine.Vector3 direction = mousePos - gun.position; 

        gun.rotation = UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(0, 20, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + UnityEngine.Quaternion.Euler(0, 0, angle) * new UnityEngine.Vector3(gunDistance, 0, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot(direction);

        GunFlipController(mousePos);

    }


    private void GunFlipController(UnityEngine.Vector3 mousePos)
    {
        if(mousePos.x < gun.position.x && gunFacingRight)
            GunFlip();
        else if(mousePos.x > gun.position.x && !gunFacingRight)
            GunFlip();
    }

    private void GunFlip()
    {
        gunFacingRight = !gunFacingRight;
        gun.localScale = new UnityEngine.Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);
    }

    public void Shoot(UnityEngine.Vector3 direction)
    {
        gunAnim.SetTrigger("Shoot");

        //UnityEngine.Vector3 bulletOffset = new UnityEngine.Vector3(3, 0, 0);
        GameObject newBullet = Instantiate(bulletPrefab, gun.position, UnityEngine.Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;

        Destroy(newBullet, 7);
    }
}
