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
        //InputManager.OnInteraction += SetTime;
        Koreographer.Instance.RegisterForEvents("Base_BPM-Drums", FireEventWDebg);
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
        if (_time + _resetTime < Time.time)
        {
            _time = Time.time;
            _interacted = false;
            _meshRend.material.color = Color.white;
        }
    }
    private void SetTime()
    {
        _meshRend.material.color = Color.red;
        _interacted = true;
        _time = Time.time;
    }
}
