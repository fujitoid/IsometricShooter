using Shooter.AI.Context;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.AI.Ghosts.Behaviour.States
{
    public class DistanceAttackState : StateBase
    {
        private NavMeshAgent _agent;
        private IWeapon _weapon;
        private float _runDistance;

        public DistanceAttackState SetAgent(NavMeshAgent agent)
        {
            _agent = agent;
            return this;
        }

        public DistanceAttackState SetWeapon(IWeapon weapon)
        {
            _weapon = weapon;
            return this;
        }

        public DistanceAttackState SetRunDistance(float distance)
        {
            _runDistance = distance;
            return this;
        }

        protected override IEnumerator OnUpdate()
        {
            while (true)
            {
                var pointRandom = Random.Range(0f, 1f);
                var points = GetRunPoints();
                var point = pointRandom < 0.5f ? points.leftPoint : points.rightPoint;

                _agent.SetDestination(point);

                var debug = Vector3.Distance(_agent.transform.position, point);

                yield return new WaitUntil(() => Vector3.Distance(_agent.transform.position, point) < 1);

                var shootCount = Random.Range(2, 6);

                for (int i = 0; i < shootCount; i++)
                {
                    _weapon.Attack();
                    yield return new WaitForSeconds(0.5f);
                } 
            }
        }

        private (Vector3 leftPoint, Vector3 rightPoint) GetRunPoints()
        {
            Vector3[] points = new Vector3[8];

            for (int i = 0; i < 8; i++)
            {
                var radians = 2 * Mathf.PI / 8 * i;

                var vertical = Mathf.Sin(radians);
                var horizontal = Mathf.Cos(radians);

                var pointDirection = new Vector3(horizontal, 0, vertical);
                var pointPosition = _agent.transform.position + pointDirection * _runDistance;
                points[i] = pointPosition;
            }

            var rightPoint = points.FirstOrDefault(x => Vector3.Angle(_agent.transform.forward * _runDistance - _agent.transform.position, x - _agent.transform.position) < 110
            && Vector3.Angle(_agent.transform.position - _agent.transform.forward * _runDistance, x - _agent.transform.position) > 70);
            var leftPoint = points.LastOrDefault(x => Vector3.Angle(_agent.transform.position - _agent.transform.forward * _runDistance, x - _agent.transform.position) < 110
            && Vector3.Angle(_agent.transform.position - _agent.transform.forward * _runDistance, x - _agent.transform.position) > 70);

            return (leftPoint, rightPoint);
        }
    }
}
