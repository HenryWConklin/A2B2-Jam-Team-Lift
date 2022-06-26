using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerBase player;
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;

    private Vector2 movement;
    public Vector2 mousePos;
    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;
    public float smoothInputSpeed;

    private void Awake()
    {
        player = GetComponent<PlayerBase>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        if (player.health <= 0)
            return;
        
        currentInputVector = Vector2.SmoothDamp(currentInputVector, movement, ref smoothInputVelocity, smoothInputSpeed);
        rb.MovePosition(rb.position + currentInputVector * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;
    }
}