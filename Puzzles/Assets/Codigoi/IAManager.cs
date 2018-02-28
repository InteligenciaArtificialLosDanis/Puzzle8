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

//Enumerado de direcciones, que definen el movimiento que ha hecho el hueco desde el nodo padre
//para llegar a la matriz del nodo I-ésimo
public enum Movimiento {Arriba, Abajo, Izquierda, Derecha, No};



public class Nodo
{

    public Nodo (int [,] tablrAux, Movimiento operador, Nodo papi,  int c) 
    {
        tablero = tablrAux;
        coste = c;
        movRealizado = operador;
        padre = papi; //;) ♥
    }


   public int coste;
   public int[,] tablero;
   public Movimiento movRealizado;
   public Nodo padre;

}

public class IAManager : MonoBehaviour {

    public GameObject GM; //GameManager

    int[,] solucion;

    Movimiento movsPosibles;
   
	// Use this for initialization
	void Start () {
        int ficha = 1;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (i == 2 && j == 2)
                    solucion[i, j] = 0;
                else
                    solucion[i, j] = ficha;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //El bfs debería devolver una pila de movimientosss 
    //Hostia puta lo mismo no sería mala idea que fuera recursivo, así controlas mejor
    //el tema de que un tablero transformado sea solucion, y por consiguiente vas subiendo el arbol y aprovechas
    //Para meter los metodos en la pila mas comodamente

    //Igualmente se puede hacer por iteracion, y llamando a otro método pero yo que sé. Ambas formas son válidas :3
    void BFS()
    {
        int[,] tableroIni = convierteMatrizGOaInt();
        Stack<Movimiento> movSolucion;

        //Creamos el nodo inicial
        Nodo raiz = new Nodo(tableroIni, Movimiento.No, null, 0);

        if (raiz.tablero == solucion) Debug.Log("De puta madre, has llegado a la solucion");

        Queue<Nodo> frontera = new Queue<Nodo>();
        frontera.Enqueue(raiz);
        if (frontera.Peek() == null) Debug.Log("SE HA ROTO QUE FLIPAS");

        List<Nodo> explorado = new List<Nodo>(); //Lista de nodos explorados

        bool fin = false;
        while (!fin && frontera.Peek() != null)
        {

            Nodo front = frontera.Dequeue();
            explorado.Add(front);

            int filaEmpty, colEmpty;
            filaEmpty = colEmpty = 0;
            buscaEmpty(front.tablero, ref filaEmpty, ref colEmpty); //Buscamos el empty en la Matriz


            //Para cada una de las direcciones posibles->
            for (int i = 0; i < 4; i++)
            {
               
                switch (i)
                {
                    case 0: //ARRIBA
                        if (movimientoLegalIA(Movimiento.Arriba, filaEmpty, colEmpty))
                        {
                            int[,] tableroHijo = modeloTransicion(front.tablero, Movimiento.Arriba, filaEmpty, colEmpty);
                            Nodo hijo = new Nodo(tableroHijo, Movimiento.Arriba, front, front.coste + 1);

                            if(!frontera.Contains(hijo) && !explorado.Contains(hijo))
                            {
                                if (esTableroFinal(hijo.tablero))
                                {
                                    Debug.Log("SUUUUUUUUUUUUUUUUU");
                                    //Llama al método de solución, que te va a construir una movida
                                    fin = true;
                                }

                                else
                                {
                                    frontera.Enqueue(hijo);
                                 
                                }
                            }
                        }
                        break;
                    case 1: //ABAJO
                        if (movimientoLegalIA(Movimiento.Abajo, filaEmpty, colEmpty))
                        {
                            int[,] tableroHijo = modeloTransicion(front.tablero, Movimiento.Abajo, filaEmpty, colEmpty);
                            Nodo hijo = new Nodo(tableroHijo, Movimiento.Abajo, front, front.coste + 1);

                            if (!frontera.Contains(hijo) && !explorado.Contains(hijo))
                            {
                                if (esTableroFinal(hijo.tablero))
                                {
                                    Debug.Log("SUUUUUUUUUUUUUUUUU");
                                    //Llama al método de solución, que te va a construir una movida
                                    fin = true;
                                }

                                else
                                {
                                    frontera.Enqueue(hijo);

                                }
                            }
                        }
                        break;

                    case 2: //DERECHA
                        if (movimientoLegalIA(Movimiento.Derecha, filaEmpty, colEmpty))
                        {
                            int[,] tableroHijo = modeloTransicion(front.tablero, Movimiento.Derecha, filaEmpty, colEmpty);
                            Nodo hijo = new Nodo(tableroHijo, Movimiento.Derecha, front, front.coste + 1);

                            if (!frontera.Contains(hijo) && !explorado.Contains(hijo))
                            {
                                if (esTableroFinal(hijo.tablero))
                                {
                                    Debug.Log("SUUUUUUUUUUUUUUUUU");
                                    //Llama al método de solución, que te va a construir una movida
                                    fin = true;
                                }

                                else
                                {
                                    frontera.Enqueue(hijo);

                                }
                            }
                        }
                        break;

                    case 3: //IZQUIERDA
                        if (movimientoLegalIA(Movimiento.Izquierda, filaEmpty, colEmpty))
                        {
                            int[,] tableroHijo = modeloTransicion(front.tablero, Movimiento.Izquierda, filaEmpty, colEmpty);
                            Nodo hijo = new Nodo(tableroHijo, Movimiento.Izquierda, front, front.coste + 1);

                            if (!frontera.Contains(hijo) && !explorado.Contains(hijo))
                            {
                                if (esTableroFinal(hijo.tablero))
                                {
                                    Debug.Log("SUUUUUUUUUUUUUUUUU");
                                    //Llama al método de solución, que te va a construir una movida
                                    fin = true;
                                }

                                else
                                {
                                    frontera.Enqueue(hijo);

                                }
                            }
                        }
                        break;

                }
            }
        }
    }

    bool movimientoLegalIA(Movimiento dir, int emptyF, int emptyC)
        {

        switch (dir)
        {
            case Movimiento.Arriba:
                if (emptyF - 1 >= 0) return true;
             break;
            case Movimiento.Abajo:
                if (emptyF + 1 <= 2) return true;
                break;
            case Movimiento.Derecha:
                if (emptyC + 1 <= 2) return true;
                break;
            case Movimiento.Izquierda:
                if (emptyC - 1 >= 0) return true;
                break;
        }

        return false;
    }

    int [,] convierteMatrizGOaInt()
    {
        GameObject[,] tablero = GM.GetComponent<GameManager>().tablero; //Toma el tablero en el momento de inicio de la IA.

        int[,] tableroRet = new int [3,3];

        for (int i = 0; i<3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if(tablero[i, j].tag == "Empty")
                    tableroRet[i, j] = tablero[i, j].GetComponent<Empty>().id;
                else
                 tableroRet[i, j] = tablero[i, j].GetComponent<Ficha>().id;
            }
        }

        return tableroRet;
    }

