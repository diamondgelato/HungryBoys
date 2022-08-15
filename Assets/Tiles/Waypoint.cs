using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlacable;
    public bool IsPlacable { get { return isPlacable; }}

    [SerializeField] GameObject ballista;

    private void OnMouseDown() {
        if (isPlacable) {
            bool isPlaced = ballista.GetComponent<Tower>().createTower(ballista, transform.position);

            isPlacable = !isPlaced;
        }
    }
}
