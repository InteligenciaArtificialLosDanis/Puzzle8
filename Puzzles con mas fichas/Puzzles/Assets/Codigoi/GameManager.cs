using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {
    public GameObject[] Fichas;
	public GameObject empty;

    public GameObject[,] tablero;

    bool[] usados = new bool[8];
	bool fin = false;


    // Use this for initialization
    void Start () {
		Debug.Log ("Start");
        tablero = new GameObject[3, 3];

        int i;			//Itera sobre fichas
        int y = 2;		//Posicion y
        int x = -4;		//Posicion X

        for (int f = 0; f < 3; f++) //FILS
        {
            for (int c = 0; c < 3; c++) //COLS
            {
				if (f == 2 && c == 2) {  
					tablero [f, c] = Instantiate (empty, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
					//empty.GetComponent<Empty>().setPosMatriz (c, f);
					empty.GetComponent<Empty>().setPosMatriz(c,f);
				}

                do
                {
                    i = Random.Range(0, 8);
                }
                while (usados[i] == true);

                tablero[f, c] = Instantiate(Fichas[i], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                usados[i] = true;

				Fichas[i].GetComponent<Ficha>().setPosMatriz (c, f); //c = matrizX, f = matrizY;
	

                x = x + 2;  //Movemos la X
            }
            x = -4;         //Restauramos la X
            y = y - 2;      //Bajamos la fila
        }
		Debug.Log ("Todos las fichas cargadas");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool movimientoLegal(GameObject ficha){
		//Si la partida no se ha acabado
	
		if (fin)
			return false;
		//Pregunta si hay un gameobject con tag empty, y si lo hay devuelve true para mover la ficha
		else {
			int fila = ficha.GetComponent<Ficha> ().getMatrizY();
			int columna = ficha.GetComponent<Ficha> ().getMatrizX();
			//Arriba
			if (fila - 1 >= 0 && tablero [fila - 1, columna] == empty) {
				swapPosMatriz (ficha);
				return true;
			}
			//Abajo
			if (fila + 1 <= 2 && tablero [fila + 1, columna] == empty) {
				swapPosMatriz (ficha);
				return true;
			}
			//Derecha
			if (columna + 1 <= 2 && tablero [fila, columna + 1] == empty) {
				swapPosMatriz (ficha);
				return true;
			}
			//Izquierda
			if (columna - 1 >= 0 && tablero [fila, columna - 1] == empty){
				swapPosMatriz (ficha);
			return true;
			}

			//Si a su alrededor no hay vacío, no puedes mover esa ficha
			return false;
		}


	}

	void swapPosMatriz(GameObject ficha){
	//Guardo la posicion de ficha. Le asigno la posicion de empty a la ficha
	//Después, meto la posicion guardada de la ficha al empty.
		int fichaF = ficha.GetComponent<Ficha>().getMatrizY();
		int fichaC = ficha.GetComponent<Ficha>().getMatrizX();	

		ficha.GetComponent<Ficha> ().setPosMatriz (empty.GetComponent<Empty> ().matrizX, empty.GetComponent<Empty> ().matrizY);

		empty.GetComponent<Empty> ().setPosMatriz (fichaC, fichaF);
	}

	void partidaAcabada() {
		//Comprueba todas las fichas a ver si están bien puestas :) (while)

		//If (todas) cout << YAAAAAY :3
		//else no.


	}
}
