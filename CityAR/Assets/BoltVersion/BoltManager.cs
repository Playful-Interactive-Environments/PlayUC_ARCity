using System.Collections;
using System.Collections.Generic;
using UdpKit;
using UnityEngine;
using UnityEngine.UI;

public class BoltManager : MonoBehaviour
{

    public int localPort = 27000;
    public InputField AdressInput;
    public void StartServer()
    {
        BoltLauncher.StartServer(new UdpEndPoint(UdpIPv4Address.Any, (ushort) localPort));
    }

    public void StartClient()
    {
        BoltLauncher.StartClient(new UdpEndPoint(UdpIPv4Address.Localhost, (ushort) localPort));
        StartCoroutine(AutoConnect());
    }
    
    IEnumerator AutoConnect()
    {
        if (BoltNetwork.SessionList.Count > 0)
        {
            print("Session found. Attempting connection");
            UdpSession session = null;
            foreach (var s in BoltNetwork.SessionList)
            {
                session = s.Value;
                break;
            }
            if (session != null)
            {
                UdpEndPoint endpoint = UdpEndPoint.Parse(session.LanEndPoint.Address + ":" + localPort);
                BoltNetwork.Connect(endpoint);
            }
        }
        else
        {
            print("No session found. Attempting manual input");
            UdpEndPoint endpoint = UdpEndPoint.Parse(AdressInput + ":" + localPort);
            BoltNetwork.Connect(endpoint);
        }
        yield return new WaitForSeconds(1f);
        if (BoltNetwork.isConnected)
        {
            print("Connection established");
            yield break;
        }
        else
        {
            BoltLauncher.Shutdown();
            print("Try a different IP. Make sure a server is hosted");
            yield break;
        }
    }

    public void Shutdown()
    {
        BoltLauncher.Shutdown();
    }
}
