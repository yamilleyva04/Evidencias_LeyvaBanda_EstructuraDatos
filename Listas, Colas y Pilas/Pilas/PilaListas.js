class Nodo {
    constructor(data) {
        this.data = data;
        this.next = null;
    }
}

class PilaLista {
    constructor() {
        this.top = null;
    }

    isEmpty() {
        return this.top === null;
    }

    push(data) {
        let nuevo = new Nodo(data);
        nuevo.next = this.top;
        this.top = nuevo;
    }

    pop() {
        if (this.isEmpty()) {
            console.log("Error: Stack Underflow");
            return null;
        }
        let d = this.top.data;
        this.top = this.top.next;
        return d;
    }

    peek() {
        if (this.isEmpty()) {
            console.log("Pila vacía");
            return null;
        }
        return this.top.data;
    }
}


let pila = new PilaLista();
pila.push(10);
pila.push(20);
pila.push(30);

console.log("Elemento superior:", pila.peek());
console.log("Extrae elemento:", pila.pop());
console.log("Nuevo elemento superior:", pila.peek());
