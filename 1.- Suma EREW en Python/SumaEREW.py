
import threading
import math

def main():

    A = [0,1,1,1,1,1,1,1,1]

    print("\nUniversidad Autónoma del Estado de México")
    print("Ingeniería en Computación")
    print("LINC33 Paradigmas de Programación II\n")
    print("Suma EREW (Exclusive-read & Exclusive-Write)")
    print(A,"\n")

    resultado = algoritmoSumaEREW(A)

    print(f"\nEl resultado de la suma es {resultado}\n")

def algoritmoSumaEREW(A):

    a = len(A) - 1
    log = int(math.log(a,2))
    threads = []

    for i in range(1,log+1):

        B = (a+1)*[None]
        resetearVectorB(A,B)

        for j in range(1,(int)(a/2)+1):
            thread = threading.Thread(target=realizarSuma,args=(i,j,A,B))
            threads.append(thread)
            thread.start()
        
        for hilo in threads:
            hilo.join()

        print(B," => ",A)

    return A[a]    

def realizarSuma(i,j,A,B):

    if(((2*j) % math.pow(2,i)) == 0):

        x = A[2*j]
        y = A[((2*j)-((int) (math.pow(2,i-1))))]

        B[2*j] = f"({x} + {y})"
        A[2*j] = x + y

def resetearVectorB(A,B):
    for i in range (0,len(A)-1): B[i] = str(A[i])

if __name__ == '__main__': main()

