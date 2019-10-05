using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager instance;

    private enum HandPreference { Right, Left}
    [SerializeField]
    [Tooltip("Pick hand for Rift quick move")]
    private HandPreference handPreference;
    [SerializeField]
    [Range(0.01f, 0.02f)]
    [Tooltip("Movement speed with thumbstick")]
    private float movementSpeed;
    [SerializeField]
    private OVRGrabber leftHandGrabber, rightHandGrabber;
    private Transform rightHandAnchor, leftHandAnchor;
    private Vector2 thumbAxis, localThumbAxis;
    private float doubleTapTimer, holdTriggerTimer;
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
        rightHandAnchor = rightHandGrabber.transform;
        leftHandAnchor = leftHandGrabber.transform;
    }

    /// <summary>
    /// Call this when user releases a lever as found interactions got sticky without this forced release
    /// </summary>
    public void ForceGrabberRelease(OVRGrabbable grabbable)
    {
        if (grabbable != null)
        {
            leftHandGrabber.ForceRelease(grabbable);
            rightHandGrabber.ForceRelease(grabbable);
        }
    }

    private void Update ()
    {
        //quick move feature for in editor rift testing
        if (Application.isEditor) 
        {
            if (handPreference == HandPreference.Right) QuickMove(OVRInput.Get(OVRInput.RawAxis2D.RThumbstick), rightHandAnchor.forward);
            else QuickMove(OVRInput.Get(OVRInput.RawAxis2D.LThumbstick), leftHandAnchor.forward);
        }

        //hold down all four buttons to reset station heights if user is not comfortable
        if (OVRInput.Get(OVRInput.Button.One) && OVRInput.Get(OVRInput.Button.Two) && OVRInput.Get(OVRInput.Button.Three) && OVRInput.Get(OVRInput.Button.Four))
        {
            LevelManager.instance.SetStationHeight(); 
        }

        //double tap commands for setting station height, resetting level, and moving stations closer
        /*
        if (OVRInput.Get(OVRInput.Button.One) && OVRInput.Get(OVRInput.Button.Two)) //user is holding down A and B on right controller
        {
            if(OVRInput.GetDown(OVRInput.Button.Three)) //User double tapped X on left controller to set user height manually
            {
                if (Time.time - doubleTapTimer < 0.3f)
                {
                    //LevelManager.instance.SetStationHeight();
                }
                doubleTapTimer = Time.time;
            }
            else if(OVRInput.GetDown(OVRInput.Button.Four)) //User doubled tapped Y on left controller to reset level to task 01
            {
                if (Time.time - doubleTapTimer < 0.3f) //reset level to task 1
                {
                    LevelManager.instance.SetTask("start", 1);
                    //need to call resetCubeGrabbable if manually resetting level
                    LevelManager.instance.outputStation.GetTaskLeverPulled(1); 
                }
                doubleTapTimer = Time.time;
            }
            else if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > 0.8f)
            {
                holdTriggerTimer += Time.deltaTime;
                if (holdTriggerTimer > 1.0f) //User holds down index trigger on left controller to change play area size
                {
                    //feature to change play area from 20'x20' to 12'x'12' and vice versa
                    LevelManager.instance.SetLevelDistance(isNear);
                    isNear = !isNear;
                    holdTriggerTimer = 0f;
                }
            }
        }
        */
    }

    private void QuickMove(Vector2 rawThumbstickAxis, Vector3 handAnchor)
    {
        thumbAxis = movementSpeed * rawThumbstickAxis;
        //Use the direction that the preferred hand is pointing to orient the direction to move the task stations around the user
        localThumbAxis.x = (-thumbAxis.x * handAnchor.z) + (-thumbAxis.y * handAnchor.x);
        localThumbAxis.y = (thumbAxis.x * handAnchor.x) + (-thumbAxis.y * handAnchor.z);
        LevelManager.instance.transform.position = new Vector3(LevelManager.instance.transform.position.x + localThumbAxis.x,
            LevelManager.instance.transform.position.y, LevelManager.instance.transform.position.z + localThumbAxis.y);
    }
}
