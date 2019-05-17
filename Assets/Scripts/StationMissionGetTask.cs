using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationMissionGetTask : MonoBehaviour
{
    private bool isTouched = false;
    public bool isPoppable = false;
    private float heightDiff;
    public Transform buttonUpPosition, buttonDownPosition;

    //collider isnt popping into the right place
    //color should pop into another color once you release not once its down

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) //Hand sphere
        {
            if(other.transform.position.y > buttonDownPosition.position.y)
            {
                isTouched = true;
                heightDiff = other.transform.position.y - transform.position.y;
                Debug.Log("button is touched" + transform.position.y.ToString());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9 && isTouched) //Hand sphere
        {
            if(transform.position.y < buttonDownPosition.position.y)
            {
                if(gameObject.name == "buttonGetTask")
                {
                    transform.root.GetComponent<StationManager>().GetTask();
                }
                if (gameObject.name == "buttonRunOutput")
                {
                    transform.root.GetComponent<StationManager>().RunOutput();
                }
                this.GetComponent<Renderer>().material.color = transform.root.GetComponent<StationManager>().hitColor;
                transform.position = buttonDownPosition.position;
                isTouched = false;
                GetComponent<BoxCollider>().center = new Vector3(0f, 1f, 0f);
                Debug.Log("button is down" + transform.position.y.ToString());
            }
            else if(transform.position.y > buttonUpPosition.position.y)
            {
                //button is at highest point, user is releasing button, dont move now so we trigger OnTriggerExit
                
                transform.position = buttonUpPosition.position;
                GetComponent<BoxCollider>().center = Vector3.zero;
                Debug.Log("button is up" + transform.position.y.ToString());
            }
            else
            {
                transform.position = new Vector3(transform.position.x, other.transform.position.y - heightDiff, transform.position.z);
                GetComponent<BoxCollider>().center = buttonUpPosition.position;
                Debug.Log("button is going down" + transform.position.y.ToString());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 && isPoppable) //Hand sphere && releasing button after not pushing it all the way down
        {
            transform.position = buttonUpPosition.position;
            this.GetComponent<Renderer>().material.color = transform.root.GetComponent<StationManager>().activeColor;
            isTouched = false;
            GetComponent<BoxCollider>().center = Vector3.zero;
            Debug.Log("button is released" + transform.position.y.ToString());
        }
    }


}
