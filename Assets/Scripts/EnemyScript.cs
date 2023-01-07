using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    public MonsterData BasicData;

    public delegate void OnKill();
    private OnKill KillEvent;

    private TowerData attackingRN;

    private Animator animator;

    [Header("Audio")]
    [SerializeField] AudioClip AttackSound;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] AudioClip SpawnSound;

    AudioSource audio;

    bool dead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Crystal").transform;
        transform.LookAt(target.position);
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(SpawnSound);
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
        dead = true;
        animator.SetTrigger("Death");
        KillEvent();
        MoneySystem.ChangeMoney(BasicData.Money);
        audio.PlayOneShot(DeathSound);
        StartCoroutine(WaitBeforeDeath(1.7f));
    }

    void Update()
    {
        if(dead == false)
        {   
            agent.SetDestination(target.position);
        }
        else
        {
            agent.isStopped=true;
            gameObject.tag = "Untagged";
            gameObject.GetComponent<Collider>().enabled=false;
            foreach (Transform child in gameObject.transform)
            {
                if (child.CompareTag("Target")) child.gameObject.SetActive(false);
            }

        }
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
        InvokeRepeating("CrashTower", 0f, 1 / BasicData.AttacSpeed);
    }

    private void TriggerExit()
    {
        animator.SetInteger("Attack", 0);
        agent.speed = BasicData.Movement * 0.03f;
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

        audio.PlayOneShot(AttackSound);
    }
}
