nombres = ["Ana", "Luis", "Carlos", "María"]
nombres[1] = "Pedro"

for nombre in nombres:
    print(nombre)


tamaño = int(input("¿Cuántos nombres quieres agregar?: "))
arr = []

for i in range(tamaño):
    valor = input(f"Ingrese el nombre {i+1}: ")
    arr.append(valor)

print("Lista final:", arr)  
