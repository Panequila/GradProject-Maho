using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class BatScript : NetworkBehaviour
{
    public HealthBar healthBar;

    ////public void Start()
    ////{
    ////    healthBar.SetMaxHealth();
    ////}
    //public void CmdGetShot(int damage, string playerName)
    //{
    //    if (healthBar.currentHealth <=5)
    //    {
    //        //NetworkServer.Destroy(gameObject);
    //        GameObject playerObject = GameObject.Find(playerName);
    //        playerObject.GetComponent<MonsterSpawner>().Kill();
    //        //Score--;
    //        //registerDeath();
    //        Debug.Log("Agent is Dead");
    //        //NetworkBehaviour.Destroy(transform.parent.gameObject);
    //        //this.GetComponent<AgentNetwork>().KillMonster();
    //        KillMonster();
    //        //Respwan();
    //    }
    //    else if(healthBar.currentHealth > 5)
    //    {
    //        Debug.Log("Enemy Got Shot");
    //        //currentHealth -= damage;
    //        //healthBar.SetHealth(damage);
    //        healthBar.TakeDamage(damage);
    //    }
    //}
    //public void KillMonster()
    //{
    //    //GetComponentInParent<AgentNetwork>().KillMonster();
    //    NetworkBehaviour.Destroy(gameObject);
    //}
}
