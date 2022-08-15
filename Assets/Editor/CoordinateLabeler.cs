using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TMP_Text))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    void Awake() {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();
        
        waypoint = GetComponentInParent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying) {
            // run code in scene view only
            DisplayCoordinates();
            UpdateObjName();
        }
        SetLabelColor();
        ToggleLabels();
    }

    void DisplayCoordinates() {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        
        label.text = coordinates.x.ToString() + ',' + coordinates.y.ToString();
    }

    void UpdateObjName() {
        transform.parent.name = coordinates.ToString();
    }

    // Sets the label colour
    void SetLabelColor() {
        if (waypoint.IsPlacable) {
            label.color = defaultColor;
        } else {
            label.color = blockedColor;
        }
    }

    void ToggleLabels() {
        if (Input.GetKeyDown(KeyCode.C)) {
            label.enabled = !label.enabled;
        }
    }
}
