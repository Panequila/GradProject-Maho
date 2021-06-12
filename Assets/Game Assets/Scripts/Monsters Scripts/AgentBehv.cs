using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine.AI;
using Mirror;
public class AgentBehv : Agent

{
    public BatAgentParent Shootset;
    public NavMeshAgent agent;
    public Transform ShootingPoint;
    public Vector3 startingPosition;
    public Rigidbody myRigidbody;
    public GameObject bullet;
    public Transform grandParent;
    public int Killcount;
    public Vector2 Position = new Vector2(0f, 0f);  

    public BatAgentParent batAgentParent;

    public float sightRange = 10, attackRange = 9;
    public bool enemyInSightRange, enemyInAttackRange;
    public bool ShotReady = true;
    public float StepsuntilShoot;
    ///data
    public int startinghealth = 500;
    public int Score = 0;
    public int Damage = 100;
    public float minsteps;
    public int Health = 100;
    public float speed = 0;
    public float dashSpeed;
    [SerializeField]
    public float dashTime;
    public float startDashTime;
    public int direction;
    public float shootingDelay;
    //GetAngle
    public GameObject[] gos;
    //public List<int> reactionTimes = new List<int>();
    public Animator anim;
    public LayerMask whatIsGroud, whatIsEnemy, whatIsBullet;
    public List<float> playerAngles = new List<float>();
    public float initialPlayerAngle;
    Vector3 targetPosition;

    //float distance = 9999;
    public Collider[] hitColliders;
    public float endEp;
    public int playersAlive = 3;
    public GameObject[] players;

    [Header("Bullet Components")]
    public List<float> bulletAngles = new List<float>();
    public GameObject[] bulletObjects;
    public Collider[] bulletDetection;
    public float bulletSightRange;


