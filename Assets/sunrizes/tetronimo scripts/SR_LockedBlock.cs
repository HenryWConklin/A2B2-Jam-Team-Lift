using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_LockedBlock : MonoBehaviour
{
    private bool foundParent = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("gridSpace") && !foundParent)
        {
            transform.parent = collision.gameObject.transform;
            GameObject row = collision.transform.parent.gameObject;
            string[] nameParse = row.name.Split('_');
            SR_TetronimoGrid.Instance.CheckRow(int.Parse(nameParse[1]));
            foundParent = true;
        }
    }

    public void DestroyBlock()
    {
        Destroy(gameObject);
    }
}