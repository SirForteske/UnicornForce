using Assets.Scripts.Player;
using Gun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        public static PlayerScript instance;
        
        public GameObject destructionFX;
        public int maxHP = 5;

        [Header("Gun Slots")]
        public List<GunSlotScript> slots;

        public int HP { get; private set; }

        public Action<int, int> OnHealthChanged;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            HP = maxHP;
        }

        private void Update()
        {
            if (HP == 0)
            {
                Destruction();
            }
        }

        //method for damage processing by 'Player'
        public void Damage(int damage)
        {
            HP = Mathf.Max(0, HP - damage);
            OnHealthChanged?.Invoke(HP, maxHP);
        }

        //'Player's' destruction procedure
        void Destruction()
        {
            Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
            Destroy(gameObject);
        }
    }
}