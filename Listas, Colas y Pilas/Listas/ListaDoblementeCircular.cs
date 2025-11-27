using System;

class NodoDoble {
    public int data;
    public NodoDoble next;
    public NodoDoble prev;

    public NodoDoble(int d) {
        data = d;
        next = null;
        prev = null;
    }
}

class ListaDoblementeCircular {
    private NodoDoble head;

    public void InsertarAlPrincipio(int data) {
        NodoDoble nuevo = new NodoDoble(data);

        if (head == null) {
            head = nuevo;
            head.next = head;
            head.prev = head;
        } else {
            NodoDoble tail = head.prev;
            nuevo.next = head;
            head.prev = nuevo;
            head = nuevo;
            head.prev = tail;
            tail.next = head;
        }
    }

    public void InsertarAlFinal(int data) {
        NodoDoble nuevo = new NodoDoble(data);

        if (head == null) {
            head = nuevo;
            head.next = head;
            head.prev = head;
        } else {
            NodoDoble tail = head.prev;
            tail.next = nuevo;
            nuevo.prev = tail;
            nuevo.next = head;
            head.prev = nuevo;
        }
    }

    public void Imprimir() {
        if (head == null) {
            Console.WriteLine("None");
            return;
        }

        NodoDoble actual = head;

        do {
            Console.Write(actual.data + " <-> ");
            actual = actual.next;
        } while (actual != head);

        Console.WriteLine("(vuelve a " + head.data + ")");
    }

    public bool Buscar(int data) {
        if (head == null) return false;

        NodoDoble actual = head;
        do {
            if (actual.data == data) return true;
            actual = actual.next;
        } while (actual != head);

        return false;
    }

    public void Eliminar(int data) {
        if (head == null) return;

        NodoDoble actual = head;

        do {
            if (actual.data == data) break;
            actual = actual.next;
        } while (actual != head);

        if (actual.data != data) return;

        if (actual == head && head.next == head) {
            head = null;
            return;
        }

        if (actual == head)
            head = actual.next;

        actual.prev.next = actual.next;
        actual.next.prev = actual.prev;
    }
}

class Program {
    static void Main() {
        ListaDoblementeCircular lista = new ListaDoblementeCircular();

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
