import threading

def main():

    A = [0,16, 22, 35, 40, 53, 66, 70, 85, 15, 18, 23, 55, 60, 69, 72, 78]

    print("\nUniversidad Autónoma del Estado de México")
    print("Ingeniería en Computación")
    print("LINC33 Paradigmas de Programación II\n")
    print("Ordenamiento EREW (Exclusive-read & Exclusive-Write)")
    print(A,"\n")

    thread = threading.Thread(target=algoritmoOrdenamientoEREW, args=(A,))
    thread.start()
    thread.join()

    algoritmoOrdenamientoEREW(A)
    print("Arreglo ordenado:")
    print(A,"\n")

def algoritmoOrdenamientoEREW(L):

    if len(L) > 1:
        
        mid = len(L) // 2
        lefthalf = L[:mid]
        righthalf = L[mid:]
        
        threadA = threading.Thread(target=algoritmoOrdenamientoEREW, args=(lefthalf,))
        threadA.start()
        
        threadB = threading.Thread(target=algoritmoOrdenamientoEREW, args=(righthalf,))
        threadB.start()
        
        threadA.join()
        threadB.join()
        
        i = j = k = 0
        
        while i < len(lefthalf) and j < len(righthalf):
            if lefthalf[i] < righthalf[j]:
                L[k] = lefthalf[i]
                i += 1
            else:
                L[k] = righthalf[j]
                j += 1
            k += 1
            
        while i < len(lefthalf):
            L[k] = lefthalf[i]
            i += 1
            k += 1
                
        while j < len(righthalf):
            L[k] = righthalf[j]
            j += 1
            k += 1

if __name__ == '__main__': main()