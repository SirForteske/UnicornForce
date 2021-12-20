using MoreMountains.Tools;
using Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(MMProgressBar))]
    public class HealthBarControllerScript : MonoBehaviour
    {
        public PlayerScript player;

        protected MMProgressBar _progressBar;

        protected void Start()
        {
            _progressBar = GetComponent<MMProgressBar>();
            player.OnHealthChanged += (currentHP, maxHP) => _progressBar.UpdateBar01(currentHP / maxHP);
        }
    }
}