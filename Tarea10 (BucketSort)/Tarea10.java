import java.util.*;

public class Tarea10 {
    public static void insertionSort(List<Float> bukt) {
        for (int j = 1; j < bukt.size(); j++) {
            float val = bukt.get(j);
            int k = j - 1;
            while (k >= 0 && bukt.get(k) > val) {
                bukt.set(k + 1, bukt.get(k));
                k--;
            }
            bukt.set(k + 1, val);
        }
    }

    public static void bucketSort(float[] arr) {
        int s = arr.length;
        List<Float>[] buckets = new List[s];
        for (int i = 0; i < s; i++)
            buckets[i] = new ArrayList<>();

        for (float j : arr) {
            int bi = (int)(s * j);
            if (bi >= s) bi = s - 1;
            buckets[bi].add(j);
        }

        for (List<Float> bukt : buckets)
            insertionSort(bukt);

        int idx = 0;
        for (List<Float> bukt : buckets)
            for (float j : bukt)
                arr[idx++] = j;
    }

    public static void main(String[] args) {
        float[] arr = {0.77f, 0.16f, 0.38f, 0.25f, 0.71f, 0.93f, 0.22f, 0.11f, 0.24f, 0.67f};
        System.out.println("Arreglo antes de ordenar:");
        System.out.println(Arrays.toString(arr));

        bucketSort(arr);

        System.out.println("Arreglo después de ordenar:");
        System.out.println(Arrays.toString(arr));
    }
}
