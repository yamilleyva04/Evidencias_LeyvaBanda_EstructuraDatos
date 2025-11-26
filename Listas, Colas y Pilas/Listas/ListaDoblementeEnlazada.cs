using System;

class NodoDoble {
    public int data;
    public NodoDoble next;
    public NodoDoble prev;

    public NodoDoble(int data) {
        this.data = data;
        next = null;
        prev = null;
    }
}

class ListaDoblementeEnlazada {
    private NodoDoble head;
    private NodoDoble tail;

    public void InsertarAlPrincipio(int data) {
        NodoDoble nuevo = new NodoDoble(data);
        if (head == null) {
            head = tail = nuevo;
        } else {
            nuevo.next = head;
            head.prev = nuevo;
            head = nuevo;
        }
    }

    public void InsertarAlFinal(int data) {
        NodoDoble nuevo = new NodoDoble(data);
        if (tail == null) {
            head = tail = nuevo;
        } else {
            tail.next = nuevo;
            nuevo.prev = tail;
            tail = nuevo;
        }
    }

    public void Imprimir() {
        NodoDoble actual = head;
        while (actual != null) {
            Console.Write(actual.data + " <-> ");
            actual = actual.next;
        }
        Console.WriteLine("None");
    }

    public bool Buscar(int data) {
        NodoDoble actual = head;
        while (actual != null) {
            if (actual.data == data) return true;
            actual = actual.next;
        }
        return false;
    }

    public void Eliminar(int data) {
        NodoDoble actual = head;

        while (actual != null && actual.data != data)
            actual = actual.next;

        if (actual == null) return;

        if (actual.prev != null)
            actual.prev.next = actual.next;
        else
            head = actual.next;

        if (actual.next != null)
            actual.next.prev = actual.prev;
        else
            tail = actual.prev;
    }
}

class Program {
    static void Main() {
        ListaDoblementeEnlazada lista = new ListaDoblementeEnlazada();
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

        lista.Eliminar(30);
        lista.Imprimir();
    }
}
