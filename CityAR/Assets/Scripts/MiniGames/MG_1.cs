using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_1 : AManager<MG_1>
{
    public GameObject WordPrefab;
    public GameObject DropPrefab;
    public List<GameObject> WordList = new List<GameObject>();
    public GameObject TargetFinance;
    public GameObject TargetSocial;
    public GameObject TargetEnvironment;
    private MGManager manager;
    private float Height;
    private float Width;
    private float threshold = 1f;
    private float spawnTime;
    public float TimeLimit = 20f;
    public int CollectedDocs;

	void Start () {
		ObjectPool.CreatePool(WordPrefab, 5);
        ObjectPool.CreatePool(DropPrefab, 3);
	    manager = MGManager.Instance;
	}

    void Update()
    {
        Height = manager.Height;
        Width = manager.Width;
        if (Time.time > spawnTime)
        {
            spawnTime = Time.time + threshold;
        }
    }

    public void SpawnWord()
    {
        ObjectPool.Spawn(WordPrefab, manager.MG_1_GO.transform);
    }
    public void InitGame()
    {
        Vector3 finDropPos = new Vector3(-Width / 3, Height / 4, 0);
        TargetFinance = ObjectPool.Spawn(DropPrefab, manager.MG_1_GO.transform, finDropPos, Quaternion.identity);
        TargetFinance.GetComponent<DropArea>().CurrentType = DropArea.DragZoneType.Finance;

        Vector3 socDropPos = new Vector3(0, Height / 4, 0);
        TargetSocial = ObjectPool.Spawn(DropPrefab, manager.MG_1_GO.transform, socDropPos, Quaternion.identity);
        TargetFinance.GetComponent<DropArea>().CurrentType = DropArea.DragZoneType.Social;

        Vector3 envDropBos = new Vector3(Width / 3, Height / 4, 0);
        TargetEnvironment = ObjectPool.Spawn(DropPrefab, manager.MG_1_GO.transform, envDropBos, Quaternion.identity);
        TargetFinance.GetComponent<DropArea>().CurrentType = DropArea.DragZoneType.Environment;
    }

    public void Reset()
    {
        ObjectPool.RecycleAll(WordPrefab);
        ObjectPool.RecycleAll(DropPrefab);
        WordList.Clear();
    }
}
