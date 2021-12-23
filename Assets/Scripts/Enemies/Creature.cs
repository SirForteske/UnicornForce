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
    public Material healedMaterial;

    protected void Start()
    {
        spriteRenderer.material = isCorrupt ? spriteRenderer.material : healedMaterial;
    }

    public virtual void ToogleCorrupted()
    {
        isCorrupt = !isCorrupt;

        spriteRenderer.material = isCorrupt ? spriteRenderer.material : healedMaterial;
    }

    public virtual void TakeDamage(float damage)
    {
        health = Mathf.Max(0, health - damage);

        if(isCorrupt && health == 0)
        {
            ToogleCorrupted();
        }
    }

    protected virtual void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerScript>().TakeDamage(1);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            var something = 0;
        }
    }
}
