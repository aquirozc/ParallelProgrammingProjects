import threading
import time

def main():

    A=[0,95,10,6,15]

    print("\nUniversidad Autónoma del Estado de México")
    print("Ingeniería en Computación")
    print("LINC33 Paradigmas de Programación II\n")
    print("Ordenamiento CRCW (Concurrent-read & Concurrent-Write)")
    print(A,"\n")

    algoritmoOrdenamientoCRCW(A)
    print("Arreglo ordenado:")
    print(A,"\n")

def algoritmoOrdenamientoCRCW(L):

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
        thread = threading.Thread(target=hiloOrdenamiento,args=(L,win,i))
        threads.append(thread)
        thread.start()
    for hilo in threads:
        hilo.join()

    return win[0]

def hiloNormalizacion(W,i):
    W[i] = 0

def hiloComparacion(L,W,i,j):
    if(L[i] > L[j]):
        W[i] += 1
    else:
        W[j] += 1

def hiloOrdenamiento(L,W,i):
    x = L[i]
    time.sleep(0.001)
    L[1 + W[i]] = x
        
if __name__ == '__main__': main()
