using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    public TextMesh FPSText;
    private float fps;


    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        FPSText.text = text;

    }
}