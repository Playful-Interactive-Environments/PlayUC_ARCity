using System.Collections;
using System.Collections.Generic;
using Bolt;
using UdpKit;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Host)]
public class ServerCallbacks : GlobalEventListener {

    public override void BoltStartDone()
    {
        BoltNetwork.Instantiate(BoltPrefabs.BoltPlayer);
        BoltNetwork.EnableLanBroadcast((ushort) 7777);
        BoltNetwork.SetHostInfo("BoltGame", null);
        
       // BoltManager.Instance.Debug += "LAN Broadcast Enabled " + Network.player.ipAddress;
    }

    public override void Connected(BoltConnection connection)
    {
        BoltNetwork.Instantiate(BoltPrefabs.BoltPlayer);
        print("Connected" + connection.ConnectionId);
    }

    public override void ConnectRequest(UdpEndPoint endpoint, IProtocolToken token)
    {
        BoltNetwork.Accept(endpoint);
       // BoltManager.Instance.Debug += "Connect Request" + endpoint.Address;
    }
}
