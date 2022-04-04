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
        [Tooltip("The time that shall pass before this projectile is auto-destroyed. 0 = infinite.")]
        public float lifeTime = 2f;

        public int Damage { get => damage; set => damage = value; }

        protected abstract void OnImpact(GameObject other);
    }
}