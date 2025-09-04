#include <iostream>
#include <string>
using namespace std;

struct Carro {
    string marca;
    string modelo;
    int año;
};

int main() {
    Carro c;
    c.marca = "Nissan";
    c.modelo = "Versa";
    c.año = 2020;

    cout << "Carro: " << c.marca << " " << c.modelo << " (" << c.año << ")" << endl;
    return 0;
}

