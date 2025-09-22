using System;

class Program {
    static void InsertionSort(int[] arr) {
        for (int i = 1; i < arr.Length; i++) {
            int temp = arr[i];
            int j = i - 1;
            while (j >= 0 && arr[j] > temp) {
                arr[j + 1] = arr[j];
                j--;
            }
            arr[j + 1] = temp;
        }
    }

    static void PrintArr(int[] arr) {
        foreach (int val in arr)
            Console.Write(val + " ");
        Console.WriteLine();
    }

    static void Main() {
        int[] arr = {70, 15, 2, 51, 60};

        Console.Write("Antes de ordenar: ");
        PrintArr(arr);

        InsertionSort(arr);

        Console.Write("Después de ordenar: ");
        PrintArr(arr);
    }
}
