class NodoDoble {
    int data;
    NodoDoble next;
    NodoDoble prev;

    public NodoDoble(int data) {
        this.data = data;
        next = null;
        prev = null;
    }
}

class ListaDoble {
    private NodoDoble head;
    private NodoDoble tail;

    public void insertarAlPrincipio(int data) {
        NodoDoble nuevo = new NodoDoble(data);
        if (head == null) {
            head = tail = nuevo;
        } else {
            nuevo.next = head;
            head.prev = nuevo;
            head = nuevo;
        }
    }

    public void insertarAlFinal(int data) {
        NodoDoble nuevo = new NodoDoble(data);
        if (tail == null) {
            head = tail = nuevo;
        } else {
            tail.next = nuevo;
            nuevo.prev = tail;
            tail = nuevo;
        }
    }

    public void imprimir() {
        NodoDoble actual = head;
        while (actual != null) {
            System.out.print(actual.data + " <-> ");
            actual = actual.next;
        }
        System.out.println("None");
    }

    public boolean buscar(int data) {
        NodoDoble actual = head;
        while (actual != null) {
            if (actual.data == data) return true;
            actual = actual.next;
        }
        return false;
    }

    public void eliminar(int data) {
        NodoDoble actual = head;

        while (actual != null && actual.data != data) {
            actual = actual.next;
        }

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

public class ListaDoblementeEnlazada {
    public static void main(String[] args) {
        ListaDoble lista = new ListaDoble();

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

        lista.eliminar(30);
        lista.imprimir();
    }
}
