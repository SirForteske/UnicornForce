using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils.Parallax
{
    public class ParallaxScript : MonoBehaviour
    {
        [Tooltip("This value will affect the speed of all layers.")]
        [SerializeField]
        private float speedMultiplier = 1f;
        [Tooltip("Layer order is from back to front, being layer 0 the farthest one.")]
        [SerializeField]
        private List<ParallaxLayer> layers;

    }
}
