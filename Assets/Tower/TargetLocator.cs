using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float range = 15;
    [SerializeField] ParticleSystem arrows;
    Transform target;
    Enemy[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        arrows = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget() {
        enemies = FindObjectsOfType<Enemy>();

        float minDistance = Vector3.Distance(transform.position, enemies[0].transform.position);
        Transform closestTarget = enemies[0].transform;
        float dist;

        foreach (Enemy enemy in enemies)
        {
            dist = Vector3.Distance(transform.position, enemy.transform.position);
        
            if (minDistance > dist) {
                closestTarget = enemy.transform;
                minDistance = dist;
            }
        }

        target = closestTarget;
    }

    void AimWeapon() {
        float distance = Vector3.Distance(transform.position, target.position);
        
        weapon.LookAt(target);

        if (distance < range) {
            Attack(true);
        } else {
            Attack(false);
        }
    }

    void Attack(bool isActive) {
        var emissionModule = arrows.emission;
        emissionModule.enabled = isActive;
    }
}
