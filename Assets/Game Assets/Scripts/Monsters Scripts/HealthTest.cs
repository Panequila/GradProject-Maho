using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class HealthTest : NetworkBehaviour
{
    [Header("Settings")]
    [SerializeField] public int maxHealth = 100;

    [SyncVar]
    public int currentHealth = 100;

    public delegate void HealthChangedDelegate(int currentHealth);
    public event HealthChangedDelegate EventHealthChanged;

    #region Server
    [Server]
    public void SetHealth(int value)
    {
        currentHealth -= value;
        EventHealthChanged?.Invoke(currentHealth);
        if(currentHealth <= 0)
        {
            NetworkBehaviour.Destroy(gameObject);
        }
    }
    public void SetMaxHealth(int value)
    {
        currentHealth = value;
    }
    public override void OnStartServer()
    {
        //SetHealth(maxHealth);
        SetMaxHealth(maxHealth);
    }
    public void CmdDealDamage(int damage, string playerName)
    {
        //GameObject playerObject = GameObject.Find(playerName);
        //playerObject.GetComponent<MonsterSpawner>().Kill();
        SetHealth(damage);
    }
    #endregion

    #region Client
    #endregion
}
