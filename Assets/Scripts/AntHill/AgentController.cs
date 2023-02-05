using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;
using ProjectDawn.LocalAvoidance;
using Unity.PlasticSCM.Editor.WebApi;

public class AgentController : MonoBehaviour
{
    private Path _pathDestination;
    private bool _waitForPathInstance = false;
    [SerializeField]
    public bool _isReturning = false;
    [SerializeField]
    private int _currentFoodIndex = 0;
    [Space]
    private Vector3 _currentFoodPosition;
    
    private NavMeshAgent _navMeshAgent;
    private bool _interactMouse = false;
    private Animator _animator;
    [Space]
    
    [SerializeField]
    private float _walkingSpeed = 0;
    private float _AnimationSpeed = 0;
    private float _speedRatio = 0;
    void Start()
    {
        InputManager.OnInteractionMouse += () => _interactMouse = true;

        if (TryGetComponent(out Path path))
            _pathDestination = path;
        else
            Debug.Log("Missing Path component", transform);
        if (TryGetComponent(out NavMeshAgent _na_navMeshAgent))
            _navMeshAgent = _na_navMeshAgent;
        else
            Debug.Log("Missing navemesh agent", transform);
        if(TryGetComponent(out Animator _anim))
            _animator = _anim;
        else
            Debug.Log("Missing animator", transform);

        //LeftOff: Set and get references to the animator and navmesh agent variables
        //Get Walking speed from NaveMeshAgent
        _walkingSpeed = _navMeshAgent.speed;
        //Get Animation speed from Animator

        SetCheckpoint();
    }
    private bool IsAtCheckpoint()
    {
        return Vector3.Distance(transform.position, _currentFoodPosition) < 5f;
    }
    

    //loop threw destination and start comming back

    // Update is called once per frame
    void Update()
    {
        if(false)//_interactMouse)
        {
            _interactMouse = false;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Vector3 mousePos = hit.point;
                _navMeshAgent.SetDestination(mousePos);
            }
        }
        if (IsAtCheckpoint() || _waitForPathInstance)
        {
            _waitForPathInstance = false;
            SetCheckpoint();

        }
    }
    private void SetCheckpoint()
    {
        if (PathManager.Instance == null)
        {
            Debug.Log("Instance is null", transform); //Some reason execution order effects this. :(
            _waitForPathInstance = true;
            return;
        }
        (_isReturning, _currentFoodIndex) = PathManager.Instance.NextCheckpoint(_currentFoodIndex, _isReturning, out _currentFoodPosition); //Null reference
        _pathDestination.Destination = _currentFoodPosition;
    }
    private void OnDisable()
    {
        InputManager.OnInteractionMouse -= () => _interactMouse = false;
    }
}
