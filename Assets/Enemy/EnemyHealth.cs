using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;

    [Tooltip("Add health to maximum health for subsequent enemies after killing current enemies")]
    [SerializeField] int difficultyRamp = 1;
    
    int currentHitPoints = 5;

    Enemy enemyRef;

    void Awake() {
        enemyRef = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other) {
        ProcessHit();
    }

    void ProcessHit() {
        currentHitPoints--;
        
        if (currentHitPoints <= 0) {
            // Destroy(gameObject);
            gameObject.SetActive(false);
            enemyRef.RewardGold();
            maxHitPoints += difficultyRamp;
        }
    }
}
