using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen object variables
    public GameObject mainMenuUI;
    public GameObject loginUI;
    public GameObject registerUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    //Functions to change the login screen UI
    public void LoginScreen() //Login Button
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
        mainMenuUI.SetActive(false);
    }
    public void RegisterScreen() //Register button
    {
        registerUI.SetActive(true);
        loginUI.SetActive(false);
        mainMenuUI.SetActive(false);
    }
    public void MainMenuScreen() //MainMenu
    {
        mainMenuUI.SetActive(true);
        loginUI.SetActive(false);
        registerUI.SetActive(false);
    }
    public void RegisterButton()
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
        mainMenuUI.SetActive(false);
    }
    public void ExitGame() //Exit Button
    {
        Application.Quit();
    }
}
