using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class PlayerScript : NetworkBehaviour
{
    // Start is called before the first frame update
    [Header("Player Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public Text PlayerText;
    [SyncVar]
    public string Pname;
    [Header("Components")]
    public HealthBar healthBar;

    [Header("Children Components")]
    public GameObject myCanvas, myCanvas2;
    public Button ShootButton;
    public GameObject projectilePrefab;
    public GameObject ShootingPoint;
    [Header("Firing")]
    public KeyCode shootKey = KeyCode.Space;

    [Header("Audio")]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip laserSound;
    //public void Start()
    //{
       
    //    currentHealth = maxHealth;
    //    //healthBar.SetMaxHealth(maxHealth);
    //}

    public override void OnStartLocalPlayer()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = laserSound;

        base.OnStartLocalPlayer();
        //if (DBManager.LoggedIn)
        //{
        //    PlayerText.text = "Player :" + DBManager.username;
        //    Pname = DBManager.username;
        //}
        //mohma 3shan el shoot tsht8l bel network ... leh m3rfsh
        myCanvas.SetActive(true);
        //myCanvas2.SetActive(true);

        //currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);

        Button btn = ShootButton.GetComponent<Button>();
        btn.onClick.AddListener(CmdFire);
    }
    public void Shoot()
    {
        Debug.Log("Shoot");
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(shootKey))
        {
           
            CmdFire();
            audioSource.Play();
        }
    }
    [Command]
    public void  CmdFire()
    {
        ////this.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
        //if (!isLocalPlayer) return;
        //if (hasAuthority)
        GameObject projectile = Instantiate
            (projectilePrefab,ShootingPoint.transform.position, ShootingPoint.transform.rotation, this.transform);
        NetworkServer.Spawn(projectile);
        //audioSource.Play();
        //NetworkManager.Instantiate(projectilePrefab, arCamera.transform.position, arCamera.transform.rotation);
        //RpcClientShot();

    }

    //public void Shoot()
    //{
    //    GameObject projectile = Instantiate(projectilePrefab, arCamera.transform.position, arCamera.transform.rotation);
    //    NetworkServer.Spawn(projectile, connectionToServer);
    //}
    //[ClientRpc]
    //void RpcClientShot()
    //{
    //    GameObject projectile = Instantiate(projectilePrefab, arCamera.transform.position, arCamera.transform.rotation);
    //    NetworkServer.Spawn(projectile);

    //}
}
