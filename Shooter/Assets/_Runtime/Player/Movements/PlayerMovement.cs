using DG.Tweening;
using Shooter.Runtime.Animations.Context;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IAnimatorInputReciver
{
    [Space, Header("References")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Transform _lookAtTarget;
    [Header("Settings")]
    [SerializeField] private float _tpMultiplicator;

    private Coroutine _tpRoutine;

    private void Awake()
    {
        Player.Instance.PlayerInput.Horizontal.Enable();
        Player.Instance.PlayerInput.Vertical.Enable();
        Player.Instance.PlayerInput.Coursor.Enable();

        Player.Instance.PlayerInput.Coursor.Enable();

        _tpRoutine = StartCoroutine(SpeedUpRoutine());
    }

    private void OnDestroy()
    {
        Player.Instance.PlayerInput.Horizontal.Disable();
        Player.Instance.PlayerInput.Vertical.Disable();
        Player.Instance.PlayerInput.Coursor.Disable();

        Player.Instance.PlayerInput.Coursor.Disable();

        StopCoroutine(_tpRoutine);
    }

    private void Update()
    {
        Move();
        RotateToPoint();
    }

    private void Move()
    {
        var x = Player.Instance.PlayerInput.Horizontal.ReadValue<float>();
        var y = Player.Instance.PlayerInput.Vertical.ReadValue<float>();

        if (x != 0 || y != 0)
        {
            var moveDirection = new Vector3(x, 0, y);
            var movePosition = transform.position + moveDirection;
            _agent.SetDestination(movePosition);
        }
        else
        {
            _agent.SetDestination(this.transform.position);
        }
    }

    private void RotateToPoint()
    {
        var lookAtPosition = new Vector3(_lookAtTarget.position.x, transform.position.y, _lookAtTarget.position.z);
        transform.DOLookAt(lookAtPosition, 1f);
    }

    private IEnumerator SpeedUpRoutine()
    {
        while (true)
        {
            if (Player.Instance.PlayerInput.Tp.triggered)
            {
                var x = Player.Instance.PlayerInput.Horizontal.ReadValue<float>();
                var y = Player.Instance.PlayerInput.Vertical.ReadValue<float>();
                var tpDirection = new Vector3(x, 0, y);
                var tpPostion = transform.position + tpDirection * _tpMultiplicator;

                var prewSpeed = _agent.speed;
                var prewAccel = _agent.acceleration;
                _agent.speed = 1000;
                _agent.acceleration = 1000;

                _agent.SetDestination(tpPostion);

                yield return new WaitForSeconds(.1f);

                _agent.speed = prewSpeed;
                _agent.acceleration = prewAccel;
            }

            yield return null;
        }
    }

    public Vector3 GetVelocity() => _agent.velocity;

    public float GetMaxSpeed() => _agent.speed;

    public float GetAngleBtwRightNTarget()
    {
        var disatnceBetweenLookAtTarget = Vector3.Distance(_lookAtTarget.position, transform.position);
        return Vector3.Angle(transform.right * disatnceBetweenLookAtTarget, _lookAtTarget.position - transform.position);
    }
}
