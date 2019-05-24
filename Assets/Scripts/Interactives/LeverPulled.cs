using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPulled : MonoBehaviour
{
    public Transform leverBase, numberWheel,leverTopYLocalPosition, zeroPosition, onePosition;
    private float localRotationX, distanceToLeverBase;
    private float leverTopMin, leverTopMax, leverBaseMin, leverBaseMax, numberWheelMin = -150f, numberWheelMax = -240f;
    private Vector3 startPosition;
    private Quaternion leverBaseStartRotation, numberWheelStartRotation;
    private LevelManager levelManager;
    private bool isActive;

    private void Awake()
    {
        levelManager = transform.root.GetComponent<LevelManager>();
        distanceToLeverBase = Vector3.Distance(transform.position, leverBase.position);
        startPosition = transform.localPosition;
        leverBaseStartRotation = leverBase.rotation;
        if(numberWheel != null) numberWheelStartRotation = numberWheel.rotation;
        leverTopMin = zeroPosition.localPosition.z;
        leverTopMax = onePosition.localPosition.z;

        if (transform.parent.name == "lever01" || transform.parent.name == "lever10")
        {
            leverBaseMin = -70f;
            leverBaseMax = -110f;
        }
        else
        {
            leverBaseMin = -60f;
            leverBaseMax = -120f;
        }
    }

    public void SetToZeroPosition()
    {
        transform.localPosition = zeroPosition.localPosition;
        leverBase.localRotation = zeroPosition.localRotation;
        if (numberWheel != null) numberWheel.rotation = numberWheelStartRotation;
    }

    public void SetToOnePosition()
    {
        transform.localPosition = onePosition.localPosition;
        leverBase.localRotation = onePosition.localRotation;
        if (numberWheel != null) numberWheel.localRotation = Quaternion.Euler(numberWheelMax, 0f, 0f);
    }

    public void Activate()
    {
        isActive = true;
        levelManager.ForceGrabberRelease(GetComponent<OVRGrabbable>());
        GetComponent<OVRGrabbable>().enabled = true;
        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().material.SetColor("_Color", levelManager.activeColor);
    }

    public void Deactivate()
    {
        isActive = false;
        levelManager.ForceGrabberRelease(GetComponent<OVRGrabbable>());
        GetComponent<OVRGrabbable>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().material.SetColor("_Color", levelManager.disabledColor);
    }


    public void SetTask() //Refactor to DisableLever method
    {
        if(transform.parent.name == "leverGetTask")
        {
            Activate();
            SetToZeroPosition();
        }
        else
        {
            Deactivate();
            SetToZeroPosition();
        }
    }

    public void GetTaskButtonPushed(int currentTask) //Refactor to EnableLever method
    {
        Activate();
    }

    public void RunOutputButtonPushed()
    {
        Deactivate();
    }

    // Update is called once per frame
    void Update ()
    {
        if(isActive)
        {
            if (transform.localPosition.z > zeroPosition.localPosition.z)
            {//went past zero position, set to zero position
                transform.localPosition = zeroPosition.localPosition;
                
                if (transform.parent.name == "lever0001") levelManager.SetNumber(1, 0); //number bit 1 = 0
                if (transform.parent.name == "lever0010") levelManager.SetNumber(2, 0); //number bit 2 = 0
                if (transform.parent.name == "lever0100") levelManager.SetNumber(3, 0); //number bit 3 = 0
                //if (transform.parent.name == "lever1000") levelManager.SetNumber(4, 0); //bit 4 = 0

                if (transform.parent.name == "leverRed") levelManager.SetColor(4, 0); //color bit 1 = 0
                if (transform.parent.name == "leverGreen") levelManager.SetColor(5, 0); //color bit 2 = 0
                if (transform.parent.name == "leverBlue") levelManager.SetColor(6, 0); //color bit 3 = 0

                if (transform.parent.name == "lever01") levelManager.SetShape(7, 0); //size bit 1 = 0
                if (transform.parent.name == "lever10") levelManager.SetShape(8, 0); //size bit 2 = 0
                
            }
            else if (transform.localPosition.z < onePosition.localPosition.z)
            {//went past one position, set to one position
                transform.localPosition = onePosition.localPosition;
                //update board total
                if (transform.parent.name == "lever0001") levelManager.SetNumber(1, 1); //number bit 1 = 1
                if (transform.parent.name == "lever0010") levelManager.SetNumber(2, 1); //number bit 2 = 1
                if (transform.parent.name == "lever0100") levelManager.SetNumber(3, 1); //number bit 3 = 1
                //if (transform.parent.name == "lever1000") levelManager.SetNumber(4, 1); //bit 4 = 1

                if (transform.parent.name == "leverRed") levelManager.SetColor(4, 1); //color bit 1 = 1
                if (transform.parent.name == "leverGreen") levelManager.SetColor(5, 1); //color bit 2 = 1
                if (transform.parent.name == "leverBlue") levelManager.SetColor(6, 1); //color bit 3 = 1

                if (transform.parent.name == "lever01") levelManager.SetShape(7, 1); //size bit 1 = 1
                if (transform.parent.name == "lever10") levelManager.SetShape(8, 1); //size bit 2 = 1

                if (transform.parent.name == "leverGetTask") GetTaskLeverPulled();
                if (transform.parent.name == "leverRunOutput") RunOutputLeverPulled();
                if (transform.parent.name == "leverSendOutput") SendOutputLeverPulled();
            }
            else transform.localPosition = new Vector3(startPosition.x, leverTopYLocalPosition.localPosition.y, transform.localPosition.z);//Mathf.Clamp(transform.localPosition.y, 0.15f, 0.18f), transform.localPosition.z);
            transform.localRotation = Quaternion.identity;

            //rebuild lever system off center rotation of 0 so its easy to find angle hence use cosine to find where to fix y position of levelTop

            //update lever shaft rotation based on user grabbing top of lever
            localRotationX = Scale(leverTopMin, leverTopMax, leverBaseMin, leverBaseMax, transform.localPosition.z);
            leverBase.localRotation = Quaternion.Euler(localRotationX, leverBase.localRotation.y, leverBase.localRotation.z);

            //update number wheel rotation based on user grabbing top of lever
            localRotationX = Scale(leverTopMin, leverTopMax, numberWheelMin, numberWheelMax, transform.localPosition.z);
            if (numberWheel != null) numberWheel.localRotation = Quaternion.Euler(localRotationX, numberWheel.localRotation.y, numberWheel.localRotation.z);
        }
	}

    private void SendOutputLeverPulled()
    {
        Deactivate();
        levelManager.OutputSent();
    }

    private void RunOutputLeverPulled()
    {
        Deactivate();
        levelManager.RunOutputButtonPushed();
    }

    private void GetTaskLeverPulled()
    {
        Deactivate();
        levelManager.GetTaskLeverPulled();
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
