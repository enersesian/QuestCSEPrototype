using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPushed : MonoBehaviour
{
    private bool isTouched = false, isPushable = true;
    public bool isPoppable = false;
    private float heightDiff, startY, startZ;
    public Transform buttonUpPosition, buttonDownPosition;

    //collider isnt popping into the right place
    //color should pop into another color once you release not once its down

    public void ResetButton(bool active)
    {
        transform.position = buttonUpPosition.position;
        GetComponent<BoxCollider>().center = Vector3.zero;
        isTouched = false;

        if (active)
        {
            this.GetComponent<Renderer>().material.color = transform.root.GetComponent<LevelManager>().activeColor;
            isPushable = true;
        }
        else
        {
            this.GetComponent<Renderer>().material.color = transform.root.GetComponent<LevelManager>().disabledColor;
            isPushable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && isPushable) //Hand sphere
        {
            if(other.transform.position.y > buttonDownPosition.position.y)
            {
                isTouched = true;
                heightDiff = other.transform.position.y - transform.position.y;
                startY = other.transform.position.y;
                startZ = transform.localPosition.z;
                Debug.Log("button is touched " + transform.localPosition.z.ToString() + " " + buttonDownPosition.localPosition.z.ToString());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9 && isTouched) //Hand sphere
        {
            if(transform.localPosition.z > buttonDownPosition.localPosition.z)
            {
                if(gameObject.name == "buttonGetTask")
                {
                    transform.root.GetComponent<LevelManager>().GetTaskButtonPushed();
                }
                if (gameObject.name == "buttonRunOutput")
                {
                    transform.root.GetComponent<LevelManager>().RunOutputButtonPushed();
                }
                if (gameObject.name == "buttonSendOutput")
                {
                    transform.root.GetComponent<LevelManager>().OutputSent();
                }
                this.GetComponent<Renderer>().material.color = transform.root.GetComponent<LevelManager>().hitColor;
                transform.position = buttonDownPosition.position;
                isTouched = false;
                isPushable = false;
                //GetComponent<BoxCollider>().center = new Vector3(0f, 1f, 0f);
                Debug.Log("button is down " + transform.localPosition.z.ToString() + " " + buttonDownPosition.localPosition.z.ToString());
            }
            else if(transform.localPosition.z < buttonUpPosition.localPosition.z)
            {
                //button is at highest point, user is releasing button, dont move now so we trigger OnTriggerExit
                
                transform.position = buttonUpPosition.position;
                GetComponent<BoxCollider>().center = Vector3.zero;
                Debug.Log("button is up " + transform.position.y.ToString());
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, startZ + 5.5f*(startY - other.transform.position.y));
                //GetComponent<BoxCollider>().center = buttonUpPosition.localPosition;
                Debug.Log("button is going down" + transform.position.y.ToString());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 && isPoppable) //Hand sphere && releasing button after not pushing it all the way down
        {
            ResetButton(true);
        }
    }
}
