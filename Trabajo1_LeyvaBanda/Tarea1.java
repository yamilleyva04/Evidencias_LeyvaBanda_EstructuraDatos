import java.util.ArrayList;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        // Arreglo inicial
        String[] nombres = {"Ana", "Luis", "Carlos", "María"};
        nombres[1] = "Pedro";

        for (String n : nombres) {
            System.out.println(n);
        }

        // Lista dinámica
        Scanner sc = new Scanner(System.in);
        System.out.print("¿Cuántos nombres quieres agregar?: ");
        int tamaño = sc.nextInt();
        sc.nextLine(); // limpiar buffer

        ArrayList<String> arr = new ArrayList<>();

        for (int i = 0; i < tamaño; i++) {
            System.out.print("Ingrese el nombre " + (i + 1) + ": ");
            String valor = sc.nextLine();
            arr.add(valor);
        }

        System.out.println("Lista final: " + arr);
    }
}
