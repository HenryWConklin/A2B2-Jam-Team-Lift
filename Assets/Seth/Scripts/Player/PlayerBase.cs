using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public ShootHandler shootHandler;


    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        shootHandler = GetComponent<ShootHandler>(); 
    }
}
