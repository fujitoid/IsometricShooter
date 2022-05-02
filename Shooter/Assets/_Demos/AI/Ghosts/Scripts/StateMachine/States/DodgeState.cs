using Shooter.AI.Ghosts.Services;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.AI.Ghosts.Behaviour.States
{
    public class DodgeState : StateBase
    {
        private float _dodgeDistance;
        private NavMeshAgent _agent;
        private UnderAttackContainer _attackContainer;

        public DodgeState SetDodgeDistance(float distance)
        {
            _dodgeDistance = distance;
            return this;
        }

        public DodgeState SetAgent(NavMeshAgent agent)
        {
            _agent = agent;
            return this;
        }

        public DodgeState SetUnderAttackContainer(UnderAttackContainer attackContainer)
        {
            _attackContainer = attackContainer;
            return this;
        }
        
        protected override IEnumerator OnUpdate()
        {
            var randomDirection = Random.Range(0f, 1f);

            var nextDirection = randomDirection <= .5f
                ? _agent.gameObject.transform.right * -1
                : _agent.gameObject.transform.right;

            if (UnityEngine.AI.NavMesh.SamplePosition(nextDirection, out var hit, 1, UnityEngine.AI.NavMesh.AllAreas) == false)
            {
                nextDirection = _agent.transform.position;
            }

            var newPosition =
                _agent.gameObject.transform.position + nextDirection * _dodgeDistance;

            var prewSpeed = _agent.speed;
            var prewAccel = _agent.acceleration;
            _agent.speed = 1000;
            _agent.acceleration = 1000;

            _agent.SetDestination(newPosition);
            
            yield return new WaitUntil(() => _agent.gameObject.transform.position == newPosition);
            _attackContainer.IsUnderAttack = false;

            _agent.speed = prewSpeed;
            _agent.acceleration = prewAccel;
        }
    } 
}
