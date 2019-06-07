using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private Transform startTransform, endTransform;
    private float animTime, waitTime;
    private Renderer wallMaterial;
    private Color startDiffuse, startSpec, endDiffuse, endSpec;
    private float valueChange;

    public void DissolveWalls()
    {
        animTime = 3f;
        waitTime = 0f;
        if (transform.GetChild(0).gameObject.activeSelf) wallMaterial = transform.GetChild(0).GetComponent<Renderer>();
        else wallMaterial = transform.GetChild(1).GetComponent<Renderer>();
        startDiffuse = wallMaterial.material.GetColor("_Color");
        startSpec = wallMaterial.material.GetColor("_SpecColor");
        endDiffuse = new Color(startDiffuse.r, startDiffuse.g, startDiffuse.b, 0f);
        endSpec = new Color(0f, 0f, 0f, 0f);
        StartCoroutine("Dissolving");
    }

    private IEnumerator Dissolving()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        float elapsedTime = 0f;
        while (wallMaterial.material.GetColor("_Color").a - endDiffuse.a  > 0.01f)
        {
            valueChange = Mathf.Lerp(startDiffuse.a, endDiffuse.a, elapsedTime / animTime);
            wallMaterial.material.SetColor("_Color", new Color(startDiffuse.r, startDiffuse.g, startDiffuse.b, valueChange));
            wallMaterial.material.SetColor("_SpecColor", new Color(valueChange, valueChange, valueChange, valueChange));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
