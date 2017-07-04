using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Client)]
public class ClientCallbacks : GlobalEventListener {

    public override void BoltStartDone()
    {
        BoltNetwork.EnableLanBroadcast((ushort)7777);
        //BoltManager.Instance.Debug += "Client Started " + Network.player.ipAddress;
    }
}
