using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPulled : MonoBehaviour
{
    public Transform leverBase, numberWheel,leverTopYLocalPosition, zeroPosition, onePosition;
    public GameObject[] lockedElements, unlockedElements;
    private float localRotationX, distanceToLeverBase;
    private float leverTopMin, leverTopMax, leverBaseMin, leverBaseMax, numberWheelMin = -150f, numberWheelMax = -240f;
    private Vector3 startPosition;
    private Quaternion leverBaseStartRotation, numberWheelStartRotation;
    private bool isActive, isOnePosition;

    private void Awake()
    {
        GetComponent<OVRGrabbable>().enabled = false;
        GetComponent<Collider>().enabled = false;
        distanceToLeverBase = Vector3.Distance(transform.position, leverBase.position);
        startPosition = transform.localPosition;
        leverBaseStartRotation = leverBase.rotation;
        if(numberWheel != null) numberWheelStartRotation = numberWheel.rotation;
        leverTopMin = zeroPosition.localPosition.z;
        leverTopMax = onePosition.localPosition.z;
        //leverBaseMin = zeroPosition.localRotation.x;
        //leverBaseMax = onePosition.localRotation.x;
        
        if (transform.parent.name == "lever01" || transform.parent.name == "lever10" || transform.parent.name == "leverTutorial")
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
        isOnePosition = false;
    }

    public void SetToOnePosition()
    {
        transform.localPosition = onePosition.localPosition;
        leverBase.localRotation = onePosition.localRotation;
        if (numberWheel != null) numberWheel.localRotation = Quaternion.Euler(numberWheelMax, 0f, 0f);
        isOnePosition = true;
    }

    public void Activate()
    {
        isActive = true;
        UserManager.instance.ForceGrabberRelease(GetComponent<OVRGrabbable>());
        GetComponent<OVRGrabbable>().enabled = true;
        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().material.SetColor("_Color", LevelManager.instance.activeColor);
        foreach(GameObject element in unlockedElements)
        {
            element.SetActive(true);
        }
        foreach (GameObject element in lockedElements)
        {
            element.SetActive(false);
        }
    }

    public void Deactivate()
    {
        isActive = false;
        UserManager.instance.ForceGrabberRelease(GetComponent<OVRGrabbable>());
        GetComponent<OVRGrabbable>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().material.SetColor("_Color", LevelManager.instance.disabledColor);
        foreach (GameObject element in unlockedElements)
        {
            element.SetActive(false);
        }
        foreach (GameObject element in lockedElements)
        {
            element.SetActive(true);
        }
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
                
                if(isOnePosition)
                {
                    if (transform.parent.name == "lever0001") LevelManager.instance.SetNumber(1, 0); //number bit 1 = 0
                    if (transform.parent.name == "lever0010") LevelManager.instance.SetNumber(2, 0); //number bit 2 = 0
                    if (transform.parent.name == "lever0100") LevelManager.instance.SetNumber(3, 0); //number bit 3 = 0
                                                                                            //if (transform.parent.name == "lever1000") levelManager.SetNumber(4, 0); //bit 4 = 0

                    if (transform.parent.name == "leverRed") LevelManager.instance.SetColor(4, 0); //color bit 1 = 0
                    if (transform.parent.name == "leverGreen") LevelManager.instance.SetColor(5, 0); //color bit 2 = 0
                    if (transform.parent.name == "leverBlue") LevelManager.instance.SetColor(6, 0); //color bit 3 = 0

                    if (transform.parent.name == "lever01") LevelManager.instance.SetShape(7, 0); //shape bit 1 = 0
                    if (transform.parent.name == "lever10") LevelManager.instance.SetShape(8, 0); //shape bit 2 = 0

                    if (transform.parent.name == "leverTutorial") LevelManager.instance.TutorialLeverPulled(false);

                }
                isOnePosition = false;
            }
            else if (transform.localPosition.z < onePosition.localPosition.z)
            {//went past one position, set to one position
                transform.localPosition = onePosition.localPosition;
                //update board total
                if(!isOnePosition)
                {
                    if (transform.parent.name == "lever0001") LevelManager.instance.SetNumber(1, 1); //number bit 1 = 1
                    if (transform.parent.name == "lever0010") LevelManager.instance.SetNumber(2, 1); //number bit 2 = 1
                    if (transform.parent.name == "lever0100") LevelManager.instance.SetNumber(3, 1); //number bit 3 = 1
                                                                                            //if (transform.parent.name == "lever1000") levelManager.SetNumber(4, 1); //bit 4 = 1

                    if (transform.parent.name == "leverRed") LevelManager.instance.SetColor(4, 1); //color bit 1 = 1
                    if (transform.parent.name == "leverGreen") LevelManager.instance.SetColor(5, 1); //color bit 2 = 1
                    if (transform.parent.name == "leverBlue") LevelManager.instance.SetColor(6, 1); //color bit 3 = 1

                    if (transform.parent.name == "lever01") LevelManager.instance.SetShape(7, 1); //shape bit 1 = 1
                    if (transform.parent.name == "lever10") LevelManager.instance.SetShape(8, 1); //shape bit 2 = 1

                    if (transform.parent.name == "leverGetTask") GetTaskLeverPulled();
                    if (transform.parent.name == "leverRunOutput") RunOutputLeverPulled();
                    if (transform.parent.name == "leverSendOutput") SendOutputLeverPulled();

                    if (transform.parent.name == "leverTutorial") LevelManager.instance.TutorialLeverPulled(true);
                }
                isOnePosition = true;
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
        LevelManager.instance.OutputSent("User pulled leverSendOutput to one position at ");
    }

    private void RunOutputLeverPulled()
    {
        Deactivate();
        LevelManager.instance.RunOutputButtonPushed("User pulled leverRunOutput to one position at ");
    }

    private void GetTaskLeverPulled()
    {
        Deactivate();
        LevelManager.instance.GetTaskLeverPulled("User pulled leverGetTask to one position at ");
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
