using System;

class Program {
    static void BubbleSort(int[] arr) {
        int n = arr.Length;
        bool intercambio;
        for (int i = 0; i < n - 1; i++) {
            intercambio = false;
            for (int j = 0; j < n - 1 - i; j++) {
                if (arr[j] > arr[j + 1]) {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                    intercambio = true;
                }
            }
            if (!intercambio) break;
        }
    }

    static void Main() {
        int[] arr = {56, 32, 17, 99, 23, 10, 78};

        Console.Write("Arreglo original: ");
        foreach (int num in arr) Console.Write(num + " ");
        Console.WriteLine();

        BubbleSort(arr);

        Console.Write("Arreglo ordenado: ");
        foreach (int num in arr) Console.Write(num + " ");
    }
}
