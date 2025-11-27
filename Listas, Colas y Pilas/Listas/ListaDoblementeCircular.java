class NodoDoble {
    int data;
    NodoDoble next;
    NodoDoble prev;

    public NodoDoble(int d) {
        data = d;
        next = null;
        prev = null;
    }
}

public class ListaDoblementeCircular {
    private NodoDoble head;

    public void insertarAlPrincipio(int data) {
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

    public void insertarAlFinal(int data) {
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

    public void imprimir() {
        if (head == null) {
            System.out.println("None");
            return;
        }

        NodoDoble actual = head;

        do {
            System.out.print(actual.data + " <-> ");
            actual = actual.next;
        } while (actual != head);

        System.out.println("(vuelve a " + head.data + ")");
    }

    public boolean buscar(int data) {
        if (head == null) return false;

        NodoDoble actual = head;
        do {
            if (actual.data == data) return true;
            actual = actual.next;
        } while (actual != head);

        return false;
    }

    public void eliminar(int data) {
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

    public static void main(String[] args) {
        ListaDoblementeCircular lista = new ListaDoblementeCircular();

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
