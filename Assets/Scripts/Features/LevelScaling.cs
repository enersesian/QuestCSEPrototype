using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScaling : MonoBehaviour
{
    private Vector3 boundarySize;
    private float levelScale;
    public Animator wallAnimator;
    public Transform roomFeaturesTransform;
    private bool isLevelScaled;

    public void SetLevelScale ()
    {
        if (isLevelScaled)
        {
            levelScale = 1f;
            ScaleLevel();
            isLevelScaled = false;
        }
        else if (OVRManager.boundary.GetConfigured())
        {
            boundarySize = OVRManager.boundary.GetDimensions(OVRBoundary.BoundaryType.OuterBoundary);
            //scale down cylinder from 7.3 to minimum of x or z boundarySize * 2
            //so if the minimum of boundarySize is 2.2, then we need to scale from 7.3 to 4.4 on the room features which is from 1.0 to 0.6
            //1.0 size should be with a boundary size of 7.3/2 or 3.65, which is ~7m across or a 21'x21' play area
            //0.7 size should be with a boundary size of 7.3/2 or 2.2, which is ~13'x'13' play area
            if (boundarySize.x < boundarySize.z) levelScale = (boundarySize.x * 2f) / 7.3f;
            else levelScale = (boundarySize.z * 2f) / 7.3f;
            if (levelScale > 1f) levelScale = 1f;
            if (levelScale < 0.6f)
            {
                levelScale = 0.6f;
                //print warning to user that their boundary space is too small to play this experience
            }
            ScaleLevel();
            isLevelScaled = true;
        }
    }

    private void ScaleLevel()
    {
        //have to turn off animator before scaling or else walls wont scale
        wallAnimator.enabled = false;
        roomFeaturesTransform.localScale = new Vector3(levelScale, 1f, levelScale);
        wallAnimator.enabled = true;
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).position = new Vector3(roomFeaturesTransform.GetChild(i).position.x, transform.GetChild(i).position.y, roomFeaturesTransform.GetChild(i).position.z);
    }
	
}
