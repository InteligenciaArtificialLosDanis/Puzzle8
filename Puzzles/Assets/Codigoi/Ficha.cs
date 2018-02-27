using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha : MonoBehaviour {
   public GameObject pieza;
    public GameObject empty;
    public GameObject gm; //Game Manager

    public int id;

   public int filaMat, colMat;

	public int filaCorrecta;
	public int colCorrecta;



    //float piezaX, piezaY, piezaZ;
    Vector3 posPieza;
	Vector3 PosOriginal;
   
    // Use this for initialization
    void Start () {
		PosOriginal = pieza.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnMouseDown()
    {
        if (gm.GetComponent<GameManager>().movimientoLegal(pieza))
        {
            posPieza = pieza.transform.position;
            pieza.transform.position = empty.transform.position;
            empty.transform.position = posPieza;
        }
    }

	bool bienPuesto (int fila, int columna){
		return (fila == filaCorrecta && columna == colCorrecta);
	}

	public void setPosMatriz (int fila, int columna){

		filaMat = fila;
		colMat  = columna;

		//Debug.Log ("soy "+ this+ " Tengo fila" + filaMat + " Y la columna" + colMat);

	}

	public void reiniciaFicha (){
		pieza.transform.position = PosOriginal;
	}

}

