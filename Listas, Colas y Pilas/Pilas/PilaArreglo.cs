using System;

class PilaArreglo {
    private int[] stack;
    private int maxSize;
    private int top;

    public PilaArreglo(int tamanoMax) {
        maxSize = tamanoMax;
        stack = new int[maxSize];
        top = -1;
    }

    public bool IsEmpty() {
        return top == -1;
    }

    public bool IsFull() {
        return top == maxSize - 1;
    }

    public void Push(int data) {
        if (IsFull()) {
            Console.WriteLine("Error: Stack Overflow");
            return;
        }
        stack[++top] = data;
    }

    public int Pop() {
        if (IsEmpty()) {
            Console.WriteLine("Error: Stack Underflow");
            return -1;
        }
        return stack[top--];
    }

    public int Peek() {
        if (IsEmpty()) {
            Console.WriteLine("Pila vacía");
            return -1;
        }
        return stack[top];
    }
}

class Program {
    static void Main() {
        PilaArreglo pila = new PilaArreglo(100);
        pila.Push(10);
        pila.Push(20);
        pila.Push(30);

        Console.WriteLine("Elemento superior: " + pila.Peek());
        Console.WriteLine("Extrae elemento: " + pila.Pop());
        Console.WriteLine("Nuevo elemento superior: " + pila.Peek());
    }
}
