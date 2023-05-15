using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    public MonsterData BasicData;

    //private ObjectPool<EnemyScript> enemyPool;

    public delegate void OnKill();
    private OnKill KillEvent = null;

    private TowerData attackingRN;

    private Animator animator;

    [Header("Audio")]
    [SerializeField] AudioClip AttackSound;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] AudioClip SpawnSound;
    new AudioSource audio;

    bool dead = false;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Crystal").transform;
        transform.LookAt(target.position);
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(SpawnSound);
        BasicData = new MonsterData();
        dead = false;
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
        yield return new WaitForSeconds(seconds); Destroy(gameObject);
        /*if (enemyPool == null) Destroy(gameObject);
        else
        {
            //TriggerExit();
            //enemyPool.Release(this);

        }*/
        
    }

    void Die()
    {
        dead = true;
        animator.SetTrigger("Death");
        if(KillEvent != null)
            KillEvent();
        MoneySystem.ChangeMoney(BasicData.Money);
        audio.PlayOneShot(DeathSound);
        StartCoroutine(WaitBeforeDeath(1.7f));
    }

    void Update()
    {
        if(!dead)
        {
            agent.isStopped = false;
            gameObject.tag = "Enemy";
            gameObject.GetComponent<Collider>().enabled = true;
            agent.SetDestination(target.position);

            foreach (Transform child in gameObject.transform)
            {
                if (child.CompareTag("Target")) child.gameObject.SetActive(true);
            }
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
        InvokeRepeating("CrashTower", 0f, 1 / BasicData.AttackSpeed);
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

        if (dead == false)
            audio.PlayOneShot(AttackSound);
    }

/*    public void SetPool(ObjectPool<EnemyScript> enemyPool)
    {
        this.enemyPool = enemyPool;
    }*/
}
