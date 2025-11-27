using System;

class Nodo {
    public int data;
    public Nodo next;

    public Nodo(int d) {
        data = d;
        next = null;
    }
}

class ColaLista {
    private Nodo front, rear;

    public ColaLista() {
        front = null;
        rear = null;
    }

    public bool IsEmpty() {
        return front == null;
    }

    public void Enqueue(int data) {
        Nodo nuevo = new Nodo(data);
        if (rear == null) {
            front = nuevo;
            rear = nuevo;
            return;
        }
        rear.next = nuevo;
        rear = nuevo;
    }

    public int Dequeue() {
        if (IsEmpty()) {
            Console.WriteLine("Cola vacía (Underflow)");
            return -1;
        }
        int d = front.data;
        front = front.next;
        if (front == null) rear = null;
        return d;
    }

    public int Peek() {
        if (IsEmpty()) {
            Console.WriteLine("Cola vacía");
            return -1;
        }
        return front.data;
    }
}

class Program {
    static void Main() {
        ColaLista cola = new ColaLista();
        cola.Enqueue(10);
        cola.Enqueue(20);
        cola.Enqueue(30);

        Console.WriteLine("Elemento frontal: " + cola.Peek());
        Console.WriteLine("Elimina: " + cola.Dequeue());
        Console.WriteLine("Nuevo frontal: " + cola.Peek());
    }
}
