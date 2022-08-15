using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startBalance = 150;
    [SerializeField] int currentBalance;
    GoldDisplay display;

    public int CurrentBalance { get { return currentBalance; }}

    public void Deposit (int amount) {
        currentBalance += Mathf.Abs(amount);
        display.UpdateGold();
    }

    public void Withdraw (int amount) {
        currentBalance -= Mathf.Abs(amount);
        display.UpdateGold();
        
        if (currentBalance < 0) {
            // lose game - reload scene
            ReloadScene();
        }
    }

    void ReloadScene () {
        int currSceneInd = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneInd);
    }

    // Start is called before the first frame update
    void Awake()
    {
        display = FindObjectOfType<GoldDisplay>();
        currentBalance = startBalance;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
