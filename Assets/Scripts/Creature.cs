using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Creature : Enemy
{
    public SpriteRenderer Renderer;
    public Color CorruptedColor;

    [SerializeField]
    private bool isCorrupt = true;

    protected override void Start()
    {
        base.Start();
        Renderer.color = isCorrupt ? CorruptedColor : Color.white;
    }

    public void ToogleCorrupted()
    {
        isCorrupt = !isCorrupt;

        Renderer.color = isCorrupt ? CorruptedColor : Color.white;
    }

    protected override void Destruction()
    {
        if (isCorrupt)
        {
            ToogleCorrupted();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
