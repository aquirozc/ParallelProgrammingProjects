import math
import threading

def main():

    A =[5,2,-1,23,-4,2,5,-2,0,5,1,5,-5,8,5,3,-2]

    print("\nUniversidad Autónoma del Estado de México")
    print("Ingeniería en Computación")
    print("LINC33 Paradigmas de Programación II\n")
    print("Busqueda EREW (Exclusive-read & Exclusive-Write)")
    print(A,"\n")

    res = algoritmoBusquedaEREW(A)

    if (res != float("inf")): print(f"El valor {A[0]} se encuentra en la posición {res}\n")
    else: print(f"El valor {A[0]} no se encuentra dentro del arreglo\n")

def algoritmoBusquedaEREW(A):

    n = len(A)
    Temp = n*[0]
    threads = [] 

    difusion(Temp,A[0])

    for i in range(1,n):
        thread = threading.Thread(target=hiloBusqueda,args=(A,Temp,i))
        threads.append(thread)
        thread.start()

    for hilo in threads:
        hilo.join() 

    return minimo(Temp)

def difusion(L,x):
    L[0] = x
    log = math.log(len(L),2)
    threads = []

    for i in range(1,int(log+1)):
        for j in range(int(math.pow(2,i-1)), int(math.pow(2,i))):
            x = i
            y = j
            thread = threading.Thread(target=hiloDifusion,args=(L,y,y-int(math.pow(2,x-1))))
            threads.append(thread)
            thread.start()
        for hilo in threads:
            hilo.join()

def minimo(L):
    n = len(L)
    for j in range(1,int(math.log(n,2))+1):
        threads =[]
        for i in range(1,int(n/math.pow(2,j)) + 1):
            thread = threading.Thread(target=hiloMinimo,args=(L,i))
            threads.append(thread)
            thread.start()
        for hilo in threads:
            hilo.join()

    return L[1]

def hiloDifusion(L,x,y):
    L[x] = L[y]

def hiloBusqueda(A,L,i):
    if(A[i] == L[i]):
        L[i] = i + 1
    else:
        L[i] = float("inf")

def hiloMinimo(L,i):
    if(L[2*i-1] > L[2*i]):
         L[i] = L[2*i]
    else:
        L[i] = L[2*i-1]


if __name__ == '__main__': main()
