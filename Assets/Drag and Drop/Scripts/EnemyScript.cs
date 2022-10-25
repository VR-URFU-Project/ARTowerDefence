using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform target;

    public int health = 100;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
