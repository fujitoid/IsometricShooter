using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.AI.Ghosts.Behaviour.States
{
    public class ChasingState : StateBase
    {
        private NavMeshAgent _agent;
        private Transform _chaseTransform;

        public ChasingState SetAgent(NavMeshAgent agent)
        {
            _agent = agent;
            return this;
        }

        public ChasingState SetChaseTransform(Transform transform)
        {
            _chaseTransform = transform;
            return this;
        }

        protected override IEnumerator OnUpdate()
        {
            while (true)
            {
                _agent.SetDestination(_chaseTransform.position);
                yield return null;
            }
        }
    } 
}
