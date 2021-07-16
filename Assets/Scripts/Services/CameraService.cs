using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraService
{

    private Camera camera;
    private Transform transform;

    public CameraService(Camera _camera)
    {
        camera = _camera;
    }

    public void AttachTo(Transform _transform)
    {
        transform = _transform;
    }

    public void Update()
    {
        if (transform)
        {
            camera.transform.position = transform.position;
            camera.transform.rotation = transform.rotation;
        }
    }
}
