using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 startPosition, endPosition;

    public float animTime = 8f;

    // Use this for initialization
    void Start ()
    {
        startPosition = new Vector3(2.4f, 0f, 0.75f);
        endPosition = new Vector3(2.4f, 4f, 0.75f);
    }

    public void Move()
    {
        StartCoroutine("Moving");
    }

    private IEnumerator Moving()
    {
        float elapsedTime = 0f;
        while (Vector3.Distance(transform.position, endPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / animTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
