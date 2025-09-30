public class Tarea8 {
    static void swap(int[] arr, int i, int j) {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    static int partition(int[] arr, int l, int h) {
        int pivot = arr[h];
        int j = l - 1;
        for (int k = l; k < h; k++) {
            if (arr[k] < pivot) {
                j++;
                swap(arr, j, k);
            }
        }
        swap(arr, j + 1, h);
        return j + 1;
    }

    static void quickSort(int[] arr, int l, int h) {
        if (l < h) {
            int pi = partition(arr, l, h);
            quickSort(arr, l, pi - 1);
            quickSort(arr, pi + 1, h);
        }
    }

    static void printArray(int[] arr) {
        for (int v : arr) {
            System.out.print(v + " ");
        }
        System.out.println();
    }

    public static void main(String[] args) {
        int[] arr = {10, 7, 8, 9, 1, 5};
        
        System.out.print("Antes: ");
        printArray(arr);

        quickSort(arr, 0, arr.length - 1);

        System.out.print("Después: ");
        printArray(arr);
    }
}

