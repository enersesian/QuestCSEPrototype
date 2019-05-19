using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPulled : MonoBehaviour
{
    public Transform leverBase, numberWheel;
    private float localRotationX, distanceToLeverBase;
    const float leverTopMin = 0.09f, leverTopMax = -0.05f, leverBaseMin = -60f, leverBaseMax = -120f, numberWheelMin = -70f, numberWheelMax = -160f;
    private Vector3 startPosition;
    private Quaternion leverBaseStartRotation, numberWheelStartRotation;
    private LevelManager LevelManager;
    private bool isActive;

    private void Start()
    {
        LevelManager = transform.root.GetComponent<LevelManager>();
        distanceToLeverBase = Vector3.Distance(transform.position, leverBase.position);
        startPosition = transform.position;
        leverBaseStartRotation = leverBase.rotation;
        numberWheelStartRotation = numberWheel.rotation;
        SetTask();
    }

    public void SetTask()
    {
        isActive = false;
        LevelManager.ForceGrabberRelease(GetComponent<OVRGrabbable>());
        GetComponent<OVRGrabbable>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Renderer>().material.SetColor("_Color", LevelManager.disabledColor);
        transform.position = startPosition;
        leverBase.rotation = leverBaseStartRotation;
        numberWheel.rotation = numberWheelStartRotation;
    }

    public void GetTaskButtonPushed(int currentTask)
    {
        isActive = true;
        GetComponent<SphereCollider>().enabled = true;
        GetComponent<OVRGrabbable>().enabled = true;
        GetComponent<Renderer>().material.SetColor("_Color", LevelManager.activeColor);
    }

    public void RunOutputButtonPushed()
    {
        isActive = false;
        LevelManager.ForceGrabberRelease(GetComponent<OVRGrabbable>());
        GetComponent<OVRGrabbable>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Renderer>().material.SetColor("_Color", LevelManager.disabledColor);
    }

    // Update is called once per frame
    void Update ()
    {
        if(isActive)
        {
            if (transform.localPosition.z > 0.0902f)
            {
                transform.localPosition = new Vector3(0f, 0.15f, 0.09f);
                //update board total
                if(transform.parent.name == "lever0001") LevelManager.SetNumber(1, 0); //bit 1 = 0
                if(transform.parent.name == "lever0010") LevelManager.SetNumber(2, 0); //bit 2 = 0
            }
            else if (transform.localPosition.z < -0.0502f)
            {
                transform.localPosition = new Vector3(0f, 0.15f, -0.05f);
                //update board total
                if (transform.parent.name == "lever0001") LevelManager.SetNumber(1, 1); //bit 1 = 1
                if (transform.parent.name == "lever0010") LevelManager.SetNumber(2, 1); //bit 2 = 1
            }
            else transform.position = new Vector3(startPosition.x, leverBase.GetChild(1).position.y, transform.position.z);//Mathf.Clamp(transform.localPosition.y, 0.15f, 0.18f), transform.localPosition.z);
            transform.localRotation = Quaternion.identity;

            //rebuild lever system off center rotation of 0 so its easy to find angle hence use cosine to find where to fix y position of levelTop

            //update lever shaft rotation based on user grabbing top of lever
            localRotationX = Scale(leverTopMin, leverTopMax, leverBaseMin, leverBaseMax, transform.localPosition.z);
            leverBase.localRotation = Quaternion.Euler(localRotationX, leverBase.localRotation.y, leverBase.localRotation.z);

            //update number wheel rotation based on user grabbing top of lever
            localRotationX = Scale(leverTopMin, leverTopMax, numberWheelMin, numberWheelMax, transform.localPosition.z);
            numberWheel.localRotation = Quaternion.Euler(localRotationX, numberWheel.localRotation.y, numberWheel.localRotation.z);
        }
	}

    public float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
        if (NewValue < NewMax) NewValue = NewMax;
        if (NewValue > NewMin) NewValue = NewMin;
        return (NewValue);
    }
}
