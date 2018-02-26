using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 //COSAS QUE NECESITAMOS

  -Lista de nodos: para simular los pasos que hemos ido tomando
  - Busque el empty 
  - Comparar un tablero con otro (para saber si son iguales y ya está explorado)
  - Explorado: Lista de tableros que ya han sido explorados (pos numericas y empty)

   -Frontera: Cola de nodos
   -Costes de todos los caminos :D

     
     */

public class nodo
{

    public nodo (GameObject [,] tablrAux, int c) 
    {
        tablero = tablrAux;
        coste = 0;
    }

    int coste;
    int filaEmpt, colEmpt;
    int filaFich, colFich;
    GameObject[,] tablero;

}

public class IAManager : MonoBehaviour {

    public GameObject GM; //GameManager

    GameObject[,] tablero;
	// Use this for initialization
	void Start () {
        	
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void BFS()
    {
        tablero = GM.GetComponent<GameManager>().tablero; //Toma el tablero en el momento de inicio de la IA.

        nodo raiz = new nodo(tablero, 0);






    }

    void buscaEmpty (out int filaEmpty, out int colEmpty)
    {
        filaEmpty = colEmpty = 0;
    }
}
