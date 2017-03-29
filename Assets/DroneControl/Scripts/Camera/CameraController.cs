using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        Debug.Log("Before target: " + target.transform.localEulerAngles + ", Before self: " + transform.localEulerAngles);

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = target.rotation;
        }

        Debug.Log("After target: " + target.transform.localEulerAngles + ", After self: " + transform.localEulerAngles);
    }

 //   public GameObject player;
 //   private Vector3 offset;

	//// Use this for initialization
	//void Start () {
 //       offset = transform.position - player.transform.position;
	//}
	
	//// Update is called once per frame
	//void LateUpdate () {
 //       transform.position = player.transform.position + offset;
	//}
}
