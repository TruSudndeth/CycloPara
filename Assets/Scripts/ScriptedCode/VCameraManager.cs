using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor.TextCore.Text;

public class VCameraManager : MonoBehaviour
{
    public static VCameraManager Instance;

    [Space]
    [SerializeField]
    private CinemachinePathBase _openingScene;
    [SerializeField]
    private CinemachinePathBase _stage1;
    [SerializeField]
    private CinemachinePathBase _stage2;
    [SerializeField]
    private CinemachinePathBase _stage3;
    [SerializeField]
    private CinemachinePathBase _stage4;
    [SerializeField]
    private CinemachinePathBase _stage5;
    
    [Space]
    private bool _isCameraNull = true; //Delete: line not using _isCameraNull
    [Space]
    private CinemachineVirtualCamera _vCamera;
    public CinemachineVirtualCamera VCamera { get { return _vCamera; } private set { } }

    [Space(25)] 
    [SerializeField]
    private float _pathSpeed = 1f;
    private float _pathPosition = 0f;
    private void Start()
    {
        _vCamera = TryGetComponent<CinemachineVirtualCamera>(out CinemachineVirtualCamera cam) ? cam : null;
        
        SetInstance();
    }
    private void SetInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        //smooth out _pathPosition float and ping pong min max
        _pathPosition += _pathSpeed * Time.fixedDeltaTime;
        _vCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = _pathPosition;
        //Get the current path position
        float pathPosition = _vCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition;
        //get the path length
        float pathLength = _vCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path.MaxUnit(CinemachinePathBase.PositionUnits.PathUnits);
        _vCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = _openingScene;
        if (pathPosition >= pathLength)
        {
            _pathPosition = 0f;
        }
    }
    public void SetCameraPath(VCameraPath cameraPath)
    {
        switch (cameraPath)
        {
            case VCameraPath.OpeningScene:
                _vCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = _openingScene;
                break;
            case VCameraPath.Stage1:
                _vCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = _stage1;
                break;
            case VCameraPath.Stage2:
                _vCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = _stage2;
                break;
            case VCameraPath.Stage3:
                _vCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = _stage3;
                break;
            case VCameraPath.Stage4:
                _vCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = _stage4;
                break;
            case VCameraPath.Stage5:
                _vCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = _stage5;
                break;
        }
        _pathPosition = 0;
    }
}

public enum VCameraPath
{
    OpeningScene,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage5
}

