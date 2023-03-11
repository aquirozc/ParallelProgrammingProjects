using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BusquedaCRCW{

    public static void Main(){

        System.Console.WriteLine("\nUniversidad Autónoma del Estado de México");
        System.Console.WriteLine("Ingeniería en Computación");
        System.Console.WriteLine("LINC33 Paradigmas de Programación II\n");
        
        System.Console.WriteLine("Busqueda CRCW (Concurrent-read & Concurrent-Write)");

        int[] demo = {0,95,10,6,15};
        ImprimirVectorNumerico(demo);
        System.Console.WriteLine();

        int posicion = MinCRCW(demo);
        System.Console.WriteLine($"El elemento mínimo es {demo[posicion]} y se encuentra en la posición {posicion}\n");


    }

    public static int MinCRCW(int[] L){

        List<Task> ListaDeTareas = new List<Task>();
        int[] win = new int[L.Length];
        int IndexMin = -1;

        for(int i = 1; i<= L.Length-1; i++){

            int a = i;

                ListaDeTareas.Add(new Task(()=> win[a] =0));
            
        }

        Parallel.ForEach(ListaDeTareas, tarea => tarea.RunSynchronously());
        ListaDeTareas = new List<Task>();

        for(int i = 1, j = i + 1; i < j & j <= L.Length-1; i++, j++){

            int a = i;
            int b = j;

            ListaDeTareas.Add(new Task(
                
                ()=> {

                    if(L[a] > L[b]){
                        win[a] = 1;
                    }else{
                        win[b] = 1;
                    }

                }

            ));

        }

        Parallel.ForEach(ListaDeTareas, tarea => tarea.RunSynchronously());
        ListaDeTareas = new List<Task>();

        for(int i = 1; i <= L.Length-1;i++){

            int a = i;

            ListaDeTareas.Add(new Task(
                
                ()=> {

                    if(win[a] == 0){

                        IndexMin = a;
                        
                    }

                }

            ));

        }

        Parallel.ForEach(ListaDeTareas, tarea => tarea.RunSynchronously());

        return IndexMin;
        

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
