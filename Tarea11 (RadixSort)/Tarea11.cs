using System;

class Tarea11 {
    static void CountingSort(int[] arr, int exp) {
        int s = arr.Length;
        int[] outputArray = new int[s];
        int[] countArray = new int[10];

        for (int j = 0; j < s; j++) {
            int idx = (arr[j] / exp) % 10;
            countArray[idx]++;
        }

        for (int j = 1; j < 10; j++)
            countArray[j] += countArray[j - 1];

        for (int j = s - 1; j >= 0; j--) {
            int idx = (arr[j] / exp) % 10;
            outputArray[countArray[idx] - 1] = arr[j];
            countArray[idx]--;
        }

        for (int j = 0; j < s; j++)
            arr[j] = outputArray[j];
    }

    static void RadixSort(int[] arr) {
        int max1 = arr[0];
        foreach (int num in arr)
            if (num > max1) max1 = num;

        for (int exp = 1; max1 / exp > 0; exp *= 10)
            CountingSort(arr, exp);
    }

    static void Main() {
        int[] arr = {171, 46, 76, 91, 803, 25, 3, 67};
        Console.WriteLine("Arreglo antes de ordenar:");
        Console.WriteLine(string.Join(" ", arr));

        RadixSort(arr);

        Console.WriteLine("Después de ordenar el arreglo:");
        Console.WriteLine(string.Join(" ", arr));
    }
}
