#include <iostream>
using namespace std;

class Nodo {
public:
    int data;
    Nodo* next;
    Nodo(int d) : data(d), next(nullptr) {}
};

class ListaCircularEnlazada {
private:
    Nodo* head;

public:
    ListaCircularEnlazada() : head(nullptr) {}

    void insertarAlPrincipio(int data) {
        Nodo* nuevo = new Nodo(data);
        if (!head) {
            head = nuevo;
            nuevo->next = head;
        } else {
            Nodo* temp = head;
            while (temp->next != head)
                temp = temp->next;

            nuevo->next = head;
            head = nuevo;
            temp->next = head;
        }
    }

    void insertarAlFinal(int data) {
        Nodo* nuevo = new Nodo(data);
        if (!head) {
            head = nuevo;
            nuevo->next = head;
        } else {
            Nodo* temp = head;
            while (temp->next != head)
                temp = temp->next;

            temp->next = nuevo;
            nuevo->next = head;
        }
    }

    void imprimir() {
        if (!head) {
            cout << "None\n";
            return;
        }
        Nodo* temp = head;
        do {
            cout << temp->data << " -> ";
            temp = temp->next;
        } while (temp != head);
        cout << "(vuelve a " << head->data << ")\n";
    }

    bool buscar(int data) {
        if (!head) return false;
        Nodo* temp = head;
        do {
            if (temp->data == data) return true;
            temp = temp->next;
        } while (temp != head);
        return false;
    }

    void eliminar(int data) {
        if (!head) return;

        if (head->data == data) {
            if (head->next == head) {
                delete head;
                head = nullptr;
                return;
            }

            Nodo* temp = head;
            while (temp->next != head)
                temp = temp->next;

            Nodo* borrar = head;
            temp->next = head->next;
            head = head->next;
            delete borrar;
            return;
        }

        Nodo* prev = head;
        Nodo* curr = head->next;

        while (curr != head) {
            if (curr->data == data) {
                prev->next = curr->next;
                delete curr;
                return;
            }
            prev = curr;
            curr = curr->next;
        }
    }
};

int main() {
    ListaCircularEnlazada lista;
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
