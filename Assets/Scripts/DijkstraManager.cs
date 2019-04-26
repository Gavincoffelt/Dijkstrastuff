using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraManager : MonoBehaviour
{
    public Node startNode;
    public Node endNode;

    public Grid grid;

    private void Start()
    {
        grid = gameObject.GetComponent<Grid>();
    }
    private static int CompareNode(Node left, Node right)
    {
        if (left.gScore == right.gScore) { return 0; }
        else if (left.gScore < right.gScore) { return -1; }
        else { return 1; }
    }

    private void Update()
    {
        Ray picker = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(picker, out hit, Mathf.Infinity))
        {
            hit.collider.GetComponent<MeshRenderer>().material.color = Color.green;
            startNode = hit.collider.GetComponent<Node>();
        }

        if (Input.GetMouseButtonDown(1) && Physics.Raycast(picker, out hit, Mathf.Infinity))
        {
            hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;
            endNode = hit.collider.GetComponent<Node>();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            FindPath();
        }
    }

    void FindPath()
    {
        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();
        Node current = startNode;
        open.Add(current);

        while(open.Count > 0)
        { 
            open.Remove(current);
            closed.Add(current);

            // left
            if(current.id % 10 != 0)
            {
                Node leftTile = grid.tiles[current.id - 1].GetComponent<Node>();
                if (!closed.Contains(leftTile))
                {
                    leftTile.gScore = current.gScore + 1;
                    leftTile.previous = current;
                    if (!closed.Contains(leftTile))
                    {
                        open.Add(leftTile);
                    }
                }
            }
            // right
            if (current.id % 10 != 9)
            {
                Node rightTile = grid.tiles[current.id + 1].GetComponent<Node>();
                if (!closed.Contains(rightTile))
                {
                    rightTile.gScore = current.gScore + 1;
                    rightTile.previous = current;
                    if (!closed.Contains(rightTile))
                    {
                        open.Add(rightTile);
                    }
                }
            }
            // above
            if (current.id < 90)
            {
                Node neighbor = grid.tiles[current.id + 10].GetComponent<Node>();
                if (!closed.Contains(neighbor))
                {
                    neighbor.gScore = current.gScore + 1;
                    neighbor.previous = current;
                    if (!closed.Contains(neighbor))
                    {
                        open.Add(neighbor);
                    }
                }
            }
            // below
            if (current.id > 9)
            {
                Node neighbor = grid.tiles[current.id - 10].GetComponent<Node>();
                if (!closed.Contains(neighbor))
                {
                    neighbor.gScore = current.gScore + 1;
                    neighbor.previous = current;
                    if (!closed.Contains(neighbor))
                    {
                        open.Add(neighbor);
                    }
                }
            }

            if (current != startNode || endNode)
            {
                current.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            open.Sort(CompareNode);
            current = open[0];
            if (current == endNode)
            {
                current.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                while (current.previous != startNode)
                {
                    current = current.previous;
                    current.gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
                }
                current = current.previous;
                current.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            }
        }

    }
    
}
