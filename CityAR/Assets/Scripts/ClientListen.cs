using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class ClientListen : MonoBehaviour {
    
    UdpClient client;
    int receivePort = 7778;
    public string serverIP;
    IPAddress groupIP = IPAddress.Parse("224.0.0.224");
    IPEndPoint remoteEP;
    public  bool listenStarted;

    public void StartClientListen()
    {
        if (listenStarted)
            return;
        remoteEP = new IPEndPoint(IPAddress.Any, receivePort);

        client = new UdpClient(remoteEP);
        client.JoinMulticastGroup(groupIP);

        client.BeginReceive(new AsyncCallback(ReceiveServerInfo), null);
        Debug.Log("Starting Client" + remoteEP);
        listenStarted = true;

    }
    void ReceiveServerInfo(IAsyncResult result)
    {
        byte[] receivedBytes = client.EndReceive(result, ref remoteEP);
        serverIP = Encoding.ASCII.GetString(receivedBytes);
        Debug.Log("Received Server Info" + serverIP);

    }
    public void StopListenning()
    {
        Debug.Log("Stop Listening");
        client.DropMulticastGroup(groupIP);
        client.Close();
        listenStarted = false;
    }
}
