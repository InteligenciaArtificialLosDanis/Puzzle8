using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Enumerado de direcciones, que definen el movimiento que ha hecho el hueco desde el nodo padre
//para llegar a la matriz del nodo I-ésimo
public enum Movimiento {Arriba, Abajo, Izquierda, Derecha, No};



public class Nodo
{

    public Nodo (int [,] tablrAux, Movimiento operador, Nodo n,  int c) 
    {
        tablero = tablrAux;
        coste = c;
        movRealizado = operador;
        padre = n;
    }

   

   public int coste;
   public int[,] tablero;
   public Movimiento movRealizado;
   public Nodo padre;

    
    /*public static bool operator ==(Nodo a, Nodo b)
    {
        Debug.Log("He entrado en el operator == :3");
        bool iguales = true;
        int i = 0;
        int j;
        while (iguales && i < 3)
        {
            j = 0;

            while (iguales && j < 3)
            {

                if (a.tablero[i, j] != b.tablero[i, j])
                    iguales = false;

                j++;
            }

            i++;
        }
        Debug.Log("He salido en el operator == :3");
        return iguales;
     
    }

    public static bool operator !=(Nodo a, Nodo b)
    {
        bool Notiguales = true;
        int i = 0;
        int j;
        while (Notiguales && i < 3)
        {
            j = 0;

            while (Notiguales && j < 3)
            {

                if (a.tablero[i, j] == b.tablero[i, j])
                    Notiguales = false;

                j++;
            }

            i++;
        }
        Debug.Log("He entrado en el operator !=        :3");
        return Notiguales;
    }
    */

   
    public override int GetHashCode()
    {
        return 0;
    }

    public bool Equals(Object a)
    {
        Debug.Log("Entré en Equals");
        return false;
    }
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

        movsSolucion = new Stack<Movimiento>();
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
        Debug.Log(frontera.Count + " ANtes del bucle");

  
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
                Movimiento nuevoMov = Movimiento.No;
                switch (i)
                {
                    case 0: //ARRIBA
                        nuevoMov = Movimiento.Arriba;
                        break;

                    case 1: //ABAJO
                        nuevoMov = Movimiento.Abajo;
                        break;

                    case 2://Izquierda
                        nuevoMov = Movimiento.Izquierda;
                        break;

                    case 3: //Derecha
                        nuevoMov = Movimiento.Derecha;
                        break;
                       

                }

                if (movimientoLegalIA(nuevoMov, filaEmpty, colEmpty))
                {
              
                    int[,] tableroHijo = new int[3, 3];
                    igualaTablero(tableroHijo, modeloTransicion(front.tablero, nuevoMov, filaEmpty, colEmpty));
                    Nodo hijo = new Nodo(tableroHijo, nuevoMov, front, front.coste + 1);

                    if (!frontera.Contains(hijo) && !containsEnLista(explorado, hijo.tablero))
                    {
                    
   
                        if (comparaTableros(hijo.tablero, solucion))
                        {

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
            i++;
        }

        return contiene;
    }
    //La lista se va a modificar si la paso solo por entrada? :3 
    bool containsEnFrontera(Queue <Nodo> lista, int[,] tablero)
    {

        foreach (Nodo item in lista)
        {
            Debug.Log(item.tablero);
        }
        bool contiene = false;
        if (lista.Count == 0) return contiene; //Si la frontera está vacia, evidentemente no está


        Nodo a = lista.Dequeue();
        int[,] tableroNodo = new int [3,3];
        igualaTablero(tableroNodo, a.tablero);

        while (!contiene)
        {
            if (!comparaTableros(tableroNodo, tablero)) {
                a = lista.Dequeue(); //A LA MIERDAAAA
                igualaTablero(tableroNodo, a.tablero);
            }


            else contiene = true;
        }
        //Si no has recorrido todos, entonces vas a devolver que contiene y reorganizas el resto
        return contiene;
       
    }

    public bool comparaTableros(int [,] tablero, int[,] tableroFuente)
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
    
    void igualaTablero( int[,] tableroDestino, int[,] tableroFuente)
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
			movsSolucion.Push(nodoSol.movRealizado);
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


        int[,] nuevoTablero = new int[3, 3]; 
        igualaTablero(nuevoTablero, tablero);


        //Lo desplazamos en funcion del movimiento dado
        switch (nuevoMov)
        {
            case Movimiento.Derecha:
                aux = nuevoTablero[filaEmpty, colEmpty + 1];
                nuevoTablero[filaEmpty, colEmpty + 1] = 0;
                nuevoTablero[filaEmpty, colEmpty] = aux;
                break;

            case Movimiento.Izquierda:
                aux = nuevoTablero[filaEmpty, colEmpty - 1];
                nuevoTablero[filaEmpty, colEmpty - 1] = 0;
                nuevoTablero[filaEmpty, colEmpty] = aux;
                break;

            case Movimiento.Arriba:
                aux = nuevoTablero[filaEmpty - 1, colEmpty];
                nuevoTablero[filaEmpty - 1, colEmpty] = 0;
                nuevoTablero[filaEmpty, colEmpty] = aux;
                break;

            case Movimiento.Abajo:
                aux = nuevoTablero[filaEmpty + 1, colEmpty];
                nuevoTablero[filaEmpty + 1, colEmpty] = 0;
                nuevoTablero[filaEmpty, colEmpty] = aux;
                break;
        }


        return nuevoTablero;

    }

  
    void buscaEmpty(int[,] tablero, ref int filaEmpty, ref int colEmpty)
    {

      

        int f = 0;
        int c = 0;

        bool stop = false;

        while (!stop && f < 3)
        {
            c = 0;

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
