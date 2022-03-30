using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public abstract class DoTShotScript : ShotScript
    {
        [Tooltip("The frequency, in seconds, this shot will deal damage.")]
        public float damageFrequency = 1f;

        public float DamageFrequency { get => damageFrequency; set => damageFrequency = value; }

        private float _nextDamageTick = 0f;
        private List<Creature> victims = new();

        protected virtual void Start()
        {
            _nextDamageTick = Time.time + damageFrequency;
        }

        protected virtual void Update()
        {
            if (Time.time >= _nextDamageTick)
            {
                _nextDamageTick = Time.time + damageFrequency;
                DoDamage();
            }
        }
        private void DoDamage()
        {
            victims = victims.Where(v => v != null && v.IsCorrupt).ToList();

            foreach (Creature victim in victims)
            {
                victim.GetComponent<Creature>().TakeDamage(damage);
                OnImpact(victim.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                victims.Add(collision.gameObject.GetComponent<Creature>());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                victims.Remove(collision.gameObject.GetComponent<Creature>());
            }
        }
    }
}