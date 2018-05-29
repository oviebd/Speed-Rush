using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

	 Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

    public void SetCameraTarget()
    {
     target= GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void MakeCameraTargetLost()
    {
        target = null;
    }
	void FixedUpdate ()
	{
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, target.localPosition.z) + offset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.localPosition, desiredPosition, smoothSpeed);
            transform.localPosition = smoothedPosition;
        }
		

		//transform.LookAt(target);
	}

}
