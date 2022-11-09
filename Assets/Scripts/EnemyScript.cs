using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    public MonsterData BasicData;

    public delegate void OnKill();
    private OnKill KillEvent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Crystal").transform;
    }

    public void TakeDamage(int amount)
    {
        BasicData.Health -= amount;

        if (BasicData.Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        KillEvent();
        MoneySystem.ChangeMoney(BasicData.Money);
        Destroy(gameObject);
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget.transform;
    }

    public void SetKillEvent(OnKill newEvent)
    {
        KillEvent = newEvent;
    }
}
