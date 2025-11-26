#include <iostream>
using namespace std;

class Nodo {
public:
    int data;
    Nodo* next;

    Nodo(int d) {
        data = d;
        next = nullptr;
    }
};

class PilaLista {
private:
    Nodo* top;

public:
    PilaLista() {
        top = nullptr;
    }

    bool isEmpty() {
        return top == nullptr;
    }

    void push(int data) {
        Nodo* nuevo = new Nodo(data);
        nuevo->next = top;
        top = nuevo;
    }

    int pop() {
        if (isEmpty()) {
            cout << "Error: Stack Underflow\n";
            return -1;
        }
        int dato = top->data;
        Nodo* temp = top;
        top = top->next;
        delete temp;
        return dato;
    }

    int peek() {
        if (isEmpty()) {
            cout << "Pila vacía\n";
            return -1;
        }
        return top->data;
    }
};

int main() {
    PilaLista pila;
    pila.push(10);
    pila.push(20);
    pila.push(30);

    cout << "Elemento superior: " << pila.peek() << endl;
    cout << "Extrae elemento: " << pila.pop() << endl;
    cout << "Nuevo elemento superior: " << pila.peek() << endl;

    return 0;
}
