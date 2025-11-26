class Nodo {
    constructor(data) {
        this.data = data;
        this.next = null;
    }
}

class ListaEnlazada {
    constructor() {
        this.head = null;
    }

    insertarAlPrincipio(data) {
        const nuevo = new Nodo(data);
        nuevo.next = this.head;
        this.head = nuevo;
    }

    insertarAlFinal(data) {
        const nuevo = new Nodo(data);

        if (this.head === null) {
            this.head = nuevo;
            return;
        }

        let ultimo = this.head;
        while (ultimo.next) {
            ultimo = ultimo.next;
        }
        ultimo.next = nuevo;
    }

    imprimir() {
        let actual = this.head;
        let out = "";
        while (actual) {
            out += actual.data + " -> ";
            actual = actual.next;
        }
        console.log(out + "None");
    }

    buscar(data) {
        let actual = this.head;
        while (actual) {
            if (actual.data === data) return true;
            actual = actual.next;
        }
        return false;
    }

    eliminar(data) {
        let actual = this.head;
        let previo = null;

        if (actual && actual.data === data) {
            this.head = actual.next;
            return;
        }

        while (actual && actual.data !== data) {
            previo = actual;
            actual = actual.next;
        }

        if (!actual) return;

        previo.next = actual.next;
    }
}

// --- Pruebas ---
const lista = new ListaEnlazada();
lista.insertarAlFinal(10);
lista.insertarAlFinal(20);
lista.insertarAlFinal(30);
lista.imprimir();

lista.insertarAlPrincipio(5);
lista.imprimir();

console.log("¿Está el 20?", lista.buscar(20));
console.log("¿Está el 99?", lista.buscar(99));

lista.eliminar(20);
lista.imprimir();

lista.eliminar(5);
lista.imprimir();
