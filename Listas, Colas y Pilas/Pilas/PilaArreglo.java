class PilaArreglo {
    private int[] stack;
    private int maxSize;
    private int top;

    public PilaArreglo(int tamanoMax) {
        maxSize = tamanoMax;
        stack = new int[maxSize];
        top = -1;
    }

    public boolean isEmpty() {
        return top == -1;
    }

    public boolean isFull() {
        return top == maxSize - 1;
    }

    public void push(int data) {
        if (isFull()) {
            System.out.println("Error: Stack Overflow");
            return;
        }
        stack[++top] = data;
    }

    public int pop() {
        if (isEmpty()) {
            System.out.println("Error: Stack Underflow");
            return -1;
        }
        return stack[top--];
    }

    public int peek() {
        if (isEmpty()) {
            System.out.println("Pila vacía");
            return -1;
        }
        return stack[top];
    }

    public static void main(String[] args) {
        PilaArreglo pila = new PilaArreglo(100);
        pila.push(10);
        pila.push(20);
        pila.push(30);

        System.out.println("Elemento superior: " + pila.peek());
        System.out.println("Extrae elemento: " + pila.pop());
        System.out.println("Nuevo elemento superior: " + pila.peek());
    }
}
