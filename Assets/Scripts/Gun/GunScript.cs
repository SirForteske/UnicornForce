using System;
using System.Collections;
using UnityEngine;

namespace Gun
{
    public abstract class GunScript : MonoBehaviour
    {
        [Header("Projectile")]
        public ProjectileScript defaultProjectilePrefab;
        public ProjectileScript equippedProjectile;
        [Header("Gun Settings")]
        public bool active = true;
        public float power;
        [Range(0, 50)]
        public float fireRate = 2;

        private bool CanShoot = true;

        protected virtual void Start()
        {
            if (equippedProjectile == null) 
                equippedProjectile = defaultProjectilePrefab;
        }

        protected virtual void Update()
        {
        }

        public virtual void Trigger(bool forceShoot = false)
        {
            if (active && (CanShoot || forceShoot))
            {
                Fire();
                CanShoot = false;
                StartCoroutine(EnableShooting(1f / fireRate));
            }
        }

        protected abstract void Fire();

        public abstract void Stop();

        IEnumerator EnableShooting(float time)
        {
            yield return new WaitForSeconds(time);
            CanShoot = true;
        }
    }
}