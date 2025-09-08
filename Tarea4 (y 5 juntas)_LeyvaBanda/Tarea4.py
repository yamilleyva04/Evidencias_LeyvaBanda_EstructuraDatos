matriz = [
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]
]

print("Matriz original:")
for fila in matriz:
    print("[", " ".join(str(x) for x in fila), "]")

print("\nMatriz en filas:")
for fila in matriz:
    for valor in fila:
        print(valor, end=" ")
print()

print("\nMatriz en columnas:")
for j in range(3):
    for i in range(3):
        print(matriz[i][j], end=" ")
print()
