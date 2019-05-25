using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStation : MonoBehaviour
{
    public LeverPulled leverTutorial;
    public Text instructionsTop, instructionsBottom, leverStatus;
    public Movement tutorialWalls;
    private float interactiveWaitTime = 5f;
    private int tutorialNumber;
    private LevelManager levelManager;
    private bool isInteractable, isTutorialLeverOn;

    void Start ()
    {
        levelManager = transform.root.GetComponent<LevelManager>();
	}

    public void SetTutorialLeverBool(bool leverState)
    {
        isTutorialLeverOn = leverState;
    }

    public void GetTaskLeverPulled(int currentTask)
    {
        //remove all tutorial objects
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
		if(isInteractable)
        {
            switch(tutorialNumber)
            {
                case 1:
                case 2:
                    if (OVRInput.Get(OVRInput.Button.One) || OVRInput.Get(OVRInput.Button.Two) || OVRInput.Get(OVRInput.Button.Three) || OVRInput.Get(OVRInput.Button.Four) ||
                        OVRInput.Get(OVRInput.RawButton.LIndexTrigger) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger) || OVRInput.Get(OVRInput.RawButton.LHandTrigger) || OVRInput.Get(OVRInput.RawButton.RHandTrigger))
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 3:
                case 4:
                    if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) || OVRInput.Get(OVRInput.RawButton.RHandTrigger))
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 5:
                    if(isTutorialLeverOn) //lever pulled to on
                    {
                        leverStatus.text = "ON";
                        StartTutorialNonInteractive();
                    }
                    break;

                case 6:
                    if (!isTutorialLeverOn) //lever pulled to off
                    {
                        leverStatus.text = "OFF";
                        StartTutorialNonInteractive();
                    }
                    break;

                default:
                    break;
            }
        }
	}

    public void StartTutorial()
    {
        StartTutorialNonInteractive();
    }

    public void StartTutorialNonInteractive()
    {
        tutorialNumber++;
        isInteractable = false;

        if (tutorialNumber == 1) instructionsTop.text = "This is a lesson on binary math, a computer science concept.";
        if (tutorialNumber == 2) instructionsTop.text = "You will be pulling levers up and down to represent binary math.";
        if (tutorialNumber == 3) instructionsTop.text = "You need to close your hand into a fist to pull a lever.";
        if (tutorialNumber == 4) instructionsTop.text = "You cannot pull a red lever because it is inactive.";
        if (tutorialNumber == 5) instructionsTop.text = "You can pull a green lever because it is active.";
        if (tutorialNumber == 6) instructionsTop.text = "In binary, 0 means off and 1 means on.";
        if (tutorialNumber == 7) instructionsTop.text = "In front of you is the task station, where you start and end tasks.";

        instructionsBottom.text = "";
        Invoke("StartTutorialInteractive", interactiveWaitTime);
    }

    private void StartTutorialInteractive()
    {
        isInteractable = true;

        if (tutorialNumber == 1) instructionsBottom.text = "Press any key to continue...";
        if (tutorialNumber == 2) instructionsBottom.text = "Press any key to continue...";
        if (tutorialNumber == 3) instructionsBottom.text = "Squeeze either middle finger to close a hand to continue...";
        if (tutorialNumber == 4) instructionsBottom.text = "Close either hand to continue...";
        if (tutorialNumber == 5)
        {
            //activate lever
            leverTutorial.Activate();
            instructionsBottom.text = "Pull lever down into its on position to continue...";
        }
        if (tutorialNumber == 6)
        {
            //user pulled lever down to on position
            instructionsBottom.text = "Pull lever up into its off position to continue...";
        }
        if (tutorialNumber == 7)
        {
            //user pulled lever up to off position
            //deactive Lever
            leverTutorial.Deactivate();
            //walls go up, revealing the stations
            tutorialWalls.Move();
            //activate Get Task button on task station
            instructionsBottom.text = "Walk to the task station and press the Get Task button.";
            levelManager.StartLevel();
        }
    }

    public void TutorialLeverPulledDown()
    {
        //user pulled lever down to on position
    }

    public void TutorialLeverPulledUp()
    {
        //user pulled lever up to off position
    }

    private void FinishTutorial()
    {
        //user pushed Get Task button on task station
        //remove tutorial sign, lever and walls
    }
}
