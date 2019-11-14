using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Guaranteed to run after all item have been processed and updated
	void LateUpdate () {
        transform.position = player.transform.position + offset;
	}
}
