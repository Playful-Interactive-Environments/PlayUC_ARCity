using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpriteHandler : MonoBehaviour
{

    public Material[] SpriteMats;
    public Material[] IdleMats;
    public SpriteRenderer spriteRenderer;
    public int matId;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        matId = Utilities.RandomInt(0, SpriteMats.Length - 1);
        MoveMat();
    }

    public void IdleMat()
    {
        spriteRenderer.material = IdleMats[matId];

    }

    public void MoveMat()
    {
        spriteRenderer.material = SpriteMats[matId];
    }
}
