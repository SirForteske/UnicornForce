using Gun;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class HornGunScript : GunScript
    {
        public enum FireModes
        {
            Default = 0, LifeTime = 1, Double = 2, Diagonal = 3
        }

        public GameObject shootEffect;
        public float shootEffectDuration = 0.2f;

        protected override void Start()
        {
            base.Start();

            if (shootEffect)
                shootEffect.SetActive(false);
        }

        public override void Stop()
        {
        }

        protected override void Fire()
        {
            var shotLifetime = equippedProjectile.lifeTime * ((FireMode >= (int)FireModes.LifeTime) ? 2 : 1);

            if (FireMode <= (int)FireModes.LifeTime)
            {
                var shot = Instantiate(equippedProjectile, transform.position, transform.rotation);
                shot.lifeTime = shotLifetime;
                shot.GetComponent<Rigidbody2D>().AddForce(transform.right * power, ForceMode2D.Impulse);
            }
            if (FireMode >= (int)FireModes.Double)
            {
                var shot = Instantiate(equippedProjectile, transform.position + 0.025f * Vector3.up, transform.rotation);
                shot.lifeTime = shotLifetime;
                shot.GetComponent<Rigidbody2D>().AddForce(transform.right * power, ForceMode2D.Impulse); 
                shot = Instantiate(equippedProjectile, transform.position - 0.025f * Vector3.up, transform.rotation);
                shot.lifeTime = shotLifetime;
                shot.GetComponent<Rigidbody2D>().AddForce(transform.right * power, ForceMode2D.Impulse);
            }
            if (FireMode >= (int)FireModes.Diagonal)
            {
                var shot = Instantiate(equippedProjectile, transform.position, transform.rotation);
                shot.lifeTime = shotLifetime;
                shot.GetComponent<Rigidbody2D>().AddForce((-transform.up + transform.right) * power * 0.5f, ForceMode2D.Impulse);
                shot = Instantiate(equippedProjectile, transform.position, transform.rotation);
                shot.lifeTime = shotLifetime;
                shot.GetComponent<Rigidbody2D>().AddForce((transform.up + transform.right) * power * 0.5f, ForceMode2D.Impulse);
            }

            PlayShootEffect();
        }

        public override void UpgradeFireMode()
        {
            FireMode = Mathf.Min(FireMode + 1, (int)FireModes.Diagonal);
        }

        public override void DowngradeFireMode()
        {
            FireMode = Mathf.Max(FireMode - 1, (int)FireModes.Default);
        }

        protected virtual void PlayShootEffect()
        {
            if (shootEffect)
                shootEffect.SetActive(true);

            StartCoroutine(StopShootEffect());
        }

        protected virtual IEnumerator StopShootEffect()
        {
            yield return new WaitForSeconds(shootEffectDuration);

            if (shootEffect)
                shootEffect.SetActive(false);
        }
    }
}