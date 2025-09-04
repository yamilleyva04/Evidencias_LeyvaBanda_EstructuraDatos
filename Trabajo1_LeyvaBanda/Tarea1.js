
let nombres = ["Ana", "Luis", "Carlos", "María"];
nombres[1] = "Pedro";

nombres.forEach(n => console.log(n));

async function main() {
  const prompt = require("prompt-sync")(); 

  let tamaño = parseInt(prompt("¿Cuántos nombres quieres agregar?: "));
  let arr = [];

  for (let i = 0; i < tamaño; i++) {
    let valor = prompt(`Ingrese el nombre ${i + 1}: `);
    arr.push(valor);
  }

  console.log("Lista final:", arr);
}

main();
