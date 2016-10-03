using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ConstructionObject : NetworkBehaviour
{
    [SyncVar]
    public bool ObjectTaken;
    public GridPieceLogic GridPieceScript;
    [SyncVar]
    public int JobsInfluence;
    [SyncVar]
    public int PollutionInfluence;
    [SyncVar]
    public float TimeStamp;
    public TextMesh PollText;
    public TextMesh JobsText;

    void Start()
    {
        if (isServer)
        {
            TimeStamp = Random.Range(5, 10);
            int chance = Random.Range(1, 11);
            int value = Random.Range(-3, 4);
            if (chance <= 5)
            {
                JobsInfluence = value;
            }
            if (chance > 5)
            {
                PollutionInfluence = value;
            }
        }
        GetComponentInChildren<Renderer>().sortingOrder = 1;
        transform.parent = ObjectManager.Instance.ImageTarget.transform;
    }

    void Update()
    {
        PollText.text = "Pollution: " + PollutionInfluence;
        JobsText.text = "Jobs: " + JobsInfluence;
        if (ObjectTaken)
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        if (!hasAuthority && !ObjectTaken)
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        if (hasAuthority && !ObjectTaken)
            gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void PlaceObject(GameObject gridpiece)
    {
        GridPieceScript = gridpiece.GetComponent<GridPieceLogic>();
        InvokeRepeating("ChangeVars", 0f, TimeStamp);
    }

    void ChangeVars()
    {

            if (GridPieceScript.PollutionRate - PollutionInfluence >= 0)
            {
                    ObjectManager.Instance.UpdatePollutionVar(GridPieceScript.Count, PollutionInfluence);
                    GridPieceScript.PolutionEffect(PollutionInfluence, TimeStamp);
            }
            if (GridPieceScript.JobsRate - JobsInfluence >= 0)
            {
                    ObjectManager.Instance.UpdateUnemploymentVar(GridPieceScript.Count, JobsInfluence);
                    GridPieceScript.UnemploymentEffect(JobsInfluence, TimeStamp);
            }
    }
}