using Shooter.AI.Context;
using Shooter.AI.Ghosts.Behaviour;
using Shooter.AI.Ghosts.Behaviour.States;
using Shooter.AI.Ghosts.Services;
using Shooter.UI.Utils;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.AI.Ghosts
{
    public class CloseGhost : MonoBehaviour, IEnemy
    {
        [SerializeField] private float _weight;
        [Space] [SerializeField] private NavMeshAgent _agent;
        [Space] [SerializeField] private float _health;
        [SerializeField] private HealthBar _healthBar;
        [Space] [SerializeField] private CloseWeapon _weapon;

        private StateMachine _stateMachine;
        private GetDistanceFromEachOther _getDistanceState;
        private UnderAttackContainer _attackContainer = new UnderAttackContainer();
        private Action<IEnemy> _onDeath;

        private int? _currentPoint = null;

        private void Start()
        {
            _getDistanceState = new GetDistanceFromEachOther()
                .SetAgent(_agent)
                .SetDistanceBeetween(1.5f);

            _getDistanceState
                .SetOwner(this)
                .SetEnterStatements(() =>
                    _getDistanceState.GetDistanceObject != null && _attackContainer.IsUnderAttack == false)
                .SetExitStatements(() => _getDistanceState.GetDistanceObject == null || _attackContainer.IsUnderAttack);

            var chaseToDistance = new ChaseToDistanceState()
                .SetAgent(_agent)
                .SetChaseTransform(Player.Instance.transform)
                .SetDistance(1)
                .SetOwner(this)
                .SetEnterStatements(() =>
                    (Vector3.Distance(Player.Instance.transform.position, transform.position) > 2) &&
                    _getDistanceState.GetDistanceObject == null && _attackContainer.IsUnderAttack == false)
                .SetExitStatements(() =>
                    Vector3.Distance(Player.Instance.transform.position, transform.position) <= 2 ||
                    _getDistanceState.GetDistanceObject != null || _attackContainer.IsUnderAttack);

            // var waiting = new WaitingState()
            //     .SetWaitTime(5)
            //     .SetOwner(this)
            //     .SetEnterStatements(() => Vector3.Distance(Player.Instance.transform.position, transform.position) <= 3 && _getDistanceState.GetDistanceObject == null && _attackContainer.IsUnderAttack == false)
            //     .SetExitStatements(() => Vector3.Distance(Player.Instance.transform.position, transform.position) > 3 || _getDistanceState.GetDistanceObject != null || _attackContainer.IsUnderAttack == true);

            var attackState = new CloseAttackState()
                .SetAgent(_agent)
                .SetWeapon(_weapon)
                .SetOwner(this)
                .SetEnterStatements(() =>
                    Vector3.Distance(Player.Instance.transform.position, transform.position) <= 1 &&
                    _getDistanceState.GetDistanceObject == null && _attackContainer.IsUnderAttack == false)
                .SetExitStatements(() =>
                    Vector3.Distance(Player.Instance.transform.position, transform.position) > 1 ||
                    _getDistanceState.GetDistanceObject != null || _attackContainer.IsUnderAttack);

            var dodgeState = new DodgeState()
                .SetAgent(_agent)
                .SetDodgeDistance(2)
                .SetUnderAttackContainer(_attackContainer)
                .SetOwner(this)
                .SetEnterStatements(() => _attackContainer.IsUnderAttack)
                .SetExitStatements(() => _attackContainer.IsUnderAttack == false);

            _stateMachine = new StateMachine(this, _getDistanceState, chaseToDistance, attackState, dodgeState);

            _healthBar.Construct(_health);
        }

        private void OnDestroy()
        {
            _stateMachine.Stop();
        }

        private void Update()
        {
            transform.LookAt(Player.Instance.transform.position);
        }

        private void OnTriggerStay(Collider other)
        {
            var enemy = other.gameObject.GetComponent<IEnemy>();

            if (enemy != null)
            {
                _getDistanceState.AddObject(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var enemy = other.gameObject.GetComponent<IEnemy>();

            if (enemy != null)
            {
                _getDistanceState.RemoveObject(other.gameObject);
            }
        }

        public void SetPoint(int? pointId)
        {
            _currentPoint = pointId;
        }

        public int? GetPoint()
        {
            return _currentPoint;
        }

        public void SetDamage(float damage)
        {
            _health -= damage;
            _healthBar.SetNewHealth(_health);

            _attackContainer.IsUnderAttack = true;

            if (_health <= 0)
            {
                _onDeath.Invoke(this);
                Destroy(this.gameObject);
            }
        }

        public void OnDeath(Action<IEnemy> action)
        {
            _onDeath = action;
        }
    }
}