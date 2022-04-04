using Gun;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class RayGunScript : GunScript
    {
        public float autoChargeRate = 1f;

        private ShotScript projectile;

        public bool Firing { get; private set; }

        public override void Stop()
        {
            Firing = false;
            if (projectile != null)
                Destroy(projectile.gameObject);

            if (CurrentAmmo < maxAmmo)
                StartCoroutine(RechargeAmmo());
        }

        protected override void Fire()
        {
            if(!Firing && CurrentAmmo == maxAmmo)
            {
                Firing = true;
                projectile = Instantiate(equippedProjectile, transform.position, transform.rotation, transform);
                StartCoroutine(ConsumeAmmo());
            }
        }

        private IEnumerator RechargeAmmo()
        {
            yield return new WaitForSeconds(1f / autoChargeRate);

            CurrentAmmo = Mathf.Min(maxAmmo, CurrentAmmo + 1);

            if (CurrentAmmo < maxAmmo)
                StartCoroutine(RechargeAmmo());
        }

        private IEnumerator ConsumeAmmo()
        {
            yield return new WaitForSeconds(1f / fireRate);

            if (active && Firing)
            {
                CurrentAmmo = Mathf.Max(0, CurrentAmmo - 1);

                if (HasAmmo)
                    StartCoroutine(ConsumeAmmo());
                else
                    Stop();
            }
        }

        public override void UpgradeFireMode()
        {
            FireMode = 0;
        }

        public override void DowngradeFireMode()
        {
            FireMode = 0;
        }
    }
}