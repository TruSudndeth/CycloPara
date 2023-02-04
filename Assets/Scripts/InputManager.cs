using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private BaseCycloParaInputs _baseInputs;
    private InputAction _WSAD;
    private InputAction _mouseLook;
    private InputAction _interaction;
    private Vector2 _move;
    private bool _interact;
    private Vector2 _look;
    private void Awake()
    {
        _baseInputs = new BaseCycloParaInputs();
    }
    void Start()
    {
        _WSAD = _baseInputs.Player.Move;
        _mouseLook = _baseInputs.Player.Look;
        _interaction = _baseInputs.Player.Interaction;
    }
    void Update()
    {
        _move = _WSAD.ReadValue<Vector2>();
        _interact = _interaction.triggered;
        _look = _mouseLook.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Debug.Log("move " + _move);
        Debug.Log("interact " + _interact);
        Debug.Log("look " + _look);
    }
    private void OnDisable()
    {
        _WSAD.Disable();
        _mouseLook.Disable();
        _interaction.Disable();
    }
    private void OnEnable()
    {
        _WSAD.Enable();
        _mouseLook.Enable();
        _interaction.Enable();
    }
}
