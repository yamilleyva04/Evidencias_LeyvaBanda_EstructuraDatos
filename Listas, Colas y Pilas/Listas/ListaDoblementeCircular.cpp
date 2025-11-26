#include <iostream>
using namespace std;

class NodoDoble {
public:
    int data;
    NodoDoble* next;
    NodoDoble* prev;

    NodoDoble(int d) : data(d), next(nullptr), prev(nullptr) {}
};

class ListaDoblementeCircular {
private:
    NodoDoble* head;

public:
    ListaDoblementeCircular() : head(nullptr) {}

    void insertarAlPrincipio(int data) {
        NodoDoble* nuevo = new NodoDoble(data);

        if (!head) {
            head = nuevo;
            head->next = head;
            head->prev = head;
        } else {
            NodoDoble* tail = head->prev;
            nuevo->next = head;
            head->prev = nuevo;
            head = nuevo;
            head->prev = tail;
            tail->next = head;
        }
    }

    void insertarAlFinal(int data) {
        NodoDoble* nuevo = new NodoDoble(data);

        if (!head) {
            head = nuevo;
            head->next = head;
            head->prev = head;
        } else {
            NodoDoble* tail = head->prev;
            tail->next = nuevo;
            nuevo->prev = tail;
            nuevo->next = head;
            head->prev = nuevo;
        }
    }

    void imprimir() {
        if (!head) {
            cout << "None\n";
            return;
        }

        NodoDoble* actual = head;
        do {
            cout << actual->data << " <-> ";
            actual = actual->next;
        } while (actual != head);

        cout << "(vuelve a " << head->data << ")\n";
    }

    bool buscar(int data) {
        if (!head) return false;

        NodoDoble* actual = head;
        do {
            if (actual->data == data) return true;
            actual = actual->next;
        } while (actual != head);

        return false;
    }

    void eliminar(int data) {
        if (!head) return;

        NodoDoble* actual = head;

        do {
            if (actual->data == data) break;
            actual = actual->next;
        } while (actual != head);

        if (actual->data != data) return;  

        if (actual == head && head->next == head) {
            delete head;
            head = nullptr;
            return;
        }

        if (actual == head)
            head = actual->next;

        actual->prev->next = actual->next;
        actual->next->prev = actual->prev;

        delete actual;
    }
};

int main() {
    ListaDoblementeCircular lista;

    lista.insertarAlFinal(10);
    lista.insertarAlFinal(20);
    lista.insertarAlFinal(30);
    lista.imprimir();

    lista.insertarAlPrincipio(5);
    lista.imprimir();

    cout << "¿Está el 20? " << lista.buscar(20) << endl;
    cout << "¿Está el 99? " << lista.buscar(99) << endl;

    lista.eliminar(20);
    lista.imprimir();

    lista.eliminar(5);
    lista.imprimir();

    lista.eliminar(30);
    lista.imprimir();
}
