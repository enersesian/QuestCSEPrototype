using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager instance;
    
    [SerializeField]
    [Range(0.01f, 0.03f)]
    [Tooltip("Move with right thumbstick")]
    private float movementSpeed;
    [SerializeField]
    private OVRGrabber leftHandGrabber, rightHandGrabber;

    private Vector2 thumbAxis;
    private float doubleTapTimer;
    private bool isNear;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update ()
    {
        if(Application.isEditor) //quick move feature for in editor rift testing
        {
            thumbAxis = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick) * movementSpeed;
            thumbAxis.x = -thumbAxis.x * Mathf.Cos(LevelManager.instance.centerEyeAnchor.eulerAngles.y * Mathf.Deg2Rad) +
                -thumbAxis.y * Mathf.Sin(LevelManager.instance.centerEyeAnchor.eulerAngles.y * Mathf.Deg2Rad);
            //thumbAxis.y = -thumbAxis.y * Mathf.Cos(LevelManager.instance.centerEyeAnchor.eulerAngles.y * Mathf.Deg2Rad) 
                 thumbAxis.y = thumbAxis.x * Mathf.Sin(LevelManager.instance.centerEyeAnchor.eulerAngles.y * Mathf.Deg2Rad);
            Debug.Log("X part of front movement = " + (thumbAxis.x * Mathf.Sin(LevelManager.instance.centerEyeAnchor.eulerAngles.y * Mathf.Deg2Rad)).ToString());
            Debug.Log("Y part of front movement = " + (thumbAxis.y * Mathf.Cos(LevelManager.instance.centerEyeAnchor.eulerAngles.y * Mathf.Deg2Rad)).ToString());
            LevelManager.instance.transform.position = new Vector3(LevelManager.instance.transform.position.x + thumbAxis.x * 0f,
                LevelManager.instance.transform.position.y, LevelManager.instance.transform.position.z + thumbAxis.y);
        }
        
        if (OVRInput.Get(OVRInput.Button.One) && OVRInput.Get(OVRInput.Button.Two))
        {
            if(OVRInput.GetDown(OVRInput.Button.Three)) //set user height manually
            {
                /* Disabled feature to change play area from 20'x20' to 12'x'12'
                levelManager.SetLevelDistance(isNear);
                isNear = !isNear;
                */

                if (Time.time - doubleTapTimer < 0.3f)
                {
                    LevelManager.instance.SetStationHeight();
                }
                doubleTapTimer = Time.time;
            }
            else if(OVRInput.GetDown(OVRInput.Button.Four))
            {
                if (Time.time - doubleTapTimer < 0.3f) //reset level to task 1
                {
                    LevelManager.instance.SetTask("start", 1);
                    //need to call resetCubeGrabbable if manually resetting level
                    LevelManager.instance.outputStation.GetTaskLeverPulled(1); 
                }
                doubleTapTimer = Time.time;
            }
        }
    }

    /// <summary>
    /// Called when user releases a lever as found interactions got sticky without this forced release
    /// </summary>
    public void ForceGrabberRelease(OVRGrabbable grabbable)
    {
        if (grabbable != null)
        {
            leftHandGrabber.ForceRelease(grabbable);
            rightHandGrabber.ForceRelease(grabbable);
        }
    }
}
