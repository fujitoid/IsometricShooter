using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Runtime.Weapone.Granate.Animations
{
    public class GranateRangeVisual : MonoBehaviour
    {
        [SerializeField] private float _startTime;
        [SerializeField] private float _rangeTime;
        [Space]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _mainRange;
        [SerializeField] private Image _animatedRange;
        [Space]
        [SerializeField] private Vector2 _from;
        [SerializeField] private Vector2 _to;

        private Sequence _sequence;
        private Coroutine _rangeCoroutine;

        public void OnStart(float range)
        {
            _canvas.transform.localScale = new Vector2(_canvas.transform.localScale.x * range, _canvas.transform.localScale.y * range);

            _sequence = DOTween.Sequence();

            _sequence.Append(_mainRange.DOFade(1, _startTime))
                .Append(_animatedRange.DOFade(1, _startTime));

            _sequence.OnComplete(PlayRange);
        }

        private void PlayRange()
        {
            _rangeCoroutine = StartCoroutine(RangeRoutine());
        }

        private IEnumerator RangeRoutine()
        {
            while (true)
            {
                _sequence?.Kill();
                _sequence = DOTween.Sequence();

                _sequence.Append(_animatedRange.rectTransform.DOSizeDelta(_from, 0))
                    .Join(_animatedRange.DOFade(0, 0))
                    .Append(_animatedRange.rectTransform.DOSizeDelta(_to, _rangeTime))
                    .Join(DOTween.Sequence().Append(_animatedRange.DOFade(1, _startTime / 2)).Append(_animatedRange.DOFade(0, _rangeTime / 2)));

                yield return _sequence?.WaitForCompletion();
                yield return null;
            }

            yield return null;
        }

        private void OnDestroy()
        {
            if (_rangeCoroutine != null)
                StopCoroutine(_rangeCoroutine);

            _rangeCoroutine = null;

            _sequence?.Kill();
        }
    }
}
