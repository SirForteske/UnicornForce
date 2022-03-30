using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public struct CreatureWave
    {
        public int row;
        public float waitTime;
        public Creature creaturePrefab;
        public Wave wavePrefab;
    }

    public class LevelControllerScript : MonoBehaviour
    {
        public int numberOfRows = 4;
        [Header("Creature Waves")]
        public List<CreatureWave> waves;
        public bool loopable = true;

        private int _currentWaveIndex = -1;
        private List<Vector2> _screenRows;

        private void Start()
        {
            _currentWaveIndex = -1;
            _screenRows = CreateRows(numberOfRows);

            if (waves != null && waves.Count > 0)
                StartCoroutine(NextEnemyWave());
        }

        private List<Vector2> CreateRows(int count)
        {
            var bottom = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
            var rowHeight = Mathf.Abs(bottom * 2) / count;
            var rows = new List<Vector2>();

            for(int i = 0; i < count; i++)
            {
                rows.Add(new Vector2(0f, bottom + rowHeight / 2f + rowHeight * i));
            }

            return rows;
        }

        private System.Collections.IEnumerator NextEnemyWave()
        {
            _currentWaveIndex++;

            if (_currentWaveIndex >= waves.Count)
            {
                if (loopable) _currentWaveIndex = 0;
                else yield return null;
            }

            yield return new WaitForSeconds(waves[_currentWaveIndex].waitTime);

            var screenRow = _screenRows[Mathf.Min(waves[_currentWaveIndex].row, _screenRows.Count - 1)];
            var wave = Instantiate(
                waves[_currentWaveIndex].wavePrefab, 
                new Vector3(screenRow.x, screenRow.y, -5f),
                Quaternion.identity,
                transform);

            StartCoroutine(NextEnemyWave());
        }
    }
}
