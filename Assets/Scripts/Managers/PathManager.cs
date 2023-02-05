using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    //Todo: when mouse is clicked make the ants return back to where they left off.
    public static PathManager Instance;
    
    [SerializeField]
    private List<Transform> _foodPath;
    [Space(25)]
    [SerializeField]
    private List<Transform> _homeToPoint1;
    [Space(20)]
    [SerializeField]
    private bool _isFullPath = false;
    [SerializeField]
    private bool _setNewPath = false;

    private List<Transform> _paths;
    public void Start()
    {
        int paths = _foodPath.Count > _homeToPoint1.Count ? _foodPath.Count : _homeToPoint1.Count;
        _paths = new List<Transform>(paths);
        SetPathList();
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Update()
    {
        if(_setNewPath)
        {
            _setNewPath = false;
            _paths.Clear();
            SetPathList();
        }
    }
    private void SetPathList()
    {
        if (_isFullPath)
        {
            _paths.AddRange(_foodPath);
        }
        else
        {
            _paths.AddRange(_homeToPoint1);
        }
    }
    // Update is called once per frame
    public Vector3 SetDestination(int currentIndex)
    {
        int index = currentIndex;
        if (currentIndex >= _foodPath.Count)
            index = 0;
        if (currentIndex < 0)
            index = 0;
        return _foodPath[index].position;
    }
    public (bool, int) NextCheckpoint(int currentIndex, bool isReturningHome, out Vector3 nextCheckpoint)
    {
        int nextIndex = currentIndex;
        if (isReturningHome)
        {
            nextIndex--;
            if (nextIndex < 0)
            {
                nextIndex = 1;
                isReturningHome = false;
            }
        }
        else
        {
            nextIndex++;
            if (nextIndex >= _paths.Count)
            {
                nextIndex = _paths.Count - 2;
                isReturningHome = true;
            }
        }
        Vector3 position  = _paths[nextIndex].position;
        nextCheckpoint = position;
        return (isReturningHome, nextIndex);
    }
}
