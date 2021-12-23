using Gun;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(LineRenderer))]
    public class RainbowGunScript : GunScript
    {
        public float maxAngle = 45f;
        public ParticleSystem endHitParticleSystem;
        public ParticleSystem endParticleSystem;
        public float lineLength = 10f;
        public LayerMask layerMask;
        public float rotateSpeed = 10f;
        public float damage = 3f;

        private LineRenderer lineRenderer;
        private RaycastHit2D hit;
        private bool shooting;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        protected override void Fire()
        {
            if (!shooting)
            {
                shooting = true;
                endParticleSystem.Play(true);
                transform.parent.parent.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Unicorn/Jet/UnicornJet_Head02");
            }

        }

        protected virtual void Update()
        {
            lineRenderer.SetPosition(0, transform.position);
            if(shooting)
            {
                Vector2 shotDirection = transform.right;

                hit = Physics2D.CircleCast(transform.position, 0.5f, shotDirection, lineLength, layerMask);

                if(hit)
                {
                    endParticleSystem.Stop(true);
                    if (!endHitParticleSystem.isPlaying)
                    {
                        endHitParticleSystem.Play(true);
                    }

                    float distance = Vector2.Distance(transform.position, hit.point);
                    lineRenderer.SetPosition(1, (Vector2)transform.position + shotDirection * distance);
                    endHitParticleSystem.transform.position = (Vector2)transform.position + shotDirection * distance;

                    if (hit.transform.gameObject.CompareTag("Enemy"))
                    {
                        hit.transform.gameObject.GetComponent<Creature>().TakeDamage(damage * Time.deltaTime);
                    }
                }
                else
                {
                    lineRenderer.SetPosition(1, (Vector2)transform.position + shotDirection * lineLength);
                    endParticleSystem.transform.position = (Vector2)transform.position + shotDirection * lineLength;
                    endHitParticleSystem.Stop(true);
                    endParticleSystem.Play(true);
                }
            }
            else
            {
                lineRenderer.SetPosition(1, transform.position);
            }
        }

        public override void Stop()
        {
            shooting = false;
            endHitParticleSystem.Stop(true);
            endParticleSystem.Stop(true);
            lineRenderer.SetPosition(1, Vector3.zero);
            transform.parent.parent.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Unicorn/Jet/UnicornJet_Head01");
        }
    }
}