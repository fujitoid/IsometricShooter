using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : ScriptableObject
{
    [SerializeField] private Vector3 _offset;

    public Vector3 Offset => _offset;
}
