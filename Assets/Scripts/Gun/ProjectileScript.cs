using System.Collections;
using UnityEngine;

namespace Gun
{
    public abstract class ProjectileScript : MonoBehaviour
    {
        [Tooltip("Damage which a projectile deals to another object. Integer")]
        public int damage;
        public float lifeTime = 2f;

        [Tooltip("Whether the projectile belongs to the ‘Enemy’ or to the ‘Player’")]
        public bool enemyBullet;

        [Tooltip("Whether the projectile is destroyed in the collision, or not")]
        public bool destroyedByCollision;

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
                Impact();
        }

        private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
        {
            if (enemyBullet && collision.tag == "Player") //if anoter object is 'player' or 'enemy sending the command of receiving the damage
            {
                ShooterPlayer.instance.Damage(damage);
            }
            else if (!enemyBullet && collision.tag == "Enemy")
            {
                collision.GetComponent<Creature>().TakeDamage(damage);
            }

            Impacted = true;
            Impact();
        }

        protected abstract void Impact();
    }
}