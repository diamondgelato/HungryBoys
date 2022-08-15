using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // [SerializeField] 
    int towerCost = 75;

    GameObject ballista;
    Transform TowerPool;
    TowerDisplay display;

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
}
