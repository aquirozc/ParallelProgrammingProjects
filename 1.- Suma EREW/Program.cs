using System;
using System.Threading.Tasks;

public class SumaEREW{

    public static void Main (string[] args){

        int[] VectorSuma = {0,1,1,1,1,1,1,1,1};

        System.Console.WriteLine("\nUniversidad Autónoma del Estado de México");
        System.Console.WriteLine("Ingeniería en Computación");
        System.Console.WriteLine("LINC33 Paradigmas de Programación II\n");

        System.Console.WriteLine("Suma EREW (Exclusive-read & Exclusive-Write)");
        ImprimirVectorNumerico(VectorSuma);
        System.Console.WriteLine();

        int ResultadoSuma = AlgoritmoSumaEREW(VectorSuma);
        System.Console.WriteLine($"\nEl resultado de la suma es igual a {ResultadoSuma}\n");
        

    }

    public static int AlgoritmoSumaEREW(int[] VectorSuma){

        int LongitudArreglo = VectorSuma.Length - 1;
        int LogBase2 = (int) Math.Log(LongitudArreglo,2);

        string[] VectorProcedimientos = new string[VectorSuma.Length];

        for(int i = 1; i < LogBase2 + 1; i++){

            ResetearVectorProcedimientos(VectorSuma, VectorProcedimientos);

            for(int j = 1;  j < (int)(LongitudArreglo/2) + 1; j++){

                int x = i;
                int y = j;

                Task Tarea = new Task(

                    () => {

                        if((2*y) % Math.Pow(2,x) == 0){

                            int a = VectorSuma[2*y];
                            int b = VectorSuma[2*j - (int)Math.Pow(2,x - 1)];

                        VectorProcedimientos[2*y] = " (" + a.ToString() + " + " + b.ToString() + ") ";
                        VectorSuma[2*y] = a + b;

                        }

                    }

                );

                Tarea.RunSynchronously();

            }

            ImprimirVectorProcedimientos(VectorProcedimientos);
            ImprimirVectorNumerico(VectorSuma);

        }


        return VectorSuma[LongitudArreglo];

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
