#include <iostream>
using namespace std;

void selection(int a[], int n) {
    for (int i = 0; i < n; i++) {
        int small = i;
        for (int j = i + 1; j < n; j++) {
            if (a[small] > a[j]) {
                small = j;
            }
        }
        swap(a[i], a[small]);
    }
}

void printArr(int a[], int n) {
    for (int i = 0; i < n; i++) {
        cout << a[i] << " ";
    }
    cout << endl;
}

int main() {
    int a[] = {65, 26, 13, 23, 12};
    int n = sizeof(a) / sizeof(a[0]);

    cout << "Arreglo antes de ser ordenado:" << endl;
    printArr(a, n);

    selection(a, n);

    cout << "Arreglo despues de ser ordenado:" << endl;
    printArr(a, n);

    return 0;
}
