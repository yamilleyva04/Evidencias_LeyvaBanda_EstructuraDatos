class NodoDoble {
    constructor(data) {
        this.data = data;
        this.next = null;
        this.prev = null;
    }
}

class ListaDoblementeCircular {
    constructor() {
        this.head = null;
    }

    insertarAlPrincipio(data) {
        const nuevo = new NodoDoble(data);

        if (!this.head) {
            this.head = nuevo;
            nuevo.next = this.head;
            nuevo.prev = this.head;
        } else {
            const tail = this.head.prev;

            nuevo.next = this.head;
            this.head.prev = nuevo;
            this.head = nuevo;

            this.head.prev = tail;
            tail.next = this.head;
        }
    }

    insertarAlFinal(data) {
        const nuevo = new NodoDoble(data);

        if (!this.head) {
            this.head = nuevo;
            nuevo.next = this.head;
            nuevo.prev = this.head;
        } else {
            const tail = this.head.prev;

            tail.next = nuevo;
            nuevo.prev = tail;
            nuevo.next = this.head;
            this.head.prev = nuevo;
        }
    }

    imprimir() {
        if (!this.head) {
            console.log("None");
            return;
        }

        let actual = this.head;

        do {
            process.stdout.write(actual.data + " <-> ");
            actual = actual.next;
        } while (actual !== this.head);

        console.log("(vuelve a " + this.head.data + ")");
    }

    buscar(data) {
        if (!this.head) return false;

        let actual = this.head;

        do {
            if (actual.data === data) return true;
            actual = actual.next;
        } while (actual !== this.head);

        return false;
    }

    eliminar(data) {
        if (!this.head) return;

        let actual = this.head;

        do {
            if (actual.data === data) break;
            actual = actual.next;
        } while (actual !== this.head);

        if (actual.data !== data) return;

        if (actual === this.head && this.head.next === this.head) {
            this.head = null;
            return;
        }

        if (actual === this.head)
            this.head = actual.next;

        actual.prev.next = actual.next;
        actual.next.prev = actual.prev;
    }
}

const lista = new ListaDoblementeCircular();
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
