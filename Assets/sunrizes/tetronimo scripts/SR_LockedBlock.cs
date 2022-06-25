using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_LockedBlock : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("gridSpace"))
        {
            transform.parent = collision.gameObject.transform;
        }
    }
}