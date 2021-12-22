using MoreMountains.Tools;
using Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(MMProgressBar))]
    public class PowerBarControllerScript : MonoBehaviour
    {
        public PlayerScript player;

        protected MMProgressBar _progressBar;

        protected void Start()
        {
            _progressBar = GetComponent<MMProgressBar>();
            player.OnPowerChanged += (currentPower, maxPower) => _progressBar.UpdateBar01(currentPower / maxPower);
        }
    }
}