using UnityEngine;
using Mirror;
public class Projectile : NetworkBehaviour
{
    public float destroyAfter = 5;
    public Rigidbody rigidBody;
    public float force = 1000;
    int damage = 5;
    public AudioSource source;
    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    // set velocity for server and client. this way we don't have to sync the
    // position, because both the server and the client simulate it.
    void Start()
    {
        source = GetComponent<AudioSource>();
        rigidBody.AddForce(transform.forward * force);
    }

    // destroy for everyone on the server
    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    // ServerCallback because we don't want a warning if OnTriggerEnter is
    // called on the client
    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        //if (other.tag.Equals("agent"))
        //{
        //    //Debug.Log(transform.parent.name);
        //    other.GetComponentInChildren<HealthBar>().GetShot(damage, transform.parent.name);

        //}
        if (other.tag.Equals("Bat"))
        {
           // Debug.Log(transform.parent.name);
            other.GetComponentInParent<HealthTest>().CmdDealDamage(damage, transform.parent.name);
            //other.GetComponent<BatScript>().GetShot2(damage, transform.parent.name);

        }
        NetworkServer.Destroy(gameObject);
    }
}

