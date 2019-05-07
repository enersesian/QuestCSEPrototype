using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationNumberLeverControl : MonoBehaviour
{
    public Transform leverBase, numberWheel;
    private float localRotationX, distanceToLeverBase;
    const float leverTopMin = 0.09f, leverTopMax = -0.05f, leverBaseMin = -60f, leverBaseMax = -120f, numberWheelMin = -70f, numberWheelMax = -160f;
    public StationNumber stationNumber;

    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        distanceToLeverBase = Vector3.Distance(transform.position, leverBase.position);
        transform.localPosition = new Vector3(0f, 0.15f, 0.09f);
    }

    // Update is called once per frame
    void Update ()
    {
        if (transform.localPosition.z > 0.091f)
        {
            transform.localPosition = new Vector3(0f, 0.15f, 0.09f);
            //update board total with a zero
            stationNumber.UpdateBitText(1, 0);
        }
        else if (transform.localPosition.z < -0.05f)
        {
            transform.localPosition = new Vector3(0f, 0.15f, -0.05f);
            //update board total with a one
            stationNumber.UpdateBitText(1, 1);
        }
        else transform.localPosition = new Vector3(0f, Mathf.Clamp(transform.localPosition.y, 0.15f, 0.18f), transform.localPosition.z);
        
        //rebuild lever system off center rotation of 0 so its easy to find angle hence use cosine to find where to fix y position of levelTop

        localRotationX = Scale(leverTopMin, leverTopMax, leverBaseMin, leverBaseMax, transform.localPosition.z);
        leverBase.localRotation = Quaternion.Euler(localRotationX, leverBase.localRotation.y, leverBase.localRotation.z);

        localRotationX = Scale(leverTopMin, leverTopMax, numberWheelMin, numberWheelMax, transform.localPosition.z);
        numberWheel.localRotation = Quaternion.Euler(localRotationX, numberWheel.localRotation.y, numberWheel.localRotation.z);
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
