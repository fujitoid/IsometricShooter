using Shooter.Core.Controllers.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.Runtime.Weapone.Granate
{
    public class GranateDroper : MonoBehaviour
    {
        [SerializeField] private GranateController _granateController;
        [Space]
        [SerializeField] private Transform _finishPoint;
        [SerializeField] private float _flySpeed;
        [Space]
        [SerializeField] private SimpleGranate _simpleGranate;

        private List<Coroutine> _dropRoutines = new List<Coroutine>();
        private Coroutine _reloadRoutine;

        public void Drop()
        {
            if (_reloadRoutine != null)
                return;

            _dropRoutines.Add(StartCoroutine(DropRoutine()));
        }

        private IEnumerator ReloadRoutine()
        {
            yield return new WaitForSeconds(_granateController.SimpleGranate.ReloadTime);
            _reloadRoutine = null;
        }

        private IEnumerator DropRoutine()
        {
            var centrePoint = (this.transform.position + _finishPoint.position) * .5f;
            centrePoint -= Vector3.up;
            var newStartPoint = this.transform.position - centrePoint;
            var newEndPoint = _finishPoint.position - centrePoint;

            if (Vector3.Distance(newStartPoint, newEndPoint) > _granateController.SimpleGranate.Distance)
                yield break;

            if (_granateController.TryDropGranate(_granateController.SimpleGranate) == false)
                yield break;

            var granate = Instantiate(_simpleGranate, this.transform.position, Quaternion.identity);
            granate.Construct(_granateController.SimpleGranate);

            var f = 0f;

            while(f < 1)
            {
                granate.transform.position = Vector3.Slerp(newStartPoint, newEndPoint, f) + centrePoint;
                f += Time.fixedDeltaTime * _flySpeed;
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            _reloadRoutine = StartCoroutine(ReloadRoutine());

            yield return new WaitForSeconds(_granateController.SimpleGranate.ExplodeTime);

            granate.Explode();

            yield return null;

            Destroy(granate.gameObject);
        }
    } 
}
