// Batalla Naval completa - Leyva Banda Ángel Yamil (versión final)
import java.util.Scanner;

public class BatallaNaval {
    static final int N = 10;
    static char[][] tablero1 = new char[N][N];
    static char[][] tablero2 = new char[N][N];
    static char[][] disparos1 = new char[N][N];
    static char[][] disparos2 = new char[N][N];
    static Scanner sc = new Scanner(System.in);

    static String[] nombresBarcos = {"Portaaviones", "Acorazado", "Crucero", "Submarino", "Destructor"};
    static int[] tamanosBarcos = {5, 4, 3, 3, 2};

    static final String RESET = "\u001B[0m";
    static final String ROJO = "\u001B[31m";
    static final String AZUL = "\u001B[34m";
    static final String VERDE = "\u001B[32m";
    static final String AMARILLO = "\u001B[33m";

    public static void main(String[] args) {
        inicializarTablero(tablero1);
        inicializarTablero(tablero2);
        inicializarTablero(disparos1);
        inicializarTablero(disparos2);

        System.out.println("=== BATALLA NAVAL ===");
        System.out.println("Tablero de 10x10\n");

        System.out.println(">>> Jugador 1 acomoda sus barcos <<<");
        colocarFlota(tablero1, AZUL, 'X');
        limpiarPantalla();

        System.out.println(">>> Jugador 2 acomoda sus barcos <<<");
        colocarFlota(tablero2, ROJO, 'O');
        limpiarPantalla();

        System.out.println("¡Comienza la batalla!\n");

        boolean turnoJugador1 = true;
        while (true) {
            if (turnoJugador1) {
                System.out.println(AZUL + "\n=== Turno del Jugador 1 ===" + RESET);
                jugarTurno(tablero2, disparos1, AZUL);
                if (flotaHundida(tablero2)) {
                    System.out.println(VERDE + "\n¡Jugador 1 gana la partida! " + RESET);
                    break;
                }
            } else {
                System.out.println(ROJO + "\n=== Turno del Jugador 2 ===" + RESET);
                jugarTurno(tablero1, disparos2, ROJO);
                if (flotaHundida(tablero1)) {
                    System.out.println(VERDE + "\n¡Jugador 2 gana la partida! " + RESET);
                    break;
                }
            }
            turnoJugador1 = !turnoJugador1;
        }
    }

    static void inicializarTablero(char[][] t) {
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                t[i][j] = '.';
            }
        }
    }

    static void imprimirTablero(char[][] t, String color) {
        System.out.print("   ");
        for (int j = 1; j <= N; j++) System.out.printf("%2d", j);
        System.out.println();
        for (int i = 0; i < N; i++) {
            System.out.print((char)('A' + i) + "  ");
            for (int j = 0; j < N; j++) {
                char c = t[i][j];
                if (c == 'X' || c == 'O') System.out.print(color + c + RESET + " ");
                else if (c == '*') System.out.print(ROJO + "* " + RESET);
                else if (c == '~') System.out.print(AMARILLO + "~ " + RESET);
                else System.out.print(". ");
            }
            System.out.println();
        }
    }

    static void colocarFlota(char[][] tablero, String color, char simbolo) {
        for (int k = 0; k < nombresBarcos.length; k++) {
            boolean colocado = false;
            while (!colocado) {
                imprimirTablero(tablero, color);
                System.out.println("\nColoca tu " + nombresBarcos[k] + " (tamaño " + tamanosBarcos[k] + ")");
                System.out.print("Ingresa coordenada inicial (ej: A1): ");
                String coord = sc.next().toUpperCase();

                if (coord.length() < 2) {
                    System.out.println("Coordenada inválida, intenta otra vez.\n");
                    continue;
                }

                System.out.print("Orientación (H = horizontal, V = vertical): ");
                char orient = sc.next().toUpperCase().charAt(0);

                int fila = coord.charAt(0) - 'A';
                int col;
                try {
                    col = Integer.parseInt(coord.substring(1)) - 1;
                } catch (NumberFormatException e) {
                    System.out.println("Coordenada inválida, intenta otra vez.\n");
                    continue;
                }

                if (puedeColocar(tablero, fila, col, orient, tamanosBarcos[k])) {
                    colocar(tablero, fila, col, orient, simbolo, tamanosBarcos[k]);
                    colocado = true;
                } else {
                    System.out.println("No se puede colocar ahí, intenta de nuevo.\n");
                }
            }
        }
    }

    static boolean puedeColocar(char[][] t, int r, int c, char o, int len) {
        if (r < 0 || r >= N || c < 0 || c >= N) return false;

        if (o == 'H') {
            if (c + len > N) return false;
            for (int j = c; j < c + len; j++)
                if (t[r][j] != '.') return false;
        } else if (o == 'V') {
            if (r + len > N) return false;
            for (int i = r; i < r + len; i++)
                if (t[i][c] != '.') return false;
        } else return false;

        return true;
    }

    static void colocar(char[][] t, int r, int c, char o, char simbolo, int len) {
        if (o == 'H') {
            for (int j = c; j < c + len; j++) t[r][j] = simbolo;
        } else if (o == 'V') {
            for (int i = r; i < r + len; i++) t[i][c] = simbolo;
        }
    }

    static void jugarTurno(char[][] tableroEnemigo, char[][] disparos, String color) {
        boolean disparoValido = false;
        while (!disparoValido) {
            System.out.println("\nTu mapa de disparos:");
            imprimirTablero(disparos, color);

            System.out.print("\nIngresa coordenada para disparar (ej: B7): ");
            String coord = sc.next().toUpperCase();
            if (coord.length() < 2) {
                System.out.println("Coordenada inválida.\n");
                continue;
            }

            int fila = coord.charAt(0) - 'A';
            int col;
            try {
                col = Integer.parseInt(coord.substring(1)) - 1;
            } catch (NumberFormatException e) {
                System.out.println("Coordenada inválida.\n");
                continue;
            }

            if (fila < 0 || fila >= N || col < 0 || col >= N) {
                System.out.println("Fuera del tablero.\n");
                continue;
            }

            if (disparos[fila][col] == '*' || disparos[fila][col] == '~') {
                System.out.println("Ya disparaste ahí.\n");
                continue;
            }

            if (tableroEnemigo[fila][col] == 'X' || tableroEnemigo[fila][col] == 'O') {
                System.out.println(VERDE + "¡Impacto!" + RESET);
                disparos[fila][col] = '*';
                tableroEnemigo[fila][col] = '*';
            } else {
                System.out.println(AMARILLO + "Agua..." + RESET);
                disparos[fila][col] = '~';
            }

            disparoValido = true;
        }
    }

    static boolean flotaHundida(char[][] t) {
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                if (t[i][j] == 'X' || t[i][j] == 'O') return false;
            }
        }
        return true;
    }

    static void limpiarPantalla() {
        System.out.print("\033[H\033[2J");
        System.out.flush();
    }
}
