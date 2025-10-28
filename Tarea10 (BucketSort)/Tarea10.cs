using System;
using System.Collections.Generic;

class Program {
    static void InsertionSort(List<float> bukt) {
        for (int j = 1; j < bukt.Count; j++) {
            float val = bukt[j];
            int k = j - 1;
            while (k >= 0 && bukt[k] > val) {
                bukt[k + 1] = bukt[k];
                k--;
            }
            bukt[k + 1] = val;
        }
    }

    static void BucketSort(float[] arr) {
        int s = arr.Length;
        List<float>[] buckets = new List<float>[s];
        for (int i = 0; i < s; i++) buckets[i] = new List<float>();

        foreach (float j in arr) {
            int bi = (int)(s * j);
            if (bi >= s) bi = s - 1;
            buckets[bi].Add(j);
        }

        foreach (var bukt in buckets)
            InsertionSort(bukt);

        int idx = 0;
        foreach (var bukt in buckets)
            foreach (float j in bukt)
                arr[idx++] = j;
    }

    static void Main() {
        float[] arr = {0.77f, 0.16f, 0.38f, 0.25f, 0.71f, 0.93f, 0.22f, 0.11f, 0.24f, 0.67f};
        Console.WriteLine("Arreglo antes de ordenar:");
        Console.WriteLine(string.Join(" ", arr));

        BucketSort(arr);

        Console.WriteLine("Arreglo después de ordenar:");
        Console.WriteLine(string.Join(" ", arr));
    }
}
