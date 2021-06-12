using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class MonsterSpawner : NetworkBehaviour
{
    //public GameObject monsterPrefab;

    //[SyncVar]
    //public int monstersLimit = 1;

    //[SyncVar]
    //public int monstersAlive = 0;

    //[SerializeField]
    //protected float spawnRate = 2;

    //[SerializeField]
    //protected Vector3 spawnPosition;

    //[SerializeField]
    //protected float xRadius = 5, zRadius = 5;

    //[SerializeField]
    //protected Transform playerPosition;

    //[SerializeField]
    //protected int playerCounter;
    //public override void OnStartLocalPlayer()
    //{
    //    base.OnStartLocalPlayer();
    //    StartCoroutine(SpawnMonsters());
    //}

    //public IEnumerator SpawnMonsters()
    //{
    //    while (true)
    //    {
    //        if (monstersAlive < monstersLimit)
    //        {
    //            Spawn();
    //            yield return new WaitForSeconds(spawnRate);
    //        }
    //        else
    //        {
    //            yield return new WaitForSeconds(spawnRate);
    //        }
    //    }
    //}

    //[Command]
    //public void Spawn()
    //{
    //    Debug.Log("Player" + gameObject.name);
    //    //Debug.Log(players[playerCounter].transform.position);
    //    spawnPosition = transform.position;
    //    spawnPosition = new Vector3(
    //        Random.Range(playerPosition.position.x - xRadius, playerPosition.position.x + xRadius),
    //        0,
    //        Random.Range(playerPosition.position.z - zRadius, playerPosition.transform.position.z + zRadius)
    //        );
    //    GameObject newMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    //    newMonster.name = "Monster of: " + gameObject.name;
    //    NetworkServer.Spawn(newMonster);
    //    monstersAlive++;
    //}

    //public void Kill()
    //{
    //    monstersAlive--;
    //}

    //void Update()
    //{

    //}
}
