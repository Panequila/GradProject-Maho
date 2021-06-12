    //using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Mirror;
//public class ProjectileScript : NetworkBehaviour
//{
//    public float destroyAfter = 5;
//    public Rigidbody rigidBody;
//    public float force = 1000;
//    public int damage = 20;

//    public override void OnStartServer()
//    {
//        Invoke(nameof(DestroySelf), destroyAfter);
//    }

//    // set velocity for server and client. this way we don't have to sync the
//    // position, because both the server and the client simulate it.
//    void Start()
//    {
//        rigidBody.AddForce(transform.forward * force);
//    }

//    // destroy for everyone on the server
//    [Server]
//    void DestroySelf()
//    {
//        NetworkServer.Destroy(gameObject);
//    }

//    // ServerCallback because we don't want a warning if OnTriggerEnter is
//    // called on the client
//    [ServerCallback]
//    void OnTriggerEnter(Collider other)
//    {
//        NetworkServer.Destroy(gameObject);

//        if (other.tag.Equals("agent"))
//        {
//            Debug.Log("Agent Hit");
//            other.GetComponent<AgentBehv>().GetShot(damage);

//        }
//        else
//        {
//            NetworkServer.Destroy(gameObject);

//        }
//    }
//}
