using System.Collections;
using UnityEngine;

namespace Gun
{
    public abstract class ProjectileScript : ShotScript
    {
        [Tooltip("The time that shall pass before this projectile is auto-destroyed. 0 = infinite.")]
        public float lifeTime = 2f;

        public bool Impacted { get; private set; }

        protected virtual void Awake()
        {
            Impacted = false;
        }

        protected virtual void Start()
        {
            StartCoroutine(Autodestroy());
        }

        private IEnumerator Autodestroy()
        {
            yield return new WaitForSeconds(lifeTime);

            if (!Impacted)
                OnImpact(null);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Creature>().TakeDamage(damage);
            }

            Impacted = true;
            OnImpact(collision.gameObject);
        }
    }
}