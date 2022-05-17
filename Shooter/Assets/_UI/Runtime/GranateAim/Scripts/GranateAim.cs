using Shooter.UI.Core;
using System.Collections;
using UnityEngine;

namespace Shooter.UI.Runtime
{
    public class GranateAim : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [Space]
        [SerializeField] private Transform _fromTarget;
        [SerializeField] private Transform _toTarget;

        private Coroutine _coroutine;
        private IInputReciver _inputReciver;

        public void SetContext(IInputReciver inputReciver)
        {
            _inputReciver = inputReciver;
        }

        public void DrawAim()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(DrawAimRoutine());
        }

        private IEnumerator DrawAimRoutine()
        {
            var prefabs = new GameObject[10];

            for(int i = 0; i < 10; i++)
            {
                prefabs[i] = Instantiate(_prefab, transform);
            }

            while(_inputReciver.IsHolded())
            {
                var centerPoint = (_fromTarget.position + _toTarget.position) * .5f;
                centerPoint -= Vector3.up;
                var newStartPoint = _fromTarget.position - centerPoint;
                var newEndPoint = _toTarget.position - centerPoint;

                for(int i = 0; i < 10; i++)
                {
                    prefabs[i].transform.position = Vector3.Slerp(newStartPoint, newEndPoint, (float)i / 10) + centerPoint;
                }

                yield return null;
            }

            foreach(var prefab in prefabs)
            {
                Destroy(prefab);
            }
        }
    } 
}
