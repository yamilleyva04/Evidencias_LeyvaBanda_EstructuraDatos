class Carro:
    def __init__(self, marca, modelo, año):
        self.marca = marca
        self.modelo = modelo
        self.año = año

c = Carro("Nissan", "Versa", 2020)
print(f"Carro: {c.marca} {c.modelo} ({c.año})")
