using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OnClickReinicio : MonoBehaviour {

	public Button reinicio;
	// Use this for initialization
	void Start () {
		Button btn = reinicio.GetComponent<Button> ();
		btn.onClick.AddListener(Reinicia);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Reinicia(){
	
	}
}
