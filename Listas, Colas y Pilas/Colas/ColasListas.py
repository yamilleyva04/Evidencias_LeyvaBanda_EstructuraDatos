class Nodo:
    def __init__(self, data):
        self.data = data
        self.next = None

class ColaLista:
    def __init__(self):
        self.front = None
        self.rear = None

    def isEmpty(self):
        return self.front is None

    def enqueue(self, data):
        nuevo = Nodo(data)
        if self.rear is None:
            self.front = nuevo
            self.rear = nuevo
            return
        self.rear.next = nuevo
        self.rear = nuevo

    def dequeue(self):
        if self.isEmpty():
            print("Cola vacía (Underflow)")
            return -1
        data = self.front.data
        self.front = self.front.next
        if self.front is None:
            self.rear = None
        return data

    def peek(self):
        if self.isEmpty():
            print("Cola vacía")
            return -1
        return self.front.data

cola = ColaLista()
cola.enqueue(10)
cola.enqueue(20)
cola.enqueue(30)

print("Elemento frontal:", cola.peek())
print("Elimina:", cola.dequeue())
print("Nuevo frontal:", cola.peek())
