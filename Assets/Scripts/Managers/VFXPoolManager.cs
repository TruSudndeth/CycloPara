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
