using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        SetInstance();
    }
    private void SetInstance()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Update()
    {
        
    }
}
