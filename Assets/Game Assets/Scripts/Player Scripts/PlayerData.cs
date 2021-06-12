using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerData : MonoBehaviour
{
    public string playerDisplay;
    // Start is called before the first frame update
    void Start()
    {
        if (DBManager.LoggedIn)
        {
            playerDisplay = DBManager.username;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
