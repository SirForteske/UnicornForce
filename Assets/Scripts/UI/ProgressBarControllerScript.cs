using MoreMountains.Tools;
using Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(MMProgressBar))]
    public class ProgressBarControllerScript : MonoBehaviour
    {
        protected MMProgressBar _progressBar;

        protected virtual void Start()
        {
            _progressBar = GetComponent<MMProgressBar>();
        }
    }
}