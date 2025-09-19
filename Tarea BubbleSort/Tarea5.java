public class Tarea5 {
    public static void bubbleSort(int[] arr) {
        int n = arr.length;
        boolean intercambio;
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

    public static void main(String[] args) {
        int[] arr = {56, 32, 17, 99, 23, 10, 78};

        System.out.print("Arreglo original: ");
        for (int num : arr) System.out.print(num + " ");
        System.out.println();

        bubbleSort(arr);

        System.out.print("Arreglo ordenado: ");
        for (int num : arr) System.out.print(num + " ");
    }
}
