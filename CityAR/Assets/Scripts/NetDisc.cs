using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetDisc : NetworkDiscovery
{

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        var items = fromAddress.Split(':');
        NetMng.Instance.DebugText.text = items[3];
        NetMng.Instance.networkAddress = items[3];
        NetMng.Instance.StartClient();
        StopBroadcast();
    }
}
