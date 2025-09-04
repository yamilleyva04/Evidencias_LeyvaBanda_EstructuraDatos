#include <iostream>
#include <vector>
using namespace std;

int main() {
    vector<int> arr = {10, 20, 30, 40, 50};

    cout << "Arreglo inicial: ";
    for (int v : arr) cout << v << " ";
    cout << endl;

    for (int i = 0; i < arr.size(); i++) {
        cout << "arr[" << i << "] = " << arr[i] << endl;
    }

    int pos, val;
    cout << "Ingrese la posición donde quiere insertar: ";
    cin >> pos;
    cout << "Ingrese el valor a insertar: ";
    cin >> val;
    arr.insert(arr.begin() + pos, val);

    cout << "Arreglo después de insertar: ";
    for (int v : arr) cout << v << " ";
    cout << endl;

    int buscar, encontrado = -1;
    cout << "Ingrese el valor a buscar: ";
    cin >> buscar;

    for (int i = 0; i < arr.size(); i++) {
        if (arr[i] == buscar) {
            encontrado = i;
            break;
        }
    }

    if (encontrado != -1)
        cout << "Elemento " << buscar << " encontrado en índice " << encontrado << endl;
    else
        cout << "Elemento no encontrado" << endl;

    return 0;
}
