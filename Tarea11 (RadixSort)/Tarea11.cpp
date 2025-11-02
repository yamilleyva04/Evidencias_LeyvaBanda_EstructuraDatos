#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

void countingSort(vector<int>& arr, int exp) {
    int s = arr.size();
    vector<int> outputArray(s);
    vector<int> countArray(10, 0);

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

void radixSort(vector<int>& arr) {
    int max1 = *max_element(arr.begin(), arr.end());
    for (int exp = 1; max1 / exp > 0; exp *= 10)
        countingSort(arr, exp);
}

int main() {
    vector<int> arr = {171, 46, 76, 91, 803, 25, 3, 67};

    cout << "Arreglo antes de ordenar:\n";
    for (int n : arr) cout << n << " ";

    radixSort(arr);

    cout << "\nDespués de ordenar el arreglo:\n";
    for (int n : arr) cout << n << " ";

    return 0;
}
