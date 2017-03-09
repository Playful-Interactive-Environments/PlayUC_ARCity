using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Networking;

public class EventManager : NetworkBehaviour
{
    public static EventManager Instance;
    public SyncListInt EventPool = new SyncListInt();
    public CSVEvents CSVEvents;
    public GameObject EventPrefab;
    public EventScript CurrentEventScript;
    public List<EventScript> Events;
    //Event Listener
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        CSVEvents = CSVEvents.Instance;
        if (isServer)
        {
            EventDispatcher.StartListening("EnvironmentEvent", EnvironmentEvent);
            EventDispatcher.StartListening("EnvironmentEvent", BudgetEvent);
            EventDispatcher.StartListening("EnvironmentEvent", QuestEvenet);
            EventDispatcher.StartListening("EnvironmentEvent", FinanceEvent);
            EventDispatcher.StartListening("EnvironmentEvent", SocialEvent);
            EventDispatcher.StartListening("NetworkDisconnect", NetworkDisconnect);
        }
    }

    void NetworkDisconnect()
    {
        Events.Clear();
    }

    void EnvironmentEvent()
    {
        
    }
    void BudgetEvent()
    {

    }
    void QuestEvenet()
    {

    }
    void FinanceEvent()
    {

    }
    void SocialEvent()
    {

    }
    void Update()
    {

    }
    
    public void TriggerEvent(string type)
    {
        //reacts on event type; store all events of this type and randomly trigger one of the chosen type
        List<int> eventIds = new List<int>();

            switch (type)
            {
                case "Crisis":
                    for (int i = 0; i <= CSVEvents.rowList.Count - 1; i++)
                        {
                            if (type.Equals(CSVEvents.rowList[i].type))
                                eventIds.Add(ConvertToInt(CSVEvents.rowList[i].id));
                        }
                        CellManager.Instance.NetworkCommunicator.HandleEvent("StartEvent", eventIds[Utilities.RandomInt(0, eventIds.Count)]);
                break;
                case "Project":
                    UIManager.Instance.Change(UIManager.UiState.DesignProject);
                    break;
                case "Quest":
                    for (int i = 0; i <= CSVEvents.rowList.Count - 1; i++)
                    {
                        if (type.Equals(CSVEvents.rowList[i].type))
                            eventIds.Add(ConvertToInt(CSVEvents.rowList[i].id));
                    }
                    CellManager.Instance.NetworkCommunicator.HandleEvent("StartEvent", eventIds[Utilities.RandomInt(0, eventIds.Count)]);
                break;
        }
    }

    public void CreateEvent(int id)
    {
        GameObject gobj = Instantiate(EventPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        EventScript eventscript = gobj.GetComponent<EventScript>();
        eventscript.id = id;
        eventscript.type = CSVEvents.GetType(id);
        eventscript.title = CSVEvents.GetTitle(id);
        eventscript.content = CSVEvents.GetContent(id);
        eventscript.choice1 = CSVEvents.GetChoice1(id);
        eventscript.choice2 = CSVEvents.GetChoice2(id);
        eventscript.effect1 = CSVEvents.GetResult1(id);
        eventscript.effect2 = CSVEvents.GetResult2(id);
        eventscript.TimeLeft = ConvertToFloat(CSVEvents.GetTime(id));
        eventscript.CurrentGoal = ConvertToInt(CSVEvents.GetGoal(id));
        NetworkServer.Spawn(gobj);
    }
    
    public EventScript FindEvent(int projectnum)
    {
        if (isClient && !isServer)
        {
            Events.Clear();
            if (isClient && !isServer)
            {
                foreach (EventScript e in FindObjectsOfType<EventScript>())
                {
                    Events.Add(e);
                }
            }
        }
        foreach (EventScript e in Events)
        {
            if (e.id == projectnum)
            {
                return e;
            }
        }
        return null;
    }

    private float ConvertToFloat(string input)
    {
        float parsedInt = 0;
        float.TryParse(input, NumberStyles.AllowLeadingSign, null, out parsedInt);
        return parsedInt;
    }

    private int ConvertToInt(string input)
    {
        int parsedInt = 0;
        int.TryParse(input, NumberStyles.AllowLeadingSign, null, out parsedInt);
        return parsedInt;
    }

    public int RandomEvent()
    {
        return Utilities.RandomInt(0, CSVEvents.rowList.Count);
    }
}