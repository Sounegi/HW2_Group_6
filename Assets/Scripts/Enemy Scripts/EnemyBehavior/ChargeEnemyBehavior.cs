using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeEnemyBehavior : MonoBehaviour
{
    //State Chasing, Charging, CoolDown
    private bool playerDetected;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask isGrounded, isPlayer;
    public float health;

    public bool showGizmo = true;

    //Attack and cooldown
    bool alreadyAttacked;
    public float attackRange;
    public bool playerInAttackRange, isCooldown;
    public float chargingTime, cooldownTime;
    public float chargeSpeed, normalSpeed;

    private bool triggered;
    public GameObject particle;

    private void Awake()
    {
        //particle = GameObject.Find("Blood Splat").GetComponent<ParticleSystem>();
        triggered = false;
        alreadyAttacked = false;
        isCooldown = false;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        health = 1;
    }

    void Update()
    {
        //check detector
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);


        if (!isCooldown && !playerInAttackRange) Chasing();
        if (!isCooldown && playerInAttackRange) ChargeAttack();
        if (isCooldown) CoolingDown();
    }

    //Chasing
    private void Chasing()
    {
        agent.speed = normalSpeed;
        agent.SetDestination(player.position);
    }

    //Attack
    private void ChargeAttack()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        
        if (!alreadyAttacked)
        {
            Debug.Log("charging");
            alreadyAttacked = true;
            Invoke(nameof(AttackPlayer), chargingTime);
        }
    }

    private void AttackPlayer()
    {
        //set destination to be behind player
        Vector3 playerpos = player.position;
        agent.speed = chargeSpeed;

        agent.SetDestination(playerpos);
        Debug.Log("going");

        isCooldown = true;
        /*
        Vector3 reach = transform.position - playerpos;
        if (reach.magnitude < 0.1f)
        {
            isCooldown = true;
        }
        */
    }




    private void OnCollisionExit(Collision other)
    {
        triggered = false;
        //this.GetComponent<BoxCollider>().enabled = true;
    }


    private void OnCollisionEnter(Collision collision)
    {

        
        if (collision.collider.tag == "Player" && !triggered)
        {

            triggered = true;
            Debug.Log("charge_enemy_collided");
            //Debug.Log("Hit da player");
            HealthManager.GetInstance().DoDamage(2);
            Instantiate(particle, collision.transform.position, Quaternion.identity);
        }
    }


        


    //Cooldown
    private void CoolingDown()
    {
        //agent.SetDestination(transform.position);
        transform.LookAt(player);

        Invoke(nameof(ResetAttack), cooldownTime);
    }

    private void ResetAttack()
    {
        //transform.LookAt(player);
        isCooldown = false;
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0f);
    }

    private void DestroyEnemy()
    {
        Destroy(Instantiate(particle, this.transform.position, Quaternion.identity), 0.5f);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        EnemyManager.GetInstance().DecreaseEnemy();
        
    }

    private void OnDrawGizmosSelected()
    {

        if (showGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Axe")
        {
            Debug.Log("Hit monster");
            TakeDamage(1);
        }
    }
}
