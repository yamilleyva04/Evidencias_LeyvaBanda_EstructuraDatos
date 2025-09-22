#include <iostream>
#include <cstdlib>
#include <ctime>  
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
    srand(time(0)); /
    
    int n = 10; 
    int arr[n];

    for (int i = 0; i < n; i++) {
        arr[i] = rand() % 100 + 1;
    }

    cout << "Arreglo original: ";
    for (int i = 0; i < n; i++) cout << arr[i] << " ";
    cout << endl;

    bubbleSort(arr, n);

    cout << "Arreglo ordenado: ";
    for (int i = 0; i < n; i++) cout << arr[i] << " ";
    cout << endl;

    return 0;
}

