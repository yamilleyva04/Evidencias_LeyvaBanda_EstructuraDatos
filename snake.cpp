#include <iostream>
#include <vector>
#include <cstdlib>
#include <ctime>
#include <windows.h>
#include <conio.h>
#include <string>
#include <algorithm>
#include <cmath>
#include <fstream>
#include <sstream>

using namespace std;

struct Coordenada {
    int x, y;
    bool operator==(const Coordenada& otro) const {
        return x == otro.x && y == otro.y;
    }
};

struct NodoCuerpo {
    Coordenada pos;
    NodoCuerpo* siguiente;
    NodoCuerpo(Coordenada p) : pos(p), siguiente(nullptr) {}
};

struct Puntaje {
    string nombre;
    int score;
};

void get_console_size(int& width, int& height) {
    CONSOLE_SCREEN_BUFFER_INFO csbi;
    GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), &csbi);
    width = csbi.srWindow.Right - csbi.srWindow.Left + 1;
    height = csbi.srWindow.Bottom - csbi.srWindow.Top + 1;
}

void gotoxy(int x, int y) {
    COORD coord;
    coord.X = x;
    coord.Y = y;
    SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), coord);
}

void set_color(int text_color, int bg_color = 0) {
    SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), text_color + (bg_color * 16));
}

const int COLOR_BORDE = 2;
const int COLOR_SNAKE = 13;
const int COLOR_COMIDA = 12;
const int COLOR_TRAMPA = 6;
const int COLOR_SCORE = 15;
const int COLOR_TITULO = 11;

void liberar_memoria(NodoCuerpo*& head) {
    NodoCuerpo* actual = head;
    while (actual != nullptr) {
        NodoCuerpo* siguiente = actual->siguiente;
        delete actual;
        actual = siguiente;
    }
    head = nullptr;
}

class JuegoSnake {
private:
    const int ANCHO = 60;
    const int ALTO = 20;
    const int SCORE_AREA_W = 20;
    const string ARCHIVO_SCORES = "snake_highscores.txt";
    const string SAVE_GAME_FILE = "snake_save.dat";
    const string SAVE_SNAKE_FILE = "snake_save_snake.dat";

    int offset_x;
    int offset_y;
    NodoCuerpo* cabeza;
    Coordenada comida;
    Coordenada trampa;
    bool hay_trampa;
    char direccion;
    bool fin_juego;
    int score;
    int comida_contador;
    int nivel;
    double velocidad;
    vector<Coordenada> obstaculos;
    vector<Puntaje> top_scores;
    HANDLE hConsole;
    CONSOLE_CURSOR_INFO cursorInfo;

public:
    JuegoSnake() : cabeza(nullptr), direccion('d'), fin_juego(false),
        score(0), comida_contador(0), offset_x(0), offset_y(0),
        nivel(1), velocidad(100), hay_trampa(false) {

        srand(time(0));
        hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
        GetConsoleCursorInfo(hConsole, &cursorInfo);
        cursorInfo.bVisible = false;
        SetConsoleCursorInfo(hConsole, &cursorInfo);

        cargar_scores();
        calcular_offsets();
    }

    ~JuegoSnake() {
        liberar_memoria(cabeza);
        guardar_scores();
        cursorInfo.bVisible = true;
        SetConsoleCursorInfo(hConsole, &cursorInfo);
    }

    void cargar_scores() {
        ifstream archivo(ARCHIVO_SCORES);
        if (!archivo.is_open()) return;
        string nombre;
        int s;
        top_scores.clear();
        while (archivo >> nombre >> s) {
            top_scores.push_back({ nombre, s });
        }
        archivo.close();
        sort(top_scores.begin(), top_scores.end(), [](const Puntaje& a, const Puntaje& b) {
            return a.score > b.score;
            });
        while (top_scores.size() > 5) {
            top_scores.pop_back();
        }
    }

    void guardar_scores() {
        ofstream archivo(ARCHIVO_SCORES);
        if (!archivo.is_open()) return;
        for (const auto& p : top_scores) {
            archivo << p.nombre << " " << p.score << endl;
        }
        archivo.close();
    }

    bool es_top_score(int nuevo_score) {
        if (nuevo_score <= 0) return false;
        if (top_scores.size() < 5) return true;
        return nuevo_score > top_scores.back().score;
    }

