using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PornalefController : MonoBehaviour {

    float rotespeed = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) {
            rotespeed = 10;
        }
        transform.Rotate(0, 0, this.rotespeed);
	}
}
