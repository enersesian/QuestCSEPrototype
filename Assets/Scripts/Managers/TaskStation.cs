using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskStation : MonoBehaviour
{
    public Text taskText, taskTextLarge;
    public ButtonPushed getButton, sendOutputButton;
    public LeverPulled leverGetTask, leverSendOutput;
    public Transform spawnLocation, miniSpawnLocation, farLocation, nearLocation;
    public Sprite cubeIcon, sphereIcon, coneIcon, ringIcon;
    public GameObject[] objectIcons;
    public GameObject[] objectIconsLarge;
    private GameObject objectSpawn;
    private Vector3 objectPosition;
    private LevelManager levelManager;

    private void Start()
    {
        //if (Application.isEditor) transform.position = new Vector3(0f, 0.2f, 0.6f); //Sitting Rift position
        levelManager = transform.root.GetComponent<LevelManager>();
    }

    public void SetLevelDistance(bool isNear)
    {
        if (isNear) transform.position = farLocation.position;  //near to far
        else transform.position = nearLocation.position;
    }

    public void SetTask(string condition)
    {
        if (condition == "start")
        {
            taskText.text = "Waiting for new task...\nPress Get Task button.";
            //taskTextLarge.text = "Waiting for new task...\nPress Get Task button.";
            sendOutputButton.ResetButton(false); //only reset at beginning as pushing this button will execute SetTask in game and can cause double execution
        }
        if(condition == "success") taskText.text = "You performed the task perfectly! \nPress Get Task button for next task.";
        if(condition == "failure") taskText.text = "Incorrect Output! \nPress Get Task button to try again.";
        //getButton.ResetButton(true);
        leverGetTask.SetTask();
        //leverSendOutput.SetTask();
        foreach (GameObject objectIcon in objectIconsLarge)
        {
            objectIcon.SetActive(false);
        }
        foreach (GameObject objectIcon in objectIcons)
        {
            objectIcon.SetActive(false);
        }
    }

    public void LevelComplete()
    {
        taskText.text = "You win! Awesome job! Now get out of here!";
        //taskTextLarge.text = "You win!\nAwesome job!";
    }

    public void GetTaskLeverPulled(int currentTask)
    {
        //sendOutputButton.ResetButton(false);
        //leverSendOutput.SetTask();

        if (currentTask == 1)
        {
            taskText.text = "Welcome to Task 1.\nBring back 1 Red Sphere.";
            //taskTextLarge.text = "Task One \n 1 Red Sphere.";
            /*
            objectSpawn = Instantiate(levelManager.outputStation.sphere);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f);
            objectSpawn.transform.localScale *= 5f;
            */

            objectIconsLarge[0].SetActive(true);
            objectIconsLarge[0].transform.position = new Vector3(
                spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[0].GetComponent<Image>().sprite = sphereIcon;
            objectIconsLarge[0].GetComponent<Image>().color = new Color(1f, 0f, 0f);

            //mini

            objectIcons[0].SetActive(true);
            objectIcons[0].transform.position = new Vector3(
                miniSpawnLocation.position.x, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[0].GetComponent<Image>().sprite = sphereIcon;
            objectIcons[0].GetComponent<Image>().color = new Color(1f, 0f, 0f);
        }

        if (currentTask == 2)
        {
            taskText.text = "Welcome to Task 2.\nBring back 2 Red Spheres.";
            //taskTextLarge.text = "Task One \n 2 Red Spheres.";
            /*
            objectSpawn = Instantiate(levelManager.outputStation.sphere);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x -0.1f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f);
            objectSpawn.transform.localScale *= 5f;

            objectSpawn = Instantiate(levelManager.outputStation.sphere);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.1f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f);
            objectSpawn.transform.localScale *= 5f;
            */
            objectIconsLarge[0].SetActive(true);
            objectIconsLarge[0].transform.position = new Vector3(
                spawnLocation.position.x + 0.1f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[0].GetComponent<Image>().sprite = sphereIcon;
            objectIconsLarge[0].GetComponent<Image>().color = new Color(1f, 0f, 0f);

            objectIconsLarge[1].SetActive(true);
            objectIconsLarge[1].transform.position = new Vector3(
                spawnLocation.position.x - 0.1f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[1].GetComponent<Image>().sprite = sphereIcon;
            objectIconsLarge[1].GetComponent<Image>().color = new Color(1f, 0f, 0f);

            //mini

            objectIcons[0].SetActive(true);
            objectIcons[0].transform.position = new Vector3(
                miniSpawnLocation.position.x + 0.05f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[0].GetComponent<Image>().sprite = sphereIcon;
            objectIcons[0].GetComponent<Image>().color = new Color(1f, 0f, 0f);

            objectIcons[1].SetActive(true);
            objectIcons[1].transform.position = new Vector3(
                miniSpawnLocation.position.x - 0.05f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[1].GetComponent<Image>().sprite = sphereIcon;
            objectIcons[1].GetComponent<Image>().color = new Color(1f, 0f, 0f);
        }

        if (currentTask == 3)
        {
            taskText.text = "Welcome to Task 3.\nBring back 4 Green Cubes.";
            //taskTextLarge.text = "Task One \n 4 Green Cubes.";
            /*
            objectSpawn = Instantiate(levelManager.outputStation.cube);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.1f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;

            objectSpawn = Instantiate(levelManager.outputStation.cube);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.1f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;

            objectSpawn = Instantiate(levelManager.outputStation.cube);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.3f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;

            objectSpawn = Instantiate(levelManager.outputStation.cube);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.3f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;
            */

            objectIconsLarge[0].SetActive(true);
            objectIconsLarge[0].transform.position = new Vector3(
                spawnLocation.position.x + 0.24f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[0].GetComponent<Image>().sprite = cubeIcon;
            objectIconsLarge[0].GetComponent<Image>().color = new Color(0f, 1f, 0f);

            objectIconsLarge[1].SetActive(true);
            objectIconsLarge[1].transform.position = new Vector3(
                spawnLocation.position.x - 0.24f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[1].GetComponent<Image>().sprite = cubeIcon;
            objectIconsLarge[1].GetComponent<Image>().color = new Color(0f, 1f, 0f);

            objectIconsLarge[2].SetActive(true);
            objectIconsLarge[2].transform.position = new Vector3(
                spawnLocation.position.x + 0.08f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[2].GetComponent<Image>().sprite = cubeIcon;
            objectIconsLarge[2].GetComponent<Image>().color = new Color(0f, 1f, 0f);

            objectIconsLarge[3].SetActive(true);
            objectIconsLarge[3].transform.position = new Vector3(
                spawnLocation.position.x - 0.08f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[3].GetComponent<Image>().sprite = cubeIcon;
            objectIconsLarge[3].GetComponent<Image>().color = new Color(0f, 1f, 0f);

            //mini

            objectIcons[0].SetActive(true);
            objectIcons[0].transform.position = new Vector3(
                miniSpawnLocation.position.x + 0.12f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[0].GetComponent<Image>().sprite = cubeIcon;
            objectIcons[0].GetComponent<Image>().color = new Color(0f, 1f, 0f);

            objectIcons[1].SetActive(true);
            objectIcons[1].transform.position = new Vector3(
                miniSpawnLocation.position.x - 0.12f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[1].GetComponent<Image>().sprite = cubeIcon;
            objectIcons[1].GetComponent<Image>().color = new Color(0f, 1f, 0f);

            objectIcons[2].SetActive(true);
            objectIcons[2].transform.position = new Vector3(
                miniSpawnLocation.position.x + 0.04f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[2].GetComponent<Image>().sprite = cubeIcon;
            objectIcons[2].GetComponent<Image>().color = new Color(0f, 1f, 0f);

            objectIcons[3].SetActive(true);
            objectIcons[3].transform.position = new Vector3(
                miniSpawnLocation.position.x - 0.04f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[3].GetComponent<Image>().sprite = cubeIcon;
            objectIcons[3].GetComponent<Image>().color = new Color(0f, 1f, 0f);
        }

        if (currentTask == 4)
        {
            taskText.text = "Welcome to Task 4.\nBring back 3 Blue Cones.";
            //taskTextLarge.text = "Task One \n 3 Blue Cones.";
            /*
            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;
            */
            objectIconsLarge[0].SetActive(true);
            objectIconsLarge[0].transform.position = new Vector3(
                spawnLocation.position.x + 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[0].GetComponent<Image>().sprite = coneIcon;
            objectIconsLarge[0].GetComponent<Image>().color = new Color(0f, 0f, 1f);

            objectIconsLarge[1].SetActive(true);
            objectIconsLarge[1].transform.position = new Vector3(
                spawnLocation.position.x - 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[1].GetComponent<Image>().sprite = coneIcon;
            objectIconsLarge[1].GetComponent<Image>().color = new Color(0f, 0f, 1f);

            objectIconsLarge[2].SetActive(true);
            objectIconsLarge[2].transform.position = new Vector3(
                spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[2].GetComponent<Image>().sprite = coneIcon;
            objectIconsLarge[2].GetComponent<Image>().color = new Color(0f, 0f, 1f);

            //mini

            objectIcons[0].SetActive(true);
            objectIcons[0].transform.position = new Vector3(
                miniSpawnLocation.position.x + 0.1f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[0].GetComponent<Image>().sprite = coneIcon;
            objectIcons[0].GetComponent<Image>().color = new Color(0f, 0f, 1f);

            objectIcons[1].SetActive(true);
            objectIcons[1].transform.position = new Vector3(
                miniSpawnLocation.position.x - 0.1f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[1].GetComponent<Image>().sprite = coneIcon;
            objectIcons[1].GetComponent<Image>().color = new Color(0f, 0f, 1f);

            objectIcons[2].SetActive(true);
            objectIcons[2].transform.position = new Vector3(
                miniSpawnLocation.position.x, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[2].GetComponent<Image>().sprite = coneIcon;
            objectIcons[2].GetComponent<Image>().color = new Color(0f, 0f, 1f);
        }

        if (currentTask == 5)
        {
            taskText.text = "Welcome to Task 5.\nBring back 5 Yellow Ring.";
            //taskTextLarge.text = "Task One \n 5 Yellow Ring.";
            /*
            objectSpawn = Instantiate(levelManager.outputStation.ring);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.1f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.ring);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.1f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            //second row

            objectSpawn = Instantiate(levelManager.outputStation.ring);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.ring);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.2f, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.ring);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.2f, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            */

            objectIconsLarge[3].SetActive(true);
            objectIconsLarge[3].transform.position = new Vector3(
                spawnLocation.position.x + 0.1f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[3].GetComponent<Image>().sprite = ringIcon;
            objectIconsLarge[3].GetComponent<Image>().color = new Color(1f, 1f, 0f);

            objectIconsLarge[4].SetActive(true);
            objectIconsLarge[4].transform.position = new Vector3(
                spawnLocation.position.x - 0.1f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[4].GetComponent<Image>().sprite = ringIcon;
            objectIconsLarge[4].GetComponent<Image>().color = new Color(1f, 1f, 0f);

            //second row

            objectIconsLarge[0].SetActive(true);
            objectIconsLarge[0].transform.position = new Vector3(
                spawnLocation.position.x + 0.2f, spawnLocation.position.y - 0.7f, spawnLocation.position.z);
            objectIconsLarge[0].GetComponent<Image>().sprite = ringIcon;
            objectIconsLarge[0].GetComponent<Image>().color = new Color(1f, 1f, 0f);

            objectIconsLarge[1].SetActive(true);
            objectIconsLarge[1].transform.position = new Vector3(
                spawnLocation.position.x - 0.2f, spawnLocation.position.y - 0.7f, spawnLocation.position.z);
            objectIconsLarge[1].GetComponent<Image>().sprite = ringIcon;
            objectIconsLarge[1].GetComponent<Image>().color = new Color(1f, 1f, 0f);

            objectIconsLarge[2].SetActive(true);
            objectIconsLarge[2].transform.position = new Vector3(
                spawnLocation.position.x, spawnLocation.position.y - 0.7f, spawnLocation.position.z);
            objectIconsLarge[2].GetComponent<Image>().sprite = ringIcon;
            objectIconsLarge[2].GetComponent<Image>().color = new Color(1f, 1f, 0f);

            //mini

            objectIcons[3].SetActive(true);
            objectIcons[3].transform.position = new Vector3(
                miniSpawnLocation.position.x + 0.05f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[3].GetComponent<Image>().sprite = ringIcon;
            objectIcons[3].GetComponent<Image>().color = new Color(1f, 1f, 0f);

            objectIcons[4].SetActive(true);
            objectIcons[4].transform.position = new Vector3(
                miniSpawnLocation.position.x - 0.05f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[4].GetComponent<Image>().sprite = ringIcon;
            objectIcons[4].GetComponent<Image>().color = new Color(1f, 1f, 0f);

            //second row

            objectIcons[0].SetActive(true);
            objectIcons[0].transform.position = new Vector3(
                miniSpawnLocation.position.x + 0.1f, miniSpawnLocation.position.y - 0.7f, miniSpawnLocation.position.z);
            objectIcons[0].GetComponent<Image>().sprite = ringIcon;
            objectIcons[0].GetComponent<Image>().color = new Color(1f, 1f, 0f);

            objectIcons[1].SetActive(true);
            objectIcons[1].transform.position = new Vector3(
                miniSpawnLocation.position.x - 0.1f, miniSpawnLocation.position.y - 0.7f, miniSpawnLocation.position.z);
            objectIcons[1].GetComponent<Image>().sprite = ringIcon;
            objectIcons[1].GetComponent<Image>().color = new Color(1f, 1f, 0f);

            objectIcons[2].SetActive(true);
            objectIcons[2].transform.position = new Vector3(
                miniSpawnLocation.position.x, miniSpawnLocation.position.y - 0.7f, miniSpawnLocation.position.z);
            objectIcons[2].GetComponent<Image>().sprite = ringIcon;
            objectIcons[2].GetComponent<Image>().color = new Color(1f, 1f, 0f);
        }

        if (currentTask == 6)
        {
            taskText.text = "Welcome to Task 6.\nBring back 7 Cyan Cones.";
            //taskTextLarge.text = "Task One \n 7 Cyan Cones.";
            /*
            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 5f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 5f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 5f;

            //second row

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.1f, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 5f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.1f, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 5f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.3f, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 5f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.3f, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 5f;
            */

            objectIconsLarge[0].SetActive(true);
            objectIconsLarge[0].transform.position = new Vector3(
                spawnLocation.position.x + 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[0].GetComponent<Image>().sprite = coneIcon;
            objectIconsLarge[0].GetComponent<Image>().color = new Color(0f, 1f, 1f);

            objectIconsLarge[1].SetActive(true);
            objectIconsLarge[1].transform.position = new Vector3(
                spawnLocation.position.x - 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[1].GetComponent<Image>().sprite = coneIcon;
            objectIconsLarge[1].GetComponent<Image>().color = new Color(0f, 1f, 1f);

            objectIconsLarge[2].SetActive(true);
            objectIconsLarge[2].transform.position = new Vector3(
                spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[2].GetComponent<Image>().sprite = coneIcon;
            objectIconsLarge[2].GetComponent<Image>().color = new Color(0f, 1f, 1f);

            //second row

            objectIconsLarge[3].SetActive(true);
            objectIconsLarge[3].transform.position = new Vector3(
                spawnLocation.position.x + 0.24f, spawnLocation.position.y - 0.7f, spawnLocation.position.z);
            objectIconsLarge[3].GetComponent<Image>().sprite = coneIcon;
            objectIconsLarge[3].GetComponent<Image>().color = new Color(0f, 1f, 1f);

            objectIconsLarge[4].SetActive(true);
            objectIconsLarge[4].transform.position = new Vector3(
                spawnLocation.position.x - 0.24f, spawnLocation.position.y - 0.7f, spawnLocation.position.z);
            objectIconsLarge[4].GetComponent<Image>().sprite = coneIcon;
            objectIconsLarge[4].GetComponent<Image>().color = new Color(0f, 1f, 1f);

            objectIconsLarge[5].SetActive(true);
            objectIconsLarge[5].transform.position = new Vector3(
                spawnLocation.position.x + 0.08f, spawnLocation.position.y - 0.7f, spawnLocation.position.z);
            objectIconsLarge[5].GetComponent<Image>().sprite = coneIcon;
            objectIconsLarge[5].GetComponent<Image>().color = new Color(0f, 1f, 1f);

            objectIconsLarge[6].SetActive(true);
            objectIconsLarge[6].transform.position = new Vector3(
                spawnLocation.position.x - 0.08f, spawnLocation.position.y - 0.7f, spawnLocation.position.z);
            objectIconsLarge[6].GetComponent<Image>().sprite = coneIcon;
            objectIconsLarge[6].GetComponent<Image>().color = new Color(0f, 1f, 1f);

            //mini

            objectIcons[0].SetActive(true);
            objectIcons[0].transform.position = new Vector3(
                miniSpawnLocation.position.x + 0.1f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[0].GetComponent<Image>().sprite = coneIcon;
            objectIcons[0].GetComponent<Image>().color = new Color(0f, 1f, 1f);

            objectIcons[1].SetActive(true);
            objectIcons[1].transform.position = new Vector3(
                miniSpawnLocation.position.x - 0.1f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[1].GetComponent<Image>().sprite = coneIcon;
            objectIcons[1].GetComponent<Image>().color = new Color(0f, 1f, 1f);

            objectIcons[2].SetActive(true);
            objectIcons[2].transform.position = new Vector3(
                miniSpawnLocation.position.x, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[2].GetComponent<Image>().sprite = coneIcon;
            objectIcons[2].GetComponent<Image>().color = new Color(0f, 1f, 1f);

            //second row

            objectIcons[3].SetActive(true);
            objectIcons[3].transform.position = new Vector3(
                miniSpawnLocation.position.x + 0.12f, miniSpawnLocation.position.y - 0.7f, miniSpawnLocation.position.z);
            objectIcons[3].GetComponent<Image>().sprite = coneIcon;
            objectIcons[3].GetComponent<Image>().color = new Color(0f, 1f, 1f);

            objectIcons[4].SetActive(true);
            objectIcons[4].transform.position = new Vector3(
                miniSpawnLocation.position.x - 0.12f, miniSpawnLocation.position.y - 0.7f, miniSpawnLocation.position.z);
            objectIcons[4].GetComponent<Image>().sprite = coneIcon;
            objectIcons[4].GetComponent<Image>().color = new Color(0f, 1f, 1f);

            objectIcons[5].SetActive(true);
            objectIcons[5].transform.position = new Vector3(
                miniSpawnLocation.position.x + 0.04f, miniSpawnLocation.position.y - 0.7f, miniSpawnLocation.position.z);
            objectIcons[5].GetComponent<Image>().sprite = coneIcon;
            objectIcons[5].GetComponent<Image>().color = new Color(0f, 1f, 1f);

            objectIcons[6].SetActive(true);
            objectIcons[6].transform.position = new Vector3(
                miniSpawnLocation.position.x - 0.04f, miniSpawnLocation.position.y - 0.7f, miniSpawnLocation.position.z);
            objectIcons[6].GetComponent<Image>().sprite = coneIcon;
            objectIcons[6].GetComponent<Image>().color = new Color(0f, 1f, 1f);
        }

        if (currentTask == 7)
        {
            taskText.text = "Welcome to Task 7.\nBring back 6 Purple Ring.";
            //taskTextLarge.text = "Task One \n 6 Purple Ring.";
            /*
            objectSpawn = Instantiate(levelManager.outputStation.ring);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.ring);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.ring);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            //second row

            objectSpawn = Instantiate(levelManager.outputStation.ring);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.ring);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.2f, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.ring);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.2f, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            */

            objectIconsLarge[0].SetActive(true);
            objectIconsLarge[0].transform.position = new Vector3(
                spawnLocation.position.x + 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[0].GetComponent<Image>().sprite = ringIcon;
            objectIconsLarge[0].GetComponent<Image>().color = new Color(1f, 0f, 1f);

            objectIconsLarge[1].SetActive(true);
            objectIconsLarge[1].transform.position = new Vector3(
                spawnLocation.position.x - 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[1].GetComponent<Image>().sprite = ringIcon;
            objectIconsLarge[1].GetComponent<Image>().color = new Color(1f, 0f, 1f);

            objectIconsLarge[2].SetActive(true);
            objectIconsLarge[2].transform.position = new Vector3(
                spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
            objectIconsLarge[2].GetComponent<Image>().sprite = ringIcon;
            objectIconsLarge[2].GetComponent<Image>().color = new Color(1f, 0f, 1f);

            //second row

            objectIconsLarge[3].SetActive(true);
            objectIconsLarge[3].transform.position = new Vector3(
                spawnLocation.position.x + 0.2f, spawnLocation.position.y - 0.7f, spawnLocation.position.z);
            objectIconsLarge[3].GetComponent<Image>().sprite = ringIcon;
            objectIconsLarge[3].GetComponent<Image>().color = new Color(1f, 0f, 1f);

            objectIconsLarge[4].SetActive(true);
            objectIconsLarge[4].transform.position = new Vector3(
                spawnLocation.position.x - 0.2f, spawnLocation.position.y - 0.7f, spawnLocation.position.z);
            objectIconsLarge[4].GetComponent<Image>().sprite = ringIcon;
            objectIconsLarge[4].GetComponent<Image>().color = new Color(1f, 0f, 1f);

            objectIconsLarge[5].SetActive(true);
            objectIconsLarge[5].transform.position = new Vector3(
                spawnLocation.position.x, spawnLocation.position.y - 0.7f, spawnLocation.position.z);
            objectIconsLarge[5].GetComponent<Image>().sprite = ringIcon;
            objectIconsLarge[5].GetComponent<Image>().color = new Color(1f, 0f, 1f);

            //mini

            objectIcons[0].SetActive(true);
            objectIcons[0].transform.position = new Vector3(
                miniSpawnLocation.position.x + 0.1f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[0].GetComponent<Image>().sprite = ringIcon;
            objectIcons[0].GetComponent<Image>().color = new Color(1f, 0f, 1f);

            objectIcons[1].SetActive(true);
            objectIcons[1].transform.position = new Vector3(
                miniSpawnLocation.position.x - 0.1f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[1].GetComponent<Image>().sprite = ringIcon;
            objectIcons[1].GetComponent<Image>().color = new Color(1f, 0f, 1f);

            objectIcons[2].SetActive(true);
            objectIcons[2].transform.position = new Vector3(
                miniSpawnLocation.position.x, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectIcons[2].GetComponent<Image>().sprite = ringIcon;
            objectIcons[2].GetComponent<Image>().color = new Color(1f, 0f, 1f);

            //second row

            objectIcons[3].SetActive(true);
            objectIcons[3].transform.position = new Vector3(
                miniSpawnLocation.position.x + 0.1f, miniSpawnLocation.position.y - 0.7f, miniSpawnLocation.position.z);
            objectIcons[3].GetComponent<Image>().sprite = ringIcon;
            objectIcons[3].GetComponent<Image>().color = new Color(1f, 0f, 1f);

            objectIcons[4].SetActive(true);
            objectIcons[4].transform.position = new Vector3(
                miniSpawnLocation.position.x - 0.1f, miniSpawnLocation.position.y - 0.7f, miniSpawnLocation.position.z);
            objectIcons[4].GetComponent<Image>().sprite = ringIcon;
            objectIcons[4].GetComponent<Image>().color = new Color(1f, 0f, 1f);

            objectIcons[5].SetActive(true);
            objectIcons[5].transform.position = new Vector3(
                miniSpawnLocation.position.x, miniSpawnLocation.position.y - 0.7f, miniSpawnLocation.position.z);
            objectIcons[5].GetComponent<Image>().sprite = ringIcon;
            objectIcons[5].GetComponent<Image>().color = new Color(1f, 0f, 1f);
        }
    }

    public void RunOutput()
    {
        //sendOutputButton.ResetButton(true);
        leverSendOutput.SetToZeroPosition();
    }

    public void PlacedOutput()
    {
        //sendOutputButton.ResetButton(true);
        leverSendOutput.Activate();
    }

    public void RemovedOutput()
    {
        //sendOutputButton.ResetButton(false);
        leverSendOutput.Deactivate();
    }
}
