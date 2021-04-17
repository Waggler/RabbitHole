using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxGoonie : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patrolling
    //public Vector3 walkPoint;
    bool walkPointSet;
    //public float walkPointRange;
    //private Transform[] patrolPoints;
    public Transform[] points;
    private int destPoint = 0;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    Transform target;
    public Transform bulletSpawn;
    private float timer = 0.0f;
    public float animShotDelay;

    public GameObject projectile;
    public float projectileHorizontalForce = 32f;
    public float projectileVerticalForce = 4f;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Animator animator;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        target = player;
        //patrolPoints = new Transform[numPatrolPoints];
        agent.autoBraking = false;
        GotoNextPoint();
    }

    void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        

        if (!playerInSightRange && !playerInAttackRange) GotoNextPoint();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }
    /*private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            {
                walkPointSet = false;
            }
        }
    }*/

    /*private void SearchWalkPoint()
    {
        //Calculate rand point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, - transform.up, 3f, whatIsGround))
        {
            walkPointSet = true;
        }

    }*/
    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }//end GoToNextPoint

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("isShooting", false);
        animator.SetBool("isMoving", true);
        timer = 0;
    }

    private void AttackPlayer()
    {
        agent.SetDestination(this.transform.position);

        transform.LookAt(player);
        animator.SetBool("isShooting", true);
        animator.SetBool("isMoving", true);
        timer += Time.deltaTime;

        if (!alreadyAttacked && timer > 1)
        {

            //Attack Code
            animator.SetTrigger("Shoot");
            StartCoroutine(ShotDelay());
            Rigidbody rb = Instantiate(projectile, bulletSpawn.position, this.transform.rotation).GetComponent<Rigidbody>();
            //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();//This Can be changed for the bullet to come at a specific position
            rb.AddForce(transform.forward * projectileHorizontalForce, ForceMode.Impulse);
            rb.AddForce(transform.up * projectileVerticalForce, ForceMode.Impulse);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(animShotDelay);
    }
}


