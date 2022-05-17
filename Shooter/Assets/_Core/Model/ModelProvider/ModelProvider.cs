using Shooter.Core.Controllers.Weapon;
using UnityEngine;

namespace Shooter.Core.Model.ModelProvider
{
    public class ModelProvider : SingletoneMonoBehavior<ModelProvider>
    {
        [SerializeField] private MainWeaponController _mainWeaponController;
        [SerializeField] private GranateController _granateController;

        protected override void Initialization()
        {
            base.Initialization();
            _mainWeaponController.CreateMainWeaponTest();
            _granateController.CreateSimpleGranate();
        }
    } 
}
