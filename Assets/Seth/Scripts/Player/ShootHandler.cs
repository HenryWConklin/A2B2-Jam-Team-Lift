using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ShootHandler : MonoBehaviour
{
    public PlayerBase player;
    [Header("Shoot Settings")] 
    public GameObject projectilePrefab;

    public Transform exitPoint;
    public int currentAmmo = 20;
    public float cooldownTime = 0.5f; 
    public bool canShoot;
    private IEnumerator Reload_Holder;
    private WaitForSeconds buffer; 

    private void Awake()
    {
        player = GetComponent<PlayerBase>();
        buffer = new WaitForSeconds(cooldownTime);
        exitPoint = transform.Find("ExitPoint");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        
    }

    private void Shoot()
    {
        if (currentAmmo <= 0)
        {
            canShoot = false;
            Reload();
        }
        else if (canShoot)
        {
            StartCoroutine(Shoot_Co());
        }

    }

    public IEnumerator Shoot_Co()
    {
        canShoot = false;
        Debug.Log(cooldownTime + " cooldownTime");
        ShootLogic(); 
        yield return buffer;
        canShoot = true;
        
    }

    private void ShootLogic()
    {
        currentAmmo -= 1;
        //TODO instantiate bullet shell or maybe some smoke particle FX?
        GameObject newBullet = Instantiate(projectilePrefab, exitPoint.position, quaternion.identity);
        Vector2 shootDir = (player.playerMovement.mousePos - (Vector2)player.transform.position).normalized;
        newBullet.GetComponent<Projectile>().Init(shootDir); 
    }

    private void Reload()
    {
        
    }
    
    
}
