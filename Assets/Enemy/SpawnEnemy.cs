using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float delay = 2.0f;
    GameObject[] enemyPool;

    void Awake() {
        PopulatePool();
    }

    void PopulatePool () {
        enemyPool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            enemyPool[i] = Instantiate(enemyPrefab, transform);
            enemyPool[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("MakeEnemy", 0f, delay);
        StartCoroutine(MakeEnemy());
    }

    void EnableObjectInPool() {
        for (int i = 0; i < poolSize; i++)
        {
            // Debug.Log(enemyPool[i].activeSelf);

            if (enemyPool[i].activeInHierarchy == false) {
                enemyPool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator MakeEnemy() {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(delay);
        }
    }
}
