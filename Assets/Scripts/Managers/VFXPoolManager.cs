using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VFXPoolManager : MonoBehaviour
{
    
    public static VFXPoolManager Instance { get; private set; }
    [SerializeField]
    private Transform[] gameObjectPool;
    private List<Transform> vfxPool;
    
    // Start is called before the first frame update
    void Start()
    {
        vfxPool = new(gameObjectPool.Count() * 3);
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnVFXFromPool(Vector3 location, VFXType vfxType)
    {
        Transform vfx = vfxPool.Find(x => x.gameObject.activeSelf == false && x.GetComponent<VFXRoot>().VFXType == vfxType);
        if (vfx == null)
        {
            vfx = Instantiate(gameObjectPool[(int)vfxType], location, Quaternion.identity);
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
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public enum VFXType
{
    vfxDefault
}