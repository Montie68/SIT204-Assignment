using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("X: "+transform.localPosition.x + "; Y: " + transform.localPosition.y);
        Vector3 pos = Vector3.zero;
        if (Input.GetAxis("Horizontal") != 0) pos.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        else if (Input.GetAxis("Vertical") != 0) pos.y = -Input.GetAxisRaw("Vertical") * Time.deltaTime;

        transform.localPosition += pos;

    }
}
