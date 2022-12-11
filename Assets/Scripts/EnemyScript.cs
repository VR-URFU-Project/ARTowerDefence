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
        transform.LookAt(target.position);
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int amount)
    {
        if (BasicData.Health <= 0) return;

        BasicData.Health -= amount;

        if (BasicData.Health <= 0)
        {
            Die();
        }
    }

    IEnumerator WaitBeforeDeath(float seconds)
    {
        agent.baseOffset = 0f;
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    void Die()
    {
        animator.SetTrigger("Death");
        KillEvent();
        MoneySystem.ChangeMoney(BasicData.Money);
        StartCoroutine(WaitBeforeDeath(1.7f));
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
        if (other.GetComponent<TowerHealthLogic>() == null) return;

        animator.SetInteger("Attack", 1);
        attackingRN = other.GetComponent<TowerHealthLogic>().Tdata;
        agent.speed = 0;
        InvokeRepeating("CrashTower", 0f, BasicData.AttacSpeed);
        //Debug.Log(other.name);
    }

    private void TriggerExit()
    {
        animator.SetInteger("Attack", 0);
        agent.speed = BasicData.Movement * 0.03f;
        //agent.speed = 0.05f;
        CancelInvoke("CrashTower");
    }

    private void CrashTower()
    {
        if (attackingRN == null || attackingRN.Health <= 0)
        {
            TriggerExit();
            return;
        }
        attackingRN.TakeDamage(BasicData.Damage);
        //Debug.Log("left hp " + attackingRN.Health);
    }
}
