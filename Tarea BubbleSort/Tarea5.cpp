#include <iostream>
using namespace std;

void bubbleSort(int arr[], int n) {
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

int main() {
    int arr[] = {56, 32, 17, 99, 23, 10, 78};
    int n = sizeof(arr) / sizeof(arr[0]);

    cout << "Arreglo original: ";
    for (int i = 0; i < n; i++) cout << arr[i] << " ";
    cout << endl;

    bubbleSort(arr, n);

    cout << "Arreglo ordenado: ";
    for (int i = 0; i < n; i++) cout << arr[i] << " ";
    cout << endl;

    return 0;
}
