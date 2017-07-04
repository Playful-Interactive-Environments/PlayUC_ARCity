using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateText : MonoBehaviour
{
    public float BlendSpeed = 2;
    public float MovementSpeed;
    public string Content;
    public Vector2 StartPos;
    public Vector2 TargetPos;
    private bool animate;
    public Text Text;
    public RectTransform Transform;
    private float currentTime;
    private float alpha;

    void Start()
    {
        Transform = GetComponent<RectTransform>();
        Text = GetComponent<Text>();
    }

    void Update()
    {
        if (animate)
        {
            currentTime += Time.fixedDeltaTime;
            Transform.anchoredPosition = Vector3.Lerp(Transform.anchoredPosition, TargetPos, MovementSpeed * Time.deltaTime);
            alpha = currentTime / BlendSpeed;
            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 1 - alpha);
        }
    }

    public void Init(Color col, string text, int fontsize, float moveSpeed, float animSpeed, Vector2 startPos, Vector2 endPos)
    {
        transform.parent = MGManager.Instance.MainCanvas.transform;
        transform.localScale = new Vector3(1, 1, 1);
        Content = text;
        StartPos = startPos;
        TargetPos = endPos;
        MovementSpeed = moveSpeed;
        BlendSpeed = animSpeed;
        Text.text = Content;
        Text.color = col;
        Text.fontSize = fontsize;
        currentTime = 0;
        alpha = 0;
        Transform.anchoredPosition = StartPos;
        Invoke("Dispose", BlendSpeed + 2f);
        animate = true;
    }

    void Dispose()
    {
        ObjectPool.Recycle(gameObject);
        animate = false;
    }
}