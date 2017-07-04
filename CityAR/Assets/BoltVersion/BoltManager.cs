using System.Collections;
using System.Collections.Generic;
using Bolt;
using UdpKit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoltManager : GlobalEventListener
{
    
    public int localPort = 27000;
    public InputField AdressInput;
    public Text DebugText;
    public string Debug;

    void Update()
    {
        DebugText.text = Debug;
    }

    public void StartServer()
    {
        BoltLauncher.StartServer(new UdpEndPoint(UdpIPv4Address.Any, (ushort)localPort));
    }

    public void StartClient()
    {

        BoltLauncher.StartClient(new UdpEndPoint(UdpIPv4Address.Any, (ushort)localPort));
        StartCoroutine(AutoConnect());
    }

    IEnumerator AutoConnect()
    {
        //wait for bolt to start
        yield return new WaitForSeconds(1f);
        
        //check if there are any sessions
        if (BoltNetwork.SessionList.Count > 0)
        {
            Debug += "\nSession found. Attempting connection";
            UdpSession session = null;
            foreach (var s in BoltNetwork.SessionList)
            {
                session = s.Value;
                break;
            }
            if (session != null)
            {
                UdpEndPoint endpoint = UdpEndPoint.Parse(session.LanEndPoint.Address + ":" + localPort);
                Debug += "\nEnd Point: " + session.LanEndPoint.Address + ":" + localPort;
                BoltNetwork.Connect(endpoint);
            }
        }
        else
        {
            Debug += "\nNo session found. Attempting manual input";
            System.Net.IPAddress aIP;
            if (!System.Net.IPAddress.TryParse(AdressInput.text, out aIP))
            {
                Debug += "\nINVALID IP!";
            }
            else
            {
                UdpEndPoint endpoint = UdpEndPoint.Parse(AdressInput.text + ":" + localPort);
                BoltNetwork.Connect(endpoint);
                Debug += "\nIp accepted. Attempting connection";
            }
        }
        yield return new WaitForSeconds(1f);
        if (BoltNetwork.isConnected)
        {
            Debug += "\nConnection established";
        }
        else
        {
            Debug += "\nTry a different IP. Make sure a server is hosted";
        }
    }

    public void Shutdown()
    {
        BoltNetwork.DisableLanBroadcast();
        BoltLauncher.Shutdown();
    }

    public void Restart()
    {

        Shutdown();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TestButton()
    {
        
        if (BoltNetwork.SessionList.Count > 0)
        {
            Debug += "\nSession found. Attempting connection";
            UdpSession session = null;
            foreach (var s in BoltNetwork.SessionList)
            {
                session = s.Value;
                break;
            }
            if (session != null)
            {
                UdpEndPoint endpoint = UdpEndPoint.Parse(session.LanEndPoint.Address + ":" + localPort);
                Debug += "\nEnd Point: " + session.LanEndPoint.Address + ":" + localPort;
                BoltNetwork.Connect(endpoint);
            }
        }
        else
        {
            Debug += "\nNo session found." + BoltNetwork.SessionList.Count;
        }

    }
}
