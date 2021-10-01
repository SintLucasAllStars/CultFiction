using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WalkerMode
{
	LolNo,
	DefinitionOfInsane,
	Indecisive
}

public class XMPLSplineWalker : MonoBehaviour
{
    public BezierSpline spline;
	public WalkerMode mode;
	public float duration;
	public bool isLooking;
	private float progress;
	private bool goingForward = true;
	

	private void Update()
	{
        if (goingForward)
        {
            progress += Time.deltaTime / duration;
            if (progress > 1f)
            {
                if (mode == WalkerMode.LolNo)
                {
                    progress = 1f;
                }
                else if (mode == WalkerMode.DefinitionOfInsane)
                {
                    progress -= 1f;
                }
                else
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }
        }
		else
		{
			progress -= Time.deltaTime / duration;
			if (progress < 0f)
			{
				progress = -progress;
				goingForward = true;
			}
		}

		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;
		if (isLooking)
		{
			transform.LookAt(position + spline.GetDirection(progress));

		}
	}
}
