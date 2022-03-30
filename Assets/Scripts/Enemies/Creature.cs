using Player;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Creature : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public float health = 2f;

    [Header("Corruption")]
    [SerializeField]
    protected bool isCorrupt = true;

    protected bool _isOutsideScreen = true;

    public bool IsCorrupt { get => isCorrupt; set => isCorrupt = value; }

    protected void Start()
    {
        if (!isCorrupt)
        {
            GetComponent<Animator>().SetBool("Cleansed", !isCorrupt);
            spriteRenderer.material.SetFloat("_GreyscaleBlend", 0f);
        }
    }

    public virtual void ToogleCorrupted()
    {
        isCorrupt = !isCorrupt;

        GetComponent<Animator>().SetBool("Cleansed", !isCorrupt);
        GetComponent<Animator>().Play(isCorrupt ? "Sad" : "Happy");
        spriteRenderer.material.SetFloat("_GreyscaleBlend", isCorrupt ? 1f : 0f);
    }

    public virtual void TakeDamage(float damage)
    {
        if (_isOutsideScreen) return;

        health = Mathf.Max(0, health - damage);

        if(isCorrupt && health == 0)
        {
            ToogleCorrupted();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerScript>().TakeDamage(1);
        }
    }

    protected virtual void OnBecameVisible()
    {
        _isOutsideScreen = false;
    }

    protected virtual void OnBecameInvisible()
    {
        _isOutsideScreen = true;
    }
}
