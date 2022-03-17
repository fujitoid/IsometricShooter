using Shooter.AI.Context;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.AI.Ghosts.Behaviour.States
{
    public class RunAwayState : StateBase
    {
        private float _runFromDistance;
        private NavMeshAgent _agent;
        private Transform _ranFromTransform;
        private IWeapon _weapon;

        private Coroutine _attackRoutine;

        public RunAwayState SetWeapon(IWeapon weapon)
        {
            _weapon = weapon;
            return this;
        }

        public RunAwayState SetRunFromDistance(float value)
        {
            _runFromDistance = value;
            return this;
        }

        public RunAwayState SetAgent(NavMeshAgent agent)
        {
            _agent = agent;
            return this;
        }

        public RunAwayState SetRunFromTransform(Transform transform)
        {
            _ranFromTransform = transform;
            return this;
        }

        public override void Enter()
        {
            base.Enter();
            if (_attackRoutine != null)
                _monoBehaviour.StopCoroutine(_attackRoutine);

            _attackRoutine = _monoBehaviour.StartCoroutine(AttackRoutine());
        }

        public override void Exit()
        {
            base.Exit();

            if(_attackRoutine != null)
            _monoBehaviour.StopCoroutine(_attackRoutine);
        }

        protected override IEnumerator OnUpdate()
        {
            while (true)
            {
                var point = GetClosestExistingPoint();
                _agent.SetDestination(point);

                yield return new WaitUntil(() => Vector3.Distance(_agent.transform.position, point) <= 3);

                yield return null;
            }
        }

        private IEnumerator AttackRoutine()
        {
            while (true)
            {
                _weapon.Attack();

                yield return new WaitForSeconds(.5f);

                yield return null;
            }
        }

        private Vector3[] GetPoints()
        {
            Vector3[] points = new Vector3[8];

            for (int i = 0; i < 8; i++)
            {
                var radians = 2 * Mathf.PI / 8 * i;

                var vertical = Mathf.Sin(radians);
                var horizontal = Mathf.Cos(radians);

                var pointDirection = new Vector3(horizontal, 0, vertical);
                var pointPosition = _ranFromTransform.position + pointDirection * _runFromDistance;
                points[i] = pointPosition;
            }

            return points;
        }

        private Vector3 GetClosestExistingPoint()
        {
            var points = GetPoints();

            var orderedPoints = points.OrderBy(x => Vector3.Distance(_agent.transform.position, x)).ToList();
            var distances = new System.Collections.Generic.List<float>();
            distances = orderedPoints.Select(x => Vector3.Distance(_agent.transform.position, x)).ToList();

            foreach (var point in orderedPoints)
            {
                if (NavMesh.SamplePosition(point, out var hit, 1, NavMesh.AllAreas))
                {
                    return point;
                }
            }

            return _agent.transform.position;
        }

        private Vector3 GetRandomPoint()
        {
            var points = GetPoints();

            var random = new System.Random();
            var shaffeledPoints = points.OrderBy(x => random.Next());

            foreach (var point in shaffeledPoints)
            {
                if (NavMesh.SamplePosition(point, out var hit, 1, NavMesh.AllAreas))
                {
                    return point;
                }
            }

            return _agent.transform.position;
        }
    } 
}
