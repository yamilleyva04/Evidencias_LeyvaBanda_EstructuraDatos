class Nodo {
    int data;
    Nodo next;

    public Nodo(int d) {
        data = d;
        next = null;
    }
}

public class ListaCircularEnlazada {
    private Nodo head;

    public void insertarAlPrincipio(int data) {
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

    public void insertarAlFinal(int data) {
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

    public void imprimir() {
        if (head == null) {
            System.out.println("None");
            return;
        }

        Nodo temp = head;
        do {
            System.out.print(temp.data + " -> ");
            temp = temp.next;
        } while (temp != head);

        System.out.println("(vuelve a " + head.data + ")");
    }

    public boolean buscar(int data) {
        if (head == null) return false;

        Nodo temp = head;
        do {
            if (temp.data == data) return true;
            temp = temp.next;
        } while (temp != head);

        return false;
    }

    public void eliminar(int data) {
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

    public static void main(String[] args) {
        ListaCircularEnlazada lista = new ListaCircularEnlazada();

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
