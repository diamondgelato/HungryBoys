using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    // [SerializeField] Node currentSearch;
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    [SerializeField] Vector2Int endCoordinates;
    public Vector2Int EndCoordinates { get { return endCoordinates; } }

    Node startNode;
    Node endNode;
    Node currentSearchNode;
    
    Dictionary<Vector2Int, Node> traversed = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();
    
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.up, Vector2Int.left, Vector2Int.down };
    GridManager gridManager;

    void Awake() {

        gridManager = FindObjectOfType<GridManager>();

        if (gridManager != null) {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            endNode = grid[endCoordinates];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath() {
        // gridManager.ResetNodes();
        // BreadthFirstSearch(startCoordinates);
        // return BuildPath();
        return GetNewPath(startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates) {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    // enqueues and connects all the neighbours of the current node for BFS
    void ExploreNeighbours() {
        List<Node> neighbours = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoords = currentSearchNode.coordinates + direction;
            // Debug.Log(neighbourCoords);
            // Node neighbour;

            if (grid.ContainsKey(neighbourCoords)) {
                neighbours.Add(grid[neighbourCoords]);
            } 
        }

        foreach (Node neighbour in neighbours)
        {
            if (!traversed.ContainsKey(neighbour.coordinates) && neighbour.isWalkable) {
                neighbour.parent = currentSearchNode;
                traversed.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            }
        }
    }

    // actual BFS
    // add start node to queue, get current node from queue, end if current node is the destination, call explore neighbours
    void BreadthFirstSearch(Vector2Int coordinates) {
        startNode.isWalkable = true;
        endNode.isWalkable = true;

        frontier.Clear();
        traversed.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        traversed.Add(coordinates, grid[coordinates]);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isTraversed = true;

            if (currentSearchNode.coordinates == endCoordinates) {
                isRunning = false;
            }

            ExploreNeighbours();
        }
    }

    // finds the path from start to end by recursively checking all the connections from the end to the start.
    List<Node> BuildPath () {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.parent != null) {
            currentNode = currentNode.parent;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        // foreach (Node node in path)
        // {
        //     if (node.parent != null) {
        //         Debug.Log(node.coordinates + " parent: " + node.parent.coordinates);
        //     } else {
        //         Debug.Log(node.coordinates + " with no parent");
        //     }
        // }

        return path;
    }

    public bool WillBlockPath (Vector2Int coordinates) {
        // Debug.Log("Recalculating path");

        if(grid.ContainsKey(coordinates)) {
            bool prevState = grid[coordinates].isWalkable; 

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = prevState;

            // path is blocked as endNode is not connected to any other node
            if(newPath.Count <= 1) {
                // calculates old path (with the isWalkable flag being reinstated)
                GetNewPath();
                // Debug.Log("no other path found");
                return true;
            }
        }
        // Debug.Log("alternate path found");
        return false;
    }

    public void NotifyEnemies () {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
