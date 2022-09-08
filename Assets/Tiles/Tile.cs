using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlacable;
    public bool IsPlacable { get { return isPlacable; }}

    [SerializeField] GameObject ballista;

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates;

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void Start() {
        if (gridManager != null) {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlacable) {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown() {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates)) {
            bool isSuccessful = ballista.GetComponent<Tower>().createTower(ballista, transform.position);
            // isPlacable = !isPlaced;

            if (isSuccessful) {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyEnemies();
            }
        }
    }
}