    void agregar_score(const string& nombre, int nuevo_score) {
        top_scores.push_back({ nombre, nuevo_score });
        sort(top_scores.begin(), top_scores.end(), [](const Puntaje& a, const Puntaje& b) {
            return a.score > b.score;
            });
        while (top_scores.size() > 5) {
            top_scores.pop_back();
        }
    }

    string leer_nombre(int x, int y) {
        string nombre = "";
        char c;
        cursorInfo.bVisible = true;
        SetConsoleCursorInfo(hConsole, &cursorInfo);
        gotoxy(x, y);
        while (true) {
            c = _getch();
            if (c == 13) {
                if (nombre.empty()) nombre = "AAA";
                break;
            }
            else if (c == 8) {
                if (!nombre.empty()) {
                    nombre.pop_back();
                    gotoxy(x + nombre.length(), y);
                    cout << " ";
                    gotoxy(x + nombre.length(), y);
                }
            }
            else if (isprint(c) && nombre.length() < 10) {
                nombre += c;
                cout << c;
            }
        }
        cursorInfo.bVisible = false;
        SetConsoleCursorInfo(hConsole, &cursorInfo);
        return nombre;
    }

    void guardar_partida() {
        ofstream archivo_juego(SAVE_GAME_FILE);
        if (!archivo_juego.is_open()) return;

        archivo_juego << score << " " << nivel << " " << velocidad << " "
            << direccion << " " << comida_contador << endl;
        archivo_juego << comida.x << " " << comida.y << endl;
        archivo_juego << hay_trampa << endl;
        if (hay_trampa) {
            archivo_juego << trampa.x << " " << trampa.y << endl;
        }
        archivo_juego << obstaculos.size() << endl;
        for (const auto& obs : obstaculos) {
            archivo_juego << obs.x << " " << obs.y << endl;
        }
        archivo_juego.close();

        ofstream archivo_serpiente(SAVE_SNAKE_FILE);
        if (!archivo_serpiente.is_open()) return;
        NodoCuerpo* actual = cabeza;
        while (actual != nullptr) {
            archivo_serpiente << actual->pos.x << " " << actual->pos.y << endl;
            actual = actual->siguiente;
        }
        archivo_serpiente.close();

        set_color(COLOR_SCORE);
        gotoxy(offset_x + (ANCHO / 2) - 8, offset_y + ALTO + 3);
        cout << "PARTIDA GUARDADA!";
        Sleep(1000);
        gotoxy(offset_x + (ANCHO / 2) - 8, offset_y + ALTO + 3);
        cout << "                 ";
    }

    bool cargar_partida() {
        ifstream archivo_juego(SAVE_GAME_FILE);
        if (!archivo_juego.is_open()) return false;

        archivo_juego >> score >> nivel >> velocidad >> direccion >> comida_contador;
        archivo_juego >> comida.x >> comida.y;
        archivo_juego >> hay_trampa;
        if (hay_trampa) {
            archivo_juego >> trampa.x >> trampa.y;
        }
        int obs_size;
        archivo_juego >> obs_size;
        obstaculos.clear();
        for (int i = 0; i < obs_size; ++i) {
            Coordenada obs;
            archivo_juego >> obs.x >> obs.y;
            obstaculos.push_back(obs);
        }
        archivo_juego.close();

        ifstream archivo_serpiente(SAVE_SNAKE_FILE);
        if (!archivo_serpiente.is_open()) return false;

        liberar_memoria(cabeza);
        int x, y;
        NodoCuerpo* actual = nullptr;
        while (archivo_serpiente >> x >> y) {
            NodoCuerpo* nuevo_nodo = new NodoCuerpo({ x, y });
            if (cabeza == nullptr) {
                cabeza = nuevo_nodo;
                actual = cabeza;
            }
            else {
                actual->siguiente = nuevo_nodo;
                actual = actual->siguiente;
            }
        }
        archivo_serpiente.close();

        fin_juego = false;
        return cabeza != nullptr;
    }

