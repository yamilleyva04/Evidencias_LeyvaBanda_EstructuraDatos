class Nodo {
    constructor(data) {
        this.data = data;
        this.next = null;
    }
}

class ListaCircularEnlazada {
    constructor() {
        this.head = null;
    }

    insertarAlPrincipio(data) {
        const nuevo = new Nodo(data);

        if (!this.head) {
            this.head = nuevo;
            nuevo.next = this.head;
        } else {
            let temp = this.head;
            while (temp.next !== this.head) {
                temp = temp.next;
            }

            nuevo.next = this.head;
            this.head = nuevo;
            temp.next = this.head;
        }
    }

    insertarAlFinal(data) {
        const nuevo = new Nodo(data);

        if (!this.head) {
            this.head = nuevo;
            nuevo.next = this.head;
        } else {
            let temp = this.head;
            while (temp.next !== this.head) {
                temp = temp.next;
            }

            temp.next = nuevo;
            nuevo.next = this.head;
        }
    }

    imprimir() {
        if (!this.head) {
            console.log("None");
            return;
        }

        let temp = this.head;

        do {
            process.stdout.write(temp.data + " -> ");
            temp = temp.next;
        } while (temp !== this.head);

        console.log("(vuelve a " + this.head.data + ")");
    }

    buscar(data) {
        if (!this.head) return false;

        let temp = this.head;

        do {
            if (temp.data === data) return true;
            temp = temp.next;
        } while (temp !== this.head);

        return false;
    }

    eliminar(data) {
        if (!this.head) return;

        if (this.head.data === data) {
            if (this.head.next === this.head) {
                this.head = null;
                return;
            }

            let temp = this.head;
            while (temp.next !== this.head) {
                temp = temp.next;
            }

            temp.next = this.head.next;
            this.head = this.head.next;
            return;
        }

        let prev = this.head;
        let curr = this.head.next;

        while (curr !== this.head) {
            if (curr.data === data) {
                prev.next = curr.next;
                return;
            }
            prev = curr;
            curr = curr.next;
        }
    }
}

const lista = new ListaCircularEnlazada();
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
