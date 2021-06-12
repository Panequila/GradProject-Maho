using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AgentNetwork : NetworkBehaviour
{
    [Header("Monster Stats")]
    [SyncVar]
    public int maxHealth = 100;
    [SyncVar]
    public int currentHealth;

    [Header("Components")]
    public HealthBar healthBar;

    //public void Start()
    //{
    //    currentHealth = maxHealth;
    //    SetMaxHealth();
    //}

    //[Server]
    //public void SetMaxHealth()
    //{
    //    Debug.Log(maxHealth);
    //    healthBar.slider.maxValue = maxHealth;
    //    healthBar.slider.value = maxHealth;

    //    //3shan ybd2 a5dr
    //    healthBar.fill.color = healthBar.gradient.Evaluate(1f);
    //}

    //[Server]
    //public void SetHealth()
    //{
    //    healthBar.slider.value = currentHealth;
    //    Debug.Log("SetHealth");
    //    //3shan tb2a mn el 0 lel 1
    //    healthBar.fill.color = healthBar.gradient.Evaluate(healthBar.slider.normalizedValue);
    //}

    //[Server]
    //public void GetShot(int damage, string playerName)
    //{
    //    Debug.Log("Enemy Got Shot");
    //    //currentHealth -= damage;
    //    healthBar.TakeDamage(damage);

    //    if (currentHealth <= 0)
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
    //}
    //public void KillMonster()
    //{
    //    NetworkBehaviour.Destroy(transform.parent.gameObject);
    //}
}
