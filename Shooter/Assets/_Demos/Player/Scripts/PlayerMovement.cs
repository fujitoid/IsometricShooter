using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputAction _horizontal;
    [SerializeField] private InputAction _vertical;
    [SerializeField] private InputAction _coursor;
    [Space]
    [SerializeField] private InputAction _tp;
    [Space, Header("References")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Camera _playerCamera;
    [Header("Settings")]
    [SerializeField] private float _tpMultiplicator;

    private Coroutine _tpRoutine;

    private void Awake()
    {
        _horizontal.Enable();
        _vertical.Enable();
        _coursor.Enable();

        _tp.Enable();

        _tpRoutine = StartCoroutine(SpeedUpRoutine());
    }

    private void OnDestroy()
    {
        _horizontal.Disable();
        _vertical.Disable();
        _coursor.Disable();

        _tp.Disable();

        StopCoroutine(_tpRoutine);
    }

    private void Update()
    {
        Move();
        RotateToPoint();
    }

    private void Move()
    {
        var x = _horizontal.ReadValue<float>();
        var y = _vertical.ReadValue<float>();

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
        var ray = _playerCamera.ScreenPointToRay(_coursor.ReadValue<Vector2>());

        if (Physics.Raycast(ray, out var hit))
        {
            var lookPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookPosition);
        }
    }

    private IEnumerator SpeedUpRoutine()
    {
        while (true)
        {
            if (_tp.triggered)
            {
                var x = _horizontal.ReadValue<float>();
                var y = _vertical.ReadValue<float>();
                var tpDirection = new Vector3(x, 0, y);
                var tpPostion = transform.position + tpDirection * _tpMultiplicator;
                transform.position = tpPostion;
                yield return new WaitForEndOfFrame();
                _agent.ResetPath();
                yield return new WaitForSeconds(1);
            }

            yield return null;
        }
    }
}
