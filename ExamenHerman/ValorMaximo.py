def encontrar_maximo(matriz):
    valor_maximo = -1
    posicion_max = (-1, -1) 

    for i, fila in enumerate(matriz):
        for j, valor in enumerate(fila):
            if valor > valor_maximo:
                valor_maximo = valor
                posicion_max = (i, j) 
    return valor_maximo, posicion_max[0], posicion_max[1]

if __name__ == "__main__":
    arr = [
        [12, 45, 23, 67, 89, 10],
        [34, 9, 56, 78, 91, 22],
        [4, 68, 99, 31, 47, 63],
        [88, 19, 42, 73, 5, 50],
        [29, 81, 14, 37, 95, 2],
        [71, 25, 58, 8, 60, 93]
    ]

    print("Arreglo:")
    for fila in arr:
        print(fila)

    valor_max, fila_max, columna_max = encontrar_maximo(arr)

    print(f"\nEl valor más alto en la matriz es: {valor_max} y se encuentra en la fila {fila_max + 1}, columna {columna_max + 1}.")