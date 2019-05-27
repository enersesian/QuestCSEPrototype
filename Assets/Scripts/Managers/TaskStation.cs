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
        if(condition == "success") taskText.text = "You performed the task perfectly! \nPress Get Task button to receive next task.";
        if(condition == "failure") taskText.text = "You did not performed the task correctly. \nPress Get Task button to try the task again.";
        //getButton.ResetButton(true);
        leverGetTask.SetTask();
        //leverSendOutput.SetTask();
        foreach (Transform child in spawnLocation)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in miniSpawnLocation)
        {
            Destroy(child.gameObject);
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

            objectSpawn = Instantiate(levelManager.outputStation.sphere);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f);
            objectSpawn.transform.localScale *= 5f;

            //mini

            objectSpawn = Instantiate(levelManager.outputStation.sphere);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f);
            objectSpawn.transform.localScale *= 2f;
        }

        if (currentTask == 2)
        {
            taskText.text = "Welcome to Task 2.\nBring back 2 Red Spheres.";
            //taskTextLarge.text = "Task One \n 2 Red Spheres.";

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

            //mini

            objectSpawn = Instantiate(levelManager.outputStation.sphere);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x - 0.05f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f);
            objectSpawn.transform.localScale *= 2f;

            objectSpawn = Instantiate(levelManager.outputStation.sphere);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x + 0.05f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f);
            objectSpawn.transform.localScale *= 2f;
        }

        if (currentTask == 3)
        {
            taskText.text = "Welcome to Task 3.\nBring back 4 Green Cubes.";
            //taskTextLarge.text = "Task One \n 4 Green Cubes.";

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

            //mini

            objectSpawn = Instantiate(levelManager.outputStation.cube);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x - 0.05f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f);
            objectSpawn.transform.localScale *= 2f;

            objectSpawn = Instantiate(levelManager.outputStation.cube);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x + 0.05f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f);
            objectSpawn.transform.localScale *= 2f;

            objectSpawn = Instantiate(levelManager.outputStation.cube);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x - 0.15f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f);
            objectSpawn.transform.localScale *= 2f;

            objectSpawn = Instantiate(levelManager.outputStation.cube);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x + 0.15f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f);
            objectSpawn.transform.localScale *= 2f;
        }

        if (currentTask == 4)
        {
            taskText.text = "Welcome to Task 4.\nBring back 3 Blue Cones.";
            //taskTextLarge.text = "Task One \n 3 Blue Cones.";

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

            //mini

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 0f, 1f);
            objectSpawn.transform.localScale *= 2f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x + 0.1f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 0f, 1f);
            objectSpawn.transform.localScale *= 2f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x - 0.1f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 0f, 1f);
            objectSpawn.transform.localScale *= 2f;
        }

        if (currentTask == 5)
        {
            taskText.text = "Welcome to Task 5.\nBring back 5 Yellow Torus.";
            //taskTextLarge.text = "Task One \n 5 Yellow Torus.";

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.1f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.1f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            //second row

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.2f, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.2f, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            //mini

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x - 0.05f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 2f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x + 0.05f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 2f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            //second row

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x, miniSpawnLocation.position.y - 0.06f, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 2f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x + 0.1f, miniSpawnLocation.position.y - 0.06f, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 2f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x - 0.1f, miniSpawnLocation.position.y - 0.06f, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
            objectSpawn.transform.localScale *= 2f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        }

        if (currentTask == 6)
        {
            taskText.text = "Welcome to Task 6.\nBring back 7 Cyan Cones.";
            //taskTextLarge.text = "Task One \n 7 Cyan Cones.";

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

            //mini

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 2f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x + 0.1f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 2f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x - 0.1f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 2f;

            //second row

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x - 0.05f, miniSpawnLocation.position.y - 0.06f, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 2f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x + 0.05f, miniSpawnLocation.position.y - 0.06f, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 2f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x - 0.15f, miniSpawnLocation.position.y - 0.06f, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 2f;

            objectSpawn = Instantiate(levelManager.outputStation.cone);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x + 0.15f, miniSpawnLocation.position.y - 0.06f, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
            objectSpawn.transform.localScale *= 2f;
        }

        if (currentTask == 7)
        {
            taskText.text = "Welcome to Task 7.\nBring back 6 Purple Torus.";
            //taskTextLarge.text = "Task One \n 6 Purple Torus.";

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.2f, spawnLocation.position.y, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            //second row

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x + 0.2f, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = spawnLocation;
            objectPosition = new Vector3(spawnLocation.position.x - 0.2f, spawnLocation.position.y - 0.15f, spawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 5f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            //mini

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 2f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x + 0.1f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 2f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x - 0.1f, miniSpawnLocation.position.y, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 2f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            //second row

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x, miniSpawnLocation.position.y - 0.06f, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 2f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x + 0.1f, miniSpawnLocation.position.y - 0.06f, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 2f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            objectSpawn = Instantiate(levelManager.outputStation.diamond);
            objectSpawn.transform.parent = miniSpawnLocation;
            objectPosition = new Vector3(miniSpawnLocation.position.x - 0.1f, miniSpawnLocation.position.y - 0.06f, miniSpawnLocation.position.z);
            objectSpawn.transform.position = objectPosition;
            objectSpawn.GetComponent<RandomMovement>().enabled = false;
            objectSpawn.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
            objectSpawn.transform.localScale *= 2f;
            objectSpawn.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
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
