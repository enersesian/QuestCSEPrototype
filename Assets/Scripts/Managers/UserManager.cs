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
    [SerializeField]
    private Transform rightHandAnchor;
    private Vector2 thumbAxis;
    private float doubleTapTimer, holdTriggerTimer;
    private bool isNear;
    public Animator tutorialWalls;

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
            //Use the direction that the right hand is pointing to orient the direction to move the task stations around the user
            thumbAxis.x = (thumbAxis.x * rightHandAnchor.forward.z) + (thumbAxis.y * rightHandAnchor.forward.x);
            thumbAxis.y = (thumbAxis.x * rightHandAnchor.forward.x) + (thumbAxis.y * rightHandAnchor.forward.z);
            LevelManager.instance.transform.position = new Vector3(LevelManager.instance.transform.position.x - thumbAxis.x,
                LevelManager.instance.transform.position.y, LevelManager.instance.transform.position.z - thumbAxis.y);
        }
        
        if (OVRInput.Get(OVRInput.Button.One) && OVRInput.Get(OVRInput.Button.Two)) //user is holding down A and B on right controller
        {
            tutorialWalls.SetTrigger("MoveDown");
            if(OVRInput.GetDown(OVRInput.Button.Three)) //User double tapped X on left controller to set user height manually
            {
                if (Time.time - doubleTapTimer < 0.3f)
                {
                    LevelManager.instance.SetStationHeight();
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
    }

    /// <summary>
    /// Call when user releases a lever as found interactions got sticky without this forced release
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
