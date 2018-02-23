using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {
    public GameObject[] Fichas;
	//int[] fichasOrdenadas = { 1, 2, 3, 4, 5, 6, 7, 8 }; 
	public GameObject empty;

    public GameObject[,] tablero;

   
	bool fin = false;


    // Use this for initialization
    void Start () {
        tablero = new GameObject[3, 3];

        int i = 0;			//Itera sobre fichas
        int y = 2;		//Posicion y
        int x = -4;		//Posicion X

        for (int f = 0; f < 3; f++) //FILS
        {
            for (int c = 0; c < 3; c++) //COLS
            {
				if (f == 2 && c == 2) {  
					tablero [f, c] = Instantiate (empty, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
					tablero [f, c].GetComponent<Empty>().setPosMatEmpty(f, c);
                }

				else{
	                tablero[f, c] = Instantiate(Fichas[i], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
					Debug.Log ("cout " + f + ", " + c);
					tablero[f, c].GetComponent<Ficha>().setPosMatriz(f, c);

					i++;		//Pasamos a la siguiente ficha
	                x = x + 2;  //Movemos la X
				}
            }
            x = -4;         //Restauramos la X
            y = y - 2;      //Bajamos la fila
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

   public bool movimientoLegal(GameObject ficha){
        //Si la partida no se ha acabado

        if (fin)
            return false;
        //Pregunta si hay un gameobject con tag empty, y si lo hay devuelve true para mover la ficha
        else
        {
            int fila = ficha.GetComponent<Ficha>().filaMat;
			int columna = ficha.GetComponent<Ficha> ().colMat;

			Debug.Log ("SOY "+ ficha+ " Mi fila es " +  ficha.GetComponent<Ficha>().filaMat  + " y mi columna " + ficha.GetComponent<Ficha> ().colMat);
            //Arriba
			if (fila - 1 >= 0 && tablero[fila - 1, columna].tag == "Empty")
            {
                swapPosMatriz(ficha);
                return true;
            }
            //Abajo
			if (fila + 1 <= 2 && tablero[fila + 1, columna].tag == "Empty")
            {
                swapPosMatriz(ficha);
                return true;
            }
            //Derecha
			if (columna + 1 <= 2 && tablero[fila, columna + 1].tag == "Empty")
            {
                swapPosMatriz(ficha);
                return true;
            }
            //Izquierda
			if (columna - 1 >= 0 && tablero[fila, columna - 1].tag == "Empty")
            {
                swapPosMatriz(ficha);
                return true;
            }

            //Si a su alrededor no hay vacío, no puedes mover esa ficha
            return false;
        }


    }

    void swapPosMatriz(GameObject ficha)
    {
        //Guardo la posicion de ficha. Le asigno la posicion de empty a la ficha
        //Después, meto la posicion guardada de la ficha al empty.
		int fichaF = ficha.GetComponent<Ficha>().filaMat;
		int fichaC = ficha.GetComponent<Ficha>().colMat;

		ficha.GetComponent<Ficha>().setPosMatriz(empty.GetComponent<Empty>().filaEmpty, empty.GetComponent<Empty>().colEmpty);
		//SIGUE DETECTANDO EL EMTY ORIGINAL, NO LA COPIA QUE INSTANCIAMOS
        empty.GetComponent<Empty>().setPosMatEmpty(fichaC, fichaF);
    }


    void partidaAcabada() {
		//Comprueba todas las fichas a ver si están bien puestas :) (while)

		//If (todas) cout << YAAAAAY :3
		//else no.


	}
}
