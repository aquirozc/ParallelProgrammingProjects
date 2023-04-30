import math
import threading

def main():

    A=[0,95,10,6,15]

    print("\nUniversidad Autónoma del Estado de México")
    print("Ingeniería en Computación")
    print("LINC33 Paradigmas de Programación II\n")
    print("Busqueda CRCW (Concurrent-read & Concurrent-Write)")
    print(A,"\n")

    index = minCRCW(A)

    print(f"El elemento mínimo es el {A[index]} y se encuentra en la posición {index+1}\n")

def minCRCW(L):

    n = len(L)
    win = n*[0]
    threads = []

    for i in range(1,n):
        thread = threading.Thread(target=hiloNormalizacion,args=(win,i))
        threads.append(thread)
        thread.start()
    for hilo in threads:
        hilo.join()

    for j in range(1,n):
        for i in range(1,j):
            thread = threading.Thread(target=hiloComparacion,args=(L,win,i,j))
            threads.append(thread)
            thread.start()
    for hilo in threads:
        hilo.join()

    for i in range (1,n):
        thread = threading.Thread(target=hiloElementoMinimo,args=(win,i))
        threads.append(thread)
        thread.start()
    for hilo in threads:
        hilo.join()

    return win[0]

def hiloNormalizacion(W,i):
    W[i] = 0

def hiloComparacion(L,W,i,j):
    if(L[i] > L[j]):
        W[i] = 1
    else:
        W[j] = 1

def hiloElementoMinimo(W,i):
    if(W[i] == 0):
        W[0] = i
        
if __name__ == '__main__': main()