import java.util.ArrayList;
import java.util.Scanner;

public class Tarea3 {
    public static void main(String[] args) {
        ArrayList<Integer> arr = new ArrayList<>();
        arr.add(10); arr.add(20); arr.add(30); arr.add(40); arr.add(50);

        System.out.println("Arreglo inicial: " + arr);
        for (int i = 0; i < arr.size(); i++) {
            System.out.println("arr[" + i + "] = " + arr.get(i));
        }

        Scanner sc = new Scanner(System.in);
        System.out.print("Ingrese la posición donde quiere insertar: ");
        int pos = sc.nextInt();
        System.out.print("Ingrese el valor a insertar: ");
        int val = sc.nextInt();
        arr.add(pos, val);

        System.out.println("Arreglo despues de insertar: " + arr);

        System.out.print("Ingrese el valor a buscar: ");
        int buscar = sc.nextInt();
        int encontrado = -1;

        for (int i = 0; i < arr.size(); i++) {
            if (arr.get(i) == buscar) {
                encontrado = i;
                break;
            }
        }

        if (encontrado != -1)
            System.out.println("Elemento " + buscar + " encontrado en " + encontrado);
        else
            System.out.println("Elemento no encontrado");
    }
}
