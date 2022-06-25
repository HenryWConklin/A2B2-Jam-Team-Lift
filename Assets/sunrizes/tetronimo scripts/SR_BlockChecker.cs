using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_BlockChecker : MonoBehaviour
{
    public GameObject lockedBlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("gridBottom") || collision.gameObject.CompareTag("lockedBlock"))
        {
            Instantiate(lockedBlock, transform.parent.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }
    }
}