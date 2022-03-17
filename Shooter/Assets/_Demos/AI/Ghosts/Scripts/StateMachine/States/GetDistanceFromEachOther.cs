using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

namespace Shooter.AI.Ghosts.Behaviour.States
{
    public class GetDistanceFromEachOther : StateBase
    {
        private GameObject _getDistanceObject;
        private float _distance;
        private UnityEngine.AI.NavMeshAgent _agent;

        public GameObject GetDistanceObject => _getDistanceObject;

        public GetDistanceFromEachOther SetAgent(UnityEngine.AI.NavMeshAgent agent)
        {
            _agent = agent;
            return this;
        }

        public GetDistanceFromEachOther SetDistanceBeetween(float distance)
        {
            _distance = distance;
            return this;
        }

        public GetDistanceFromEachOther AddObject(GameObject gameObject)
        {
            _getDistanceObject = gameObject;
            return this;
        }

        public GetDistanceFromEachOther RemoveObject(GameObject gameObject)
        {
            _getDistanceObject = null;
            return this;
        }

        protected override IEnumerator OnUpdate()
        {
            while (_getDistanceObject != null)
            {
                var points = GetPointsAroundObject(_agent.gameObject);
                var currentPoint = GetFarestPoint(points);

                _agent.SetDestination(currentPoint);

                yield return new WaitUntil(() => Vector3.Distance(_agent.transform.position, currentPoint) <= 1);

                yield return null;
            }
        }

        private Vector3[] GetPointsAroundObject(GameObject gameObject)
        {
            Vector3[] points = new Vector3[8];

            for (int i = 0; i < 8; i++)
            {
                var radians = 2 * Mathf.PI / 8 * i;

                var vertical = Mathf.Sin(radians);
                var horizontal = Mathf.Cos(radians);

                var pointDirection = new Vector3(horizontal, 0, vertical);
                var pointPosition = gameObject.transform.position + pointDirection * _distance;
                points[i] = pointPosition;
            }

            return points;
        }

        private Vector3 GetFarestPoint(Vector3[] points)
        {
            var orderedPoints = points.OrderByDescending(x => Vector3.Distance(_getDistanceObject.transform.position, x));

            foreach (var point in orderedPoints)
            {
                if (UnityEngine.AI.NavMesh.SamplePosition(point, out var hit, 1, UnityEngine.AI.NavMesh.AllAreas))
                    return point;
            }

            return _agent.transform.position;
        }

        private Vector3 GetNearestPoint(Vector3[] points)
        {
            var orderedPoint = points.OrderBy(x => Vector3.Distance(_agent.transform.position, x));

            foreach (var point in orderedPoint)
            {
                if (NavMesh.SamplePosition(point, out var hit, 1, NavMesh.AllAreas))
                    return point;
            }

            return _agent.transform.position;
        }
    } 
}
