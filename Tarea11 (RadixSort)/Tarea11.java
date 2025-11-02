import java.util.Arrays;

public class Tarea11 {
    public static void countingSort(int[] arr, int exp) {
        int s = arr.length;
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

    public static void radixSort(int[] arr) {
        int max1 = Arrays.stream(arr).max().getAsInt();
        for (int exp = 1; max1 / exp > 0; exp *= 10)
            countingSort(arr, exp);
    }

    public static void main(String[] args) {
        int[] arr = {171, 46, 76, 91, 803, 25, 3, 67};
        System.out.println("Arreglo antes de ordenar: ");
        for (int n : arr) System.out.print(n + " ");

        radixSort(arr);

        System.out.println("\nDespués de ordenar el arreglo:");
        for (int n : arr) System.out.print(n + " ");
    }
}
