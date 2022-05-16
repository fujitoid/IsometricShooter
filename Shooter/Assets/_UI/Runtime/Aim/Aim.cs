using Shooter.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.UI.Runtime
{
    public class Aim : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset = new Vector3();
        [SerializeField] private Camera _playerCamera;
        [Space]
        [SerializeField] private Image _icon;

        private void OnEnable()
        {
            UIProvider.Instance.InputsContainer.MousePosition.Enable();
        }

        private void OnDisable()
        {
            UIProvider.Instance.InputsContainer.MousePosition.Disable();
        }

        private void Update()
        {
            var ray = _playerCamera
                .ScreenPointToRay(UIProvider.Instance.InputsContainer.MousePosition.ReadValue<Vector2>());

            if(Physics.Raycast(ray, out var hit))
            {
                this.transform.position = hit.point + _offset;
            }
        }
    } 
}
