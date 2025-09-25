def main():
    numeros = []
    par = 0
    imp = 0
    suma = 0
    print("Da 10 números:")
    for i in range(10):
        num = int(input(f"Número {i+1}: "))
        numeros.append(num)
        suma += num

        if num % 2 == 0:
            par += 1
        else:
            imp += 1
    prom = suma / 10
    print("\n--- Resultados ---")
    print("Números:", numeros)
    print("Pares:", par)
    print("Impares:", imp)
    print("Promedio:", prom)


if __name__ == "__main__":
    main()

