using System;

class Tarea7 {
    static void Selection(int[] a) {
        for (int i = 0; i < a.Length; i++) {
            int small = i;
            for (int j = i + 1; j < a.Length; j++) {
                if (a[small] > a[j]) {
                    small = j;
                }
            }
            int temp = a[i];
            a[i] = a[small];
            a[small] = temp;
        }
    }

    static void PrintArr(int[] a) {
        foreach (int val in a) {
            Console.Write(val + " ");
        }
        Console.WriteLine();
    }

    static void Main() {
        int[] a = {65, 26, 13, 23, 12};

        Console.WriteLine("Arreglo antes de ser ordenado:");
        PrintArr(a);

        Selection(a);

        Console.WriteLine("Arreglo despues de ser ordenado:");
        PrintArr(a);
    }
}
