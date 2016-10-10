using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RoleManager : NetworkBehaviour
{
    public static RoleManager Instance = null;
    public enum RoleType
    {
        Environment, Finance, Social
    }

    [SyncVar]
    public bool Environment;
    [SyncVar]
    public bool Finance;
    [SyncVar]
    public bool Social;
    public RoleType CurrentRole;

    void Awake () {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
	
	void Update () {

	}
}
