using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldDisplay : MonoBehaviour
{
    Bank bank;
    TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();

        scoreText = GetComponent<TMP_Text>();
        // scoreText.text = "Gold: " + bank.CurrentBalance.ToString();
        UpdateGold();
    }

    public void UpdateGold () {
        scoreText.text = "Gold: " + bank.CurrentBalance.ToString();
    }
}
