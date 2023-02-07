using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public delegate void Interaction();
    public static event Interaction OnInteractionMouse;
    public static event Interaction OnInteractionSpace;
    public static InputManager Instance { get; private set; }
    public Vector2 Move { get { return _move; } private set { } }
    public Vector2 Look { get  { return _look; } private set { } }
    
    
    private BaseCycloParaInputs _baseInputs;
    private InputAction _WSAD;
    private InputAction _mouseLook;
    private InputAction _actionMouse;
    private InputAction _actionSpace;
    private Vector2 _look;
    private Vector2 _move;
    private bool _interactMouse = false;
    private bool _interactSpace = false;
    private bool _interactMouseFixed = false;
    private bool _interactSpaceFixed = false;
    private void Awake()
    {
        _baseInputs = new BaseCycloParaInputs();
        SetInstance();
    }
    void Start()
    {
        _WSAD = _baseInputs.Player.Move;
        _mouseLook = _baseInputs.Player.Look;
        _actionMouse = _baseInputs.Player.ActionMouse;
        _actionSpace = _baseInputs.Player.ActionSpace;
        EnableInputs();
    }
    private void SetInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void EnableInputs()
    {
        _WSAD.Enable();
        _mouseLook.Enable();
        _actionMouse.Enable();
        _actionSpace.Enable();
    }
    public void DisableInputs()
    {
        _WSAD.Disable();
        _mouseLook.Disable();
        _actionMouse.Disable();
        _actionSpace.Disable();
        //check is _actionMouse is Disabled
        if (_mouseLook.phase == InputActionPhase.Disabled)
        {
            Debug.Log("MouseLook is Disabled");
        }
    }
    void Update()
    {
        _move = _WSAD.ReadValue<Vector2>();
        _look = _mouseLook.ReadValue<Vector2>();

        _interactMouse = _actionMouse.triggered;
        if(_interactMouse)
        {
            _interactMouse = false;
            _interactMouseFixed = true;
        }
        _interactSpace = _actionSpace.triggered;
        if(_interactSpace)
        {
            _interactSpace = false;
            _interactSpaceFixed = true;
        }
    }
    private void FixedUpdate()
    {
        if(_interactMouseFixed)
        {
            _interactMouseFixed = false;
            OnInteractionMouse?.Invoke();
        }
        if (_interactSpaceFixed)
        {
            _interactSpaceFixed = false;
            OnInteractionSpace?.Invoke();
        }
    }
    private void OnDisable()
    { 
        _WSAD.Disable();
        _mouseLook.Disable();
        _actionMouse.Disable();
        _actionSpace.Disable();
    }
}
