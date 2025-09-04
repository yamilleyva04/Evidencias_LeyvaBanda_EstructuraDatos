class Carro {
    String marca;
    String modelo;
    int año;
}

public class Tarea2 {
    public static void main(String[] args) {
        Carro c = new Carro();
        c.marca = "Nissan";
        c.modelo = "Versa";
        c.año = 2020;

        System.out.println("Carro: " + c.marca + " " + c.modelo + " (" + c.año + ")");
    }
}
