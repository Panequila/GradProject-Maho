using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public float TimeToLive = 1.75f;
    public int MonsterDamage = 100, PlayerDamage = 40;

    public GameObject parentObject;
    public GameObject monsterObject;
    private void Start()
    {
        Destroy(gameObject, TimeToLive);
    }

    void Update()
    {   

        transform.Translate(Vector3.forward * 15 * Time.deltaTime);
        //transform.GetComponent<AgentBehv>().GetShot(Damage);

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Destroy(gameObject);
            parentObject = transform.parent.gameObject; //hat gameObject el parent
           // Debug.Log(parentObject);
            monsterObject = parentObject.transform.Find("Bat_Agent").gameObject; //hat gameObject el monster el gowa el parent
           // Debug.Log(monsterObject);
            other.GetComponent<Player_AI>().GetShot(MonsterDamage, monsterObject.GetComponent<AgentBat>());
            // tempPlayer = other.GetComponent<AgentBehv>();
            // other.GetComponent<Player_AI>().GetShot(Damage,tempPlayer);
            // Debug.Log("El player edrb"); 
        }
        else if (other.tag.Equals("wall"))
        {
           // Debug.Log("Hit the Wall");

            parentObject = transform.parent.gameObject; //hat gameObject el parent
            monsterObject = parentObject.transform.Find("Bat_Agent").gameObject; //hat gameObject el monster el gowa el parent
            monsterObject.GetComponent<AgentBat>().AddReward(-1f);
            //Debug.Log(parentObject);
            Destroy(gameObject);
        }
    }
}
