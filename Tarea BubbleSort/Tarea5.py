import random
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
arreglo = [random.randint(1, 100) for _ in range(10)]

print("Arreglo original:", arreglo)
bubblesort(arreglo)
print("Arreglo ordenado:", arreglo)
