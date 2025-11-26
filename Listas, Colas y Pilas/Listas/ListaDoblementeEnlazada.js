class NodoDoble {
    constructor(data) {
        this.data = data;
        this.next = null;
        this.prev = null;
    }
}

class ListaDoblementeEnlazada {
    constructor() {
        this.head = null;
        this.tail = null;
    }

    insertarAlPrincipio(data) {
        const nuevo = new NodoDoble(data);
        if (!this.head) {
            this.head = this.tail = nuevo;
        } else {
            nuevo.next = this.head;
            this.head.prev = nuevo;
            this.head = nuevo;
        }
    }

    insertarAlFinal(data) {
        const nuevo = new NodoDoble(data);
        if (!this.tail) {
            this.head = this.tail = nuevo;
        } else {
            this.tail.next = nuevo;
            nuevo.prev = this.tail;
            this.tail = nuevo;
        }
    }

    imprimir() {
        let actual = this.head;
        let out = "";
        while (actual) {
            out += actual.data + " <-> ";
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

        while (actual && actual.data !== data)
            actual = actual.next;

        if (!actual) return;

        if (actual.prev)
            actual.prev.next = actual.next;
        else
            this.head = actual.next;

        if (actual.next)
            actual.next.prev = actual.prev;
        else
            this.tail = actual.prev;
    }
}

const lista = new ListaDoblementeEnlazada();
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

lista.eliminar(30);
lista.imprimir();
