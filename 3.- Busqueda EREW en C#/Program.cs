using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BusquedaEREW{

    public static void Main(){

        System.Console.WriteLine("\nUniversidad Autónoma del Estado de México");
        System.Console.WriteLine("Ingeniería en Computación");
        System.Console.WriteLine("LINC33 Paradigmas de Programación II\n");
        
        System.Console.WriteLine("\nBusqueda EREW (Exclusive-read & Exclusive-Write)");

        int[] demostracion =  new int[] {5,-1,23,-4,2,5,-2,0,5,1,5,-5,8,5,3,-2};
        ImprimirVectorNumerico(demostracion);
        System.Console.WriteLine();
        

        int Posicion = AlgoritmoBusquedaEREW(demostracion, demostracion[0]);

        if (Posicion == int.MaxValue){
            Console.WriteLine($"El valor {demostracion[0]} no se encuentra dentro del arreglo");
        }else{
            Console.WriteLine($"El valor {demostracion[0]} se encuentra en la posición {Posicion}");
        }

        System.Console.WriteLine();

    }

    private static void difusion(int[]A, int z){

        A[1] = z;

        for(int i = 1; i < (int) (A.Length)/2 - 1; i++){

            List<Task> ListaDeTareas = new List<Task>();
            
            for(int j = (int)(Math.Pow(2, i-1) + 1); j <= (int)Math.Pow(2,i);j++){

                int x = i;
                int y = j;

                ListaDeTareas.Add(new Task( () =>{A[y] = A[y-(int)Math.Pow(2,x-1)];}));
            
            }

            Parallel.ForEach(ListaDeTareas, tarea => tarea.RunSynchronously());


        }

    }

    private static int Minimo(int[] L){

        int n = L.Length;

        for(int j = 1; j <= (int) Math.Log(n,2) - 1;j++){

            List<Task> ListaDeTareas = new List<Task>();

            for(int i = 1; i <= (int) n / Math.Pow(2,j);i++){

                int x = i;

               ListaDeTareas.Add(new Task(

                    () =>{

                        if(L[2*x-1] >  L[2*x]){

                            L[x] = L[2*x];

                        }else{

                            L[x] = L[2*x-1];

                        }

                    }

                ));

            }

            Parallel.ForEach(ListaDeTareas, tarea => tarea.RunSynchronously());
            
        }

        return L[1];

    }

    public static int AlgoritmoBusquedaEREW(int[] L, int x){

        int[] temp = new int[L.Length];

        difusion(temp,x); 

        for(int i = 1; i <= L.Length -1;i++){

            List<Task> ListaDeTareas = new List<Task>();

            ListaDeTareas.Add(new Task(

                () => {

                    if(L[i] == temp[i]){

                        temp[i] = i + 1;

                    }else{

                        temp[i] = int.MaxValue;

                    }

                }

            ));

            Parallel.ForEach(ListaDeTareas, tarea => tarea.RunSynchronously());

        }

        return Minimo(temp);

    }

    private static void ImprimirVectorNumerico(int[] array){

        System.Console.Write("[");

        for (int i = 0; i < array.Length; i++){

            string output = array[i].ToString();

            if (i != array.Length-1){

                 output += ", ";
                
            }

            System.Console.Write(output);
            
        }

        System.Console.Write("]\n");

    }

    
    
}
