using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SR_TetronimoGrid : MonoBehaviour
{
    public static SR_TetronimoGrid Instance;

    public List<GameObject> rows = new List<GameObject>();

    private bool[,] gridLayout = new bool[10, 8];

    public GameObject blockPrefab;

    void Start()
    {
        if (Instance == null)
            Instance = this;

        //StartCoroutine(FillGrid());
    }

    public void PutBlockInGrid(GameObject block)
    {

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

    IEnumerator FillGrid()
    {
        for (int i = 0; i < rows.Count; i++)
        {
            List<BoxCollider2D> rowElements = rows[i].GetComponentsInChildren<BoxCollider2D>().ToList();

            for (int j = 0; j < rowElements.Count; j++)
            {
                GameObject block = Instantiate(blockPrefab, rowElements[j].gameObject.transform.position, Quaternion.identity);
                gridLayout[i, j] = true;
                Debug.Log("Grid bool at x:" + i + ", y:" + j + " is " + gridLayout[i, j]);
                block.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
                yield return new WaitForSeconds(0.5f);
                Debug.Log("Spawned block [" + i + "], [" + j + "]");
            }
        }
    }
}