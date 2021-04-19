using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxtrotShoot : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

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

    //Audio
    public AudioSource audiosource;
    public AudioClip clip;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        target = player;
        //patrolPoints = new Transform[numPatrolPoints];
        agent.autoBraking = false;
    }

    void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        //agent.SetDestination(this.transform.position);

        transform.LookAt(player);
        //if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
        else
        {
            animator.SetBool("isShooting", false);
        }
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

    private void ChasePlayer()
    {
        //agent.SetDestination(player.position);
        animator.SetBool("isShooting", false);
        timer = 0;
    }

    private void AttackPlayer()
    {
        animator.SetBool("isShooting", true);
        timer += Time.deltaTime;

        if (!alreadyAttacked && timer > 1)
        {

            //Attack Code

            StartCoroutine("ShotDelay");
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
        animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(animShotDelay);
        audiosource.PlayOneShot(clip);
        Rigidbody rb = Instantiate(projectile, bulletSpawn.position, this.transform.rotation).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * projectileHorizontalForce, ForceMode.Impulse);
        rb.AddForce(transform.up * projectileVerticalForce, ForceMode.Impulse);
        rb.AddForce(-transform.right * 2, ForceMode.Impulse);


        

    }
}
