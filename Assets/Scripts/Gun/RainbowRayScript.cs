using Gun;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class RainbowRayScript : DoTShotScript
    {
        public GameObject rayTrunk;
        public float trunkDelay = 0.6f;

        protected override void Start()
        {
            rayTrunk.SetActive(false);
            StartCoroutine(ShowTrunk());
        }

        IEnumerator ShowTrunk()
        {
            yield return new WaitForSeconds(trunkDelay);

            rayTrunk.SetActive(true);
        }

        protected override void OnImpact(GameObject other)
        {
        }
    }
}