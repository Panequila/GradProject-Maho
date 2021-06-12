using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Login : MonoBehaviour
{
    public GameObject myCanvas;
    public InputField usernameField;
    public InputField passwordField;

    public Button submitButton;

    public void CallLogin()
    {
        StartCoroutine(LoginPlyaer());
    }
    IEnumerator LoginPlyaer() 
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("maho.website/login.php", form);
        yield return www;
        if (www.text == "0")
        {
            myCanvas.SetActive(false);
            DBManager.username = usernameField.text;
           // DBManager.id=
            //StartHost();
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            //StartHost();
            //if (NetworkManager.networkSceneName == "Maho")
            //{
            //    StartServer();
            //    //NetworkClient.RegisterPrefab(playerPrefab);
            //}
            Debug.Log("deebalpha");


        }
        else 
        {
            Debug.Log("UserLogin failed");
        }
    }
    //public override virtual void onserveraddplayer(networkconnection conn)
    //{
    //    transform startpos = getstartposition();
    //    gameobject player = startpos != null
    //        ? instantiate(playerprefab, startpos.position, startpos.rotation)
    //        : instantiate(playerprefab);

    //    // instantiating a "player" prefab gives it the name "player(clone)"
    //    // => appending the connectionid is way more useful for debugging!
    //    player.name = $"{playerprefab.name} [connid={conn.connectionid}]";
    //    networkserver.addplayerforconnection(conn, player);
    //}
    //public override void OnStartClient()
    //{
    //    base.OnStartClient();

    //}
    public void VerifyInputs()
    {
        //hynf3 tdos 3leh wlla la2
        submitButton.interactable = (usernameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }

}
