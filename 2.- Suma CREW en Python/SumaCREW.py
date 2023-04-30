import math
import threading
import time

def main():

    A =[0,5,2,10,1,8,12,7,3]

    print("\nUniversidad Autónoma del Estado de México")
    print("Ingeniería en Computación")
    print("LINC33 Paradigmas de Programación II\n")
    print("Suma CREW (Concurrent-read & Exclusive-Write)")
    print(A,"\n")

    resultado = algoritmoSumaCREW(A)

    print(f"\nEl resultado de la suma es {resultado}\n")

def algoritmoSumaCREW(A):
    a = len(A) - 1
    log = int(math.log(a,2))
    threads = []

    for i in range(1,log+1):
        B = (a+1)*[None]
        resetearVectorB(A,B)

        for j in range(int(math.pow(2,i-1)),a + 1):
            thread = threading.Thread(target=realizarSuma,args=(i,j,A,B))
            threads.append(thread)
            thread.start()

        for hilo in threads:
            hilo.join()

        print(B," => ", A)
    return A[a]   

def realizarSuma(i,j,A,B):
    x = A[j]
    y = A[j-int(math.pow(2,i-1))]
    time.sleep(0.001)
    A[j] = x + y
    B[j] = f"({x} + {y})"

def resetearVectorB(A,B):
    for i in range (0,len(A)-1): B[i] = str(A[i])

if __name__ == '__main__': main()