    void generar_obstaculos(int num_a_generar = 1) {
        for (int i = 0; i < num_a_generar; ++i) {
            Coordenada nueva_obs;
            bool colision;
            do {
                colision = false;
                nueva_obs.x = 1 + rand() % ANCHO;
                nueva_obs.y = 1 + rand() % ALTO;
                NodoCuerpo* actual = cabeza;
                while (actual != nullptr) {
                    if (nueva_obs == actual->pos) {
                        colision = true;
                        break;
                    }
                    actual = actual->siguiente;
                }
                if (colision) continue;
                if (nueva_obs == comida || (hay_trampa && nueva_obs == trampa)) {
                    colision = true;
                    continue;
                }
                for (const auto& obs : obstaculos) {
                    if (nueva_obs == obs) {
                        colision = true;
                        break;
                    }
                }
            } while (colision);
            obstaculos.push_back(nueva_obs);
        }
    }

    void generar_comida() {
        bool colision;
        do {
            colision = false;
            comida.x = 1 + rand() % ANCHO;
            comida.y = 1 + rand() % ALTO;
            NodoCuerpo* actual = cabeza;
            while (actual != nullptr) {
                if (comida == actual->pos) {
                    colision = true;
                    break;
                }
                actual = actual->siguiente;
            }
            if (colision) continue;
            for (const auto& obs : obstaculos) {
                if (comida == obs) {
                    colision = true;
                    break;
                }
            }
        } while (colision);
    }

    void generar_trampa() {
        bool colision;
        do {
            colision = false;
            trampa.x = 1 + rand() % ANCHO;
            trampa.y = 1 + rand() % ALTO;
            NodoCuerpo* actual = cabeza;
            while (actual != nullptr) {
                if (trampa == actual->pos) {
                    colision = true;
                    break;
                }
                actual = actual->siguiente;
            }
            if (colision) continue;
            if (trampa == comida) colision = true;
            for (const auto& obs : obstaculos)
                if (trampa == obs) { colision = true; break; }
        } while (colision);
        hay_trampa = true;
    }

    void calcular_offsets() {
        int screen_w, screen_h;
        get_console_size(screen_w, screen_h);
        const int TOTAL_CONTENT_W = (ANCHO + 2) + SCORE_AREA_W;
        const int TOTAL_CONTENT_H = ALTO + 2;
        offset_x = (screen_w - TOTAL_CONTENT_W) / 2;
        offset_y = (screen_h - TOTAL_CONTENT_H) / 2;
        if (offset_x < 0) offset_x = 0;
        if (offset_y < 0) offset_y = 0;
    }

    void inicializar_juego() {
        liberar_memoria(cabeza);
        fin_juego = false;
        direccion = 'd';
        nivel = 1;
        velocidad = 100;
        calcular_offsets();
        cabeza = new NodoCuerpo({ ANCHO / 4, ALTO / 2 });
        NodoCuerpo* actual = cabeza;
        for (int i = 1; i < 5; ++i) {
            actual->siguiente = new NodoCuerpo({ ANCHO / 4 - i, ALTO / 2 });
            actual = actual->siguiente;
        }
        obstaculos.clear();
        generar_comida();
        generar_obstaculos(1);
        hay_trampa = false;
    }

    void dibujar_borde() {
        set_color(COLOR_TITULO);
        string titulo = "=== SNAKE GAME ===";
        gotoxy(offset_x + (ANCHO + 2) / 2 - (titulo.length() / 2), offset_y - 2);
        cout << titulo;
        set_color(COLOR_BORDE);
        for (int i = 0; i < ANCHO + 2; i++) {
            gotoxy(offset_x + i, offset_y + 0); cout << "#";
            gotoxy(offset_x + i, offset_y + ALTO + 1); cout << "#";
        }
        for (int i = 1; i <= ALTO; i++) {
            gotoxy(offset_x + 0, offset_y + i); cout << "#";
            gotoxy(offset_x + ANCHO + 1, offset_y + i); cout << "#";
        }
    }

