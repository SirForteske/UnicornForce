using Gun;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class GunSlotScript : MonoBehaviour
    {
        public KeyCode trigger;
        public GunScript defaultGun;
        public bool enabled = true;

        private GunScript currentGun;

        private void Start()
        {
            if(defaultGun != null)
            {
                SetGun(defaultGun);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(enabled && currentGun != null && Input.GetKey(trigger))
            {
                currentGun.Trigger();
            }
        }

        public void SetGun(GunScript gunPrefab)
        {
            Destroy(currentGun);
            currentGun = Instantiate(gunPrefab, transform.position, transform.rotation, transform);
            currentGun.transform.localScale = transform.localScale;
        }
    }
}