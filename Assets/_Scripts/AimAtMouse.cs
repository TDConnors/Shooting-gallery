using UnityEngine;
using System.Collections;

public class AimAtMouse : MonoBehaviour {

public GameObject target;
    public float rotateSpeed = 5;

    void Start() 
	{
      //Cursor.visible = false;
    }

    void LateUpdate() {
      float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
      float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
      target.transform.RotateAround(target.transform.position, Vector3.up, horizontal);
      target.transform.RotateAround(target.transform.position, Vector3.left, vertical);
      Vector3 eulerAngles = target.transform.rotation.eulerAngles;
      eulerAngles.z = 0;//or whatever rotation degrees you want
      target.transform.eulerAngles = eulerAngles;
  }
}