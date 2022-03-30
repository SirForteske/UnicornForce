using Gun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class GunSlotScript : MonoBehaviour
    {
        public GunScript defaultGun;

        public GunScript EquippedGun { get; private set; }


        protected virtual void Start()
        {
            if(defaultGun != null)
            {
                EquipGun(defaultGun);
            } 
        }

        public void EquipGun(GunScript gunPrefab)
        {
            Destroy(EquippedGun);
            EquippedGun = Instantiate(gunPrefab, transform.position, transform.rotation, transform);
            EquippedGun.transform.localScale = transform.localScale;
        }
    }
}