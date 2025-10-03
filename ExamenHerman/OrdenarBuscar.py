
def buscar_valor(arreglo, valor):
    for i, elemento in enumerate(arreglo):
        if elemento == valor:
            return i
    return -1

def ordenamiento(arr):
  
    for i in range(1, len(arr)):
        valor_actual = arr[i]
        posicion = i

        while posicion > 0 and arr[posicion - 1] > valor_actual:
            arr[posicion] = arr[posicion - 1]
            posicion -= 1
        
        arr[posicion] = valor_actual

def main():
    arr = [1,4,5,89,34,36,79,92,102,20]
    print(f"Arreglo original: {arr}")

    ordenamiento(arr)
    print(f"Arreglo ordenado: {arr}")

    while True:
        try:
            valor_buscado = int(input('\nIngrese el valor a buscar: '))

            indice_encontrado = buscar_valor(arr, valor_buscado)

            if indice_encontrado != -1:
                print(f"El numero {valor_buscado} se encontró en la posición: {indice_encontrado + 1}")
            else:
                print(f"El numero {valor_buscado} no se encontró en el arreglo.")
        except ValueError:
            print("Por favor, ingrese un número válido.")
        respuesta = input("\n¿Desea realizar otra búsqueda? (s/n): ")
        if respuesta.lower() != 's':
            break

if __name__ == "__main__":
    main()