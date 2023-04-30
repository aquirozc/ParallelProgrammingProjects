import threading

def main():

    A = [[0,0,0],[0,4,5],[0,3,2]]
    B = [[0,0,0],[0,4,5],[0,3,2]]

    n = len(A[0])
    C = [[[0 for k in range(n)] for j in range(n)] for i in range(n)]

    print("\nUniversidad Autónoma del Estado de México")
    print("Ingeniería en Computación")
    print("LINC33 Paradigmas de Programación II\n")
    print("Multiplicación de matrices EREW (Concurrent-read & Exclusive-Write)")

    multMatriz(C,A,B)

def multMatriz(C,A,B):

    n = len(C[0])
    m = n - 1

    S = [[["" for k in range(m)] for j in range(m)] for i in range(m)]
    R = [[[0 for k in range(m)] for j in range(m)] for i in range(m)]

    for k in range(1,n):
        for i in range(1,n):
            threads = []
            for j in range(1,n):
                thread = threading.Thread(target=hiloCalcularPaso01,args=(C,A,B,S,R,i,j,k))
                threads.append(thread)
                thread.start()
            for hilo in threads:
                hilo.join()

    print("Paso 01:")
    imprimirProcedimientoDesglosado(S,R)

    S = [[["" for k in range(m-1)] for j in range(m)] for i in range(m)]
    R = [[[0 for k in range(m-1)] for j in range(m)] for i in range(m)]

    for k in range(2,n):
        for i in range(1,n):
            threads = []
            for j in range(1,n):
                thread = threading.Thread(target=hiloCalcularPaso02,args=(C,A,B,S,R,i,j,k))
                threads.append(thread)
                thread.start()
            for hilo in threads:
                hilo.join()

    print("Paso 02:")
    imprimirProcedimientoDesglosado(S,R)
    

def hiloCalcularPaso01(C,A,B,S,R,x,y,z):
    C[x][y][z] = A[x][z] * B[z][y]
    S[x-1][y-1][z-1] =f"C[{x},{y},{z}] = (A[{x},{z}] => {A[x][z]}) * (B[{z},{y}] => {B[z][y]})"
    R[x-1][y-1][z-1] = C[x][y][z]

def hiloCalcularPaso02(C,A,B,S,R,x,y,z):
    C[x][y][z] = C[x][y][z] + C[x][y][z-1]
    S[x-1][y-1][z-2] = f"C[{x},{y},{z}] = C[{x},{y},{z}] + C[{x},{y},{z-1}]"
    R[x-1][y-1][z-2] = C[x][y][z]

def imprimirProcedimientoDesglosado(S,R):
    for k in range(len(S[0][0])):
        for i in range(len(S)):
            print("| ", end="")
            for j in range(len(S[0])):
                salida = S[i][j][k]
                if j != len(S[0]) - 1:
                    salida += ", "
                print(salida, end="")
            print(" | === | ", end="")
            for j in range(len(S[0])):
                salida = str(R[i][j][k])
                salida = str(salida).ljust(4)
                if j != len(R[0]) - 1:
                    salida += ", "
                print(salida, end="")
            print(" |")
        print()

if __name__ == '__main__': main()
