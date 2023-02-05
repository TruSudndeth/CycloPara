using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PulseMaterialEmision : MonoBehaviour
{
    //add an event to tell other scripts to change to the next camera to invoke
    //the next pulse
    public static event NextCameraView OnNextCameraView;
    public delegate void NextCameraView();

    public Material material;
    public float pulseSpeed = 1f;
    [SerializeField]
    public float pulseIntensity = 5f;
    public float emission;

    private bool _nextCamera = false;
    // Start is called before the first frame update
    void Start()
    {
        //listen for mouse event form InputManager
        InputManager.OnInteractionMouse += () => _nextCamera = true;
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if(_nextCamera)
        {
            //do a raycast hit to see if this object was hit
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                if (hit.transform == transform)
                {
                    //if it was hit, invoke the next camera event
                    OnNextCameraView?.Invoke();
                    _nextCamera = false;
                    Debug.Log("Hit spore", transform);
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        emission = Mathf.PingPong(Time.time * pulseSpeed, pulseIntensity);
        Color baseColor = Color.yellow; //Replace this with whatever you want for your base color at emission level '1'

        Color finalColor = emission * new Color(baseColor.r,baseColor.g,baseColor.b, 1.0f);

        material.SetColor("_EmissionColor", finalColor);
    }
}
