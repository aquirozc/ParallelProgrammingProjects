using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MultiplicacionDeMatricesCREW{

    public static void Main(){

        int [,] A = {{0,0,0},{0,4,5},{0,3,2}};
        int [,] B = {{0,0,0},{0,4,5},{0,3,2}};

        int n = A.GetLength(0);
        int[,,] C = new int[n,n,n];

        System.Console.WriteLine("\nUniversidad Autónoma del Estado de México");
        System.Console.WriteLine("Ingeniería en Computación");
        System.Console.WriteLine("LINC33 Paradigmas de Programación II\n");
       
        System.Console.WriteLine("Multiplicación de matrices CREW (Concurrent-read & Exclusive-Write)\n");

        MultMatriz(C,A,B);

    }


    public static int[,,] MultMatriz(int[,,] C, int[,] A, int[,] B){

        int n = C.GetLength(0);
        int m = n-1;

        string[,,] S = new string[m,m,m];
        int[,,] R = new int[m,m,m];

        List<Task> ListaDeTareas;

        for(int k = 1; k <= n; k++){

            for(int i = 1; i <= n; i++){

                ListaDeTareas = new List<Task>();

                for(int j = 1; j <= n; j++){

                    int x = i;
                    int y = j;
                    int z = k;

                    ListaDeTareas.Add(new Task(

                        () => {
                            
                            C[x,y,z] = A[x,z] * B[z,y];

                            S[x-1,y-1,z-1] =$"C[{x},{y},{z}] = (A[{x},{z}] => {A[x,z]}) * (B[{z},{y}] => {B[z,y]})";
                            R[x-1,y-1,z-1] = C[x,y,z];

                        }
                        
                    ));

                }

                Parallel.ForEach(ListaDeTareas, tarea => tarea.RunSynchronously());

            }

        }

       
        Console.WriteLine("Paso 1:\n");
        ImprimirRenglonDesglosado(S,R);

        S = new string[m,m,m-1];
        R = new int[m,m,m-1];

        for(int k = 2; k <= n; k++){

            for(int i = 1; i <= n; i++){

                ListaDeTareas = new List<Task>();

                for(int j = 1; j <= n; j++){

                    int x = i;
                    int y = j;
                    int z = k;

                    ListaDeTareas.Add(new Task(
                        
                        () => {
                            
                            C[x,y,z] = C[x,y,z] + C[x,y,z-1];
                            S[x-1,y-1,z-2] =$"C[{x},{y},{z}] = C[{x},{y},{z}] + C[{x},{y},{z-1}]";
                            R[x-1,y-1,z-2] = C[x,y,z];

                        }

                    ));

                }

                Parallel.ForEach(ListaDeTareas, tarea => tarea.RunSynchronously());

            }

        }

        Console.WriteLine("Paso 2:\n");
        ImprimirRenglonDesglosado(S,R);

        return R;

    }

    private static void ImprimirRenglonDesglosado(string[,,] S, int[,,] R){

            int SMax = FindMaxLenghtInS(S);
            int RMax = FindMaxLenghtInR(R);

            for(int k = 0; k <= S.GetLength(2) - 1; k++){

                for(int i = 0; i<= S.GetLength(0) - 1; i++){

                    Console.Write("| ");

                    for(int j = 0; j <=  S.GetLength(1) - 1; j++){

                        string salida;

                        salida = S[i,j,k];
                        salida = salida.PadRight(SMax,' ');

                        if(j != S.GetLength(1) - 1){
                            salida += ", "; 
                        }

                        Console.Write(salida);

                    }

                    Console.Write(" | === | ");


                    for(int j = 0; j <= S.GetLength(1) - 1; j++){

                        string salida;

                        salida = R[i,j,k].ToString();
                        salida = salida.PadRight(RMax,' ');

                        if(j != S.GetLength(1) - 1){
                            salida += ", "; 
                        }

                        Console.Write(salida);

                    }

                    Console.Write(" |\n");

                }

                Console.Write("\n");

            }
        
    }

    private static int FindMaxLenghtInS(string[,,] S){

        int MaxLength = S[0,0,0].Length;

        foreach(string Element in S){

            if(Element.Length > MaxLength){

                MaxLength = Element.Length;

            }

        }

        return MaxLength;

    }

    private static int FindMaxLenghtInR(int[,,]R){

        int MaxLength = R[0,0,0].ToString().Length;

        foreach(int Element in R){

            if(Element.ToString().Length > MaxLength){

                MaxLength = Element.ToString().Length;

            }

        }

        return MaxLength;


    }

}