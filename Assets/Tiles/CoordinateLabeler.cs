using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] [RequireComponent(typeof(TMP_Text))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color traversedColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0);
    [SerializeField] Node nodeObj;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    // Waypoint waypoint;
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid;

    void Awake() {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        gridManager = FindObjectOfType<GridManager>();
    }

    void Start() {
        DisplayCoordinates();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying) {
            // run code in scene view only
            DisplayCoordinates();
            UpdateObjName();
            label.enabled = true;
        }
        SetLabelColor();
        ToggleLabels();
    }

    void ToggleLabels() {
        if (Input.GetKeyDown(KeyCode.C)) {
            label.enabled = !label.enabled;
        }
    }

    // Sets the label colour
    void SetLabelColor() {
        // if (waypoint.IsPlacable) 
        // if (Application.isPlaying) {
        if (gridManager == null) { return; }

        // Debug.Log("coordinates: " + coordinates);
        Node node = gridManager.GetNode(coordinates);   // <-- problem phase 1: 'coordinates' is not being set properly by the runtime of this statement

        if (node == null) { 
            // Debug.Log("node is null, returning");
            return;
        }

        nodeObj = node;

        if (node.isPath) {
            // Debug.Log("Path color for " + coordinates);
            label.color = pathColor;
        } else if (node.isTraversed) {
            // Debug.Log("Traversed color for " + coordinates);
            label.color = traversedColor;
        } 
        else if (!node.isWalkable) {
            label.color = blockedColor; 
        } 
        else {
            // Debug.Log("Normal color for " + coordinates);
            label.color = defaultColor;
        } 
        // }
    }

    void DisplayCoordinates() {
        if (gridManager == null) { 
            // Debug.Log("grid manager null");     // <-- problem phase 2: 'gridManager' is null over here, causing coordinates to not be set
            return; 
        }

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);    // <-- problem phase 3: but then how does the number show up correctly if coordinates is not set
        
        label.text = coordinates.x.ToString() + ',' + coordinates.y.ToString();
    }

    void UpdateObjName() {
        transform.parent.name = coordinates.ToString();
    }
}
