using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empty : MonoBehaviour {

	public GameObject empty;
    public int id;

	Vector3 posOriginal;
	// Use this for initialization
	void Start () {
		posOriginal = empty.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void reiniciaEmpty(){
		empty.transform.position = posOriginal;
	}

}
