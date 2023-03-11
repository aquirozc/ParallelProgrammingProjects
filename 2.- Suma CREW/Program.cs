using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SumaCREW{

    public static void Main(){

        int[] VectorSuma = {5,2,10,1,8,12,7,3};

        System.Console.WriteLine("\nUniversidad Autónoma del Estado de México");
        System.Console.WriteLine("Ingeniería en Computación");
        System.Console.WriteLine("LINC33 Paradigmas de Programación II\n");
    
        System.Console.WriteLine("Suma CREW (Concurrent-read & Exclusive-Write)");
        ImprimirVectorNumerico(VectorSuma);
        System.Console.WriteLine();

        int resultadoSuma = AlgoritmoSumaCREW(VectorSuma);
        System.Console.WriteLine($"\nEl resultado de la suma es igual a {resultadoSuma}\n");


    }

    public static int AlgoritmoSumaCREW(int[] VectorSuma){

        int LongitudArreglo = VectorSuma.Length - 1;
        int LogBase2 = (int) Math.Log(LongitudArreglo + 1,2);

        string[] VectorProcedimientos = new string[VectorSuma.Length];

        for(int i = 1; i <= LogBase2 ;i++){

            ResetearVectorProcedimientos(VectorSuma, VectorProcedimientos);

            int[] VectorResultados = new int[LongitudArreglo + 1];
            List<Task> ListaDeEscritura = new List<Task>();
            List<Task> ListaDeLectura = new List<Task>();

            for (int j = (int)Math.Pow(2,i-1); j <= LongitudArreglo; j++){

                int a = i;
                int b = j;

                
                ListaDeLectura.Add(new Task(() => {RealizarSuma(a,b,VectorSuma,VectorProcedimientos,VectorResultados);}));
                ListaDeEscritura.Add(new Task(() => {EscribirSuma(b,VectorSuma,VectorResultados);}));
                
            }

            Parallel.ForEach(ListaDeLectura, tarea => tarea.RunSynchronously());
            Parallel.ForEach(ListaDeEscritura, tarea => tarea.RunSynchronously());
            
            ImprimirVectorProcedimientos(VectorProcedimientos);
            ImprimirVectorNumerico(VectorSuma);

        }

        return  VectorSuma[LongitudArreglo];

    }

    private static void RealizarSuma(int i, int j, int[] VectorSuma, string[] VectorProcedimientos,int[] VectorResultados){
        
        int a = VectorSuma[j];
        int b = VectorSuma[j-(int)Math.Pow(2,i-1)];

        VectorProcedimientos[j] = "(" + a.ToString() + " + " + b.ToString() + ")";   

        VectorResultados[j] =a + b;

    }

    private static void EscribirSuma (int j, int[]VectorSuma, int[]VectorResultados){

        VectorSuma[j] = VectorResultados[j];

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

    private static void ImprimirVectorProcedimientos(string[] array){

        System.Console.Write("[");

        for (int i = 0; i < array.Length; i++){

            string output = array[i];

            if (i != array.Length-1){

                 output += ", ";
                
            }

            System.Console.Write(output);

        }

        System.Console.Write("] ==> ");

    }

    private static void ResetearVectorProcedimientos(int[] vectorOriginal, string[] vectorProcedimientos){

        for (int i = 0; i < vectorOriginal.Length; i++){

            vectorProcedimientos[i] = vectorOriginal[i].ToString();

        }
    }

}