using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using SonicBloom.Koreo;

public class ScriptEventListiner : MonoBehaviour
{
    [SerializeField]
    private float _time = 0f;
    [SerializeField]
    private float _resetTime = 3f;
    private bool _interacted = false;
    private bool _alternate = false;
    
    public delegate void KoreographyEventCallback(KoreographyEvent koreoEvent);

    private MeshRenderer _meshRend;
    void Start()
    {
        _meshRend = GetComponent<MeshRenderer>();
        Koreographer.Instance.RegisterForEvents("Base_BPM-Drums", FireEventWDebg);
        //InputManager.OnInteraction += SetTime;
    }
    void FireEventWDebg(KoreographyEvent koreoE)
    {
        Debug.Log("Koreo Event fired");
        if(_alternate)
        {
            transform.localScale = new Vector3(1, 1, 1);
            _meshRend.material.color = Color.red;
            _alternate = false;
        }
        else
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            _meshRend.material.color = Color.blue;
            _alternate = true;
        }
    }
    void Update()
    {
    }
}
