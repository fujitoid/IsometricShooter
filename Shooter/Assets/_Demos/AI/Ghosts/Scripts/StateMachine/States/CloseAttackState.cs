using System.Collections;
using Shooter.AI.Context;
using Shooter.AI.Ghosts.Behaviour;
using UnityEngine;
using UnityEngine.AI;

public class CloseAttackState : StateBase
{
    private NavMeshAgent _agent;
    private IWeapon _weapon;

    public CloseAttackState SetAgent(NavMeshAgent agent)
    {
        _agent = agent;
        return this;
    }

    public CloseAttackState SetWeapon(IWeapon weapon)
    {
        _weapon = weapon;
        return this;
    }
    
    protected override IEnumerator OnUpdate()
    {
        while (true)
        {
            _weapon.Attack();
            Debug.Log("attack!", _agent.gameObject);
            yield return new WaitForSeconds(1);
        }
    }
}
