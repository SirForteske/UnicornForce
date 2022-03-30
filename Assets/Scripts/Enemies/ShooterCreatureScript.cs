using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Enemies
{
    [RequireComponent(typeof(FollowThePath))]
    public class ShooterCreatureScript : Creature
    {
        public override void ToogleCorrupted()
        {
            base.ToogleCorrupted();

            GetComponent<FollowThePath>().ExitPath();
        }

        protected override void OnBecameInvisible()
        {
            base.OnBecameInvisible();
            Destroy(gameObject);
        }
    }
}