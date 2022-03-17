using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace  Shooter.AI.Ghosts.Behaviour.States
{
    public class ChaseToDistanceState : StateBase
    {
        private float _distance;
        private NavMeshAgent _agent;
        private Transform _chaseTransform;

        public ChaseToDistanceState SetAgent(NavMeshAgent agent)
        {
            _agent = agent;
            return this;
        }

        public ChaseToDistanceState SetDistance(float distance)
        {
            _distance = distance;
            return this;
        }

        public ChaseToDistanceState SetChaseTransform(Transform transform)
        {
            _chaseTransform = transform;
            return this;
        }

        protected override IEnumerator OnUpdate()
        {
            while (true)
            {
                var betweenDirection = _chaseTransform.position - _agent.transform.position;
                var distance = Vector3.Distance(_chaseTransform.position, _agent.transform.position);
                var pointCoef = 1 - (_distance / distance);

                var newDirection = new Vector3(betweenDirection.x * pointCoef, 0, betweenDirection.z * pointCoef);
                var newPosition = _agent.transform.position + newDirection;
                _agent.SetDestination(newPosition);

                yield return new WaitUntil(() => Vector3.Distance(_agent.transform.position, newPosition) <= 3);
                yield return null;
            }
        }
    } 
}
