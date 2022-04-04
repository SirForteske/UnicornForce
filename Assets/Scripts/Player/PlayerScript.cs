using Assets.Scripts.Player;
using Gun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerScript : MonoBehaviour
    {
        public static PlayerScript instance;

        [Header("Animation")]
        public Animator animatorController;

        [Header("Health")]
        public GameObject destructionFX;
        public int maxHP = 5;
        public float immunityTime = 2f;
        [Header("Power")]
        public int maxPower = 10;
        public float powerConsumption = 1f;

        [Header("Gun Slots")]
        public GunSlotScript primarySlot;
        public GunSlotScript secondarySlot;

        public int HP { get; private set; }
        public float Power { get; private set; }

        private bool isImmune = false;
        private bool superPowerActive = false;

        public Action<int, int> OnHealthChanged;
        public Action<float, int> OnPowerChanged;


        private void Awake()
        {
            if (instance == null)
                instance = this;

            HP = maxHP;
        }

        private void Start()
        {
            animatorController = GetComponent<Animator>();
        }

        private void Update()
        {
            if (HP == 0)
            {
             //   Destruction();
            }
        }

        //method for damage processing by 'Player'
        public void TakeDamage(int damage)
        {
            if (!isImmune)
            {
                HP = Mathf.Max(0, HP - damage);
                OnHealthChanged?.Invoke(HP, maxHP);
                StartCoroutine(StartImmunityTime(immunityTime));
            }
        }

        public void PowerUp(float power)
        {
        }

        //'Player's' destruction procedure
        void Destruction()
        {
            Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
            Destroy(gameObject);
        }

        IEnumerator StartImmunityTime(float time)
        {
            isImmune = true;
            yield return new WaitForSeconds(time);
            isImmune = false;
        }

        IEnumerator ConsumePower(float amount)
        {
            yield return new WaitForSeconds(1);

        //    AddPower(-amount);

            if (superPowerActive)
            {
                StartCoroutine(ConsumePower(amount));
            }
        }

        private void OnPrimaryShot(InputValue inputValue)
        {
            if (primarySlot.EquippedGun.CanShoot)
            {
                primarySlot.EquippedGun.active = inputValue.isPressed;
                secondarySlot.EquippedGun.active = !inputValue.isPressed;

                primarySlot.EquippedGun.Trigger(inputValue.isPressed);
                animatorController.SetInteger("ShootMode", inputValue.isPressed ? 1 : 0);
            }
        }

        private void OnSecondaryShot(InputValue inputValue)
        {
            if (secondarySlot.EquippedGun.CanShoot)
            {
                primarySlot.EquippedGun.active = !inputValue.isPressed;
                secondarySlot.EquippedGun.active = inputValue.isPressed;

                secondarySlot.EquippedGun.Trigger(inputValue.isPressed);
                animatorController.SetInteger("ShootMode", inputValue.isPressed ? 2 : 0);
            }
        }
    }
}