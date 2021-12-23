using Gun;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class GunSlotScript : MonoBehaviour
    {
        public KeyCode trigger;
        public GunScript defaultGun;
        public bool enabled = true;

        public GunScript CurrentGun { get; private set; }

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
            if(enabled && CurrentGun != null && Input.GetKey(trigger))
            {
                CurrentGun.Trigger();
            }
        }

        public void SetGun(GunScript gunPrefab)
        {
            Destroy(CurrentGun);
            CurrentGun = Instantiate(gunPrefab, transform.position, transform.rotation, transform);
            CurrentGun.transform.localScale = transform.localScale;
        }
    }
}