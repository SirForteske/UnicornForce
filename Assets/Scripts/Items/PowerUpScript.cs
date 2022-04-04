using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class PowerUpScript : PickableItemScript
    {
        protected override void OnPlayerPick(GameObject player)
        {
            var pl = player.GetComponent<PlayerScript>();

            pl.primarySlot.EquippedGun.UpgradeFireMode();

            Destroy(gameObject);
        }
    }
}
