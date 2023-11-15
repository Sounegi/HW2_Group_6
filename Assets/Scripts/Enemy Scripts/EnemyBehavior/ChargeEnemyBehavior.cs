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

    private void Awake()
    {
        alreadyAttacked = false;
        isCooldown = false;
        agent = GetComponent<NavMeshAgent>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player") {
            Debug.Log("Hit da player");
            //HealthManager.GetInstance().DoDamage(1);
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

    private void OnDrawGizmosSelected()
    {

        if (showGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
