using ProjectDawn.LocalAvoidance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAvoidance_test : MonoBehaviour
{
    [SerializeField]
    private bool _testSonar = false;
    void Start()
    {
        var sonar = new SonarAvoidance(transform.position, transform.forward, transform.up, 5f, 5f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //if(_testSonar)
        //sonar = new SonarAvoidance(transform.position, transform.forward, transform.up, 5f, 5f, 2.5f);
    }
}
