﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha : MonoBehaviour {
    public GameObject pieza;
    public GameObject empty;

	public GameObject gm; //Game Manager

	public int id;

	 int matrizX, matrizY;

	public int PosCorrectaY;
	public int PosCorrectaX;


    //float piezaX, piezaY, piezaZ;
    Vector3 posPieza;
   
    // Use this for initialization
    void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnMouseDown()
    {
		//Si es true, entonces la matriz ya viene cambiada
		if (gm.GetComponent<GameManager>().movimientoLegal (pieza)) {
			posPieza = pieza.transform.position;
			pieza.transform.position = empty.transform.position;
			empty.transform.position = posPieza;
		}
    }

	bool bienPuesto (int x, int y){
		return (x == PosCorrectaX && y == PosCorrectaY);
	}

	public void setPosMatriz (int x, int y){

		matrizX = x;
		matrizY = y;

	}
	public int getMatrizX(){ return matrizX;
	}

	public int getMatrizY(){ return matrizY;
	}
}

