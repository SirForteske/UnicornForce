using Gun;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerProjectileScript : ProjectileScript
    {
        public Color[] colors;

        private Animator _animator;


        protected override void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
            GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
        }

        protected override void Impact()
        {
            if(_animator)
            {
                var collider = GetComponent<Collider2D>();
                if (collider) collider.enabled = false;
                var rigidBody = GetComponent<Rigidbody2D>();
                if (rigidBody) rigidBody.velocity = Vector2.zero;
                _animator.SetBool("Destroyed", true);
                Destroy(gameObject, 0.5f);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}