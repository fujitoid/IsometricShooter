using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.AI.Ghosts.Behaviour
{
    public class StateMachine
    {
        private MonoBehaviour _monoBehaviour;
        private List<StateBase> _states = new List<StateBase>();
        private StateBase _currentState;

        private Coroutine _coroutine;

        public StateMachine(MonoBehaviour monoBehaviour, params StateBase[] states)
        {
            _monoBehaviour = monoBehaviour;
            _states = states.ToList();

            if (_coroutine != null)
                _monoBehaviour.StopCoroutine(_coroutine);

            _coroutine = _monoBehaviour.StartCoroutine(CheckStates());
        }

        public StateMachine Stop()
        {
            if (_coroutine != null)
                _monoBehaviour.StopCoroutine(_coroutine);

            return this;
        }

        private IEnumerator CheckStates()
        {
            while (true)
            {
                if (_currentState != null)
                {
                    yield return new WaitUntil(() => _currentState.CanExit());
                    _currentState.Exit();
                }

                var currentState = _states.FirstOrDefault(x => x.CanEnter());

                if (currentState != null)
                {
                    _currentState = currentState;
                    _currentState.Enter();
                }

                yield return null;
            }
        }
    } 
}
