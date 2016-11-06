using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GlobalManager : NetworkBehaviour
{
    public static GlobalManager Instance = null;
    public class PlayerData
    {
        public string RoleType;
        public bool RoleTaken;
        public int Rating;
        public int Budget;
        public List<int> CurrentProjects = new List<int>();
    }

    [SyncVar]
    public float GameDuration;
    private float _currentTime;

    [SyncVar]
    public float MonthDuration = 10f;
    
    PlayerData EnvironmentPlayer = new PlayerData();
    PlayerData SocialPlayer = new PlayerData();
    PlayerData FinancePlayer = new PlayerData();

    void Awake () {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
	
	void Update ()
	{
	    _currentTime += Time.deltaTime;
	}

    public void SavePlayerData(string roletype, bool roletaken, int rating, int budget)
    {
        switch (roletype)
        {
            case "Environment":
                EnvironmentPlayer.RoleTaken = roletaken;
                EnvironmentPlayer.RoleTaken = roletaken;
                EnvironmentPlayer.Rating = rating;
                EnvironmentPlayer.Budget = budget;

                break;
            case "Social":
                SocialPlayer.RoleTaken = roletaken;

                break;
            case "Finance":
                FinancePlayer.RoleTaken = roletaken;

                break;

        }
    }
}
