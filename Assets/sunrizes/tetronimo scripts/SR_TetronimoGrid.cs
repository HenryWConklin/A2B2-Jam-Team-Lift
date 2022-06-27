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

    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(CheckGridLines());
            //CorruptGrid(CheckGridLines());
        }
        
    }

    ///<summary>Checks if row is filled with locked blocks.</summary>
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

    ///<summary>Checks how many lines have locked blocks in them and then returns the number.</summary>
    public int CheckGridLines()
    {
        int linesWithElements = 0;

        for (int i = 0; i < rows.Count; i++)
        {
            List<Transform> rowElements = rows[i].GetComponentsInChildren<Transform>().ToList();

            for (int j = 0; j < rowElements.Count; j++)
            {
                if (rowElements[j].GetComponentInChildren<SR_LockedBlock>())
                {
                    linesWithElements++;
                    break;
                }    
            }
        }

        return linesWithElements;
    }
    
    ///<summary>Corrupts the grid up to a certain line. Needs a line number as a parameter.</summary>
    public void CorruptGrid(int upToLine)
    {
        for (int i = 0; i < upToLine; i++)
        {
            List<Transform> rowElements = rows[i].GetComponentsInChildren<Transform>().ToList();

            for (int j = 1; j < rowElements.Count; j++)
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
                        if (!rowElements[j].GetComponentInChildren<SR_LockedBlock>())
                        {
                            GameObject newLockBlock = Instantiate(lockedBlock, rowElements[j].position, Quaternion.identity, rowElements[j].transform);
                            newLockBlock.transform.localScale = new Vector3(1, 1, 1);
                            Debug.Log("Instantied block with index j: [" + j + "] and parent name is [" + rowElements[j].gameObject.name + "]");
                        }
                        else
                        {
                            continue;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
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