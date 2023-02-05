using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AnimateCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _camera;
    private float _pathPosition = 0;
    [SerializeField]
    private float _pathSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //smooth out _pathPosition float and ping pong min max
        _pathPosition += _pathSpeed * Time.fixedDeltaTime;

        _camera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = _pathPosition;



    }
}
