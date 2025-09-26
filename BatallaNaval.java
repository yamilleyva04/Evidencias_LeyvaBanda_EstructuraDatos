import java.util.Scanner;

public class BatallaNaval {
    static final int N = 10;
    static char[][] tablero1 = new char[N][N];
    static char[][] tablero2 = new char[N][N];
    static Scanner sc = new Scanner(System.in);

    static String[] nombresBarcos = {"Portaaviones", "Acorazado", "Crucero", "Submarino", "Destructor"};
    static int[] tamanosBarcos = {5, 4, 3, 3, 2};

    static final String RESET = "\u001B[0m";
    static final String ROJO = "\u001B[31m";
    static final String AZUL = "\u001B[34m";

    public static void main(String[] args) {
        inicializarTablero(tablero1);
        inicializarTablero(tablero2);

        System.out.println("=== BATALLA NAVAL - ACOMODO DE BARCOS ===");

        System.out.println("\n>>> Jugador 1 acomoda sus barcos:");
        colocarFlota(tablero1, AZUL, 'S');

        System.out.println("\n>>> Jugador 2 acomoda sus barcos:");
        colocarFlota(tablero2, ROJO, 'B');

        System.out.println("\n=== TABLERO FINAL JUGADOR 1 ===");
        imprimirTablero(tablero1, AZUL);

        System.out.println("\n=== TABLERO FINAL JUGADOR 2 ===");
        imprimirTablero(tablero2, ROJO);
    }

    static void inicializarTablero(char[][] t) {
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                t[i][j] = '.';
    }

    static void imprimirTablero(char[][] t, String color) {
        System.out.print("   ");
        for (int j = 1; j <= N; j++) System.out.print(j + " ");
        System.out.println();
        for (int i = 0; i < N; i++) {
            System.out.print((char)('A'+i) + "  ");
            for (int j = 0; j < N; j++) {
                if (t[i][j] == '.')
                    System.out.print(". ");
                else
                    System.out.print(color + t[i][j] + RESET + " ");
            }
            System.out.println();
        }
    }

    static void colocarFlota(char[][] tablero, String color, char simbolo) {
        for (int k = 0; k < nombresBarcos.length; k++) {
            boolean colocado = false;
            while (!colocado) {
                imprimirTablero(tablero, color);
                System.out.println("Coloca tu " + nombresBarcos[k] + " (tamaño " + tamanosBarcos[k] + ")");
                System.out.print("Coordenada inicial (ej. A1): ");
                String coord = sc.next().toUpperCase();
                System.out.print("Orientación H o V: ");
                char orient = sc.next().toUpperCase().charAt(0);

                int fila = coord.charAt(0) - 'A';
                int col = Integer.parseInt(coord.substring(1)) - 1;

                if (puedeColocar(tablero, fila, col, orient, tamanosBarcos[k])) {
                    colocar(tablero, fila, col, orient, simbolo, tamanosBarcos[k]);
                    colocado = true;
                } else {
                    System.out.println("No se puede colocar ahí, intenta otra vez.");
                }
            }
        }
    }

    static boolean puedeColocar(char[][] t, int r, int c, char o, int len) {
        if (r < 0 || r >= N || c < 0 || c >= N) return false;
        if (o == 'H') {
            if (c + len > N) return false;
            for (int j = c; j < c + len; j++) if (t[r][j] != '.') return false;
        } else if (o == 'V') {
            if (r + len > N) return false;
            for (int i = r; i < r + len; i++) if (t[i][c] != '.') return false;
        } else return false;
        return true;
    }

    static void colocar(char[][] t, int r, int c, char o, char simbolo, int len) {
        if (o == 'H') {
            for (int j = c; j < c + len; j++) t[r][j] = simbolo;
        } else {
            for (int i = r; i < r + len; i++) t[i][c] = simbolo;
        }
    }
}
