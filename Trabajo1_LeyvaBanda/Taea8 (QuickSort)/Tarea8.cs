using System;

class Tarea8 {
    static void Swap(int[] arr, int i, int j) {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    static int Partition(int[] arr, int l, int h) {
        int pivot = arr[h];
        int j = l - 1;
        for (int k = l; k < h; k++) {
            if (arr[k] < pivot) {
                j++;
                Swap(arr, j, k);
            }
        }
        Swap(arr, j + 1, h);
        return j + 1;
    }

    static void QuickSortAlgo(int[] arr, int l, int h) {
        if (l < h) {
            int pi = Partition(arr, l, h);
            QuickSortAlgo(arr, l, pi - 1);
            QuickSortAlgo(arr, pi + 1, h);
        }
    }

    static void Main() {
        int[] arr = {10, 7, 8, 9, 1, 5};
        QuickSortAlgo(arr, 0, arr.Length - 1);
        Console.WriteLine(string.Join(" ", arr));
    }
}
