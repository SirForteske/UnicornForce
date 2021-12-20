using Gun;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerProjectileScript : ProjectileScript
    {
        public Color[] colors;

        private void Awake()
        {
            GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}