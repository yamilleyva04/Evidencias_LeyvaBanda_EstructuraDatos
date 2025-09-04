#include <iostream>
#include <vector>
#include <string>

using namespace std;


int main() {
    
    string nombres[4] = {"Ana", "Luis", "Carlos", "María"};
    nombres[1] = "Pedro";

    for (string n : nombres) {
        cout << n << endl;
    }

    int tamaño;
    cout << "¿Cuántos nombres quieres agregar?: ";
    cin >> tamaño;

    vector<string> arr;
    for (int i = 0; i < tamaño; i++) {
        string valor;
        cout << "Ingrese el nombre " << i + 1 << ": ";
        cin >> valor;
        arr.push_back(valor);
    }

    cout << "Lista final: ";
    for (string v : arr) {
        cout << v << " ";
    }
    cout << endl;

    return 0;
}
