using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [Space]
    [SerializeField] private Vector3 _offset;

    private Player _player;

    private void Start()
    {
        _player = Player.Instance;
    }

    private void Update()
    {
        var dirPosition = _player.transform.position + _offset;
        _camera.transform.position = dirPosition;
    }
}
