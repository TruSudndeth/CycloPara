using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public delegate void Interaction();
    public static event Interaction OnInteraction;
    
    public static InputManager Instance { get; private set; }
    private BaseCycloParaInputs _baseInputs;
    private InputAction _WSAD;
    private InputAction _mouseLook;
    private InputAction _interaction;
    private Vector2 _move;
    private bool _interact;
    private bool _interactFixed = false;
    private Vector2 _look;

    [SerializeField]
    private bool mouse = false;
    [SerializeField]
    private bool wsad = false;
    [SerializeField]
    private bool space = false;
    [SerializeField]
    private bool disableInputs;
    private void Awake()
    {
        _baseInputs = new BaseCycloParaInputs();
    }
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _WSAD = _baseInputs.Player.Move;
        _mouseLook = _baseInputs.Player.Look;
        _interaction = _baseInputs.Player.Interaction;
        EnableInputs();
    }
    private void EnableInputs()
    {
        _WSAD.Enable();
        _mouseLook.Enable();
        _interaction.Enable();
    }
    private void DisableInputs()
    {
        _WSAD.Disable();
        _mouseLook.Disable();
        _interaction.Disable();
    }
    void Update()
    {
        _move = _WSAD.ReadValue<Vector2>();
        _look = _mouseLook.ReadValue<Vector2>();
        _interact = _interaction.triggered;
        if(_interact)
        {
            _interactFixed = true;
        }
    }
    private void FixedUpdate()
    {
        //Add Trigger evenets here
        if(_interactFixed)
        {
            _interactFixed = false;
            OnInteraction?.Invoke();
        }
    }
    private void OnDisable()
    { 
        _WSAD.Disable();
        _mouseLook.Disable();
        _interaction.Disable();
    }
    private void OnEnable()
    {
        //_WSAD.Enable();
        //_mouseLook.Enable();
        //_interaction.Enable();
    }
}
