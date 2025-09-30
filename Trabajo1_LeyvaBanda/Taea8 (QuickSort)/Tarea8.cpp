#include <iostream>
using namespace std;

void swap(int &x, int &y) {
    int temp = x;
    x = y;
    y = temp;
}

int partition(int arr[], int l, int h) {
    int pivot = arr[h];
    int j = l - 1;
    for (int k = l; k < h; k++) {
        if (arr[k] < pivot) {
            j++;
            swap(arr[j], arr[k]);
        }
    }
    swap(arr[j + 1], arr[h]);
    return j + 1;
}

void quickSort(int arr[], int l, int h) {
    if (l < h) {
        int pi = partition(arr, l, h);
        quickSort(arr, l, pi - 1);
        quickSort(arr, pi + 1, h);
    }
}

int main() {
    int arr[] = {10, 7, 8, 9, 1, 5};
    int n = sizeof(arr) / sizeof(arr[0]);
    quickSort(arr, 0, n - 1);
    for (int i = 0; i < n; i++) cout << arr[i] << " ";
    return 0;
}
