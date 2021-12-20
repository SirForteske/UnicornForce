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
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var shotDirection = (mousePos - transform.position).normalized;
            shotDirection = new Vector2(Mathf.Max(angleOffset, shotDirection.x), shotDirection.y);
            var shot = Instantiate(defaultProjectilePrefab, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody2D>().AddForce(shotDirection * power, ForceMode2D.Impulse);
        }
    }
}