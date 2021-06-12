using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class MyManager : NetworkManager
{
    // Start is called before the first frame update
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
    }

    
}
