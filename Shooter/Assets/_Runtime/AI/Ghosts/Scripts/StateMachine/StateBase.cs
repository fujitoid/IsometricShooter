using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Shooter.AI.Ghosts.Behaviour
{
    public abstract class StateBase
    {
        protected MonoBehaviour _monoBehaviour;
        private Func<bool> _enterStatement;
        private Func<bool> _exitStatement;

        private Coroutine _coroutine;

        public StateBase SetOwner(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
            return this;
        }

        public StateBase SetEnterStatements(Func<bool> enterStatement)
        {
            _enterStatement = enterStatement;
            return this;
        }

        public StateBase SetExitStatements(Func<bool> exitStatement)
        {
            _exitStatement = exitStatement;
            return this;
        }

        public bool CanEnter()
        {
            return _enterStatement();
        }

        public bool CanExit()
        {
            //Debug.Log($"{this} : {_exitStatement()}");
            return _exitStatement();
        }

        protected abstract IEnumerator OnUpdate();

        public virtual void Enter()
        {
            if (_coroutine != null)
                _monoBehaviour.StopCoroutine(_coroutine);

            _coroutine = _monoBehaviour.StartCoroutine(OnUpdate());
        }
        public virtual void Exit()
        {
            if (_coroutine != null)
                _monoBehaviour.StopCoroutine(_coroutine);
        }
    } 
}
