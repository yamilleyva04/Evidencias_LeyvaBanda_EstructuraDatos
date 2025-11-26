using System;

class Nodo {
    public int data;
    public Nodo next;

    public Nodo(int data) {
        this.data = data;
        this.next = null;
    }
}

class ListaEnlazada {
    private Nodo head;

    public void InsertarAlPrincipio(int data) {
        Nodo nuevo = new Nodo(data);
        nuevo.next = head;
        head = nuevo;
    }

    public void InsertarAlFinal(int data) {
        Nodo nuevo = new Nodo(data);
        if (head == null) {
            head = nuevo;
            return;
        }

        Nodo ultimo = head;
        while (ultimo.next != null) {
            ultimo = ultimo.next;
        }
        ultimo.next = nuevo;
    }

    public void Imprimir() {
        Nodo actual = head;
        while (actual != null) {
            Console.Write(actual.data + " -> ");
            actual = actual.next;
        }
        Console.WriteLine("None");
    }

    public bool Buscar(int data) {
        Nodo actual = head;
        while (actual != null) {
            if (actual.data == data) return true;
            actual = actual.next;
        }
        return false;
    }

    public void Eliminar(int data) {
        Nodo actual = head;
        Nodo previo = null;

        if (actual != null && actual.data == data) {
            head = actual.next;
            return;
        }

        while (actual != null && actual.data != data) {
            previo = actual;
            actual = actual.next;
        }

        if (actual == null) return;

        previo.next = actual.next;
    }
}

class Program {
    static void Main() {
        ListaEnlazada lista = new ListaEnlazada();
        lista.InsertarAlFinal(10);
        lista.InsertarAlFinal(20);
        lista.InsertarAlFinal(30);
        lista.Imprimir();

        lista.InsertarAlPrincipio(5);
        lista.Imprimir();

        Console.WriteLine("¿Está el 20? " + lista.Buscar(20));
        Console.WriteLine("¿Está el 99? " + lista.Buscar(99));

        lista.Eliminar(20);
        lista.Imprimir();

        lista.Eliminar(5);
        lista.Imprimir();
    }
}
