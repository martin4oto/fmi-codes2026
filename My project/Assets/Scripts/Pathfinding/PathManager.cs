using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager instance;
    public Vector2Int gridSize;
    public int gridOffset;
    Node[,] grid;
    public GameObject testSquare;
    public Vector2Int c1;
    public Vector2Int c2;
    public bool tetsts;
    Node nullNode;
    void Awake()
    {
        instance = this;
        nullNode = new Node(-1, -1);
        GenerateGrid();
    }
    void GenerateGrid()
    {
        grid = new Node[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2 worldPosition = new Vector2(x * gridOffset + transform.position.x, y * gridOffset + transform.position.y);
                RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 1);

                if (hit.collider == null){
                    grid[x, y] = new Node(x, y);
                    Instantiate(testSquare, new Vector2(x * gridOffset + transform.position.x, y * gridOffset + transform.position.y), Quaternion.identity);
                    if (x - 1 != -1 && grid[x-1, y] != nullNode)
                    {
                        grid[x, y].connections.Add(grid[x-1, y]);
                        grid[x-1, y].connections.Add(grid[x, y]);
                    }
                    if (y - 1 != -1 && grid[x, y-1] != nullNode)
                    {
                        grid[x, y].connections.Add(grid[x, y-1]);
                        grid[x, y-1].connections.Add(grid[x, y]);
                    }
                }
                else
                {
                    grid[x, y] = nullNode;
                }
            }
        }
    }
    List<Node> GetPath(Vector2Int start, Vector2Int end)
    {
        return GetPath(grid[start.x, start.y], grid[end.x, end.y]);
    }
    List<Node> GetPath(Node start, Node end)
    {
        return BFS(start, end);
    }

    List<Node> BFS(Node start, Node end)
    {
        Queue<Node> unexplored = new();
        List<Node> explored = new();
        Dictionary<Node, Node> cameFrom = new();
        Node current = start;
        do{
            AddConnectionsToQueue(unexplored, explored, cameFrom, current);
            explored.Add(current);
            current = unexplored.Dequeue();
            Debug.Log("us: " + unexplored.Count);
        }while (unexplored.Count > 0 && current != end);

        if (current == end)
        {
            List<Node> path = new();
            current = end;
            while (current != start)
            {
                path.Add(current);
                current = cameFrom[current];
            }
            path.Add(start);
            path.Reverse();
            return path;
        }
        return null;
    }
    void AddConnectionsToQueue(Queue<Node> queue, List<Node> explored, Dictionary<Node, Node> cameForm, Node node)
    {
        foreach(Node connection in node.connections)
        {
            if (!queue.Contains(connection) && !explored.Contains(connection)){ 
                queue.Enqueue(connection);
                cameForm.Add(connection, node);
            }
        }
    }

    void Update()
    {
        if (tetsts)
        {
            tetsts = false;
            List<Node> path = GetPath(c1, c2);
            foreach(Node node in path)
            {
                Instantiate(testSquare, new Vector2(node.x * gridOffset + transform.position.x, node.y * gridOffset + transform.position.y), Quaternion.identity);
            }
        }
    }

}

public struct Node
{
    public int x;
    public int y;
    public List<Node> connections;
    public Node(int _x, int _y)
    {
        x = _x;
        y = _y;
        connections = new List<Node>();
    }

    public static bool operator ==(Node left, Node right)
    {
        if (left.x == right.x && left.y == right.y) return true;
        return false;
    }

    public static bool operator !=(Node left, Node right)
    {
        if (left.x == right.x && left.y == right.y) return false;
        return true;
    }
    public override bool Equals(object obj)
    {
        return base.Equals (obj);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
