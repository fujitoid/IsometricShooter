using Shooter.AI.Context;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.AI.Ghosts.Spawner
{
    public class GhostSpawner : MonoBehaviour
    {
        [SerializeField] private CloseGhost _closeEnemy;
        [SerializeField] private DistanceGhost _distanceEnemy;
        [Space]
        [SerializeField] private int _totalCount;
        [SerializeField] private int _currentCount;
        [SerializeField] private float _spawnKD;
        [Space]
        [SerializeField] private List<Transform> _points;

        private List<IEnemy> _enemies = new List<IEnemy>();
        private Coroutine _coroutine;
        private int _spawnRequests = 0;

        private void Awake()
        {
            Spawn();
        }

        private void Spawn()
        {
            if (_totalCount <= 0)
                return;

            if (_coroutine != null)
            {
                _spawnRequests++;
                return;
            }

            _coroutine = StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            var newEnemies = new List<IEnemy>();

            var spawnCount = _currentCount - _enemies.Count;
            _totalCount -= spawnCount;

            var randomRange = Random.Range(0, spawnCount + 1);

            var closeEnemiesCount = spawnCount - randomRange;

            if (_enemies.Count > 0)
                yield return new WaitForSeconds(_spawnKD);

            for (int i = 0; i < spawnCount; i++)
            {
                var randomPointIndex = Random.Range(0, _points.Count);
                var randomPoint = _points[randomPointIndex];

                if (closeEnemiesCount > 0)
                {
                    var closeEnemy = Instantiate(_closeEnemy, randomPoint);
                    closeEnemy.OnDeath(OnDeath);
                    _enemies.Add(closeEnemy);
                    closeEnemiesCount--;

                    yield return new WaitForSeconds(_spawnKD);

                    continue;
                }

                var distanceEnemy = Instantiate(_distanceEnemy, randomPoint);
                distanceEnemy.OnDeath(OnDeath);
                _enemies.Add(distanceEnemy);

                yield return new WaitForSeconds(_spawnKD);
            }

            _coroutine = null;

            if (_spawnRequests > 0)
            {
                _spawnRequests--;
                Spawn();
            }
        }

        private void OnDeath(IEnemy enemy)
        {
            _enemies.Remove(enemy);

            Spawn();
        }
    } 
}
