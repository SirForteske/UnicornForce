using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PickableItemScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pickupFX;
    [SerializeField]
    private float fxDuration = 0.25f;
    
    public bool IsPlayerOver { get; private set; }

    protected virtual void Start()
    {
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickupFX != null)
                Destroy(Instantiate(pickupFX, transform.position, Quaternion.identity), fxDuration);

            OnPlayerPick(other.gameObject);
        }
    }

    protected abstract void OnPlayerPick(GameObject player);
}
