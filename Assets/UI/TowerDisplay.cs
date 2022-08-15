using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerDisplay : MonoBehaviour
{
    TMP_Text towerText;
    Transform towerPool;

    // Start is called before the first frame update
    void Start()
    {
        towerPool = GameObject.Find("Tower Pool").GetComponent<Transform>();

        towerText = GetComponent<TMP_Text>();
        UpdateTowers();
    }

    public void UpdateTowers () {

        towerText.text = "Towers: " + towerPool.childCount;
    }
}
