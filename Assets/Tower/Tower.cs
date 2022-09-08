using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // add variable to control build delay.
    [SerializeField] float delay = 1f;
    int towerCost = 75;

    GameObject ballista;
    Transform TowerPool;
    TowerDisplay display;

    void Start() {
        StartCoroutine(Build());
    }

    public bool createTower(GameObject tower, Vector3 position) {
        TowerPool = GameObject.Find("Tower Pool").GetComponent<Transform>();
        display = FindObjectOfType<TowerDisplay>();
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null) { 
            return false; 
        }

        if (bank.CurrentBalance >= towerCost) {
            ballista = Instantiate(tower, position, Quaternion.identity, TowerPool);
            bank.Withdraw(towerCost);
            display.UpdateTowers();

            return true;
        } else {
            // Debug.Log("Balance not enough: " + bank.CurrentBalance);
        }
        return false;
    }

    IEnumerator Build() {
        // turn off all children and grandchildren
        foreach (Transform child in transform)     
        {  
            child.gameObject.SetActive(false);   
            // Debug.Log(child.gameObject.name);
        }   

        // enable sequencially
        foreach (Transform child in transform)     
        {  
            child.gameObject.SetActive(true);   
            // Debug.Log(child.gameObject.name);
            yield return new WaitForSeconds(delay);
        }   
    }
}
