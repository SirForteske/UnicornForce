using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class ProjectileScript : MonoBehaviour
    {
        [Tooltip("Damage which a projectile deals to another object. Integer")]
        public int damage;

        [Tooltip("Whether the projectile belongs to the ‘Enemy’ or to the ‘Player’")]
        public bool enemyBullet;

        [Tooltip("Whether the projectile is destroyed in the collision, or not")]
        public bool destroyedByCollision;

        private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
        {
            if (enemyBullet && collision.tag == "Player") //if anoter object is 'player' or 'enemy sending the command of receiving the damage
            {
                ShooterPlayer.instance.Damage(damage);
                if (destroyedByCollision)
                    Destruction();
            }
            else if (!enemyBullet && collision.tag == "Enemy")
            {
                collision.GetComponent<Enemy>().GetDamage(damage);
                if (destroyedByCollision)
                    Destruction();
            }
        }

        void Destruction()
        {
            Destroy(gameObject);
        }
    }
}