using UnityEngine;

namespace Shooter.Runtime.Animations.Context
{
    public interface IAnimatorInputReciver
    {
        public Vector3 GetVelocity();
        public float GetMaxSpeed();
        public float GetAngleBtwRightNTarget();
    } 
}
