#include <iostream>
#include <windows.h> 
using namespace std;

void MostrarTablero(char tablero[3][3]) { //Funcion para mostrar el tablero al iniciar
    HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);//Lee la consola para agregar color despues
    cout << "\n";
    for (int i = 0; i < 3; i++) {
        cout << " ";
        for (int j = 0; j < 3; j++) {
            char c = tablero[i][j];
            if (c == 'X') SetConsoleTextAttribute(hConsole, 12); //agrega color rojo
            else if (c == 'O') SetConsoleTextAttribute(hConsole, 10); //agrega color verde
            else SetConsoleTextAttribute(hConsole, 9); //los numeros quedan en el color azul por el system01
            cout << c;
            SetConsoleTextAttribute(hConsole, 9); //se mantienen los separadores en azul
            if (j < 2) cout << " | ";//ciclo anidado para recorrer filas y columnas
        }
        cout << "\n";
        if (i < 2) cout << "---+---+---\n"; 
    }
    cout << "\n";
}

bool ganador(char tab[3][3], char jugador) {//funcion para saber el ganador
    for (int i = 0; i < 3; i++)
        if (tab[i][0] == jugador && tab[i][1] == jugador && tab[i][2] == jugador)//recorre las filas de manera horizontal y verifica si todas las columnas tienen la misma marca
            return true;

    for (int j = 0; j < 3; j++)
        if (tab[0][j] == jugador && tab[1][j] == jugador && tab[2][j] == jugador) //recorre las columnas de manera vertical y verifica si todas las columnas tienen la misma marca
            return true;

    if (tab[0][0] == jugador && tab[1][1] == jugador && tab[2][2] == jugador) return true;//recorre de manera diagonal y verifica si todas las columnas tienen la misma marca
    if (tab[0][2] == jugador && tab[1][1] == jugador && tab[2][0] == jugador) return true;

    return false;
}

bool empate(char tableroo[3][3]) {//funcion para empate
    for (int i = 0; i < 3; i++)//Recorre todas las filas y columnas
        for (int j = 0; j < 3; j++)
            if (tableroo[i][j] != 'X' && tableroo[i][j] != 'O')//verifica si hay casillas sin marcar
                return false;//si encuentra una casilla sin marcar manda false
    return true;//si todas las casillas estan marcadas manda true
}

int main() {
    system("color 01");
    char seguir;//variable para continuar (es para el ciclo do while)

    do {//ciclo do para continuar partida o no
        system("cls");//funcion para limpiar pantalla
        char Tablerooo[3][3] = { {'1','2','3'}, {'4','5','6'}, {'7','8','9'} };//crea el tablero y lo muestra
        char Jugador = 'X';
        int movi;

        cout << "----------------------------GATO---------------------------\n";
        cout << "INSTRUCCIONES\n";
        cout << "Elige un numero del 1-9 para elegir la casilla.\n";
        cout << "Al hacer linea de manera horizontal/vertical o diagonal ganaras\n";
        cout << "----------------------------SUERTE---------------------------\n";


        while (true) {
            MostrarTablero(Tablerooo);//manda a llamar la funcion y la muestra

            cout << "Turno del jugador " << Jugador << ". Ingresa un numero del 1-9 para seleccionar tu casilla: ";
            cin >> movi;//lee el movimiento que se realizo

            if (movi < 1 || movi > 9) {//validacion para permitir solamente elementos del 1-9
                cout << "Movimiento invalido. Intenta de nuevo.\n";
                continue;//regresa el while
            }

            int row = (movi - 1) / 3;
            int col = (movi - 1) % 3;

            if (Tablerooo[row][col] == 'X' || Tablerooo[row][col] == 'O') {//validacion para mostrar que la casilla esta ocupada
                cout << "Casilla ocupada. Intenta de nuevo.\n";
                continue;//regresa el while
            }

            Tablerooo[row][col] = Jugador;

            if (ganador(Tablerooo, Jugador)) {//muestra ganador 
                MostrarTablero(Tablerooo);
                cout << "Felicidades! Gano el jugador " << Jugador << "!!!!!\n";
                break;
            }
            else if (empate(Tablerooo)) {//muestra empate
                MostrarTablero(Tablerooo);
                cout << "Empate. Ningun jugador gano:(.\n";
                break;
            }

            if (Jugador == 'X') {//condicion para alternar jugadores
                Jugador = 'O';
            }
            else {
                Jugador = 'X';
            }
        }

        cout << "Quieres Jugar otra vez? (s/n): ";//pregunta si quiere volver a jugar
        cin >> seguir;

    } while (seguir == 's' || seguir == 'S');//fin del ciclo do while 

    cout << "Gracias por jugar. Hasta luego!!!!!\n"; 
    return 0;
}
