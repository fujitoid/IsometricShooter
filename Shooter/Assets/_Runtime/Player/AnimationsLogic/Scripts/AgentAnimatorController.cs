using Shooter.Runtime.Animations.Context;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.Runtime.Animations
{
    public enum AnimateType
    {
        Player = 1,
        Enemy = 2,
    }

    public class AgentAnimatorController : MonoBehaviour
    {
        [SerializeField] private AnimateType _animateType;
        [SerializeField] private float _speed = 2f;
        [Space]
        [SerializeField] private Animator _animator;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _target;

        private Vector3 _cameraForward;
        private Vector3 _moveDirection;
        private Vector3 _input;

        private float _forwardAmount;
        private float _turnAmount;

        private bool _isReadyForDropingGranate = false;

        private IAnimatorInputReciver _inputReciver;

        private void Awake()
        {
            _inputReciver = _target.gameObject.GetComponent<IAnimatorInputReciver>();

            if(_animateType == AnimateType.Player)
            {
                Player.Instance.PlayerInput.DropGranate.started += x => _isReadyForDropingGranate = true;
                Player.Instance.PlayerInput.DropGranate.canceled += x => _isReadyForDropingGranate = false;
                Player.Instance.PlayerInput.Shoot.performed += x => ThrowGranate();
            }
        }

        private void FixedUpdate()
        {
            if ((_inputReciver.GetVelocity().magnitude > 0))
                PlayMoveAnimation();
            else
                PlayRotateAnimation();

            Debug.Log(_inputReciver.GetVelocity().magnitude);
        }

        private void PlayRotateAnimation()
        {
            _animator.SetFloat("Forward", 0, .1f, Time.deltaTime * _speed);
            _animator.SetFloat("Turn", 0, .1f, Time.deltaTime * _speed);

            if (_inputReciver.GetAngleBtwRightNTarget() > 91)
            {
                _animator.SetBool("IsLeft", true);
                _animator.SetBool("IsRight", false);
            }
            else if (_inputReciver.GetAngleBtwRightNTarget() < 89)
            {
                _animator.SetBool("IsRight", true);
                _animator.SetBool("IsLeft", false);
            }
            else
            {
                _animator.SetBool("IsRight", false);
                _animator.SetBool("IsLeft", false);
            }
        }

        private void PlayMoveAnimation()
        {
            _animator.SetBool("IsRight", false);
            _animator.SetBool("IsLeft", false);

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

            _animator.SetFloat("Forward", _forwardAmount, .1f, Time.deltaTime * _speed);
            _animator.SetFloat("Turn", _turnAmount, .1f, Time.deltaTime * _speed);
        }

        private void ThrowGranate()
        {
            if (_isReadyForDropingGranate == false)
                return;

            _animator.ResetTrigger("ThrowGranate");
            _animator.SetTrigger("ThrowGranate");
        }
    }
}
