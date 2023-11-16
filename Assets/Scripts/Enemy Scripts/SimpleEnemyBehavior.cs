using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyBehavior : MonoBehaviour
{
    //[SerializeField] private GameObject sightDetector;
    //[SerializeField] private GameObject attackDetector;
    private bool playerDetected;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask isGrounded, isPlayer;

    public bool showGizmo = true;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float health;

    //Attacking
    public float timeBetweenAttacks;

    protected bool alreadyAttacked;



    //State
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange, wait;

    // Start is called before the first frame update
    private void Awake()
    {
        wait = false;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //check detector
        playerInSight = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);


        //if (!playerInSight && !playerInAttackRange) Patroling();
        if (wait) Waiting();
        if (playerInSight && !playerInAttackRange && !wait) ChasePLayer();
        if (playerInSight && playerInAttackRange && !wait) AttackPlayer();
    }


    private void Waiting()
    {
        agent.SetDestination(transform.position);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, isGrounded))
        {
            walkPointSet = true;
        }
    }


    protected virtual void ChasePLayer()
    {
        agent.SetDestination(player.position);
    }


    protected virtual void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attacking code


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    protected virtual void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0f);
    }

    private void DestroyEnemy()
    {
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
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    }
}

