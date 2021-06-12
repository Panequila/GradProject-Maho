using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager 
{
    public static string username;
    public static int id;
  


    public static bool LoggedIn { get { return username != null; } }

    public static void Logout()
    {
        username = null;
    }
}
