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
    public Renderer leverGlow;

    private void Start()
    {
        Deactivate();

        distanceToLeverBase = Vector3.Distance(transform.position, leverBase.position);
        startPosition = transform.localPosition;
        leverBaseStartRotation = leverBase.rotation;
        if(numberWheel != null) numberWheelStartRotation = numberWheel.rotation;
        leverTopMin = zeroPosition.localPosition.z;
        leverTopMax = onePosition.localPosition.z;
        //leverBaseMin = zeroPosition.localRotation.x;
        //leverBaseMax = onePosition.localRotation.x;
        
        if (transform.parent.name == "lever01" || transform.parent.name == "lever10") //shape station
        {
            leverBaseMin = 270f;
            leverBaseMax = 240f;
        }
        else if (transform.parent.name == "leverTutorial") //tutorial station
        {
            leverBaseMin = -110f;
            leverBaseMax = -150f;
        }
        else if (transform.parent.name == "leverRunOutput") //output station
        {
            leverBaseMin = -120f;
            leverBaseMax = -160f;
        }
        else if(transform.parent.name == "leverGetTask" || transform.parent.name == "leverSendOutput") //task station
        {
            leverBaseMin = -80f;
            leverBaseMax = -120f;
        }
        else if(transform.parent.name == "leverRed" || transform.parent.name == "leverGreen" || transform.parent.name == "leverBlue") //color station
        {
            leverBaseMin = -120f;
            leverBaseMax = -160f;
        }
        else //number station
        {
            leverBaseMin = -80f;
            leverBaseMax = -120f;
        }
    }

    private void SetLeverToZeroPosition()
    {
        //zeroPosition.localPosition.z < onePosition.localPosition.z
        transform.localPosition = zeroPosition.localPosition;

        if (isOnePosition)
        {
            if (transform.parent.name == "lever0001") LevelManager.instance.SetNumber(1, 0); //number bit 1 = 0
            if (transform.parent.name == "lever0010") LevelManager.instance.SetNumber(2, 0); //number bit 2 = 0
            if (transform.parent.name == "lever0100") LevelManager.instance.SetNumber(3, 0); //number bit 3 = 0

            if (transform.parent.name == "leverRed") LevelManager.instance.SetColor(4, 0); //color bit 1 = 0
            if (transform.parent.name == "leverGreen") LevelManager.instance.SetColor(5, 0); //color bit 2 = 0
            if (transform.parent.name == "leverBlue") LevelManager.instance.SetColor(6, 0); //color bit 3 = 0

            if (transform.parent.name == "lever01") LevelManager.instance.SetShape(7, 0); //shape bit 1 = 0
            if (transform.parent.name == "lever10") LevelManager.instance.SetShape(8, 0); //shape bit 2 = 0

            if (transform.parent.name == "leverTutorial") LevelManager.instance.TutorialLeverPulled(false);

        }
        isOnePosition = false;
    }

    private void SetLeverToOnePosition()
    {
        //went past one position, set to one position
        transform.localPosition = onePosition.localPosition;
        //update board total
        if (!isOnePosition)
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

            if (transform.parent.name == "leverGetTask")
            {
                Deactivate();
                LevelManager.instance.GetTaskLeverPulled("User pulled leverGetTask to one position at ");
            }
            if (transform.parent.name == "leverRunOutput")
            {
                Deactivate();
                LevelManager.instance.RunOutputLeverPulled("User pulled leverRunOutput to one position at ");
            }
            if (transform.parent.name == "leverSendOutput")
            {
                Deactivate();
                LevelManager.instance.OutputSent("User pulled leverSendOutput to one position at ");
            }

            if (transform.parent.name == "leverTutorial") LevelManager.instance.TutorialLeverPulled(true);
        }
        isOnePosition = true;
    }

    private void Update()
    {
        if (isActive)
        {
            //Had to add additional check as now zeroPosition.z is not guarenteed to be greater than onePosition.z
            if(zeroPosition.localPosition.z > onePosition.localPosition.z)
            {
                if (transform.localPosition.z > zeroPosition.localPosition.z) SetLeverToZeroPosition();
                else if (transform.localPosition.z < onePosition.localPosition.z) SetLeverToOnePosition();
                else transform.localPosition = new Vector3(startPosition.x, leverTopYLocalPosition.localPosition.y, transform.localPosition.z);//Mathf.Clamp(transform.localPosition.y, 0.15f, 0.18f), transform.localPosition.z);
            }
            else 
            {
                if (transform.localPosition.z < zeroPosition.localPosition.z) SetLeverToZeroPosition();
                else if (transform.localPosition.z > onePosition.localPosition.z) SetLeverToOnePosition();
                else transform.localPosition = new Vector3(startPosition.x, leverTopYLocalPosition.localPosition.y, transform.localPosition.z);//Mathf.Clamp(transform.localPosition.y, 0.15f, 0.18f), transform.localPosition.z);
            }

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

    public void SetTask()
    {
        if (transform.parent.name == "leverGetTask")
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

    public void Activate()
    {
        Debug.Log(transform.parent.name.ToString() + " activated");
        isActive = true;
        UserManager.instance.ForceGrabberRelease(GetComponent<OVRGrabbable>());
        GetComponent<OVRGrabbable>().enabled = true;
        GetComponent<Collider>().enabled = true;
        leverGlow.material.SetColor("_EmissionColor", LevelManager.instance.activeColor);
        foreach (GameObject element in unlockedElements)
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
        Debug.Log(transform.parent.name.ToString() + " deactivated");
        isActive = false;
        UserManager.instance.ForceGrabberRelease(GetComponent<OVRGrabbable>());
        GetComponent<OVRGrabbable>().enabled = false;
        GetComponent<Collider>().enabled = false;
        leverGlow.material.SetColor("_EmissionColor", Color.red);//LevelManager.instance.disabledColor);
        foreach (GameObject element in unlockedElements)
        {
            element.SetActive(false);
        }
        foreach (GameObject element in lockedElements)
        {
            element.SetActive(true);
        }
    }

    public void SetToZeroPosition()
    {
        transform.localPosition = zeroPosition.localPosition;
        leverBase.localRotation = zeroPosition.localRotation;
        if (numberWheel != null) numberWheel.localRotation = Quaternion.Euler(numberWheelMin, numberWheel.localRotation.y, numberWheel.localRotation.z);
        isOnePosition = false;
    }

    public void SetToOnePosition()
    {
        transform.localPosition = onePosition.localPosition;
        leverBase.localRotation = onePosition.localRotation;
        if (numberWheel != null) numberWheel.localRotation = Quaternion.Euler(numberWheelMax, numberWheel.localRotation.y, numberWheel.localRotation.z);
        isOnePosition = true;
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
