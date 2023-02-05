using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSingalton : MonoBehaviour
{
    public static ManagerSingalton Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
        }
        
        else
            Destroy(gameObject);
    }
}
