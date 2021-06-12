using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class BatAgentParent : NetworkBehaviour
{
    public AgentBat agentBat;




    public float shootTime = 3;
    // Update is called once per frame
    public void Shoot()
    {

        if (agentBat.ShotReady == false)
            return;
        else
        {

            GameObject projectile = Instantiate(agentBat.bullet, agentBat.ShootingPoint.position, agentBat.ShootingPoint.rotation, agentBat.grandParent);
            NetworkServer.Spawn(projectile);

            agentBat.ShotReady = false;
            agentBat.StepsuntilShoot = agentBat.minsteps;
            agentBat.RequestDecision();
            agentBat.shootingDelay = 0f;
            agentBat.Shootset.shootTime = agentBat.minsteps;
        }
    }

    void Update()
    {
        if (shootTime >= 0)
        {
            Shoot();
            shootTime = shootTime - Time.deltaTime;
        }
        if (agentBat.dashTime >= 0)
        {
            agentBat.dashTime = agentBat.dashTime - Time.deltaTime;
        }
        // Debug.Log(initialPlayerAngle);
        //Shoot();
        agentBat.Calculations();
        agentBat.LimitMove();
        //Debug.Log(StepsuntilShoot);
        //agentBat.endEp += 1 * Time.deltaTime;
        agentBat.shootingDelay += 1 * Time.deltaTime;
        agentBat.ShootingDelayTime();
       // agentBat.TimeBeforeEnd();
        //Dash(playerRotation);
        agentBat.enemyInAttackRange = Physics.CheckSphere(transform.position, agentBat.attackRange, agentBat.whatIsEnemy);
        agentBat.enemyInSightRange = Physics.CheckSphere(transform.position, agentBat.sightRange, agentBat.whatIsEnemy);
        //Debug.Log(direction);
        //myRigidbody.velocity = Vector3.left * dashSpeed;
        //transform.position += Vector3.left * dashSpeed;
        //Debug.Log(ShotReady);
        if (agentBat.ShotReady == false)
        {
            agentBat.StepsuntilShoot = agentBat.StepsuntilShoot - (1 * Time.deltaTime);
            if (agentBat.StepsuntilShoot <= 0)
            {
                agentBat.ShotReady = true;
                agentBat.RequestDecision();
            }
        }
    }
}
