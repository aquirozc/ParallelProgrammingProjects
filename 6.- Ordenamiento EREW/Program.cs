
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class OrdenamientoEREW{

    public static void Main(){

        System.Console.WriteLine("\nUniversidad Autónoma del Estado de México");
        System.Console.WriteLine("Ingeniería en Computación");
        System.Console.WriteLine("LINC33 Paradigmas de Programación II\n");
       
        System.Console.WriteLine("Ordenamiento EREW (Exclusive-read & Exclusive-Write)");

        int[] demo = {0,16, 22, 35, 40, 53, 66, 70, 85, 15, 18, 23, 55, 60, 69, 72, 78};
        ImprimirVectorNumerico(demo);
        System.Console.WriteLine();

        MergeSort(demo,1, demo.Length-1);
        ImprimirVectorNumerico(demo);
        System.Console.WriteLine();
        
    }

    public static void MergeSort(int[] L, int A, int B)
    {
        int PuntoMedio = (B + A) / 2;
        if (A< B)
        {
            Parallel.Invoke(() => {
                MergeSort(L, A, PuntoMedio);
                MergeSort(L, PuntoMedio + 1, B);
                OddEvenMerge(L, A, B);
            });
        }
    }

    public static void Interchange(int[] L, int a, int b)
    {
        int aux;
        aux = L[a];
        L[a] = L[b];
        L[b] = aux;

    }
    public static void OddEvenSplit(int[] L, int[] odd, int[] even, int ini, int fin)
    {
        int cont1 = 1, cont2 = 1;
        Parallel.For(0, 1, f => {
            for (int i = ini; i <= fin; i++)
            {
                if ((i % 2) == 0)
                {
                    even[cont1] = L[i];
                    cont1++;
                }
                else
                {
                    odd[cont2] = L[i];
                    cont2++;
                }
            }
        });
    }

    public static void OddEvenMerge(int[] L, int ini, int fin)
    {
        int n = fin - ini + 1;
        int[] odd = new int[16];
        int[] even = new int[16];
        if (n == 2)
        {
            if (L[ini] > L[fin])
            {
                Interchange(L, ini, fin);
            }
        }
        else
        {

            OddEvenSplit(L, odd, even, ini, fin);
            Parallel.Invoke(() => {
                OddEvenMerge(odd, 1, (n / 2));
                OddEvenMerge(even, 1, (n / 2));
            });
            int aux2 = fin - n;

            Parallel.For(0, 1, f => {
                for (int i = 1; i <= (n / 2); i++)
                {

                    Intercala(L, odd, even, i, aux2);
                }
            });


            for (int i = ini; i <= ((fin / 2) - 1); i++)
            {
                int a, b;
                a = 2 * i;
                b = (2 * i) + 1;
                if ((L[a]) > (L[b]))
                {
                    Interchange(L, a, b);
                }
            }
        }

    }
    public static void Intercala(int[] m, int[] odd, int[] even, int i, int aux)
    {
        m[(2 * i) - 1 + aux] = odd[i];
        m[(2 * i) + aux] = even[i];
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