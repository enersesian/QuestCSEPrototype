using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PaintingInput : MonoBehaviour
{
    [SerializeField]
    [Range(0.3f, 2f)]
    private float distance = 1f;
    [SerializeField]
    [Range(0.1f, 0.3f)]
    private float distanceChange = 0.2f;
    [SerializeField]
    [Range(0.1f, 2f)]
    private float size = 1.0f;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float emissionStrength = 0.5f;
    [SerializeField]
    private GameObject spawnObjectPrefab;
    [SerializeField]
    private Transform rightHandAnchor, leftHandAnchor;

    private Color paintedObjectColor, paintedObjectEmission;
    private int shape = 0;
    private float opacityStrength = 0.5f;
    private int animationState = 0;
    private float animationSpeed = 1f;
    private GameObject primitive;
    private float red = 1.0f, green = 1.0f, blue = 1.0f, destroyTime = 3f, timeToDestroy = 3f;
    private Vector3 clickPosition;
    private bool timedDestroyIsOn = true, isAnimTypeRandom = true, isAnimSpeedRandom = true, isSpawnTypeRandom = true, isSpawnTimeRandom = true;
    private Vector3 lastClickPositionRight = Vector3.zero, lastClickPositionLeft = Vector3.zero;

    void Update ()
    {
        //user released right controller hand trigger so reset lastPosition to avoid large spawns at next paint drop
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) < 0.9f) lastClickPositionRight = Vector3.zero;

        //user released left controller hand trigger so reset lastPosition to avoid large spawns at next paint drop
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) < 0.9f) lastClickPositionLeft = Vector3.zero;

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) > 0.9f)
        {
            clickPosition = rightHandAnchor.forward * (distance + Random.Range(-distanceChange, distanceChange)) + rightHandAnchor.transform.position;
            SpawnPaintedObject(clickPosition, lastClickPositionRight);
            lastClickPositionRight = clickPosition;
        }

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) > 0.9f)
        {
            clickPosition = leftHandAnchor.forward * (distance + Random.Range(-distanceChange, distanceChange)) + leftHandAnchor.transform.position;
            SpawnPaintedObject(clickPosition, lastClickPositionLeft);
            lastClickPositionLeft = clickPosition;
        }
    }

    private void SpawnPaintedObject(Vector3 currentPosition, Vector3 lastPosition)
    {
        primitive = Instantiate(spawnObjectPrefab, clickPosition, Quaternion.identity);

        //the first spawned object of a paint stroke needs an initial randomized size as it doesnt have a previous spawned object to reference
        if (lastPosition == Vector3.zero) primitive.transform.localScale = new Vector3(Random.Range(0.1f, 0.2f) * size, Random.Range(0.1f, 0.2f) * size, Random.Range(0.1f, 0.2f) * size);
        //we can reference the previous spawned object to randomize size against
        else
        {
            float x = Mathf.Clamp(Random.Range(.01f, size) * Mathf.Abs(lastPosition.x - currentPosition.x), .01f, size * 3f);
            float y = Mathf.Clamp(Random.Range(.01f, size) * Mathf.Abs(lastPosition.y - currentPosition.y), .01f, size * 3f);
            float z = Mathf.Clamp(Random.Range(.01f, size) * Mathf.Abs(lastPosition.z - currentPosition.z), .01f, size * 3f);
            primitive.transform.localScale = new Vector3(x, y, z);
        }

        //prefabs are built to only have renderers on their children not root
        foreach (Transform child in primitive.transform)
        {
            if (child.gameObject.GetComponent<Renderer>() != null)
            {
                paintedObjectColor = new Color(Random.Range(0.0f, red), Random.Range(0.0f, green), Random.Range(0.0f, blue), opacityStrength);
                child.gameObject.GetComponent<Renderer>().material.color = paintedObjectColor;
                primitive.gameObject.GetComponent<PrefabData>().initialColorInfo.Add(paintedObjectColor);
                paintedObjectEmission = new Color(paintedObjectColor.r * emissionStrength, paintedObjectColor.g * emissionStrength, paintedObjectColor.b * emissionStrength);
                child.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", paintedObjectEmission);
            }
        }

        //prefabs are built to only have animators on their root
        if (primitive.GetComponent<Animator>() != null)
        {
            if (isAnimTypeRandom) animationState = (int)Random.Range(0f, 2.99f);
            primitive.GetComponent<Animator>().SetInteger("state", animationState);

            if (isAnimSpeedRandom) primitive.GetComponent<Animator>().speed = Random.Range(0f, animationSpeed);
            else primitive.GetComponent<Animator>().speed = animationSpeed;
            primitive.GetComponent<PrefabData>().initialAnimSpeed = primitive.GetComponent<Animator>().speed;
        }

        primitive.transform.parent = this.transform;
        if (timedDestroyIsOn)
        {
            if (isSpawnTimeRandom) Destroy(primitive, Random.Range(0f, timeToDestroy));
            else Destroy(primitive, timeToDestroy);
        }
    }
}
