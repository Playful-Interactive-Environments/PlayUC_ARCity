using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Networking;

public class GlobalEvents : NetworkBehaviour
{
    public static GlobalEvents Instance;
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
        Invoke("PopulateEventPool", .1f);
        if (isServer)
        {
            EventManager.StartListening("EnvironmentEvent", EnvironmentEvent);
            EventManager.StartListening("EnvironmentEvent", BudgetEvent);
            EventManager.StartListening("EnvironmentEvent", QuestEvenet);
            EventManager.StartListening("EnvironmentEvent", FinanceEvent);
            EventManager.StartListening("EnvironmentEvent", SocialEvent);
        }
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
    
    public void TriggerRandomEvent()
    {
        CellManager.Instance.NetworkCommunicator.HandleEvent("StartEvent", GenerateRandomEvent());
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
        eventscript.TimeLeft = ConvertToFloat(CSVEvents.GetTime(id));
        eventscript.CurrentGoal = ConvertToInt(CSVEvents.GetGoal(id));
        NetworkServer.Spawn(gobj);
    }

    public int GenerateRandomEvent()
    {
        if (EventPool.Count == 0)
            PopulateEventPool();
        int randomProject = Random.Range(0, EventPool.Count);
        int returnId = EventPool[randomProject];
        EventPool.RemoveAt(randomProject);
        return returnId;
    }
    void PopulateEventPool()
    {
        if (isServer)
        {
            for (int i = 1; i <= CSVEvents.rowList.Count; i++)
            {
                EventPool.Add(i);
            }
        }
    }

    public EventScript FindProject(int projectnum)
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
}