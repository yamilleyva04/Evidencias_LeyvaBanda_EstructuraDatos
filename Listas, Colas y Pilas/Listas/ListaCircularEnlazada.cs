using System;

class Nodo {
    public int data;
    public Nodo next;

    public Nodo(int d) {
        data = d;
        next = null;
    }
}

class ListaCircularEnlazada {
    private Nodo head;

    public void InsertarAlPrincipio(int data) {
        Nodo nuevo = new Nodo(data);

        if (head == null) {
            head = nuevo;
            nuevo.next = head;
        } else {
            Nodo temp = head;
            while (temp.next != head)
                temp = temp.next;

            nuevo.next = head;
            head = nuevo;
            temp.next = head;
        }
    }

    public void InsertarAlFinal(int data) {
        Nodo nuevo = new Nodo(data);

        if (head == null) {
            head = nuevo;
            nuevo.next = head;
        } else {
            Nodo temp = head;
            while (temp.next != head)
                temp = temp.next;

            temp.next = nuevo;
            nuevo.next = head;
        }
    }

    public void Imprimir() {
        if (head == null) {
            Console.WriteLine("None");
            return;
        }

        Nodo temp = head;
        do {
            Console.Write(temp.data + " -> ");
            temp = temp.next;
        } while (temp != head);

        Console.WriteLine("(vuelve a " + head.data + ")");
    }

    public bool Buscar(int data) {
        if (head == null) return false;

        Nodo temp = head;
        do {
            if (temp.data == data) return true;
            temp = temp.next;
        } while (temp != head);

        return false;
    }

    public void Eliminar(int data) {
        if (head == null) return;

        if (head.data == data) {
            if (head.next == head) {
                head = null;
                return;
            }

            Nodo temp = head;
            while (temp.next != head)
                temp = temp.next;

            temp.next = head.next;
            head = head.next;
            return;
        }

        Nodo prev = head;
        Nodo curr = head.next;

        while (curr != head) {
            if (curr.data == data) {
                prev.next = curr.next;
                return;
            }
            prev = curr;
            curr = curr.next;
        }
    }
}

class Program {
    static void Main() {
        ListaCircularEnlazada lista = new ListaCircularEnlazada();

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
