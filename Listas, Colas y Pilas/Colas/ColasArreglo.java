import java.util.Scanner;

public class ColasArreglo {
    static final int MAXSIZE = 5;
    static int[] queue = new int[MAXSIZE];
    static int front = -1, rear = -1;
    static Scanner sc = new Scanner(System.in);

    static void insertar() {
        System.out.print("\nIngrese el elemento: ");
        int elemento = sc.nextInt();

        if (rear == MAXSIZE - 1) {
            System.out.println("OVERFLOW");
            return;
        }

        if (front == -1 && rear == -1)
            front = rear = 0;
        else
            rear++;

        queue[rear] = elemento;
        System.out.println("Elemento insertado.");
    }

    static void eliminar() {
        if (front == -1 || front > rear) {
            System.out.println("UNDERFLOW");
            return;
        }

        int elemento = queue[front];
        if (front == rear) front = rear = -1;
        else front++;

        System.out.println("Elemento eliminado: " + elemento);
    }

    static void mostrar() {
        if (front == -1 || front > rear) {
            System.out.println("La cola está vacía.");
            return;
        }

        System.out.println("\nElementos:");
        for (int i = front; i <= rear; i++)
            System.out.println(queue[i]);
    }

    public static void main(String[] args) {
        int opcion = 0;
        while (opcion != 4) {
            System.out.println("1. Insertar\n2. Eliminar\n3. Mostrar\n4. Salir");
            System.out.print("Opción: ");
            opcion = sc.nextInt();

            switch (opcion) {
                case 1: insertar(); break;
                case 2: eliminar(); break;
                case 3: mostrar(); break;
            }
        }
    }
}
