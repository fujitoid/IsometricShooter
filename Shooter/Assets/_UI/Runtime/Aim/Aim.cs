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
        [Space]
        [SerializeField] private LayerMask _layerMask;

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

            if(Physics.Raycast(ray, out var hit, 100, _layerMask))
            {
                this.transform.position = hit.point + _offset;
            }
        }
    } 
}
