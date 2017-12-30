using UnityEngine;
using System.Collections;

public class AimAtCursor : MonoBehaviour {
    public GameObject target;
    public float rotateSpeed = 5;

    void Start() {
        transform.parent = target.transform;
        transform.LookAt(target.transform);
    }

    void LateUpdate() {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;	
		target.transform.rotation = Quaternion.Euler(target.transform.eulerAngles.x - vertical,Mathf.Clamp ((target.transform.eulerAngles.y + horizontal), 200, 340),0);
    }

}