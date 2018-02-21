using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha : MonoBehaviour {
   public GameObject pieza;
    public GameObject empty;

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
        posPieza = pieza.transform.position;
        pieza.transform.position = empty.transform.position;
        empty.transform.position = posPieza;
    }

	bool bienPuesto (int x, int y){
		return (x == PosCorrectaX && y == PosCorrectaY);
	}

	public void setPosMatriz (int x, int y){

		matrizX = x;
		matrizY = y;

	}
}

