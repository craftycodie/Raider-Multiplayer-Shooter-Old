﻿using UnityEngine;
using System.Collections;

public class FlyCameraController : CameraController
{
    MovementController movController;
    void Start()
    {
        base.Start();

        movController = GetComponent<MovementController>();
        movController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
        MoveCamera();
        LockCamZRotation();
        LockCamPointZRotation();
    }

    void RotateCamera()
    {
        //Looking up and down, needs to be inverted for some reason...
        float _yRot = Input.GetAxisRaw("Mouse X");
        float _xRot = -Input.GetAxisRaw("Mouse Y");

        //If the camera is set to inverted mode, invert the rotation.
        if (modeController.firstPersonCamSettings.inverted)
        {
            _xRot = -_xRot;
        }

        Vector3 _camPointRotation = new Vector3(_xRot, _yRot, 0f) * modeController.firstPersonCamSettings.lookSensitivity;

        _camPointRotation = ApplyXBufferToRotation(cam.transform.eulerAngles, _camPointRotation);

        //Apply rotation
        camPoint.transform.Rotate(_camPointRotation);
    }

    void MoveCamera()
    {
        float _movX = Input.GetAxis("Horizontal");
        float _movZ = Input.GetAxis("Vertical");

        camPoint.transform.Translate(new Vector3(_movX, 0, _movZ));
    }

    //The fly camera disables movement, so when it's destroyed movement needs to be re-enabled.
    void OnDestroy()
    {
        movController.enabled = true;
    }
}
