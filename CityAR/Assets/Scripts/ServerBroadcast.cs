using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class ServerBroadcast : MonoBehaviour {

    UdpClient serverOriginator;
    string serverIP;
    int broadcastPort = 25123;
    IPAddress groupIP = IPAddress.Parse("224.0.0.224");
    IPEndPoint remoteEP;
    public bool broadcastStarted;

    public void StartServerBroadcast()
    {
        if (broadcastStarted)
            return;
        //Get Server IP
        serverIP = Network.player.ipAddress;

        //Create UDP Client for broadcasting the server
        serverOriginator = new UdpClient();

        serverOriginator.JoinMulticastGroup(groupIP);

        remoteEP = new IPEndPoint(groupIP, broadcastPort);
        broadcastStarted = true;
        //Broadcast IP
        InvokeRepeating("BroadcastServerIP", 0, 1f);
    }
    void BroadcastServerIP()
    {
        //Debug.Log(Time.realtimeSinceStartup + ": Broadcasting IP:" + serverIP);
        byte[] buffer = ASCIIEncoding.ASCII.GetBytes(serverIP);
        serverOriginator.Send(buffer, buffer.Length, remoteEP);
    }
    public void StopBroadcasting()
    {
        Debug.Log("Stop Broadcast");
        CancelInvoke("BroadcastServerIP");
        serverOriginator.DropMulticastGroup(groupIP);
        serverOriginator.Close();
        broadcastStarted = false;
    }
}