    void actualizar_score_display() {
        set_color(COLOR_SCORE);
        const int SCORE_X_START = offset_x + ANCHO + 3;
        int current_high_score = (top_scores.empty()) ? 0 : top_scores[0].score;

        gotoxy(SCORE_X_START, offset_y + 2);  cout << "Nivel: " << nivel << "      ";
        gotoxy(SCORE_X_START, offset_y + 4);  cout << "SCORE: " << score << "      ";
        gotoxy(SCORE_X_START, offset_y + 5);  cout << "HIGH SCORE: " << current_high_score << "      ";
        gotoxy(SCORE_X_START, offset_y + 6);  cout << "COMIDA: " << comida_contador << "      ";

        gotoxy(SCORE_X_START, offset_y + 10); cout << "W, A, S, D para mover";
        gotoxy(SCORE_X_START, offset_y + 11); cout << "X: Salir";
        gotoxy(SCORE_X_START, offset_y + 12); cout << "G: Guardar";

        gotoxy(SCORE_X_START, offset_y + 13); cout << "---- TOP 5 ----      ";
        for (int i = 0; i < 5; ++i) {
            gotoxy(SCORE_X_START, offset_y + 14 + i);
            if (i < top_scores.size()) {
                string nombre_trunc = top_scores[i].nombre.substr(0, 8);
                cout.width(8);
                cout << left << nombre_trunc << ": " << top_scores[i].score << "          ";
            }
            else {
                cout << i + 1 << ". ---                 ";
            }
        }
        cout.width(0);
    }

    void input() {
        if (_kbhit()) {
            char key = tolower(_getch());
            switch (key) {
            case 'w': if (direccion != 's') direccion = 'w'; break;
            case 's': if (direccion != 'w') direccion = 's'; break;
            case 'a': if (direccion != 'd') direccion = 'a'; break;
            case 'd': if (direccion != 'a') direccion = 'd'; break;
            case 'x': fin_juego = true; break;
            case 'g': guardar_partida(); break;
            }
        }
    }

    void cortar_cola() {
        if (cabeza == nullptr || cabeza->siguiente == nullptr) return;
        NodoCuerpo* actual = cabeza;
        NodoCuerpo* previo = nullptr;
        while (actual->siguiente != nullptr) {
            previo = actual;
            actual = actual->siguiente;
        }
        gotoxy(actual->pos.x + offset_x, actual->pos.y + offset_y);
        cout << " ";
        if (previo != nullptr) {
            previo->siguiente = nullptr;
        }
        delete actual;
    }

    int longitud_serpiente() {
        int len = 0;
        NodoCuerpo* actual = cabeza;
        while (actual != nullptr) {
            len++;
            actual = actual->siguiente;
        }
        return len;
    }

    void mover_serpiente(bool crecer) {
        Coordenada nueva_pos = cabeza->pos;
        switch (direccion) {
        case 'w': nueva_pos.y--; break;
        case 's': nueva_pos.y++; break;
        case 'a': nueva_pos.x--; break;
        case 'd': nueva_pos.x++; break;
        }
        NodoCuerpo* nueva_cabeza = new NodoCuerpo(nueva_pos);
        nueva_cabeza->siguiente = cabeza;
        cabeza = nueva_cabeza;
        if (!crecer) cortar_cola();
    }

    void logica() {
        if (cabeza == nullptr) {
            fin_juego = true;
            return;
        }

        if (cabeza->pos == comida) {
            score += 10;
            comida_contador++;
            mover_serpiente(true);
            generar_comida();
            if (comida_contador % 2 == 0)
                generar_obstaculos(1);
            if (!hay_trampa && comida_contador % 3 == 0)
                generar_trampa();
            if (longitud_serpiente() >= 10 * nivel) {
                nivel++;
                velocidad = velocidad * 0.9;
            }
        }
        else mover_serpiente(false);

        if (hay_trampa && cabeza->pos == trampa) {
            cortar_cola();
            hay_trampa = false;
            if (longitud_serpiente() <= 1)
                fin_juego = true;
        }

        if (cabeza->pos.x <= 0) cabeza->pos.x = ANCHO;
        else if (cabeza->pos.x > ANCHO) cabeza->pos.x = 1;
        if (cabeza->pos.y <= 0) cabeza->pos.y = ALTO;
        else if (cabeza->pos.y > ALTO) cabeza->pos.y = 1;

        for (const auto& obs : obstaculos)
            if (cabeza->pos == obs) { fin_juego = true; return; }

        NodoCuerpo* actual = cabeza->siguiente;
        while (actual != nullptr) {
            if (cabeza->pos == actual->pos) {
                fin_juego = true;
                return;
            }
            actual = actual->siguiente;
        }
    }

