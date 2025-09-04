arr = [10, 20, 30, 40, 50]
print("Arreglo inicial:", arr)
for i in range(len(arr)):
    print(f"arr[{i}] = {arr[i]}")
pos = int(input("Ingrese la posición donde quiere insertar: "))
val = int(input("Ingrese el valor a insertar: "))
arr.insert(pos, val)
print("Arreglo despues de insertar:", arr)
buscar = int(input("Ingrese el valor a buscar: "))
encontrado = -1
for i in range(len(arr)):
    if arr[i] == buscar:
        encontrado = i
        break

if encontrado != -1:
    print(f"Elemento {buscar} encontrado en {encontrado}")
else:
    print("Elemento no encontrado")
