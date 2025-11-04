using System;

class Tarea13 {
    static void Merge(int[] a, int l, int m, int r) {
        int a1 = m - l + 1;
        int a2 = r - m;
        int[] L = new int[a1];
        int[] R = new int[a2];

        for (int i = 0; i < a1; i++)
            L[i] = a[l + i];
        for (int j = 0; j < a2; j++)
            R[j] = a[m + 1 + j];

        int i1 = 0, j1 = 0, k = l;

        while (i1 < a1 && j1 < a2) {
            if (L[i1] <= R[j1]) a[k++] = L[i1++];
            else a[k++] = R[j1++];
        }

        while (i1 < a1) a[k++] = L[i1++];
        while (j1 < a2) a[k++] = R[j1++];
    }

    static void MergeSort(int[] a, int l, int r) {
        if (l < r) {
            int m = l + (r - l) / 2;
            MergeSort(a, l, m);
            MergeSort(a, m + 1, r);
            Merge(a, l, m, r);
        }
    }

    static void Main() {
        int[] a = {39, 28, 44, 11};
        Console.WriteLine("Antes de ordenar el arreglo:");
        Console.WriteLine(string.Join(" ", a));
        MergeSort(a, 0, a.Length - 1);
        Console.WriteLine("Después de ordenar el arreglo:");
        Console.WriteLine(string.Join(" ", a));
    }
}
