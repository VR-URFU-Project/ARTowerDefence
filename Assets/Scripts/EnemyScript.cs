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

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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
        //if (Vector3.Distance(transform.position, target.position) < 2)
        //{
        //    animator.SetInteger("IfCrystalIsNear", 2);
        //    //IfCrystalIsNear.defaultBool = true;
        //}
        //else animator.SetInteger("IfCrystalIsNear", 1);
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
        if (other.GetComponent<Canon>() == null)
        {
            if (other.GetComponent<CrystalLogic>() == null) return;
            attackingRN = other.GetComponent<CrystalLogic>().Tdata;
        }
        else
            attackingRN = other.GetComponent<Canon>().Tdata;
        agent.speed = 0;
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
