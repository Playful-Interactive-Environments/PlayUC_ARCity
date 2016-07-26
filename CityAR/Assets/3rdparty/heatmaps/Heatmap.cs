// Alan Zucconi
// www.alanzucconi.com
using UnityEngine;
using System.Collections;
using Vuforia;



public class Heatmap : MonoBehaviour
{
    public float Intensity;
    public float Radius;
    public int Count = 50;

    public Vector3[] positions;
    public float[] radiuses;
    public float[] intensities;

    public Material material;




    void Start ()
    {
        positions = new Vector3[Count];
        radiuses = new float[Count];
        intensities= new float[Count];
        InvokeRepeating("ChangeValues", 0f, 5f);
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = new Vector3(Random.Range(- ValueManager.Instance.MapWidth / 2, ValueManager.Instance.MapWidth / 2), 0, Random.Range(- ValueManager.Instance.MapHeight / 2, ValueManager.Instance.MapHeight / 2));
           
        }
    }

    void Update()
    {
        
        
    }

    public void ChangeValues()
    {
        material.SetInt("_Points_Length", positions.Length);
        for (int i = 0; i < positions.Length; i++)
        {
            material.SetVector("_Points" + i.ToString(), positions[i]);
            Vector2 properties = new Vector2(radiuses[i], intensities[i]);
            material.SetVector("_Properties" + i.ToString(), properties);
            radiuses[i] = Random.Range(0, Radius);
            intensities[i] = Random.Range(0, Intensity);
        }
    }
}
