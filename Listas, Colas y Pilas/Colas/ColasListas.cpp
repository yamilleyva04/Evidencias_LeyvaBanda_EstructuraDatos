#include <iostream>
using namespace std;

class Nodo {
public:
    int data;
    Nodo* next;
    Nodo(int d) : data(d), next(nullptr) {}
};

class ColaLista {
private:
    Nodo* front;
    Nodo* rear;
public:
    ColaLista() : front(nullptr), rear(nullptr) {}

    bool isEmpty() {
        return front == nullptr;
    }

    void enqueue(int data) {
        Nodo* nuevo = new Nodo(data);
        if (rear == nullptr) {
            front = rear = nuevo;
            return;
        }
        rear->next = nuevo;
        rear = nuevo;
    }

    int dequeue() {
        if (isEmpty()) {
            cout << "Cola vacía (Underflow)\n";
            return -1;
        }
        int d = front->data;
        Nodo* temp = front;
        front = front->next;
        if (front == nullptr) rear = nullptr;
        delete temp;
        return d;
    }

    int peek() {
        if (isEmpty()) {
            cout << "Cola vacía\n";
            return -1;
        }
        return front->data;
    }
};

int main() {
    ColaLista cola;
    cola.enqueue(10);
    cola.enqueue(20);
    cola.enqueue(30);

    cout << "Elemento frontal: " << cola.peek() << endl;
    cout << "Elimina: " << cola.dequeue() << endl;
    cout << "Nuevo frontal: " << cola.peek() << endl;
}
