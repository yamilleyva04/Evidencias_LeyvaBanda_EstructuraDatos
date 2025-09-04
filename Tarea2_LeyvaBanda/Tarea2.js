class Carro {
  constructor(marca, modelo, año) {
    this.marca = marca;
    this.modelo = modelo;
    this.año = año;
  }
}

let c = new Carro("Nissan", "Versa", 2020);
console.log(`Carro: ${c.marca} ${c.modelo} (${c.año})`);
