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

        for (int i = 1; i < LogBase2 + 1; i++){

                ResetearVectorProcedimientos(VectorSuma,VectorProcedimientos);
                List<Task> ListaDeTareas = new List<Task>();

                for(int j = (int)Math.Pow(2, i - 1) + 1;  j < LongitudArreglo + 1; j++){

                    int x = i;
                    int y = j;

                    ListaDeTareas.Add(new Task(

                        () => {

                            if ((y % ((int)(Math.Pow(2, x)))) == 0){

                                    int a = VectorSuma[y];
                                    int b = VectorSuma[(y) - (int)(Math.Pow(2, x - 1))];

                                    VectorSuma[y] = a + b;
                                    VectorProcedimientos[y] = " (" + a.ToString() + " + " + b.ToString() + ") ";

                            }

                        }

                    ));

                }

                Parallel.ForEach(ListaDeTareas, tarea => tarea.RunSynchronously());

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
