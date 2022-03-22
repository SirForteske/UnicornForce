using System.Collections;
using UnityEngine;

namespace Gun
{
    public class GunScript : MonoBehaviour
    {
        public ProjectileScript defaultProjectilePrefab;
        public float power;
        [Range(0, 50)]
        public float fireRate = 2;
        public bool enabled = true;

        private bool CanShoot = true;

        // Use this for initialization
        void Start()
        {
        }

        public virtual void Trigger(bool forceShoot = false)
        {
            if (enabled && (CanShoot || forceShoot))
            {
                Fire();
                CanShoot = false;
                StartCoroutine(EnableShooting(1f / fireRate));
            }
        }

        protected virtual void Fire()
        {
            var shotDirection = new Vector2(1f, 0f);
            var shot = Instantiate(defaultProjectilePrefab, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody2D>().AddForce(shotDirection * power, ForceMode2D.Impulse);
        }

        public virtual void Stop()
        {

        }

        IEnumerator EnableShooting(float time)
        {
            yield return new WaitForSeconds(time);
            CanShoot = true;
        }
    }
}