class Nodo {
    constructor(data) {
        this.data = data;
        this.next = null;
    }
}

class ColaLista {
    constructor() {
        this.front = null;
        this.rear = null;
    }

    isEmpty() {
        return this.front === null;
    }

    enqueue(data) {
        const nuevo = new Nodo(data);
        if (this.rear === null) {
            this.front = nuevo;
            this.rear = nuevo;
            return;
        }
        this.rear.next = nuevo;
        this.rear = nuevo;
    }

    dequeue() {
        if (this.isEmpty()) {
            console.log("Cola vacía (Underflow)");
            return -1;
        }
        const d = this.front.data;
        this.front = this.front.next;
        if (this.front === null) this.rear = null;
        return d;
    }

    peek() {
        if (this.isEmpty()) {
            console.log("Cola vacía");
            return -1;
        }
        return this.front.data;
    }
}

// Prueba
const cola = new ColaLista();
cola.enqueue(10);
cola.enqueue(20);
cola.enqueue(30);

console.log("Elemento frontal:", cola.peek());
console.log("Elimina:", cola.dequeue());
console.log("Nuevo frontal:", cola.peek());
