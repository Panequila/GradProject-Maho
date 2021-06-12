using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class Player : NetworkBehaviour
{
    public GameObject myCanvas;
    public Button ShootButton;
    [Header("Firing")]
    public KeyCode shootKey = KeyCode.Space;
    public GameObject projectilePrefab;

    void Start()
    {
        if(isLocalPlayer)
        {
            Debug.Log("Canvas Enables");
            myCanvas.SetActive(true);
            Debug.Log(myCanvas);
            Button btn = ShootButton.GetComponent<Button>();
            btn.onClick.AddListener(CmdFire);
        }
    }
    void Update()
    {
        // movement for local player
        if (!isLocalPlayer)
        {
            return;
        }
        // shoot
        if (Input.GetKeyDown(shootKey))
        {
            CmdFire();
        }
    }
    public void Shoot()
    {
        Debug.Log("Shooting");
        if (!isLocalPlayer)
        {
            return;
        }
        CmdFire();
    }
    // this is called on the server
    [Command]
    void CmdFire()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        NetworkServer.Spawn(projectile);
    }
}
