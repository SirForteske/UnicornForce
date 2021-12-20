using Assets.Scripts.Player;
using Gun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerShootScript : MonoBehaviour
    {
        [Header("Gun Slots")]
        public List<GunSlotScript> slots;
    }
}