MAXSIZE = 5
queue = [None] * MAXSIZE
front = -1
rear = -1

def insertar():
    global front, rear
    elemento = int(input("Ingrese el elemento: "))

    if rear == MAXSIZE - 1:
        print("OVERFLOW")
        return

    if front == -1 and rear == -1:
        front = rear = 0
    else:
        rear += 1

    queue[rear] = elemento
    print("Elemento insertado.")

def eliminar():
    global front, rear
    if front == -1 or front > rear:
        print("UNDERFLOW")
        return

    elemento = queue[front]
    if front == rear:
        front = rear = -1
    else:
        front += 1

    print("Elemento eliminado:", elemento)

def mostrar():
    if front == -1 or front > rear:
        print("La cola está vacía.")
        return

    print("Elementos:")
    for i in range(front, rear + 1):
        print(queue[i])
