using System.Collections;
using UnityEngine;
using Util;

public class CameraSwitch : MonoBehaviour
{
    public Camera[] Cameras;
    Camera CurrentCamera => Cameras[_indexLooper];
    
    int _sharedPriority = 10;
    IndexLooper _indexLooper;

    void Start()
    {
        _indexLooper = Cameras.Length;
    }

    void ResetAllCameraDepths()
    {
        for (int i = 0; i < Cameras.Length; i++)
            Cameras[i].depth = _sharedPriority;
    }
    
    public void SetDesiredCamera(int id)
    {
        _indexLooper.Value = id;
        ResetAllCameraDepths();
        CurrentCamera.depth = _sharedPriority + 1;
    }

    public void NextCamera()
    {
        SetDesiredCamera(_indexLooper.Next());
    }

    public void PreviousCamera()
    {
        SetDesiredCamera(_indexLooper.Previous());
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            NextCamera();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            PreviousCamera();
        }
    }
}