    void dibujar() {
        set_color(COLOR_BORDE);
        for (const auto& obs : obstaculos) {
            gotoxy(obs.x + offset_x, obs.y + offset_y);
            cout << (char)219;
        }
        if (hay_trampa) {
            set_color(COLOR_TRAMPA);
            gotoxy(trampa.x + offset_x, trampa.y + offset_y);
            cout << "X";
        }
        set_color(COLOR_SNAKE);
        NodoCuerpo* actual = cabeza;
        while (actual != nullptr) {
            gotoxy(actual->pos.x + offset_x, actual->pos.y + offset_y);
            cout << (actual == cabeza ? '@' : 'o');
            actual = actual->siguiente;
        }
        set_color(COLOR_COMIDA);
        gotoxy(comida.x + offset_x, comida.y + offset_y);
        cout << 'O';
        actualizar_score_display();
    }

    char mostrar_menu() {
        system("cls");
        calcular_offsets();
        int menu_x = offset_x + (ANCHO + 2 + SCORE_AREA_W) / 2 - 10;
        int menu_y = offset_y + ALTO / 2 - 3;

        set_color(COLOR_TITULO);
        gotoxy(menu_x - 2, menu_y - 2); cout << "=== SNAKE GAME ===";
        set_color(COLOR_SCORE);
        gotoxy(menu_x, menu_y + 1); cout << "1. Nueva Partida";
        gotoxy(menu_x, menu_y + 2); cout << "2. Cargar Partida";
        gotoxy(menu_x, menu_y + 3); cout << "3. Salir";

        return tolower(_getch());
    }

    void iniciar_partida_loop(bool es_nueva_partida) {
        if (es_nueva_partida) {
            score = 0;
            comida_contador = 0;
            inicializar_juego();
        }
        else {
            if (!cargar_partida()) {
                gotoxy(offset_x + 10, offset_y + ALTO / 2 + 5);
                set_color(COLOR_COMIDA);
                cout << "Error: No se encontro partida guardada.";
                Sleep(1500);
                return;
            }
        }

        system("cls");
        dibujar_borde();

        while (true) {
            if (!fin_juego) {
                input();
                logica();
                dibujar();
                Sleep((int)velocidad);
            }
            else {
                bool nuevo_top_score = es_top_score(score);
                int current_high_score = (top_scores.empty()) ? 0 : top_scores[0].score;
                set_color(COLOR_SCORE);
                string mensaje_go = "GAME OVER. RECORD: " + to_string(current_high_score) + " Score: " + to_string(score);
                int centro_x_relativo = (ANCHO + 2) / 2 - (mensaje_go.length() / 2);
                int y_base = offset_y + ALTO / 2 - 1;

                gotoxy(centro_x_relativo + offset_x, y_base);
                cout << "===============================";
                gotoxy(centro_x_relativo + offset_x, y_base + 1);
                cout << mensaje_go;

                int y_prompt = y_base + 2;

                if (nuevo_top_score) {
                    gotoxy(centro_x_relativo + offset_x, y_prompt);
                    cout << "¡NUEVO TOP 5! Nombre: ";
                    string nombre = leer_nombre(centro_x_relativo + offset_x + 22, y_prompt);
                    agregar_score(nombre, score);
                    guardar_scores();
                    actualizar_score_display();
                    y_prompt++;
                }

                gotoxy(centro_x_relativo + offset_x, y_prompt);
                cout << "Jugar de nuevo? (S/N): ";

                char respuesta = tolower(_getch());
                while (respuesta != 's' && respuesta != 'n') {
                    respuesta = tolower(_getch());
                }

                if (respuesta == 's') {
                    score = 0;
                    comida_contador = 0;
                    inicializar_juego();
                    system("cls");
                    dibujar_borde();
                }
                else {
                    guardar_scores();
                    break;
                }
            }
        }
    }

    void run() {
        while (true) {
            char opcion = mostrar_menu();
            if (opcion == '1') {
                iniciar_partida_loop(true);
            }
            else if (opcion == '2') {
                iniciar_partida_loop(false);
            }
            else if (opcion == '3' || opcion == 'x') {
                return;
            }
        }
    }
};

int main() {
    JuegoSnake juego;
    juego.run();

    gotoxy(0, 25);
    set_color(COLOR_SCORE);
    cout << "\nPresiona cualquier tecla para salir...";
    _getch();
    return 0;
}