using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBat : AgentBehv
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Calculations()
    {
        base.Calculations();
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (dashTime >= 0)
    //    {
    //        dashTime = dashTime - Time.deltaTime;
    //    }
    //    // Debug.Log(initialPlayerAngle);
    //    //Shoot();
    //    Calculations();
    //    //Debug.Log(StepsuntilShoot);
    //    endEp += 1 * Time.deltaTime;
    //    shootingDelay += 1 * Time.deltaTime;
    //    ShootingDelayTime();
    //    TimeBeforeEnd();
    //    //Dash(playerRotation);
    //    enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);
    //    enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsEnemy);
    //    //Debug.Log(direction);
    //    //myRigidbody.velocity = Vector3.left * dashSpeed;
    //    //transform.position += Vector3.left * dashSpeed;
    //    //Debug.Log(ShotReady);
    //    if (ShotReady == false)
    //    {
    //        StepsuntilShoot = StepsuntilShoot - (1 * Time.deltaTime);
    //        if (StepsuntilShoot <= 0)
    //        {
    //            ShotReady = true;
    //            RequestDecision();
    //        }
    //    }
    //}
}


