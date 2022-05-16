using Shooter.AI.Context;
using Shooter.AI.Ghosts.Behaviour;
using Shooter.AI.Ghosts.Behaviour.States;
using Shooter.AI.Ghosts.Services;
using Shooter.AI.Ghosts.Weapon.Distance.Streight;
using Shooter.UI.Runtime;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DistanceGhost : MonoBehaviour, IEnemy
{
    [SerializeField] private float _health;
    [SerializeField] private HealthBar _healthBar;
    [Space]
    [SerializeField] private NavMeshAgent _agent;
    [Space]
    [SerializeField] private DistanceStreightWeapon _streightWeapon;

    private StateMachine _stateMachine;
    private GetDistanceFromEachOther _getDistanceState;

    private UnderAttackContainer _attackContainer = new UnderAttackContainer();
    private Action<IEnemy> _onDeath;

    private void Start()
    {
        var playerTransform = Player.Instance.transform;

        var chasing = new ChasingState()
            .SetAgent(_agent)
            .SetChaseTransform(playerTransform)
            .SetOwner(this)
            .SetEnterStatements(() => (Vector3.Distance(playerTransform.position, transform.position) > 20) && _getDistanceState.GetDistanceObject == null && _attackContainer.IsUnderAttack == false)
            .SetExitStatements(() => (Vector3.Distance(playerTransform.position, transform.position) <= 20) || _getDistanceState.GetDistanceObject != null || _attackContainer.IsUnderAttack == true);

        var runAway = new RunAwayState()
            .SetAgent(_agent)
            .SetWeapon(_streightWeapon)
            .SetRunFromTransform(playerTransform)
            .SetRunFromDistance(15)
            .SetOwner(this)
            .SetEnterStatements(() => (Vector3.Distance(playerTransform.position, transform.position) <= 10) && _getDistanceState.GetDistanceObject == null && _attackContainer.IsUnderAttack == false)
            .SetExitStatements(() => (Vector3.Distance(playerTransform.position, transform.position) >= 15) || _getDistanceState.GetDistanceObject != null || _attackContainer.IsUnderAttack == true);

        //var waiting = new WaitingState()
        //    .SetWaitTime(5)
        //    .SetOwner(this)
        //    .SetEnterStatements(() => (Vector3.Distance(playerTransform.position, transform.position) > 15 || Vector3.Distance(playerTransform.position, transform.position) <= 15) && _getDistanceState.GetDistanceObject == null && _attackContainer.IsUnderAttack == false)
        //    .SetExitStatements(() => (Vector3.Distance(playerTransform.position, transform.position) <= 15 || Vector3.Distance(playerTransform.position, transform.position) > 15) || _getDistanceState.GetDistanceObject != null || _attackContainer.IsUnderAttack == true);

        var attack = new DistanceAttackState()
            .SetAgent(_agent)
            .SetWeapon(_streightWeapon)
            .SetRunDistance(5)
            .SetOwner(this)
            .SetEnterStatements(() => (Vector3.Distance(playerTransform.position, transform.position) > 15 || Vector3.Distance(playerTransform.position, transform.position) <= 20) && _getDistanceState.GetDistanceObject == null && _attackContainer.IsUnderAttack == false)
            .SetExitStatements(() => (Vector3.Distance(playerTransform.position, transform.position) < 15 || Vector3.Distance(playerTransform.position, transform.position) > 20) || _getDistanceState.GetDistanceObject != null || _attackContainer.IsUnderAttack == true);

        _getDistanceState = new GetDistanceFromEachOther()
            .SetAgent(_agent)
            .SetDistanceBeetween(1.5f);

        _getDistanceState
            .SetOwner(this)
            .SetEnterStatements(() => _getDistanceState.GetDistanceObject != null && _attackContainer.IsUnderAttack == false)
            .SetExitStatements(() => _getDistanceState.GetDistanceObject == null || _attackContainer.IsUnderAttack == true);

        var dodgeState = new DodgeState()
            .SetAgent(_agent)
            .SetDodgeDistance(2)
            .SetUnderAttackContainer(_attackContainer)
            .SetOwner(this)
            .SetEnterStatements(() => _attackContainer.IsUnderAttack == true)
            .SetExitStatements(() => _attackContainer.IsUnderAttack == false);

        _stateMachine = new StateMachine(this, chasing, runAway, attack, _getDistanceState, dodgeState);

        _healthBar.Construct(_health);
    }

    private void OnDestroy()
    {
        _stateMachine.Stop();
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

    private void Update()
    {
        transform.LookAt(Player.Instance.transform.position);
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
