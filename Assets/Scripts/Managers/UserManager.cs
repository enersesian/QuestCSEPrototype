using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    /// <summary>
    /// Singleton variable
    /// </summary>
    public static UserManager instance;

    [SerializeField]
    private float movementSpeed = 0.01f;
    [SerializeField]
    private OVRGrabber leftHandGrabber, rightHandGrabber;

    private Vector2 leftThumbAxis;
    private float doubleTapTimer;
    private bool isNear;

    /// <summary>
    /// Used for initialization before the game starts
    /// </summary>
    void Awake()
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

    void Update ()
    {
        if(Application.isEditor) //quick move feature for in editor rift testing
        {
            leftThumbAxis = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick) * movementSpeed;
            LevelManager.instance.transform.position = new Vector3(LevelManager.instance.transform.position.x + leftThumbAxis.x,
                LevelManager.instance.transform.position.y, LevelManager.instance.transform.position.z + leftThumbAxis.y);
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

    public void ForceGrabberRelease(OVRGrabbable grabbable)
    {
        if (grabbable != null)
        {
            leftHandGrabber.ForceRelease(grabbable);
            rightHandGrabber.ForceRelease(grabbable);
        }
    }
}
