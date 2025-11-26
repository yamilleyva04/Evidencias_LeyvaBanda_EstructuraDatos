const readline = require("readline");

const MAXSIZE = 5;
let queue = new Array(MAXSIZE);
let front = -1, rear = -1;

function insertar(elemento) {
    if (rear === MAXSIZE - 1) {
        console.log("OVERFLOW");
        return;
    }
    if (front === -1 && rear === -1) front = rear = 0;
    else rear++;

    queue[rear] = elemento;
    console.log("Elemento insertado.");
}

function eliminar() {
    if (front === -1 || front > rear) {
        console.log("UNDERFLOW");
        return;
    }

    let elemento = queue[front];
    if (front === rear) front = rear = -1;
    else front++;

    console.log("Elemento eliminado:", elemento);
}

function mostrar() {
    if (front === -1 || front > rear) {
        console.log("La cola está vacía.");
        return;
    }

    console.log("Elementos:");
    for (let i = front; i <= rear; i++) console.log(queue[i]);
}

