class Nodo {
    int data;
    Nodo next;

    public Nodo(int d) {
        data = d;
        next = null;
    }
}

class PilaLista {
    private Nodo top;

    public boolean isEmpty() {
        return top == null;
    }

    public void push(int data) {
        Nodo nuevo = new Nodo(data);
        nuevo.next = top;
        top = nuevo;
    }

    public int pop() {
        if (isEmpty()) {
            System.out.println("Error: Stack Underflow");
            return -1;
        }
        int dato = top.data;
        top = top.next;
        return dato;
    }

    public int peek() {
        if (isEmpty()) {
            System.out.println("Pila vacía");
            return -1;
        }
        return top.data;
    }

    public static void main(String[] args) {
        PilaLista pila = new PilaLista();
        pila.push(10);
        pila.push(20);
        pila.push(30);

        System.out.println("Elemento superior: " + pila.peek());
        System.out.println("Extrae elemento: " + pila.pop());
        System.out.println("Nuevo elemento superior: " + pila.peek());
    }
}
