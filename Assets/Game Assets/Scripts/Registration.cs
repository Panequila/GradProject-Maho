using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Registration : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Button submitButton;

    public UIManager uiManager;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }
    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("maho.website/register.php", form);
        yield return www; 
        if(www.text == "0") 
        {
            Debug.Log("User Created Successfully.");
            uiManager.RegisterButton();
            //SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("User Creation Failed. Error #" + www.text);
        }
    }

    public void VerifyInputs()
    {
        //hynf3 tdos 3leh wlla la2
        submitButton.interactable = (usernameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }

}
