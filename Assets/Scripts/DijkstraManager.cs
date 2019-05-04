using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraManager : MonoBehaviour
{
    public Node startNode;
    public Node endNode;
    public Grid grid;
    public int totalG;
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
        startNode.gScore = 0;
        endNode.gScore = 0;
        while(open.Count > 0)
        {
            totalG = totalG += current.gScore;
            open.Remove(current);
            closed.Add(current);

            // left
            if(current.id % 10 != 0)
            {
                Node leftTile = grid.tiles[current.id - 1].GetComponent<Node>();
                if (!closed.Contains(leftTile))
                {
                    if (!open.Contains(leftTile))
                    {
                        leftTile.TotalScore = (leftTile.gScore + current.TotalScore);
                        leftTile.previous = current;
                        open.Add(leftTile);
                    }
                    else
                    {
                        int TestScore = leftTile.gScore + current.TotalScore;
                        if (TestScore < leftTile.TotalScore)
                        {
                            leftTile.TotalScore = TestScore;
                            leftTile.previous = current;
                        }
                    }
                }
            }
            // right
            if (current.id % 10 != 9)
            {
                Node rightTile = grid.tiles[current.id + 1].GetComponent<Node>();
                if (!closed.Contains(rightTile))
                {
                    if (!open.Contains(rightTile))
                    {
                        rightTile.TotalScore = (rightTile.gScore + current.TotalScore);
                        rightTile.previous = current;
                        open.Add(rightTile);
                    }
                    else
                    {
                        int TestScore = rightTile.gScore + current.TotalScore;
                        if (TestScore < rightTile.TotalScore)
                        {
                            rightTile.TotalScore = TestScore;
                            rightTile.previous = current;
                        }
                    }
                }
            }
            // above
            if (current.id < 90)
            {
                Node neighbor = grid.tiles[current.id + 10].GetComponent<Node>();
                if (!closed.Contains(neighbor))
                {
                    if (!open.Contains(neighbor))
                    {
                        neighbor.TotalScore = (neighbor.gScore + current.TotalScore);
                        neighbor.previous = current;
                        open.Add(neighbor);
                    }
                    else
                    {
                        int TestScore = neighbor.gScore + current.TotalScore;
                        if (TestScore < neighbor.TotalScore)
                        {
                            neighbor.TotalScore = TestScore;
                            neighbor.previous = current;
                        }
                    }
                }
            }
            // below
            if (current.id > 9)
            {
                Node neighbor = grid.tiles[current.id - 10].GetComponent<Node>();
                if (!closed.Contains(neighbor))
                {
                    if (!open.Contains(neighbor))
                    {
                        neighbor.TotalScore = (neighbor.gScore + current.TotalScore);
                        neighbor.previous = current;
                        open.Add(neighbor);
                    }
                    else
                    {
                        int TestScore = neighbor.gScore + current.TotalScore;
                        if (TestScore < neighbor.TotalScore)
                        {
                            neighbor.TotalScore = TestScore;
                            neighbor.previous = current;
                        }
                    }
                }
            }

            if (current != startNode || endNode)
            {
                current.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            open.Sort(CompareNode);
            if (open.Count > 0)
            {
                current = open[0];
            }
        }
        current = endNode;

        current.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        while (current.previous != startNode)
        {
            current = current.previous;
            current.gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
        }
        current = current.previous;
        current.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }
        

    
    
}
