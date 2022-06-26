using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SR_TetronimoGrid : MonoBehaviour
{
    public static SR_TetronimoGrid Instance;

    public List<GameObject> rows = new List<GameObject>();

    public GameObject blockPrefab;
    public GameObject lockedBlock;

    private bool corruptionStarted = false;

    void Start()
    {
        if (Instance == null)
            Instance = this;

        //StartCoroutine(FillGrid());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !corruptionStarted)
        {
            StartCoroutine(CorruptGrid());
        }
    }

    public void CheckRow(int rowNumber)
    {
        int blockCount = 0;

        GameObject rowToCheck = rows[rowNumber];
        Transform[] rowElements = rowToCheck.GetComponentsInChildren<Transform>();

        foreach (Transform t in rowElements)
        {
            if (t.GetComponentInChildren<SR_LockedBlock>())
                blockCount++;

            if (blockCount >= rowElements.Length)
                Debug.Log("ROW IS FULL");
        }
    }

    public void CheckGrid()
    {
        for (int i = 0; i < rows.Count; i++)
        {
            int rowCounter = 0;

            List<Transform> rowElements = rows[i].GetComponentsInChildren<Transform>().ToList();

            for (int j = 0; j < rowElements.Count; j++)
            {
                if (rowElements[j].GetComponentInChildren<SR_TetronimoBlock>())
                    rowCounter++;

                if (rowCounter >= rowElements.Count)
                    Debug.Log("ROW IS FULL");
            }
        }
    }

    IEnumerator CorruptGrid()
    {
        corruptionStarted = true;

        for (int i = 0; i < rows.Count; i++)
        {
            Transform[] rowElements = rows[i].GetComponentsInChildren<Transform>();

            for (int j = 0; j < rowElements.Length; j++)
            {
                int chance = Random.Range(0, 2);

                switch (chance)
                {
                    case 0:
                        if (rowElements[j].GetComponentInChildren<SR_LockedBlock>())
                        {
                            rowElements[j].GetComponentInChildren<SR_LockedBlock>().DestroyBlock();
                        }
                        else
                        {
                            continue;
                        }

                        break;

                    case 1:
                        if (rowElements[j].GetComponentInChildren<SR_LockedBlock>())
                        {
                            continue;
                        }
                        else
                        {
                            Instantiate(lockedBlock, rowElements[j].position, Quaternion.identity, rowElements[j]);
                        }
                        break;

                    default:
                        break;
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        corruptionStarted = false;
    }

    IEnumerator FillGrid()
    {
        for (int i = 0; i < rows.Count; i++)
        {
            List<BoxCollider2D> rowElements = rows[i].GetComponentsInChildren<BoxCollider2D>().ToList();

            for (int j = 0; j < rowElements.Count; j++)
            {
                GameObject block = Instantiate(blockPrefab, rowElements[j].gameObject.transform.position, Quaternion.identity);
                block.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
                yield return new WaitForSeconds(0.5f);
                Debug.Log("Spawned block [" + i + "], [" + j + "]");
            }
        }
    }
}