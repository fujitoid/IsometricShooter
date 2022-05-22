using Shooter.Runtime.Animations.Context;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.Runtime.Animations
{
    public class AgentAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _target;

        private Vector3 _cameraForward;
        private Vector3 _moveDirection;
        private Vector3 _input;

        private float _forwardAmount;
        private float _turnAmount;

        private IAnimatorInputReciver _inputReciver;

        private void Awake()
        {
            _inputReciver = _target.gameObject.GetComponent<IAnimatorInputReciver>();
        }

        private void FixedUpdate()
        {
            if (_inputReciver.GetVelocity().magnitude <= .1f)
                PlayRotateAnimation();
            else
                PlayMoveAnimation();

            Debug.Log("Speed: " + _inputReciver.GetVelocity().magnitude.ToString());
            Debug.Log("Angle: " + _inputReciver.GetAngleBtwRightNTarget().ToString());
        }

        private void PlayRotateAnimation()
        {
            if (_inputReciver.GetAngleBtwRightNTarget() > 91 || _inputReciver.GetAngleBtwRightNTarget() < 89)
            {
                _animator.SetFloat("Forward", _inputReciver.GetAngleBtwRightNTarget() / 180, .1f, Time.deltaTime);
                _animator.SetFloat("Turn", _inputReciver.GetAngleBtwRightNTarget() / 180, .1f, Time.deltaTime);
            }
            else
            {
                _animator.SetFloat("Forward", 0, .1f, Time.deltaTime);
                _animator.SetFloat("Turn", 0, .1f, Time.deltaTime);
            }
        }

        private void PlayMoveAnimation()
        {
            var coefVector = _inputReciver.GetVelocity() / _inputReciver.GetMaxSpeed();

            var horizontal = coefVector.x;
            var vertical = coefVector.z;

            if (_camera != null)
            {
                _cameraForward = Vector3.Scale(_camera.transform.up, new Vector3(1, 0, 1)).normalized;
                _moveDirection = vertical * _cameraForward + horizontal * _camera.transform.right;
            }
            else
            {
                _moveDirection = vertical * Vector3.forward + horizontal * Vector3.right;
            }

            if (_moveDirection.magnitude > 1)
                _moveDirection.Normalize();

            _input = _moveDirection;

            Vector3 localMove = transform.InverseTransformDirection(_input);
            _turnAmount = localMove.x;
            _forwardAmount = localMove.z;

            _animator.SetFloat("Forward", _forwardAmount, .1f, Time.deltaTime);
            _animator.SetFloat("Turn", _turnAmount, .1f, Time.deltaTime);
        }
    }
}
