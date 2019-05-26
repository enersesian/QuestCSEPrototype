using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStation : MonoBehaviour
{
    public LeverPulled leverTutorial;
    public Text instructionsTop, instructionsBottom, leverStatus;
    public Movement tutorialWalls, tutorialDisplay;
    public Transform[] movementTransforms;
    private float interactiveWaitTime = 5f;
    private int tutorialNumber;
    private LevelManager levelManager;
    private bool isInteractable, isTutorialLeverOn, isGetTaskLeverPulled, isTutorialAtNumberStation, isTutorialNumberLeverPulled, 
        isTutorialColorLeverPulled, isTutorialAtColorStation, isTutorialAtShapeStation, isTutorialShapeLeverPulled, 
        isTutorialAtOutputStation, isTutorialOutputLeverPulled, isTutorialContainerPickedUp, isTutorialAtTaskStation, 
        isTutorialContainerPlacedOutput, isTutorialOutputSent;

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
        isGetTaskLeverPulled = true;
    }

    public void TutorialNumberLeverIsPulled()
    {
        isTutorialNumberLeverPulled = true;
    }

    public void TutorialColorLeverIsPulled()
    {
        isTutorialColorLeverPulled = true;
    }

    public void TutorialShapeLeverIsPulled()
    {
        isTutorialShapeLeverPulled = true;
    }

    public void TutorialRunOutputLeverIsPulled()
    {
        isTutorialOutputLeverPulled = true;
    }

    public void TutorialContainerPickedUp()
    {
        isTutorialContainerPickedUp = true;
    }

    public void TutorialContainerPlacedOutput()
    {
        isTutorialContainerPlacedOutput = true;
    }

    public void TutorialOutputSent()
    {
        isTutorialOutputSent = true;
    }

    private void TutorialAtNumberStation()
    {
        isTutorialAtNumberStation = true;
    }

    private void TutorialAtColorStation()
    {
        isTutorialAtColorStation = true;
    }

    private void TutorialAtShapeStation()
    {
        isTutorialAtShapeStation = true;
    }

    private void TutorialAtOutputStation()
    {
        isTutorialAtOutputStation = true;
    }

    private void TutorialAtTaskStation()
    {
        isTutorialAtTaskStation = true;
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

                case 7:
                    if(isGetTaskLeverPulled)
                    {
                        //remove tutorial walls and lever
                        tutorialWalls.gameObject.SetActive(false);
                        leverTutorial.transform.parent.parent.parent.gameObject.SetActive(false);
                        //move tutorial sign to number station
                        StartTutorialNonInteractive();
                    }
                    break;

                case 8:
                    if (isTutorialAtNumberStation)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 9:
                    if(isTutorialNumberLeverPulled)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 10:
                    if (isTutorialAtColorStation)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 11:
                    if (isTutorialColorLeverPulled)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 12:
                    if (isTutorialAtShapeStation)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 13:
                    if (isTutorialShapeLeverPulled)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 14:
                    if (isTutorialAtOutputStation)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 15:
                    if (isTutorialOutputLeverPulled)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 16:
                    if (isTutorialContainerPickedUp)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 17:
                    if (isTutorialAtTaskStation)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 18:
                    if (isTutorialContainerPlacedOutput)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 19:
                    if (isTutorialOutputSent)
                    {
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

    private void EndTutorial()
    {
        tutorialDisplay.gameObject.SetActive(false);
    }

    public void StartTutorialNonInteractive()
    {
        tutorialNumber++;
        isInteractable = false;

        if (tutorialNumber == 1) instructionsTop.text = "Welcome to C-Spresso! Our ship got damaged in a storm. Could you help us fix our ship? ";
        if (tutorialNumber == 2) instructionsTop.text = "Great! First, let me get you familiar with the ship’s features. Look at your hands. See how you have fingers that you can wiggle around?";
        if (tutorialNumber == 3) instructionsTop.text = "You need to close your hand into a fist to pull a lever.";
        if (tutorialNumber == 4) instructionsTop.text = "You cannot pull a red lever because it is inactive."; 
        if (tutorialNumber == 5) instructionsTop.text = "You can pull a green lever because it is active.";
        if (tutorialNumber == 6) instructionsTop.text = "In binary, 0 means off and 1 means on.";
        if (tutorialNumber == 7) instructionsTop.text = "In front of you is the task station, where you start and end tasks.";
        if (tutorialNumber == 8) instructionsTop.text = "You must return to the task station with 1 Red Sphere.";
        if (tutorialNumber == 9) instructionsTop.text = "This is the number station, where you set the number of objects.";
        if (tutorialNumber == 10) instructionsTop.text = "Follow me to the color station to set the color of objects.";
        if (tutorialNumber == 11) instructionsTop.text = "This is the color station, where you set the color of objects.";
        if (tutorialNumber == 12) instructionsTop.text = "Follow me to the shape station  to set the shape of objects.";
        if (tutorialNumber == 13) instructionsTop.text = "This is the shape station, where you set the shape of objects.";
        if (tutorialNumber == 14) instructionsTop.text = "Follow me to the output station to generate output for the task.";
        if (tutorialNumber == 15) instructionsTop.text = "This is the output station, where you generate output for the task.";
        if (tutorialNumber == 16) instructionsTop.text = "Grab the output container and carry it to the task station.";
        if (tutorialNumber == 17) instructionsTop.text = "Follow me to the task station to send output for the task.";
        if (tutorialNumber == 18) instructionsTop.text = "Place the output container on the Place Output location.";
        if (tutorialNumber == 19) instructionsTop.text = "Pull the Send Output lever to finish the task.";
        if (tutorialNumber == 20) instructionsTop.text = "Congrats! Now proceed with task 2 by yourself.";

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
            tutorialWalls.Move(movementTransforms[0], movementTransforms[1], 8f, 0f);
            //activate Get Task button on task station
            instructionsBottom.text = "Walk to the task station and press the Get Task button.";
            tutorialDisplay.Move(movementTransforms[2], movementTransforms[3], 8f, 4f);
            levelManager.StartLevel();
        }
        if (tutorialNumber == 8)
        {
            instructionsBottom.text = "Follow me to the number station to start your task.";
            tutorialDisplay.Move(movementTransforms[3], movementTransforms[4], 8f, 4f);
            Invoke("TutorialAtNumberStation", 13f);
        }
        if (tutorialNumber == 9) instructionsBottom.text = "Pull lever labeled \"1\" down to its on position to set output to 1.";
        if (tutorialNumber == 10)
        {
            tutorialDisplay.Move(movementTransforms[4], movementTransforms[5], 8f, 0f);
            Invoke("TutorialAtColorStation", 9f);
        }
        if (tutorialNumber == 11) instructionsBottom.text = "Pull lever labeled \"Red\" down to its on position to set output to Red.";
        if (tutorialNumber == 12)
        {
            tutorialDisplay.Move(movementTransforms[5], movementTransforms[6], 8f, 0f);
            Invoke("TutorialAtShapeStation", 9f);
        }
        if (tutorialNumber == 13) instructionsBottom.text = "Pull the right lever down to its on position to set output to Sphere.";
        if (tutorialNumber == 14)
        {
            tutorialDisplay.Move(movementTransforms[6], movementTransforms[7], 8f, 0f);
            Invoke("TutorialAtOutputStation", 9f);
        }
        if (tutorialNumber == 15) instructionsBottom.text = "Pull the Run Output lever to generate output.";
        if (tutorialNumber == 16) instructionsBottom.text = "Close your hand on container and keep hand closed to carry container.";
        if (tutorialNumber == 17)
        {
            tutorialDisplay.Move(movementTransforms[7], movementTransforms[8], 4f, 0f);
            tutorialDisplay.Move(movementTransforms[8], movementTransforms[9], 4f, 5f);
            Invoke("TutorialAtTaskStation", 9f);
        }
        if (tutorialNumber == 20) EndTutorial();
    }

    private void FinishTutorial()
    {
        //user pushed Get Task button on task station
        //remove tutorial sign, lever and walls
    }
}
