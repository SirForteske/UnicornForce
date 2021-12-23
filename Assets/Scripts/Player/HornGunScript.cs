using Gun;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class HornGunScript : GunScript
    {
        public float angleOffset = 0.5f;

        protected override void Fire()
        {
            var shot = Instantiate(defaultProjectilePrefab, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody2D>().AddForce(transform.right * power, ForceMode2D.Impulse);
        }
    }
}