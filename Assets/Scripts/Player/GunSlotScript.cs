using Gun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class GunSlotScript : MonoBehaviour
    {
        public GunScript defaultGun;
        public bool active = true;
        public bool autoFire = false;

        public GunScript CurrentGun { get; private set; }


        protected virtual void Start()
        {
            if(defaultGun != null)
            {
                SetGun(defaultGun);
            }
        }

        protected virtual void Update()
        {
            if (autoFire) CurrentGun.Trigger(false);
        }

        public void SetGun(GunScript gunPrefab)
        {
            Destroy(CurrentGun);
            CurrentGun = Instantiate(gunPrefab, transform.position, transform.rotation, transform);
            CurrentGun.transform.localScale = transform.localScale;
        }
    }
}