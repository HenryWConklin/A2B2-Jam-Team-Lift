using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_TetronimoBlock : MonoBehaviour
{
    public int unitMove;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(BlockDrop());
    }

    IEnumerator BlockDrop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y - unitMove));
        }
    }    
}