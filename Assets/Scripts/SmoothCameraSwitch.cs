using Unity.VisualScripting;
using UnityEngine;
using Util;

public class SmoothCameraSwitch : MonoBehaviour
{
    public static float DefaultSwitchTime = 1f;
    public Camera[] Cameras;
    public Camera MainCamera;
    Camera DesiredCamera => Cameras[_indexLooper];


    /// <summary>
    /// Scale the time by the distance of the cameras
    /// </summary>
    public bool AutoSwitchTime;
    private bool _prevAutoSwitchTime;

    public Easings.Interpolator Interpolator;
    
    public float SwitchTime = 2f;
    public float AutoSwitchSpeed = 8f;

    private bool _autoSwitchTime;
    float _timer;
    int _sharedPriority = 10;
    IndexLooper _indexLooper;
    TransformSnap _start;
    TransformSnap _destination;

    private void Start()
    {
        Interpolator = Easings.ExpoOut;
        
        _indexLooper = Cameras.Length;
        for (int i = 0; i < Cameras.Length; i++)
            Cameras[i].depth = _sharedPriority;
        MainCamera.depth = _sharedPriority + 1;

        var mainTf = MainCamera.transform;
        var desTf = DesiredCamera.transform;
        mainTf.position = desTf.position;
        mainTf.rotation = desTf.rotation;

        StartSwitching();
    }

    public void SetDesiredCamera(Camera cam)
    {
        var id = Cameras.IndexOf(cam);
        if (id < 0)
        {
            Debug.LogError("Camera was not found in CameraSwitcher");
            return;
        }

        SetDesiredCamera(id);
    }

    public void SetDesiredCamera(int id)
    {
        _indexLooper.Value = id;
        StartSwitching();
    }

    public void NextCamera()
    {
        SetDesiredCamera(_indexLooper.Next());
    }

    public void PreviousCamera()
    {
        SetDesiredCamera(_indexLooper.Previous());
    }

    void StartSwitching()
    {
        _start = new TransformSnap(MainCamera.transform.position, MainCamera.transform.eulerAngles);
        _destination = new TransformSnap(DesiredCamera.transform.position, DesiredCamera.transform.eulerAngles);

        _timer = 0f;
        if (AutoSwitchTime)
        {
            var distance = Vector3.Distance(_start.Position, _destination.Position);
            SwitchTime = distance / AutoSwitchSpeed;
        }
    }

    void Update()
    {
        if (_prevAutoSwitchTime != AutoSwitchTime)
        {
            _prevAutoSwitchTime = AutoSwitchTime;
            SwitchTime = DefaultSwitchTime;
        }
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            NextCamera();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            PreviousCamera();
        }

        if (_timer < SwitchTime)
        {
            _timer += Time.deltaTime;
            var t = _timer / SwitchTime;
            t = Interpolator(t);
            MainCamera.transform.position = Vector3.Lerp(_start.Position, _destination.Position, t);
            MainCamera.transform.eulerAngles = Vector3.Lerp(_start.Euler, _destination.Euler, t);
        }
    }
}