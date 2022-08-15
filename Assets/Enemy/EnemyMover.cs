using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;
    //  = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    Enemy enemyRef;

    void Awake () {
        enemyRef = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        // add path tiles to path list
        GetPath();
        // send to first waypoint
        ReturnToStart();
        // make enemy follow the path 
        StartCoroutine(FollowPath());
    }

    void ReturnToStart() {
        transform.position = path[0].transform.position;
    }

    void FinishPath() {
        // Destroy(gameObject);
        gameObject.SetActive(false);
        // bad things happen
        enemyRef.StealGold();
        Debug.Log("Oh no, you let an enemy get through!");
    }

    IEnumerator FollowPath () {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
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

    void GetPath () {
        path.Clear();

        GameObject Path = GameObject.FindGameObjectWithTag("Path");
        path = new List<Waypoint>(Path.GetComponentsInChildren<Waypoint>());
    }
}
