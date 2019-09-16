using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStation : MonoBehaviour
{
    [SerializeField]
    private Text instructionsTop, instructionsBottom, tutorialLeverStatus;
    [SerializeField]
    private LeverPulled leverTutorial;
    [SerializeField]
    private Movement tutorialDisplay;
    [SerializeField]
    private Animator tutorialWalls, speechBubble, eggy;
    [SerializeField]
    private Transform[] movementTransforms;
    public GameObject highFiveCollider;

    private float interactiveWaitTime, eggyWaitForWallDrop = 25f, eggyMoveTimeToTaskStation = 3f;
    private int tutorialNumber = -1;
    private bool isInteractable, isTutorialLeverOn, isGetTaskLeverPulled, isTutorialAtNumberStation, isTutorialNumberLeverPulled, 
        isTutorialColorLeverPulled, isTutorialAtColorStation, isTutorialAtShapeStation, isTutorialShapeLeverPulled, 
        isTutorialAtOutputStation, isTutorialOutputLeverPulled, isTutorialContainerPickedUp, isTutorialAtTaskStation, 
        isTutorialContainerPlacedOutput, isTutorialOutputSent;

    public bool isEggyTouched;

    public void SetLevelDistance(bool isNear) //Sets distance of tutorial walls based on 20'x20' or 12'x12' space
    {
        if (isNear) //near to far
        {
            tutorialWalls.transform.GetChild(0).gameObject.SetActive(true); //far
            tutorialWalls.transform.GetChild(1).gameObject.SetActive(false); //near
        }
        else //far to near
        {
            tutorialWalls.transform.GetChild(0).gameObject.SetActive(false); //far
            tutorialWalls.transform.GetChild(1).gameObject.SetActive(true); //near
        }
    }

    //Main tutorial loop methods

    private void Start () //Ensure tutorial lever is disabled when starting
    {
        leverTutorial.Deactivate();
        if (Application.isEditor) eggyWaitForWallDrop = 5f;
        tutorialWalls.transform.position = new Vector3(0f, -0.22f, 0f);
	}

    private void CountdownTimer()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        float duration = eggyWaitForWallDrop;
        float totalTime = 0;
        while (totalTime < duration)
        {
            instructionsBottom.text = (duration - totalTime).ToString("F0");
            totalTime += Time.deltaTime;
            yield return null;
        }
    }

    public void StartTutorialNonInteractive() //Starts next tutorial step and updates top noninteractive part of instructions
    {
        tutorialNumber++;
        isInteractable = false;

        if (tutorialNumber == 0) instructionsTop.text = "Welcome to C-Spresso! Our ship got damaged in a storm. Could you help us fix our ship? ";
        if (tutorialNumber == 1) instructionsTop.text = "Now squeeze your middle finger to close your hand. This is how you will pull levers and grab objects.";//"Great! First, let me get you familiar with the ship’s features. Look at your hands. See how you have fingers that you can wiggle around?";
        if (tutorialNumber == 2) instructionsTop.text = "Great! Let's get you comfortable with closing your hands by letting you paint all around you!";
        if (tutorialNumber == 3) instructionsTop.text = "When you are done painting, lets teach you how to interact with levers by closing your hand and pulling on them.";
        if (tutorialNumber == 4) instructionsTop.text = "When the ball on the lever is black, it means it's locked. Since the storm, many of the levers are locked and I need help to unlock them.";
        if (tutorialNumber == 5) instructionsTop.text = "Notice the ball on the lever is white now. This means it is unlocked and you can pull it into its on position.";
        if (tutorialNumber == 6) instructionsTop.text = "Great! Let's return the lever back to its off position so that we can enter the main chamber.";
        if (tutorialNumber == 7) instructionsTop.text = "You're doing great! Wait for the walls to drop, then follow me to the task station.";
        if (tutorialNumber == 8) instructionsTop.text = "You have been given your first task to get one red sphere!";
        if (tutorialNumber == 9) instructionsTop.text = "This is the number station, where you set the number of objects.";
        if (tutorialNumber == 10) instructionsTop.text = "Great job! Follow me to the shape station.";
        if (tutorialNumber == 11) instructionsTop.text = "This is the shape station, where you set the shape of objects.";
        if (tutorialNumber == 12) instructionsTop.text = "Great job! Follow me to the color station.";
        if (tutorialNumber == 13) instructionsTop.text = "This is the color station, where you set the color of objects.";
        if (tutorialNumber == 14) instructionsTop.text = "Great job! Follow me to the output station.";
        if (tutorialNumber == 15) instructionsTop.text = "This is the output station, where you generate output for the task.";
        if (tutorialNumber == 16) instructionsTop.text = "Grab the output container and carry it to the task station.";
        if (tutorialNumber == 17) instructionsTop.text = "Great job! Follow me to the task station.";
        if (tutorialNumber == 18) instructionsTop.text = "Place the output container on the Place Output location.";
        if (tutorialNumber == 19) instructionsTop.text = "Great job! Pull the Send Output lever to finish the task.";
        if (tutorialNumber == 20) instructionsTop.text = "Congratulations! Now proceed through the rest of the tasks by yourself.";

        instructionsBottom.text = "";
        if (tutorialNumber < 7) interactiveWaitTime = 1f;
        else if (tutorialNumber == 7)
        {
            CountdownTimer();
            interactiveWaitTime = eggyWaitForWallDrop + eggyMoveTimeToTaskStation;
        }
        else if (tutorialNumber == 8) interactiveWaitTime = 3f;
        else if (tutorialNumber == 10 || tutorialNumber == 12 || tutorialNumber == 14) interactiveWaitTime = 1f;
        else if (tutorialNumber == 17) interactiveWaitTime = 1f;
        else interactiveWaitTime = 3f;
        
        Invoke("StartTutorialInteractive", interactiveWaitTime);
    }

    private void StartTutorialInteractive() //Updates bottom interactive part of instructions per tutorial step
    {
        isInteractable = true;

        if (tutorialNumber == 0)
        {
            instructionsBottom.text = "Give me a high five to continue...";
            highFiveCollider.SetActive(true);
            eggy.SetTrigger("highFiveWaiting");
        }
        if (tutorialNumber == 1) instructionsBottom.text = "Squeeze either middle finger to close a hand to continue..."; //"Press any of your fingers to continue...";
        if (tutorialNumber == 2)
        {
            instructionsBottom.text = "Close either hand to paint all around you...";
            TurnOnPaintingFeature();
        }
        if (tutorialNumber == 3)
        {
            instructionsBottom.text = "Give me a high five when you are done painting and want to continue...";
            highFiveCollider.SetActive(true);
            eggy.SetTrigger("highFiveWaiting");
        }
        if (tutorialNumber == 4) instructionsBottom.text = "Close either hand to continue...";
        if (tutorialNumber == 5)
        {

            instructionsBottom.text = "Pull lever down into its on position to continue...";
        }
        if (tutorialNumber == 6)
        {
            //user pulled lever down to on position
            instructionsBottom.text = "Pull lever back into its off position to continue...";
        }
        if (tutorialNumber == 7)
        {
            //move tutorial to task station

            //activate Get Task button on task station
            instructionsBottom.text = "This is where you start and end tasks.\nPull the Get Task lever to get your first task.";

            //levelManager.StartLevel();
        }
        if (tutorialNumber == 8)
        {
            instructionsBottom.text = "Follow me to the number station to start your task.";
            tutorialDisplay.Move(movementTransforms[3], movementTransforms[4], 2f, 3f);
            Invoke("CloseSpeechBubble", 1f);
            Invoke("EggyMove", 2f);
            Invoke("TutorialAtNumberStation", 5f);
        }
        if (tutorialNumber == 9) instructionsBottom.text = "Pull lever labeled \"1\" down to its on position to set output to 1.";

        if (tutorialNumber == 10)
        {
            tutorialDisplay.Move(movementTransforms[4], movementTransforms[5], 0f, 4f);
            Invoke("TutorialAtShapeStation", 4f);
        }
        if (tutorialNumber == 11) instructionsBottom.text = "Pull the right lever down to its on position to set output to Sphere.";

        if (tutorialNumber == 12)
        {
            tutorialDisplay.Move(movementTransforms[5], movementTransforms[6], 0f, 4f);
            Invoke("TutorialAtColorStation", 4f);
        }
        if (tutorialNumber == 13) instructionsBottom.text = "Pull lever labeled \"4\" down to its on position to set output to Red.";

        if (tutorialNumber == 14)
        {
            tutorialDisplay.Move(movementTransforms[5], movementTransforms[7], 0f, 4f);
            Invoke("TutorialAtOutputStation", 4f);
        }
        if (tutorialNumber == 15) instructionsBottom.text = "Pull the Run Output lever to generate output.";
        if (tutorialNumber == 16) instructionsBottom.text = "Close your hand on container and keep hand closed to carry container.";
        if (tutorialNumber == 17)
        {
            tutorialDisplay.Move(movementTransforms[7], movementTransforms[3], 0f, 4f);
            Invoke("TutorialAtTaskStation", 4f);
        }
        if (tutorialNumber == 20) EndTutorial();
    }

    private void Update() //Checks if user is done with interactive part of tutorial to move to next instruction
    {
        if (isInteractable)
        {
            switch (tutorialNumber)
            {
                case 0:
                    if(isEggyTouched)
                    {
                        highFiveCollider.SetActive(false);
                        eggy.SetTrigger("highFiveHit");
                        StartTutorialNonInteractive();
                        LevelManager.instance.SetStationHeight();
                    }
                    break;

                case 1:
                    //if (OVRInput.Get(OVRInput.Button.One) || OVRInput.Get(OVRInput.Button.Two) || OVRInput.Get(OVRInput.Button.Three) || OVRInput.Get(OVRInput.Button.Four) ||
                    //OVRInput.Get(OVRInput.RawButton.LIndexTrigger) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger) || OVRInput.Get(OVRInput.RawButton.LHandTrigger) || OVRInput.Get(OVRInput.RawButton.RHandTrigger))
                    if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) || OVRInput.Get(OVRInput.RawButton.RHandTrigger))
                    {
                        StartTutorialNonInteractive();
                        LevelManager.instance.SetStationHeight();
                        
                        //Invoke("TurnOnPaintingFeature", 2f); //wait to prevent painting before user reads prompt
                        //GetComponent<PaintingInput>().enabled = true; //turn on painting feature
                    }
                    break;

                case 2:
                    if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) || OVRInput.Get(OVRInput.RawButton.RHandTrigger))
                    {
                        StartTutorialNonInteractive();
                        isEggyTouched = false; //reset bool for case 3 testing
                    }
                    break;

                case 3:
                    if (isEggyTouched)
                    {
                        highFiveCollider.SetActive(false);
                        eggy.SetTrigger("highFiveHit");
                        StartTutorialNonInteractive();
                        GetComponent<PaintingInput>().enabled = false; //turn off painting feature
                    }
                    break;

                case 4:
                    if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) || OVRInput.Get(OVRInput.RawButton.RHandTrigger))
                    {
                        //activate lever
                        leverTutorial.Activate();
                        StartTutorialNonInteractive();
                    }
                    break;

                case 5:
                    if (isTutorialLeverOn) //lever pulled to on
                    {
                        tutorialLeverStatus.text = "ON";

                        StartTutorialNonInteractive();
                    }
                    break;

                case 6:
                    if (!isTutorialLeverOn) //lever pulled to off
                    {
                        tutorialLeverStatus.text = "OFF";

                        //user pulled lever up to off position
                        //deactive Lever
                        leverTutorial.Deactivate();

                        //walls go up, revealing the stations 
                        //tutorialWalls.Move(movementTransforms[0], movementTransforms[1], 8f, 0f);
                        tutorialWalls.SetTrigger("MoveDown");
                        tutorialDisplay.Move(movementTransforms[2], movementTransforms[3], eggyWaitForWallDrop, eggyMoveTimeToTaskStation);
                        Invoke("CloseSpeechBubble", eggyWaitForWallDrop - 1f);
                        Invoke("EggyMove", eggyWaitForWallDrop);
                        Invoke("StartTask01", eggyWaitForWallDrop + eggyMoveTimeToTaskStation);
                        StartTutorialNonInteractive();
                    }
                    break;

                case 7:
                    if (isGetTaskLeverPulled)
                    {
                        //remove tutorial walls and lever
                        //tutorialWalls.gameObject.SetActive(false);
                        leverTutorial.transform.parent.parent.parent.gameObject.SetActive(false);


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
                    if (isTutorialNumberLeverPulled)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 10:
                    if (isTutorialAtShapeStation)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 11:
                    if (isTutorialShapeLeverPulled)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 12:
                    if (isTutorialAtColorStation)
                    {
                        StartTutorialNonInteractive();
                    }
                    break;

                case 13:
                    if (isTutorialColorLeverPulled)
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
                        //Invoke("StartTutorialNonInteractive", 1f); //wait one second as a triggerEnter event sets this off not a grab event
                        //isTutorialContainerPickedUp = false; //to stop repeat executions and breaking tutorial
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

    //

    public void SetTutorialLeverBool(bool leverState)
    {
        isTutorialLeverOn = leverState;

        if (leverState && !Application.isEditor)
        {
            HCInvestigatorManager.instance.WriteTextData(0, "User pulled tutorial lever at " + DateTime.Now.ToString("hh:mm:ss"));
        }
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

    private void StartTask01()
    {
        LevelManager.instance.SetTask("start", 1);
        speechBubble.SetTrigger("open");
        //eggy.SetTrigger("floating");
    }

    private void TutorialAtNumberStation()
    {
        isTutorialAtNumberStation = true;
        speechBubble.SetTrigger("open");
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



    private void EndTutorial()
    {
        tutorialDisplay.gameObject.SetActive(false);
    }

    private void FinishTutorial()
    {
        //user pushed Get Task button on task station
        //remove tutorial sign, lever and walls
    }

    private void TurnOnPaintingFeature()
    {
        GetComponent<PaintingInput>().enabled = true; //turn on painting feature
    }

    private void CloseSpeechBubble()
    {
        speechBubble.SetTrigger("close");
    }

    private void EggyMove()
    {
        eggy.SetTrigger("moving");
    }
}
