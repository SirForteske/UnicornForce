using System;
using System.Collections;
using UnityEngine;

namespace Gun
{
    public abstract class GunScript : MonoBehaviour
    {
        [Header("Projectile")]
        public ShotScript defaultProjectilePrefab;
        public ShotScript equippedProjectile;
        [Header("Gun Settings")]
        public bool active = true;
        public bool autofire = false;
        public float power;
        [Tooltip("The maximum amount of ammo this weapon can hold. 0 = infinite.")]
        public int maxAmmo = 0;
        [Range(0, 50)]
        public float fireRate = 2;

        private bool _canShoot = true;

        public int CurrentAmmo { get; set; }
        public bool HasAmmo { get => CurrentAmmo > 0 || maxAmmo == 0; }
        public bool CanShoot => _canShoot;


        protected virtual void Start()
        {
            CurrentAmmo = maxAmmo;

            if (equippedProjectile == null) 
                equippedProjectile = defaultProjectilePrefab;
        }

        protected virtual void Update()
        {
            if (autofire) Trigger(true);
        }

        public virtual void Trigger(bool trigger)
        {
            if(!trigger)
            {
                Stop();
                return;
            }

            if (active && _canShoot && HasAmmo)
            { 
                OnFire();
                CurrentAmmo = Mathf.Max(0, CurrentAmmo - 1);
                if (fireRate > 0)
                {
                    _canShoot = false;
                    StartCoroutine(EnableShooting(1f / fireRate));
                }
            }
        }

        protected abstract void OnFire();

        public abstract void Stop();

        IEnumerator EnableShooting(float time)
        {
            yield return new WaitForSeconds(time);
            _canShoot = true;
        }
    }
}