using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Host)]
public class ServerCallbacks : GlobalEventListener {

    public override void BoltStartDone()
    {
        BoltNetwork.EnableLanBroadcast();
        BoltNetwork.Instantiate(BoltPrefabs.BoltPlayer);
        print("BoltStartDone");
    }

    public override void Connected(BoltConnection connection)
    {
        BoltNetwork.Instantiate(BoltPrefabs.BoltPlayer);
        print("Connected" + connection.ConnectionId);
    }
}