    //Asumo que cuando este método es llamado, el movimiento es legal
    int[,] modeloTransicion(int[,] tablero, Movimiento nuevoMov, int filaEmpty, int colEmpty)
    {
       
        int aux; //Valor auxiliar para el swap

        //Lo desplazamos en funcion del movimiento dado
        switch (nuevoMov)
        {
            case Movimiento.Arriba:
                aux = tablero[filaEmpty, colEmpty + 1];
                tablero[filaEmpty, colEmpty + 1] = 0;
                tablero[filaEmpty, colEmpty] = aux;
             break;

            case Movimiento.Abajo:
                aux = tablero[filaEmpty, colEmpty - 1];
                tablero[filaEmpty, colEmpty - 1] = 0;
                tablero[filaEmpty, colEmpty] = aux;
                break;

            case Movimiento.Derecha:
                aux = tablero[filaEmpty +1, colEmpty];
                tablero[filaEmpty + 1, colEmpty] = 0;
                tablero[filaEmpty, colEmpty] = aux;
                break;

            case Movimiento.Izquierda:
                aux = tablero[filaEmpty -1, colEmpty];
                tablero[filaEmpty -1, colEmpty] = 0;
                tablero[filaEmpty, colEmpty] = aux;
                break;
        }

        int[,] nuevoTablero = tablero; //Guardo los cambios, y los devuelvo
                                       //¿Que lo podría pasar por referencia? También.

        return nuevoTablero;

    }

    bool esTableroFinal(int [,] tablero)
    {
        return tablero == solucion; //¿Se puede hacer así? ¿O hay que hacer un bucle que vaya mirando uno por uno?

    }
    void buscaEmpty(int[,] tablero, ref int filaEmpty, ref int colEmpty)
    {

      

        int f = 0;
        int c = 0;

        bool stop = false;

        while (!stop && f < 3)
        {
            while(!stop && c < 3)
            {
                if(tablero[f,c] == 0)
                {
                    filaEmpty = f;
                    colEmpty = c;
                    stop = true;   //Detenemos el bucle :3
                }
                c++;
            }
            f++;
        }


    }
}
