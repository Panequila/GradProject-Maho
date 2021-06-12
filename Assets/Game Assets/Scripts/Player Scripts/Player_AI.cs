using UnityEngine;
using UnityEngine.AI;


public class Player_AI : MonoBehaviour
{
    public PlayerBullet bullet;
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public Vector3 bulletRotation;
    public NavMeshAgent agent;
    public Transform enemy;
    public static int Score = 0;
    public float timealive = 0f;
    public int StartingHealth = 100;
    public int CurrentHealth = 100;
    public int Damage = 50;
    public LayerMask whatIsGroud, whatIsEnemy;
    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    public GameObject projectile;
    //States
    public float sightRange, attackRange;
    public bool enemyInSightRange, enemyInAttackRange;
    public AgentBat shooter;
    public GameObject playersParent;
    // Start is called before the first frame update

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        bullet = GetComponent<PlayerBullet>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(shooter.playersAlive);
        CallRespwan(shooter);
        enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);
        enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsEnemy);
        if (!enemyInSightRange && !enemyInAttackRange) Patrolling();
        if (enemyInSightRange && !enemyInAttackRange) ChaseEnemy();
        if (enemyInAttackRange && enemyInSightRange) AttackEnemy();
        Punishcamp();
    }
    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {

        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGroud))
            walkPointSet = true;
    }
    private void ChaseEnemy()
    {
        agent.SetDestination(enemy.position);
    }

    private void AttackEnemy()
    {
        //Make sure enemy doesnt move
        agent.SetDestination(transform.position);

        Vector3 targetPostition = new Vector3(enemy.position.x,
                                        this.transform.position.y,
                                        enemy.position.z);
        transform.LookAt(targetPostition);
        if (!alreadyAttacked)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Agent");
            var direction = transform.forward;
            // Debug.Log("Shot");
            Debug.DrawRay(start: shootingPoint.position, direction * 10f, Color.green, 2f);
            if (Physics.Raycast(shootingPoint.position, direction, out var hit, 20f, layerMask))
            {
                //Debug.Log(hit.collider.gameObject);
                //GameObject monsterObject = hit.collider.gameObject; 
                ////Debug.Log(transform.GetComponent<AgentBehv>());
                //monsterObject.GetComponent<AgentBehv>().Dash(transform.rotation);
                //Debug.Log("I hit someone");
            }
            ///Attack code here
            Instantiate(projectile, shootingPoint.position, transform.rotation, transform.parent);
            // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }


    public void GetShot(int damage, AgentBat shooter)
    {
        ApplyDamage(damage, shooter);
    }
    private void ApplyDamage(int damage, AgentBat shooter)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die(shooter);
        }
    }
    private void Die(AgentBat shooter)
    {
        transform.position = new Vector3(Random.Range(transform.position.x - 6f, transform.position.x + 6f), 0f, Random.Range(transform.position.z - 5f, transform.position.z + 7f)); ; ;
        gameObject.SetActive(false);
        Score++;
        shooter.registerKill();
        shooter.Killcount++;
        //Debug.Log("Player Killed");
        timealive = 0;
    }
    public void Punishcamp()
    {
        timealive += Time.deltaTime;
        if (timealive >= 8)
        {

            timealive = 0;
            //transform.GetComponent<AgentBehv>().camperpoints();
            shooter.camperpoints();

        }

    }
    public void Respwan()
    {
        Debug.Log("Respwaning now ");
        CurrentHealth = StartingHealth;
        gameObject.SetActive(true);
        playersParent.GetComponent<Players>().ActivatePlayers();
        shooter.Killcount = 1;

        //transform.position = new Vector3(0, 0f, 4); ;

    }
    private void CallRespwan(AgentBat shooter)
    {

        if (shooter.playersAlive == 0)
        {
            shooter.playersAlive = 3;

            Debug.Log("Call Respawn");

            Respwan();
        }
    }


}
