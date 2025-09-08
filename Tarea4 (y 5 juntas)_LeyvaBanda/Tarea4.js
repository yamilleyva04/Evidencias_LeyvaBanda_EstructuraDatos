let matriz = [
  [1, 2, 3],
  [4, 5, 6],
  [7, 8, 9]
];

console.log("Matriz original:");
for (let i = 0; i < 3; i++) {
  let fila = "[ ";
  for (let j = 0; j < 3; j++) {
    fila += matriz[i][j] + " ";
  }
  fila += "]";
  console.log(fila);
}

console.log("\nMatriz en filas:");
for (let i = 0; i < 3; i++) {
  for (let j = 0; j < 3; j++) {
    process.stdout.write(matriz[i][j] + " ");
  }
}

console.log("\nMatriz en columnas:");
for (let j = 0; j < 3; j++) {
  for (let i = 0; i < 3; i++) {
    process.stdout.write(matriz[i][j] + " ");
  }
}
console.log();
