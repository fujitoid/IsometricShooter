using System.Collections;
using UnityEngine;

namespace Shooter.AI.Ghosts.Behaviour.States
{
    public class WaitingState : StateBase
    {
        private float _duration;

        public WaitingState SetWaitTime(float duration)
        {
            _duration = duration;
            return this;
        }

        protected override IEnumerator OnUpdate()
        {
            yield return new WaitForSeconds(_duration);
        }
    } 
}
