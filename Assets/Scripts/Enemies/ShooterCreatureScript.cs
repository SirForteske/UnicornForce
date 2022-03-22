using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Enemies
{
    public class ShooterCreatureScript : Creature
    {
        public CreatureExitPointScript exitPointPrefab;
        public float scapeSpeed = 20f;

        private Transform targetExit;


        public override void ToogleCorrupted()
        {
            base.ToogleCorrupted();

            GetComponent<FollowThePath>().movingIsActive = isCorrupt;
            
            if (!isCorrupt)
            {
                var roots = SceneManager.GetActiveScene().GetRootGameObjects();
                List<Transform> exits = new();

                foreach(GameObject root in roots)
                {
                    exits.AddRange(root.GetComponentsInChildren<CreatureExitPointScript>().Select(c => c.transform));
                }

                targetExit = GetClosestExit(exits.ToArray());
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().angularVelocity = 0f;
                GetComponent<Rigidbody2D>().gravityScale = 0f;
                GetComponent<Rigidbody2D>().AddForce((targetExit.position - transform.position).normalized * scapeSpeed, ForceMode2D.Force);
            }
        }

        Transform GetClosestExit(Transform[] exits)
        {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector2 currentPosition = transform.position;
            foreach (Transform potentialTarget in exits)
            {
                Vector2 directionToTarget = new Vector2(potentialTarget.position.x, potentialTarget.position.y) - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }

            return bestTarget;
        }
    }
}