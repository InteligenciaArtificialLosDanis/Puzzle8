using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {
    public GameObject[] Fichas;
	public GameObject empty;

    public GameObject[,] tablero;
	public Ficha fichaJuego;
	public Empty emptyJuego;

    bool[] usados = new bool[8];
	bool fin = false;


    // Use this for initialization
    void Start () {
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
					emptyJuego.setPosMatriz (x, y);
				}

                do
                {
                    i = Random.Range(0, 8);
                }
                while (usados[i] == true);

                tablero[f, c] = Instantiate(Fichas[i], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                usados[i] = true;

				fichaJuego.setPosMatriz (x, y);

                x = x + 2;  //Movemos la X
            }
            x = -4;         //Restauramos la X
            y = y - 2;      //Bajamos la fila
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	bool movimientoLegal(GameObject ficha){
		//Si la partida no se ha acabado
	
		if (fin)
			return false;
		//Pregunta si hay un gameobject con tag empty, y si lo hay devuelve true para mover la ficha
		else {
		//Arriba
			//if (){
			//}
			return false;
		}


	}

	void partidaAcabada() {
		//Comprueba todas las fichas a ver si están bien puestas :) (while)

		//If (todas) cout << YAAAAAY :3
		//else no.


	}
}
