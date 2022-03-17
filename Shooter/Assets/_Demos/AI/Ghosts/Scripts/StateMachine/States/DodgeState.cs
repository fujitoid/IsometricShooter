using Shooter.AI.Ghosts.Services;
using System.Collections;
using System.Collections.Generic;
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
            var directionRange = Random.Range(0f, 1f);
            var direction = directionRange < 0.5 ? 1 : -1;

            var moveDirection = new Vector3(_dodgeDistance * direction, 0, 0);
            var movePosition = _agent.transform.position + moveDirection;
            _agent.transform.position = movePosition;

            yield return new WaitUntil(() => Vector3.Distance(_agent.transform.position, movePosition) == 0);

            _attackContainer.IsUnderAttack = false;
        }
    } 
}
