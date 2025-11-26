class Nodo {
    int data;
    Nodo next;

    public Nodo(int data) {
        this.data = data;
        this.next = null;
    }
}

class ListaSimple {
    private Nodo head;

    public void insertarAlPrincipio(int data) {
        Nodo nuevo = new Nodo(data);
        nuevo.next = head;
        head = nuevo;
    }

    public void insertarAlFinal(int data) {
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

    public void imprimir() {
        Nodo actual = head;
        while (actual != null) {
            System.out.print(actual.data + " -> ");
            actual = actual.next;
        }
        System.out.println("None");
    }

    public boolean buscar(int data) {
        Nodo actual = head;
        while (actual != null) {
            if (actual.data == data) return true;
            actual = actual.next;
        }
        return false;
    }

    public void eliminar(int data) {
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

public class ListaEnlazada {
    public static void main(String[] args) {

        ListaSimple lista = new ListaSimple();

        lista.insertarAlFinal(10);
        lista.insertarAlFinal(20);
        lista.insertarAlFinal(30);
        lista.imprimir();

        lista.insertarAlPrincipio(5);
        lista.imprimir();

        System.out.println("¿Está el 20? " + lista.buscar(20));
        System.out.println("¿Está el 99? " + lista.buscar(99));

        lista.eliminar(20);
        lista.imprimir();

        lista.eliminar(5);
        lista.imprimir();
    }
}
