using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public class OrdenamientoCRCW{

    public static void Main(){
    
        System.Console.WriteLine("\nUniversidad Autónoma del Estado de México");
        System.Console.WriteLine("Ingeniería en Computación");
        System.Console.WriteLine("LINC33 Paradigmas de Programación II\n");
       
        System.Console.WriteLine("Ordenamiento CRCW (Concurrent-read & Concurrent-Write)");

        int[] demo = {0,95,10,6,15};

        ImprimirVectorNumerico(demo);
        System.Console.WriteLine();

        System.Console.WriteLine("Vector ordenado: ");
        AlgoritmoOrdenamientoCRCW(demo);
        ImprimirVectorNumerico(demo);
        System.Console.WriteLine();

    }

    public static void AlgoritmoOrdenamientoCRCW(int[] L){

        List<Task> ListaDeTareas = new List<Task>();
        int[] win = new int[L.Length];
        int[] aux = new int[L.Length];

        for(int i = 1; i<= L.Length-1; i++){

            int a = i;

            ListaDeTareas.Add(new Task(()=> win[a] = 0));
            
        }

        Parallel.ForEach(ListaDeTareas, tarea => tarea.RunSynchronously());
        

        for(int i = 1; i  <= L.Length-1; i++){

            ListaDeTareas = new List<Task>();

            for(int j = i + 1; j <= L.Length-1;j++){

                int a = i;
                int b = j;

                ListaDeTareas.Add(new Task(
                
                ()=> {

                    if(L[a] > L[b]){
                        win[a] = win[a]+ 1;
                    }else{
                        win[b] = win[b] + 1;
                    }

                }

                ));

            }

             Parallel.ForEach(ListaDeTareas, tarea => tarea.RunSynchronously());
            

        }

       
        ListaDeTareas = new List<Task>();

        for(int i = 1; i<= L.Length-1; i++){

            int a = i;

            ListaDeTareas.Add(new Task(()=> aux[a] = L[a]));
            
        }

        Parallel.ForEach(ListaDeTareas, tarea => tarea.RunSynchronously());
        ListaDeTareas = new List<Task>();

        for(int i = 1; i<= L.Length-1; i++){

            int a = i;

            ListaDeTareas.Add(new Task(()=> L[1 + win[a]] = aux[a]));
            
        }

        Parallel.ForEach(ListaDeTareas, tarea => tarea.RunSynchronously());

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