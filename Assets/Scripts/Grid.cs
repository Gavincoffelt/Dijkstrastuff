using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject prefab;

    public int rows = 10;
    public int cols = 10;

    public GameObject[] tiles;

    private void Start()
    {
        tiles = new GameObject[rows * cols];

        for(int i = 0; i < rows * cols; ++i)
        {
            tiles[i] = Instantiate(prefab, new Vector3(i % cols, 0.0f, i / rows), Quaternion.identity);
            tiles[i].name = i.ToString();
            tiles[i].GetComponent<Node>().id = i;
        }
    }
}
