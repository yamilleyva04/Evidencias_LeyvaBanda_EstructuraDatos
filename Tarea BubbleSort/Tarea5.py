def bubblesort(arreglo):
    n = len(arreglo)
    for i in range(n):
        intercambio = False
        for j in range(0, n - i - 1):
            if arreglo[j] > arreglo[j + 1]:
                arreglo[j], arreglo[j + 1] = arreglo[j + 1], arreglo[j]
                intercambio = True
        if not intercambio:
            break       
arreglo = [56, 32, 17, 99, 23, 10, 78]
print("Arreglo original:", arreglo)
bubblesort(arreglo)
print("Arreglo ordenada:", arreglo)