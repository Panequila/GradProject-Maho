using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class HealthBar : NetworkBehaviour
{
    [Header("Settings")]
    [SerializeField] public int maxHealth = 100;

    [SyncVar]
    public int currentHealth = 100;

    public delegate void HealthChangedDelegate(int currentHealth);
    public event HealthChangedDelegate EventHealthChanged;

    #region Server
    public void SetHealth(int value)
    {
        currentHealth -= value;
        Debug.Log(this.transform.gameObject.name + currentHealth);
        EventHealthChanged?.Invoke(currentHealth);
        if (currentHealth <= 0)
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
        Debug.Log(this.transform.gameObject.name + "CMD Deal Damage");
        SetHealth(damage);
    }

    #endregion

    #region Client
    #endregion
}
