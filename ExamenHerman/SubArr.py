def ordenar_ascendente(subarreglo):
    arr = subarreglo[:]
    for i in range(1, len(arr)):
        temp = arr[i]
        j = i - 1
        while j >= 0 and temp < arr[j]:
            arr[j + 1] = arr[j]
            j -= 1
        arr[j + 1] = temp
    return arr

def ordenar_descendente(subarreglo):
    arr = subarreglo[:]
    for i in range(1, len(arr)):
        temp = arr[i]
        j = i - 1
        while j >= 0 and temp > arr[j]:
            arr[j + 1] = arr[j]
            j -= 1
        arr[j + 1] = temp
    return arr

def ordenar_alternativo(subarreglo):
    ordenado = ordenar_ascendente(subarreglo)
    resultado_alternativo = []
    izquierda, derecha = 0, len(ordenado) - 1
    while izquierda <= derecha:
        if izquierda == derecha:
            resultado_alternativo.append(ordenado[izquierda])
            break
        resultado_alternativo.append(ordenado[izquierda])
        resultado_alternativo.append(ordenado[derecha])
        
        izquierda += 1
        derecha -= 1
        
    return resultado_alternativo
if __name__ == "__main__":
    cubo = [
       
        [
            [8, 68, 86, 88, 96], 
            [29, 73, 33, 84, 4], 
            [28, 11, 44, 53, 22], 
            [52, 1, 38, 7, 61],
              [99, 50, 48, 6, 71]
        ],
       
        [
            [15, 81, 5, 76, 41],
              [64, 25, 36, 91, 18], 
              [49, 70, 58, 2, 83], 
              [30, 62, 12, 93, 55], 
              [77, 3, 40, 67, 21]
        ],
       
        [
            [87, 39, 17, 51, 24], 
            [66, 9, 74, 32, 89], 
            [13, 59, 92, 20, 43], 
            [98, 35, 60, 16, 75], 
            [27, 82, 46, 56, 10]
        ],
        
        [
            [31, 94, 65, 23, 54], 
            [80, 19, 42, 90, 37], 
            [72, 57, 26, 85, 14], 
            [47, 97, 34, 63, 95], 
            [69, 45, 79, 100, 28]
        ],
        
        [
            [20, 1, 3, 15, 12],
            [88, 53, 41, 30, 22], 
            [9, 18, 27, 36, 45], 
            [50, 60, 70, 80, 90], 
            [99, 81, 64, 49, 31]
        ]
    ]

    for i, capa in enumerate(cubo):
        print(f"Arreglo #{i + 1}")
        print("\n" )
        for j, fila in enumerate(capa):
            print(f"  Fila #{j + 1} (del arreglo{i + 1}): {fila}")
            
            asc = ordenar_ascendente(fila)
            desc = ordenar_descendente(fila)
            alt = ordenar_alternativo(fila)
            
            print(f"Ascendente:  {asc}")
            print(f"Descendente: {desc}")
            print(f"Alternativo: {alt}\n")