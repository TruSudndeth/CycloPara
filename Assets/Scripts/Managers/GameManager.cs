using Cinemachine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //Inputs
    private bool _interactMouse = false;
    [SerializeField]
    private int _MangerFrameIterations = 10;
    //Aduio
    [SerializeField]
    private AudioManager _KoreoPlayer;
    private bool _isKoreoPlayerNull = true;
    private Koreography _stage;
    //Global Volume
    [SerializeField]
    private GlobalVolumeManager _globalVolume;
    private bool _isGlobalVolumeNull = true;
    //VirtualCamera
    private VCameraManager _vCamera;
    private bool _isCameraNull = true;
    void Start()
    {
        //StartCoroutine(CheckNullKoreoPlayer());
        //StartCoroutine(AccessGlobalVolumeInstance());
        //_KoreoPlayer = StartCoroutine(SetInstance.CheckInstance);

        StartCoroutine(GetInstance(AudioManager.Instance, x => _KoreoPlayer = x, x => _isKoreoPlayerNull = x));
        StartCoroutine(GetInstance(GlobalVolumeManager.Instance,x => _globalVolume = x, x => _isGlobalVolumeNull = x));
        StartCoroutine(GetInstance(VCameraManager.Instance, x => _vCamera = x, x => _isCameraNull = x));
        StartCoroutine(StartingGame());
        
        ManagerInstance();
    }
    IEnumerator GetInstance<T>(T checkInstance, Action<T> setInstance, Action<bool> isNull) where T : class
    {
        for(int i = 0;i<_MangerFrameIterations; i++)
        {
            if (i >= _MangerFrameIterations)
                Debug.Log("Instance of " + checkInstance.GetType() + " was not found");
            if (checkInstance == null)
                yield return new WaitForEndOfFrame();
            else
            {
                setInstance(checkInstance);
                isNull(false);
                Debug.Log("Loded Instance " + checkInstance.GetType() + " To " + setInstance.Target);
                break;
            }
        }
    }
    private void StartGame()
    {
        //Testing: check if input is disabled
        InputManager.OnInteractionSpace += () => _interactMouse = true;
        Debug.Log("StartGame");
        //AudioManger
        _stage = _KoreoPlayer.KoreoStage1;
        _KoreoPlayer.LoadKoreoTrack(_stage, 0, false);
        _KoreoPlayer.PlayKoreoTrack(true);
        //GlobalVolume setup.
        _globalVolume.SetDepthOfField(0.0f, 93.3f);
        //CameraSetup
        _vCamera.SetCameraPath(VCameraPath.OpeningScene);

        //Start mini Game 1
        _globalVolume.SetDepthOfField(0.0f, 12.0f);
        //set inputs that will be used in Stage 1
        
        //Listen for event mini game completed
        //Start mini Game 2
        //Listen for event mini game completed
        //Start mini Game 3
        //Listen for event mini game completed
        //Start mini Game 4
        //Listen for event mini game completed
        //Start mini Game 5
        //Listen for event mini game completed
        //JumpBack to main game
        
    }
    IEnumerator StartingGame()
    {
        while (_isKoreoPlayerNull || _isGlobalVolumeNull || _isCameraNull)
        {
            yield return new WaitForEndOfFrame();
        }
        StartGame();
        _isKoreoPlayerNull = false;
        yield return null;
    }
    private void ManagerInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(_interactMouse)
        {
            //Testing: check if input is disabled
            _interactMouse = false;
            InputManager.Instance.DisableInputs();
        }
    }
    private void OnDisable()
    {
        InputManager.OnInteractionSpace -= () => _interactMouse = true;
    }
}
