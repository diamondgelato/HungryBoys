using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    List<Node> path = new List<Node>();
    GridManager gridManager;
    Pathfinder pathfinder;
    Enemy enemyRef;

    void Awake () {
        enemyRef = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void OnEnable()
    {
        // send to first waypoint
        ReturnToStart();
        // add path tiles to path list
        RecalculatePath(true);
    }

    void ReturnToStart() {
        // transform.position = path[0].transform.position;

        // new pathfinding
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    void FinishPath() {
        // Destroy(gameObject);
        gameObject.SetActive(false);
        // bad things happen
        enemyRef.StealGold();
        Debug.Log("Oh no, you let an enemy get through!");
    }

    IEnumerator FollowPath () {
        // foreach (Tile waypoint in path)
        for (int i = 1; i < path.Count; i++ )
        {
            Vector3 startPosition = transform.position;
            // Vector3 endPosition = waypoint.transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);

            float travelPercent = 0f;

            while (travelPercent < 1f) {
                travelPercent += Time.deltaTime * speed;

                transform.LookAt(endPosition);

                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    void RecalculatePath (bool resetPath) {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath) {
            coordinates = pathfinder.StartCoordinates;
        } else {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        
        // Debug.Log(gameObject.name + " " + coordinates);
        path.Clear();

        // old pathfinding
        // GameObject Path = GameObject.FindGameObjectWithTag("Path");
        // path = new List<Tile>(Path.GetComponentsInChildren<Tile>());

        // new pathfinding
        path = pathfinder.GetNewPath(coordinates);

        // stop enemy from following any old paths
        StopAllCoroutines(); 
        // make enemy follow the path
        StartCoroutine(FollowPath());
    }
}
