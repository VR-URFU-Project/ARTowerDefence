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

    private TowerData attackingRN;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Canon>() == null) return;
        agent.speed = 0;
        attackingRN = other.GetComponent<Canon>().Tdata;
        InvokeRepeating("CrashTower", 0f, 1f);
        Debug.Log(other.name);
    }

    private void TriggerExit()
    {
        //agent.speed = (float)BasicData.Movement/100;
        agent.speed = 0.05f;
        CancelInvoke("CrashTower");
    }

    private void CrashTower()
    {
        if (attackingRN == null || attackingRN.Health<=0) TriggerExit();
        attackingRN.TakeDamage(BasicData.Damage);
        Debug.Log("left hp "+ attackingRN.Health);
    }
}
