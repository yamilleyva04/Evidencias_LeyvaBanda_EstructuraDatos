#include <iostream>
using namespace std;

class NodoDoble {
public:
    int data;
    NodoDoble* next;
    NodoDoble* prev;

    NodoDoble(int data) {
        this->data = data;
        next = nullptr;
        prev = nullptr;
    }
};

class ListaDoblementeEnlazada {
private:
    NodoDoble* head;
    NodoDoble* tail;

public:
    ListaDoblementeEnlazada() {
        head = nullptr;
        tail = nullptr;
    }

    void insertar_al_principio(int data) {
        NodoDoble* nuevo = new NodoDoble(data);
        if (!head) {
            head = tail = nuevo;
        } else {
            nuevo->next = head;
            head->prev = nuevo;
            head = nuevo;
        }
    }

    void insertar_al_final(int data) {
        NodoDoble* nuevo = new NodoDoble(data);
        if (!tail) {
            head = tail = nuevo;
        } else {
            tail->next = nuevo;
            nuevo->prev = tail;
            tail = nuevo;
        }
    }

    void imprimir_lista() {
        NodoDoble* actual = head;
        while (actual) {
            cout << actual->data << " <-> ";
            actual = actual->next;
        }
        cout << "None\n";
    }

    bool buscar(int data) {
        NodoDoble* actual = head;
        while (actual) {
            if (actual->data == data) return true;
            actual = actual->next;
        }
        return false;
    }

    void eliminar(int data) {
        NodoDoble* actual = head;

        while (actual && actual->data != data)
            actual = actual->next;

        if (!actual) return;

        if (actual->prev)
            actual->prev->next = actual->next;
        else
            head = actual->next;

        if (actual->next)
            actual->next->prev = actual->prev;
        else
            tail = actual->prev;

        delete actual;
    }
};

int main() {
    ListaDoblementeEnlazada lista;
    lista.insertar_al_final(10);
    lista.insertar_al_final(20);
    lista.insertar_al_final(30);
    lista.imprimir_lista();

    lista.insertar_al_principio(5);
    lista.imprimir_lista();

    cout << "¿Está el 20? " << lista.buscar(20) << endl;
    cout << "¿Está el 99? " << lista.buscar(99) << endl;

    lista.eliminar(20);
    lista.imprimir_lista();

    lista.eliminar(5);
    lista.imprimir_lista();

    lista.eliminar(30);
    lista.imprimir_lista();
}
