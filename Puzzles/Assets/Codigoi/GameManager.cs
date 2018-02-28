using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour {
    public GameObject[] Fichas;
	//int[,] fichasOrdenadas = { 1, 2, 3, 4, 5, 6, 7, 8 }; 
	public GameObject empty;

	public GameObject IAManager;

    public GameObject[,] tablero;

   
	bool fin = false;


    // Use this for initialization
    void Start () {
        tablero = new GameObject[3, 3];
		generaMatrizOrdenada();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void generaMatrizOrdenada(){
		int i = 0;			//Itera sobre fichas
		
		for (int f = 0; f < 3; f++) { //FILS
			for (int c = 0; c < 3; c++) { //COLS
				if (f == 2 && c == 2) {  
					
					tablero [f, c] = empty;
					empty.GetComponent<Empty> ().reiniciaEmpty ();
				} else {
					Fichas[i].GetComponent<Ficha>().reiniciaFicha();
					tablero [f, c] = Fichas [i];
					//Debug.Log ("cout " + f + ", " + c);
					tablero [f, c].GetComponent<Ficha> ().setPosMatriz (f, c);

					i++;		//Pasamos a la siguiente ficha

				}
			}
		}
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

			//Debug.Log ("Soy "+ficha+ " Mi fila es " +ficha.GetComponent<Ficha>().filaMat+ " y mi columna " + ficha.GetComponent<Ficha> ().colMat);
            //Arriba
			if (fila - 1 >= 0 && tablero[fila - 1, columna].tag == "Empty")
            {
                swapPosMatriz(ficha, fila - 1, columna);
                return true;
            }
            //Abajo
			if (fila + 1 <= 2 && tablero[fila + 1, columna].tag == "Empty")
            {         
               swapPosMatriz(ficha, fila + 1, columna);
                return true;
            }
            //Derecha
			if (columna + 1 <= 2 && tablero[fila, columna + 1].tag == "Empty")
            { 
                swapPosMatriz(ficha, fila, columna + 1);
                return true;
            }
            //Izquierda
			if (columna - 1 >= 0 && tablero[fila, columna - 1].tag == "Empty")
            {
                swapPosMatriz(ficha, fila, columna - 1);
                return true;
            }

            //Si a su alrededor no hay vacío, no puedes mover esa ficha
            return false;
        }


    }

    void swapPosMatriz(GameObject ficha, int filaEmpty, int columnaEmpty)
    {
        int fila = ficha.GetComponent<Ficha>().filaMat;
        int columna = ficha.GetComponent<Ficha>().colMat;

        tablero[fila, columna] = empty;
        tablero[filaEmpty, columnaEmpty] = ficha;
        ficha.GetComponent<Ficha>().setPosMatriz(filaEmpty, columnaEmpty);

        //Set matriz de ambos


    }


    bool partidaAcabada() {
		//Comprueba todas las fichas a ver si están bien puestas :) (while)

		bool correcto = true;

		int f, c;
		f = c = 0;
		while (correcto && f < 3) {
			while (correcto && c < 3) {
				if (!tablero [f, c].GetComponent<Ficha> ().bienPuesta (f, c))
					correcto = false;
				c++;
			}
			f++;
		}

		return correcto;

	}

	void aplicaBSF(){
		Stack<Movimiento> rutaSol = IAManager.GetComponent<IAManager>().movsSolucion;
		while (rutaSol.Peek () != null) {
		//Toma la pos del empty
		
		//Busca la ficha en el sentido dado por desapilar rutaSol

		//Mueve esa ficha, actualizando la matriz del tablero 

		//Llamas luego a move para representar el movimiento en el editor
		
		//gg ez
		
		
		}
	}
}