    public void Start()
    {
        
        myRigidbody = GetComponent<Rigidbody>();
        startingPosition.x = Position.x;
        startingPosition.z = Position.y;
    }
    public override void OnEpisodeBegin()
    {
        sightRange = 15f;
        attackRange = 15f;
        gos = GameObject.FindGameObjectsWithTag("Player");
        // if(gos == null)
        // {
        //  Debug.Log("mfeesh players");
        //  }
        // else
        // {
        //Debug.Log("feh players");

        //  }
        // enemy = GetComponent<Player_AI>();
        anim = GetComponent<Animator>();
        //Debug.Log("EpisodeBegin");
        transform.position = startingPosition;
        ShotReady = true;
        //agent = GetComponent<NavMeshAgent>();
        enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);
        enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsEnemy);
        endEp = 0;


    }
    public virtual void Calculations()
    {
        {
            gos = GameObject.FindGameObjectsWithTag("Player");
            hitColliders = Physics.OverlapSphere(transform.position, sightRange, whatIsEnemy, QueryTriggerInteraction.Collide);

            if (hitColliders.Length > 0)
            {


                for (int i = 0; i < hitColliders.Length; i++)
                {
                    for (int j = 0; j < gos.Length; j++)
                    {
                        if (gos[j].gameObject.tag == hitColliders[i].gameObject.tag)
                        {
                            targetPosition = gos[j].transform.position - transform.position;
                            Vector3 forward = transform.forward;
                            float rayAngle = Vector3.Angle(targetPosition, forward);
                            playerAngles.Insert(j, rayAngle);
                        }
                    }
                }
                //    for (int y = 0; y <= hitColliders.Length; y++)
                //    {
                //        if (hitColliders[y].gameObject.tag == "Player")
                //        {


                //            targetPosition = hitColliders[y].transform.position - transform.position;
                //            Vector3 forward = transform.up;
                //            float rayAngle = Vector3.Angle(targetPosition, transform.forward);
                //            playerAngles.Insert(y, rayAngle);


                //        }
                //    }
                //}

                //players = GameObject.FindGameObjectsWithTag("Player");
                //   foreach (GameObject player in players)
                //   {
                //       targetPosition = player.transform.position - transform.position;
                //       Vector3 forward = transform.up;

                //       float rayAngle = Vector3.Angle(targetPosition, transform.forward);
                //       playerAngles.Insert(i, rayAngle);
                //       i++;
                //   }


                initialPlayerAngle = playerAngles[0];
                for (int z = 0; z < playerAngles.Count; z++)
                {
                    if (playerAngles[z] < initialPlayerAngle)
                    {
                        initialPlayerAngle = playerAngles[z];
                    }
                }
                playerAngles.Clear();
                if (initialPlayerAngle > 13 && initialPlayerAngle < 45)
                {
                    AddReward(0.06f);

                }
                else if (initialPlayerAngle > 0 && initialPlayerAngle <= 13)
                {
                    AddReward(0.1f);

                }
                else if (initialPlayerAngle > 45 && initialPlayerAngle < 90)
                {
                    AddReward(-0.001f);
                }
                else if (initialPlayerAngle > 90 && initialPlayerAngle < 125)
                {
                    AddReward(-0.02f);
                }
                else if (initialPlayerAngle > 125 && initialPlayerAngle < 180)
                {
                    AddReward(-0.05f);
                }


            }
            else
            {
                //Debug.Log("No enemies in sight ");
            }

        }



    }
    //public virtual void Shoot()
    //{


    //    if (ShotReady == false)
    //        return;
    //    else
    //    {
    //        StartCoroutine(AttackRoutine());

    //        GameObject projectile = Instantiate(bullet, ShootingPoint.position, ShootingPoint.rotation, grandParent);
    //        NetworkServer.Spawn(projectile);

    //        ShotReady = false;
    //        StepsuntilShoot = minsteps;
    //        RequestDecision();
    //        shootingDelay = 0f;
    //        Shootset.shootTime = minsteps;
    //    }
    //}
    public void LimitMove()
    {
        if (transform.position.x >= startingPosition.x + 20f || transform.position.x <= startingPosition.x - 20f)
        {
            AddReward(-0.001f);
        }
        if (transform.position.z <= startingPosition.z - 20f || transform.position.z >= startingPosition.z + 20f)
        {
            AddReward(-0.001f);
        }
    }
        public void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }
    public void Move()
    {
        StartCoroutine(MovementRoutine());
    }
    public void Idle()
    {
        //StartCoroutine(IdleRoutine());
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "wall")
            AddReward(-0.1f);

    }
    IEnumerator AttackRoutine()
    {
        anim.SetInteger("Condition", 2);
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(1f);
        anim.SetInteger("Condition", 0);
        anim.SetBool("Attacking", false);

    }
    IEnumerator MovementRoutine()
    {
        anim.SetInteger("Condition", 1);
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("Condition", 0);

    }
    public override void OnActionReceived(float[] vectorAction)
    {

        if ((vectorAction[0]) == 1)
        {
            batAgentParent.Shoot();
        }

        if (vectorAction[1] == 1)
        {
            myRigidbody.velocity = new Vector3(speed, myRigidbody.velocity.y, myRigidbody.velocity.z);
            Move();
            direction = 3;

        }
        else if (vectorAction[1] == 2)
        {
            myRigidbody.velocity = new Vector3(-speed, myRigidbody.velocity.y, myRigidbody.velocity.z);
            Move();
            direction = 4;

        }
        if (vectorAction[2] == 1)
        {
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y, speed);
            Move();
            direction = 1;
        }
        else if (vectorAction[2] == 2)
        {
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y, -speed);
            Move();
            direction = 2;

        }
        if (vectorAction[3] == 1)
        {
            transform.Rotate(Vector3.up, 300 * Time.deltaTime);
        }
        else if (vectorAction[3] == 2)
        {
            transform.Rotate(Vector3.up, -300 * Time.deltaTime);
        }



        //if (vectorAction[4] == 1 && direction == 1 && dashTime <= 0.1)
        //{
        //    myRigidbody.velocity = Vector3.forward * dashSpeed;
        //    dashTime = startDashTime;
        //}
        //else if (vectorAction[4] == 2 && direction == 2 && dashTime <= 0.1)
        //{
        //    myRigidbody.velocity = Vector3.back * dashSpeed;
        //    dashTime = startDashTime;
        //}
        //else if (vectorAction[4] == 3 && direction == 3 && dashTime <= 0.1)
        //{
        //    myRigidbody.velocity = Vector3.right * dashSpeed;
        //    dashTime = startDashTime;

        //}
        //else if (vectorAction[4] == 4 && direction == 4 && dashTime <= 0.1)
        //{
        //    myRigidbody.velocity = Vector3.left * dashSpeed;
        //    dashTime = startDashTime;
        //}

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
        sensor.AddObservation(gameObject.transform.rotation.y);
        sensor.AddObservation(initialPlayerAngle);
        sensor.AddObservation(shootingDelay);
        sensor.AddObservation(transform.position.x);
        sensor.AddObservation(transform.position.z);

    }
    public override void Initialize()
    {
        startingPosition.x = Position.x;
        startingPosition.z = Position.y;

    }
    public override void Heuristic(float[] actionsOut)
    {

        actionsOut[0] = 0;
        if (Input.GetKey(KeyCode.P))
        {
            actionsOut[0] = 1;
            Debug.Log("ALO");

        }
        actionsOut[1] = 0;

        if (Input.GetKey(KeyCode.D))
        {
            actionsOut[1] = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[1] = 2;

        }

        actionsOut[2] = 0;
        if (Input.GetKey(KeyCode.W))
        {
            actionsOut[2] = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            actionsOut[2] = 2;
        }
        actionsOut[3] = 0;
        if (Input.GetKey(KeyCode.X))
        {
            actionsOut[3] = 1;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            actionsOut[3] = 2;
        }

        //actionsOut[4] = 0;
        //if (Input.GetKey(KeyCode.I))
        //{
        //    actionsOut[4] = 1;
        //}
        //if (Input.GetKey(KeyCode.K))
        //{
        //    actionsOut[4] = 2;
        //}
        //if (Input.GetKey(KeyCode.L))
        //{
        //    actionsOut[4] = 3;
        //}
        //if (Input.GetKey(KeyCode.J))
        //{
        //    actionsOut[4] = 4;
        //}

    }
    public void Reset()
    {
        Debug.Log("Reset");
    }
    public void registerKill()
    {
        playersAlive--;
        //Debug.Log(playersAlive);
        AddReward(2f);
        if (playersAlive <= 0)
        {
            Debug.Log("Player Killed");
            EndEpisode();
        }
    }
    public void camperpoints()
    {

        AddReward(-1);

    }
    public void registerDeath()
    {

        AddReward(-1f / Killcount);

    }
    public void GetShot(int damage)
    {

        ApplyDamage(damage);
    }
    public void ApplyDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Score--;
        registerDeath();
        Debug.Log("Agent is Dead");
        Respwan();
    }
    public void Respwan()
    {
        EndEpisode();
        //Player.GetComponent<Player_AI>().Respwan();
        Health = startinghealth;
        transform.position = startingPosition; /*new Vector3(Random.Range(transform.position.x - 6, transform.position.x + 6f), 0, Random.Range(transform.position.z - 5f, transform.position.z + 7f))*/; ;
    }
    public void TimeBeforeEnd()
    {       
        if (endEp >= 50)
        {
            EndEpisode();
            endEp = 0;
            shootingDelay = 0f;
        }
    }
    public void ShootingDelayTime()
    {
        if (shootingDelay >= 5f)
        {

            AddReward(-1f);
            shootingDelay = 0f;
        }

    }
    //public void Dash(Quaternion playerRotation)
    //{
    //    //bulletDetection = Physics.OverlapSphere(transform.position, bulletSightRange, whatIsBullet, QueryTriggerInteraction.Collide);

    //    //if (bulletDetection.Length > 0)
    //    //{
    //    //    for (int i = 0; i < bulletDetection.Length; i++)
    //    //    {
    //    //        for (int j = 0; j < bulletObjects.Length; j++)
    //    //        {
    //    //            if (bulletObjects[j].gameObject.tag == bulletDetection[i].gameObject.tag)
    //    //            {
    //    //                targetPosition = bulletObjects[j].transform.position - transform.position;
    //    //                Vector3 forward = transform.forward;
    //    //                float rayAngle = Vector3.Angle(targetPosition, forward);
    //    //                Debug.DrawRay(start: transform.position, rayAngle, Color.green, 2f);
    //    //                bulletAngles.Insert(j, rayAngle);
    //    //            }
    //    //        }
    //    //    }
    //    //float value = (float)((Mathf.Atan2(playerRotation, transform.rotation) / Math.PI) * 180f);
    //    //if (value < 0) value += 360f;

    //    float angle = Quaternion.Angle(playerRotation, transform.rotation);

    //    Debug.Log("Angle Difference: " + angle);
    //    if (dashTime <= 0.1)
    //    {

    //        if (angle > 0 && angle < 45 || angle < 135 && angle > 90)
    //        {
    //            //rb.AddForce(Vector3.left * dashSpeed, ForceMode.Force);
    //            int movement = Random.Range(0, 1);
    //            if (movement == 1) transform.position += Vector3.left * dashSpeed;
    //            if (movement == 0) transform.position += Vector3.right * dashSpeed;

    //            dashTime = startDashTime;
    //            Debug.Log("90 < 180");
    //        }
    //        else if (angle > 45 && angle < 90 || angle > 135 && angle < 180)
    //        {
    //            int movement2 = Random.Range(0, 1);

    //            if (movement2 == 1) transform.position += Vector3.back * dashSpeed;
    //            if (movement2 == 0) transform.position += Vector3.forward * dashSpeed;
    //            //transform.position += Vector3.back * dashSpeed;
    //            dashTime = startDashTime;
    //            Debug.Log("180 < 270");
    //            //}
    //            //else if (angle > 270 && angle < 360)
    //            //{
    //            //    transform.position += Vector3.right * dashSpeed;
    //            //    dashTime = startDashTime;
    //            //    Debug.Log("270 < 360");

    //            //}
    //            //else if (angle > 0 && angle < 90)
    //            //{
    //            //    transform.position += Vector3.left * dashSpeed;
    //            //    dashTime = startDashTime;
    //            //    Debug.Log("0 < 90");

    //            //}
    //        }

    //    }
    //}

    // Update is called once per frame
    public void Update()
    {
        if (dashTime >= 0)
        {
            dashTime = dashTime - Time.deltaTime;
        }
        //Debug.Log(initialPlayerAngle);
        Calculations();
        //Debug.Log(StepsuntilShoot);
        LimitMove();
        endEp += 1 * Time.deltaTime;
        shootingDelay += 1 * Time.deltaTime;
        ShootingDelayTime();
        //TimeBeforeEnd();
        //Dash(playerRotation);
        enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);
        enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsEnemy);
        //Debug.Log(direction);
        //myRigidbody.velocity = Vector3.left * dashSpeed;
        //transform.position += Vector3.left * dashSpeed;
        //Debug.Log(ShotReady);
        if (ShotReady == false)
        {
            StepsuntilShoot = StepsuntilShoot - (1 * Time.deltaTime);
            if (StepsuntilShoot <= 0)
            {
                ShotReady = true;
                RequestDecision();
            }
        }
    }




}
