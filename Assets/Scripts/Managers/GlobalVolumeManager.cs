using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeManager : MonoBehaviour
{
    [SerializeField]
    private Volume _globalVolume;
    
    public static GlobalVolumeManager Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        _globalVolume = GetComponent<Volume>();
        ManagerInstance();
    }
    private void ManagerInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetDepthOfField(float start, float end)
    {
        _globalVolume.profile.TryGet(out DepthOfField depthOfField);
        depthOfField.gaussianStart.value = start;
        depthOfField.gaussianEnd.value = end;
    }
}
