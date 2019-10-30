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
    private bool isNear, shouldRotateLeft = true, shouldRotateRight = true;

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
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        //quick move feature for both rift and quest builds at request of xrlibraries
        if (handPreference == HandPreference.Right)
        {
            MoveWithController(OVRInput.Get(OVRInput.RawAxis2D.RThumbstick), rightHandAnchor.forward);
            RotateWithController(OVRInput.Get(OVRInput.RawAxis2D.LThumbstick), rightHandAnchor.forward);
        }
        else
        {
            MoveWithController(OVRInput.Get(OVRInput.RawAxis2D.LThumbstick), rightHandAnchor.forward);
            RotateWithController(OVRInput.Get(OVRInput.RawAxis2D.RThumbstick), rightHandAnchor.forward);
        }

        //hold down all four buttons to reset station heights if user is not comfortable
        if ((OVRInput.Get(OVRInput.Button.One) && OVRInput.Get(OVRInput.Button.Two)) || (OVRInput.Get(OVRInput.Button.Three) && OVRInput.Get(OVRInput.Button.Four)))
        {
            //LevelManager.instance.SetStationHeight(); 
        }

        if (OVRInput.GetDown(OVRInput.Button.One) && OVRInput.Get(OVRInput.Button.Two) && OVRInput.Get(OVRInput.Button.Three) && OVRInput.Get(OVRInput.Button.Four))
        {
            //LevelManager.instance.SetLevelScale();
        }

    }

    private void MoveWithController(Vector2 rawThumbstickAxis, Vector3 handAnchor)
    {
        thumbAxis = movementSpeed * rawThumbstickAxis;
        //Use the direction that the preferred hand is pointing to orient the direction to move the task stations around the user
        localThumbAxis.x = (-thumbAxis.x * handAnchor.z) + (-thumbAxis.y * handAnchor.x);
        localThumbAxis.y = (thumbAxis.x * handAnchor.x) + (-thumbAxis.y * handAnchor.z);
        transform.position = new Vector3(transform.position.x - localThumbAxis.x, transform.position.y, transform.position.z - localThumbAxis.y);
        //LevelManager.instance.transform.position = new Vector3(LevelManager.instance.transform.position.x + localThumbAxis.x,
            //LevelManager.instance.transform.position.y, LevelManager.instance.transform.position.z + localThumbAxis.y);
    }

    private void RotateWithController(Vector2 rawThumbstickAxis, Vector3 handAnchor)
    {
        if (rawThumbstickAxis.x > -.9f && !shouldRotateLeft) shouldRotateLeft = true;
        if (rawThumbstickAxis.x < .9f && !shouldRotateRight) shouldRotateRight = true;

        if (rawThumbstickAxis.x < -.9f && shouldRotateLeft)
        {
            transform.Rotate(0f, -15f, 0f);
            shouldRotateLeft = false;
        }

        if (rawThumbstickAxis.x > .9f && shouldRotateRight)
        {
            transform.Rotate(0f, 15f, 0f);
            shouldRotateRight = false;
        }
    }
}
