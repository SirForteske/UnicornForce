using Gun;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class HornGunScript : GunScript
    {
        public GameObject shootEffect;
        public float shootEffectDuration = 0.2f;

        protected override void Start()
        {
            if (shootEffect)
                shootEffect.SetActive(false);
        }

        public override void Stop()
        {
        }

        protected override void Fire()
        {
            var shot = Instantiate(defaultProjectilePrefab, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody2D>().AddForce(transform.right * power, ForceMode2D.Impulse);
            PlayShootEffect();
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