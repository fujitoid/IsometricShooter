using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputContainer : ScriptableObject
{
    [SerializeField] private InputAction _horizontal;
    [SerializeField] private InputAction _vertical;
    [SerializeField] private InputAction _coursor;
    [SerializeField] private InputAction _tp;
    [SerializeField] private InputAction _shoot;
    [SerializeField] private InputAction _dropGranate;
    
    public InputAction Horizontal => _horizontal;
    public InputAction Vertical => _vertical;
    public InputAction Coursor => _coursor;
    public InputAction Tp => _tp;
    public InputAction Shoot => _shoot;
    public InputAction DropGranate => _dropGranate;
}
