#include <iostream>
#include <vector>
using namespace std;

void merge(vector<int>& a, int l, int m, int r) {
    int a1 = m - l + 1;  
    int a2 = r - m;     

    vector<int> L(a1);
    vector<int> R(a2);

    for (int i = 0; i < a1; i++)
        L[i] = a[l + i];
    for (int j = 0; j < a2; j++)
        R[j] = a[m + 1 + j];

    int i = 0, j = 0, k = l;

    while (i < a1 && j < a2) {
        if (L[i] <= R[j]) {
            a[k] = L[i];
            i++;
        } else {
            a[k] = R[j];
            j++;
        }
        k++;
    }

    while (i < a1) {
        a[k] = L[i];
        i++;
        k++;
    }

    while (j < a2) {
        a[k] = R[j];
        j++;
        k++;
    }
}

void mergeSort(vector<int>& a, int l, int r) {
    if (l < r) {
        int m = l + (r - l) / 2;  
        mergeSort(a, l, m);      
        mergeSort(a, m + 1, r);  
        merge(a, l, m, r);       
    }
}

int main() {
    vector<int> a = {39, 28, 44, 11};

    cout << "Antes de ordenar el arreglo:\n";
    for (int x : a) cout << x << " ";
    cout << endl;

    mergeSort(a, 0, a.size() - 1);

    cout << "Después de ordenar el arreglo:\n";
    for (int x : a) cout << x << " ";
    cout << endl;

    return 0;
}
