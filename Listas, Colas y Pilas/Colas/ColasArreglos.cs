using System;

class ColaArreglo {
    const int MAXSIZE = 5;
    static int[] queue = new int[MAXSIZE];
    static int front = -1, rear = -1;

    static void Insertar() {
        Console.Write("\nIngrese el elemento: ");
        int elemento = int.Parse(Console.ReadLine());

        if (rear == MAXSIZE - 1) {
            Console.WriteLine("\nOVERFLOW");
            return;
        }

        if (front == -1 && rear == -1) {
            front = rear = 0;
        } else {
            rear++;
        }

        queue[rear] = elemento;
        Console.WriteLine("Elemento insertado.");
    }

    static void Eliminar() {
        if (front == -1 || front > rear) {
            Console.WriteLine("\nUNDERFLOW");
            return;
        }

        int elemento = queue[front];
        if (front == rear) front = rear = -1;
        else front++;

        Console.WriteLine($"Elemento eliminado: {elemento}");
    }

    static void Mostrar() {
        if (front == -1 || front > rear) {
            Console.WriteLine("La cola está vacía.");
            return;
        }

        Console.WriteLine("\nElementos en la cola:");
        for (int i = front; i <= rear; i++)
            Console.WriteLine(queue[i]);
    }

    static void Main(string[] args) {
        int opcion = 0;

        while (opcion != 4) {
            Console.WriteLine("\n1. Insertar\n2. Eliminar\n3. Mostrar\n4. Salir");
            Console.Write("Opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion) {
                case 1: Insertar(); break;
                case 2: Eliminar(); break;
                case 3: Mostrar(); break;
            }
        }
    }
}
