using System;

class Nodo {
    public int data;
    public Nodo next;

    public Nodo(int d) {
        data = d;
        next = null;
    }
}

class PilaLista {
    private Nodo top;

    public bool IsEmpty() {
        return top == null;
    }

    public void Push(int data) {
        Nodo nuevo = new Nodo(data);
        nuevo.next = top;
        top = nuevo;
    }

    public int Pop() {
        if (IsEmpty()) {
            Console.WriteLine("Error: Stack Underflow");
            return -1;
        }
        int d = top.data;
        top = top.next;
        return d;
    }

    public int Peek() {
        if (IsEmpty()) {
            Console.WriteLine("Pila vacía");
            return -1;
        }
        return top.data;
    }
}

class Program {
    static void Main() {
        PilaLista pila = new PilaLista();
        pila.Push(10);
        pila.Push(20);
        pila.Push(30);

        Console.WriteLine("Elemento superior: " + pila.Peek());
        Console.WriteLine("Extrae elemento: " + pila.Pop());
        Console.WriteLine("Nuevo elemento superior: " + pila.Peek());
    }
}
