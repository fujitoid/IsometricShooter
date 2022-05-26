using Shooter.AI.Context;
using Shooter.Runtime.Weapone.Granate.Animations;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.Runtime.Weapone.Granate
{
    public class SimpleGranate : MonoBehaviour, IPlayerGranate
    {
        [SerializeField] private GranateRangeVisual _rangeVisual;
        [SerializeField] private GameObject _particles;

        private Core.Model.Player.Weapone.SimpleGranate _modelGranate;
        private List<IEnemy> _enemies = new List<IEnemy>();

        public Core.Model.Player.Weapone.SimpleGranate ModelGranate => _modelGranate;

        public void Construct(Core.Model.Player.Weapone.SimpleGranate simpleGranate)
        {
            _modelGranate = simpleGranate;

            var sphereCollider = gameObject.GetComponent<SphereCollider>();
            sphereCollider.radius = _modelGranate.Radius;
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.gameObject.GetComponent<IEnemy>();

            if (enemy != null)
                _enemies.Add(enemy);
        }

        private void OnTriggerExit(Collider other)
        {
            var enemy = other.gameObject.GetComponent<IEnemy>();

            if (enemy != null)
                _enemies.Remove(enemy);
        }

        public void OnFinishFly()
        {
            _rangeVisual.OnStart(_modelGranate.Radius);
        }

        public void Explode()
        {
            foreach(var enemy in _enemies)
            {
                if (enemy == null)
                    continue;

                enemy?.SetDamage(_modelGranate.Damage);
            }

            Instantiate(_particles, transform.position, Quaternion.identity);
        }
    }
}
