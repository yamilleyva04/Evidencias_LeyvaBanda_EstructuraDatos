#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

void insertionSort(vector<float>& bukt) {
    for (int j = 1; j < bukt.size(); j++) {
        float val = bukt[j];
        int k = j - 1;
        while (k >= 0 && bukt[k] > val) {
            bukt[k + 1] = bukt[k];
            k--;
        }
        bukt[k + 1] = val;
    }
}

void bucketSort(vector<float>& arr) {
    int s = arr.size();
    vector<vector<float>> buckets(s);

    for (float j : arr) {
        int bi = s * j;
        if (bi >= s) bi = s - 1;
        buckets[bi].push_back(j);
    }

    for (auto& bukt : buckets)
        insertionSort(bukt);

    int idx = 0;
    for (auto& bukt : buckets)
        for (float j : bukt)
            arr[idx++] = j;
}

int main() {
    vector<float> arr = {0.77, 0.16, 0.38, 0.25, 0.71, 0.93, 0.22, 0.11, 0.24, 0.67};
    cout << "Arreglo antes de ordenar:\n";
    for (float x : arr) cout << x << " ";
    cout << endl;

    bucketSort(arr);

    cout << "Arreglo después de ordenar:\n";
    for (float x : arr) cout << x << " ";
    cout << endl;
}
