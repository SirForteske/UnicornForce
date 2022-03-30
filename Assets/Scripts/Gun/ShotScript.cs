using UnityEngine;

namespace Gun
{
    [RequireComponent(
        typeof(Rigidbody2D),
        typeof(Collider2D))]
    public abstract class ShotScript: MonoBehaviour
    {
        [Tooltip("Damage which a projectile deals to another object. Integer")]
        public int damage;

        public int Damage { get => damage; set => damage = value; }

        protected abstract void OnImpact(GameObject other);
    }
}