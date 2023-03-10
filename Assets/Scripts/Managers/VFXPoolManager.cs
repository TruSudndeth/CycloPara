using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VFXPoolManager : MonoBehaviour
{
    
    public static VFXPoolManager Instance { get; private set; }
    [SerializeField]
    private Transform[] _vfxTransforms;
    [SerializeField]
    private List<Transform> vfxPool;
    private bool _mouseClicked = false;
    
    // Start is called before the first frame update
    void Start()
    {
        vfxPool = new(_vfxTransforms.Count() * 3);
        //do a lamda express for imputmanager interact mouse set _mouseClicked to true
        PopulateVFXPool();
        SubscribeTeEvents();
    }
    private void SubscribeTeEvents()
    {
        InputManager.OnInteractionMouse += () => _mouseClicked = true;
    }
    private void PopulateVFXPool()
    {
        foreach (var vfx in _vfxTransforms)
        {
            for (int i = 0; i < 3; i++)
            {
                var vfxInstance = Instantiate(vfx, transform);
                vfxInstance.gameObject.SetActive(false);
                vfxPool.Add(vfxInstance);
            }
        }
    }
    void Update()
    {
        
        if (_mouseClicked)
        {
            _mouseClicked = false;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Vector3 mousePos = hit.point;
                SpawnVFXFromPool(mousePos, VFXType.vfxDefault);
            }
        }

    }
    private void SpawnVFXFromPool(Vector3 location, VFXType vfxType)
    {
        Transform vfx = vfxPool.Find(x => x.gameObject.activeSelf == false && x.GetComponent<VFXRoot>().VFXType == vfxType);
        if (vfx == null)
        {
            vfx = Instantiate(_vfxTransforms[(int)vfxType], location, Quaternion.identity);
            vfx.parent = transform;
            vfxPool.Add(vfx);
        }
        else
        {
            vfx.position = location;
            vfx.gameObject.SetActive(true);
        }
    }
    private void Awake()
    {
        SetInstance();
    }
    private void SetInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}

public enum VFXType
{
    vfxDefault
}