const readline = require('readline').createInterface({
  input: process.stdin,
  output: process.stdout
});

let arr = [10, 20, 30, 40, 50];
console.log("Arreglo inicial:", arr);

for (let i = 0; i < arr.length; i++) {
  console.log(`arr[${i}] = ${arr[i]}`);
}

readline.question("Ingrese la posición donde quiere insertar: ", (posInput) => {
  let pos = parseInt(posInput);

  readline.question("Ingrese el valor a insertar: ", (valInput) => {
    let val = parseInt(valInput);

    if (!isNaN(pos) && !isNaN(val) && pos >= 0 && pos <= arr.length) {
      arr.splice(pos, 0, val);
      console.log("Arreglo después de insertar:", arr);

      readline.question("Ingrese el valor a buscar: ", (buscarInput) => {
        let buscar = parseInt(buscarInput);
        if (!isNaN(buscar)) {
          let encontrado = -1;
          for (let i = 0; i < arr.length; i++) {
            if (arr[i] === buscar) {
              encontrado = i;
              break;
            }
          }

          if (encontrado !== -1) {
            console.log(`Elemento ${buscar} encontrado en índice ${encontrado}`);
          } else {
            console.log("Elemento no encontrado");
          }
        } else {
          console.log("No ingresaste un número válido");
        }
        readline.close();
      });

    } else {
      console.log("Posición o valor inválido");
      readline.close();
    }
  });
});
