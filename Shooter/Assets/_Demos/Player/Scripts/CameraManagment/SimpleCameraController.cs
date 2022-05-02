using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [Space] [SerializeField] private CameraSettings _settings;

    private Player _player;

    private void Start()
    {
        _player = Player.Instance;
    }

    private void Update()
    {
        var dirPosition = _player.transform.position + _settings.Offset;
        _camera.transform.position = dirPosition;
    }
}
