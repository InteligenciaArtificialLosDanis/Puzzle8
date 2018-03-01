using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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

    int[,] solucion = new int[3,3];

	public Stack<Movimiento> movsSolucion;	//Stack de movimientos
   
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
                {
                    solucion[i, j] = ficha;
                    ficha++;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //El bfs debería devolver una pila de movimientos
    //Hostia puta lo mismo no sería mala idea que fuera recursivo, así controlas mejor
    //el tema de que un tablero transformado sea solucion, y por consiguiente vas subiendo el arbol y aprovechas
    //Para meter los metodos en la pila mas comodamente

    //Igualmente se puede hacer por iteracion, y llamando a otro método pero yo que sé. Ambas formas son válidas :3
    public void BFS()
    {
        int[,] tableroIni = new int[3, 3];

        tableroIni = convierteMatrizGOaInt();
        

        //Creamos el nodo inicial
        Nodo raiz = new Nodo(tableroIni, Movimiento.No, null, 0);

        if (raiz.tablero == solucion) Debug.Log("De puta madre, has llegado a la solucion");

        Queue<Nodo> frontera = new Queue<Nodo>();
        frontera.Enqueue(raiz);
        Debug.Log(frontera.Count + " ANtes del puto bucle");

  
        List<int[,]> explorado = new List<int[,]>(); //Lista de tableros explorados
        

        bool fin = false;
        while (!fin && frontera.Count != 0)
        {

            Nodo front = frontera.Dequeue();
            explorado.Add(front.tablero);

            int filaEmpty, colEmpty;
            filaEmpty = colEmpty = 0;
            buscaEmpty(front.tablero, ref filaEmpty, ref colEmpty); //Buscamos el empty en la Matriz

            Debug.Log("Empty: " + filaEmpty + " " + colEmpty);

            //Para cada una de las direcciones posibles->
            for (int i = 0; i < 4; i++)
            {
               
                switch (i)
                {
                    case 0: //ARRIBA
                        if (movimientoLegalIA(Movimiento.Arriba, filaEmpty, colEmpty))
                        {
                            Debug.Log("Movimiento legal arriba");
                            int[,] tableroHijo = new int[3,3];
                            igualaTablero(ref tableroHijo, modeloTransicion(front.tablero, Movimiento.Arriba, filaEmpty, colEmpty));
                            Nodo hijo = new Nodo(tableroHijo, Movimiento.Arriba, front, front.coste + 1);
                            Debug.Log("Me he creado bien y mi movimiento fue: " + hijo.movRealizado);
                              
                            if(!frontera.Contains(hijo) && !containsEnLista(explorado, hijo.tablero))
                            {
                                Debug.Log("Test de si se se desapila: " + frontera.Count);
                                if (comparaTableros(hijo.tablero, solucion))
                                {
                                    Debug.Log("SUUUUUUUUUUUUUUUUU");
                                    //Llama al método de solución
                                    DevuelveSolucion(hijo);
                                    fin = true;
                                }

                                else
                                {
                                    frontera.Enqueue(hijo);
                                    Debug.Log("Hijo metido en la frontera");
                                 
                                }
                            }
                        }
                        break;

                    case 1: //ABAJO
                        if (movimientoLegalIA(Movimiento.Abajo, filaEmpty, colEmpty))
                        {
                            Debug.Log("Movimiento legal abajo");
                            int[,] tableroHijo = new int[3, 3];
                            igualaTablero(ref tableroHijo, modeloTransicion(front.tablero, Movimiento.Abajo, filaEmpty, colEmpty));
                            Nodo hijo = new Nodo(tableroHijo, Movimiento.Abajo, front, front.coste + 1);
                            Debug.Log("Me he creado bien y mi movimiento fue: " + hijo.movRealizado);

                            if (!frontera.Contains(hijo) && !containsEnLista(explorado, hijo.tablero))
                            {
                                Debug.Log("Test de si se se desapila: " + frontera.Count);
                                if (comparaTableros(hijo.tablero, solucion))
                                {
                                    Debug.Log("SUUUUUUUUUUUUUUUUU");
                                    //Llama al método de solución
									DevuelveSolucion(hijo);
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
                            Debug.Log("Movimiento legal derecha");
                            int[,] tableroHijo = new int[3, 3];
                            igualaTablero(ref tableroHijo, modeloTransicion(front.tablero, Movimiento.Derecha, filaEmpty, colEmpty));
                            Nodo hijo = new Nodo(tableroHijo, Movimiento.Derecha, front, front.coste + 1);
                            Debug.Log("Me he creado bien y mi movimiento fue: " + hijo.movRealizado);

                            if (!frontera.Contains(hijo) && !containsEnLista(explorado, hijo.tablero))
                            {
                                Debug.Log("Test de si se se desapila: " + frontera.Count);
                                if (comparaTableros(hijo.tablero, solucion))
                                {
                                    Debug.Log("SUUUUUUUUUUUUUUUUU");
                                    //Llama al método de solución
									DevuelveSolucion(hijo);
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
                            Debug.Log("Movimiento legal izquierda");
                            int[,] tableroHijo = new int[3, 3];
                            igualaTablero(ref tableroHijo, modeloTransicion(front.tablero, Movimiento.Izquierda, filaEmpty, colEmpty));
                            Nodo hijo = new Nodo(tableroHijo, Movimiento.Izquierda, front, front.coste + 1);
                            Debug.Log("Me he creado bien y mi movimiento fue: " + hijo.movRealizado);

                            if (!frontera.Contains(hijo) && !containsEnLista(explorado, hijo.tablero))
                            {
                                Debug.Log("Test de si se se desapila: " + frontera.Count);
                                if (comparaTableros(hijo.tablero, solucion))
                                {
                                    Debug.Log("SUUUUUUUUUUUUUUUUU");
                                    //Llama al método de solución
									DevuelveSolucion(hijo);
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

    bool containsEnLista(List<int[,]> lista, int[,] tablero)
    {
        int i = 0;
        bool contiene = false;
        while(!contiene && i < lista.Count)
        {
            if (comparaTableros(lista[i], tablero))
                contiene = true;
        }

        return contiene;
    }
    //La lista se va a modificar si la paso solo por entrada? :3 
    bool containsEnFrontera(Queue <Nodo> lista, int[,] tablero)
    {

       
        bool contiene = false;
        if (lista.Count == 0) return contiene; //Si la frontera está vacia, evidentemente no está


        Nodo a = lista.Dequeue();
        int[,] tableroNodo = new int [3,3];
        igualaTablero(ref tableroNodo, a.tablero);

        while (!contiene)
        {
            if (!comparaTableros(tableroNodo, tablero)) {
                a = lista.Dequeue(); //A LA MIERDAAAA
                igualaTablero(ref tableroNodo, a.tablero);
            }


            else contiene = true;
        }
        //Si no has recorrido todos, entonces vas a devolver que contiene y reorganizas el resto
        return contiene;
       
    }

    bool comparaTableros(int [,] tablero, int[,] tableroFuente)
    {

        bool iguales = true;
        int i = 0;
        int j;
        while (iguales && i < 3)
        {
            j = 0;

            while (iguales && j < 3)
            {

                if (tablero[i, j] != tableroFuente[i, j])
                    iguales = false;

                j++;
            }

            i++;
        }

        return iguales;
    }
    
    void igualaTablero(ref int[,]  tableroDestino, int[,] tableroFuente)
    {

       for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                tableroDestino[i, j] = tableroFuente[i, j];
            }
        }
        
    }

	//Método recursivo que carga en una pila (LIFO) los movimientos realizados hasta la solucion
	void DevuelveSolucion(Nodo nodoSol){
		if(nodoSol.padre != null){
			movsSolucion.Push (nodoSol.movRealizado);
            Debug.Log(nodoSol.movRealizado);
			DevuelveSolucion (nodoSol.padre);
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
            case Movimiento.Derecha:
                aux = tablero[filaEmpty, colEmpty + 1];
                tablero[filaEmpty, colEmpty + 1] = 0;
                tablero[filaEmpty, colEmpty] = aux;
                break;

            case Movimiento.Izquierda:
                aux = tablero[filaEmpty, colEmpty - 1];
                tablero[filaEmpty, colEmpty - 1] = 0;
                tablero[filaEmpty, colEmpty] = aux;
                break;

            case Movimiento.Arriba:
                aux = tablero[filaEmpty - 1, colEmpty];
                tablero[filaEmpty - 1, colEmpty] = 0;
                tablero[filaEmpty, colEmpty] = aux;
                break;

            case Movimiento.Abajo:
                aux = tablero[filaEmpty + 1, colEmpty];
                tablero[filaEmpty + 1, colEmpty] = 0;
                tablero[filaEmpty, colEmpty] = aux;
                break;
        }


        int[,] nuevoTablero = new int[3, 3]; //Guardo los cambios, y los devuelvo
                                             //¿Que lo podría pasar por referencia? También.
        igualaTablero(ref nuevoTablero, tablero);

        return nuevoTablero;

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
