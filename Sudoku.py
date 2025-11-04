import tkinter as tk
from tkinter import messagebox, simpledialog
import random

# Definimos los rangos de dificultad según tu imagen
RANGOS_DIFICULTAD = {
    "MF": (36, 44), # Muy Fácil
    "F":  (32, 35), # Fácil
    "M":  (28, 31), # Medio
    "D":  (24, 27), # Difícil
    "MD": (17, 23)  # Muy Difícil
}

# --- ¡NUEVO! ---
# Implementamos la regla "5 Sudokus x nivel"
# Aquí guardamos 5 tableros resueltos diferentes.
# En una app real, podrías cargarlos desde archivos.
LISTA_TABLEROS_BASE = [
    [ # Tablero 1
        [5, 3, 4, 6, 7, 8, 9, 1, 2],
        [6, 7, 2, 1, 9, 5, 3, 4, 8],
        [1, 9, 8, 3, 4, 2, 5, 6, 7],
        [8, 5, 9, 7, 6, 1, 4, 2, 3],
        [4, 2, 6, 8, 5, 3, 7, 9, 1],
        [7, 1, 3, 9, 2, 4, 8, 5, 6],
        [9, 6, 1, 5, 3, 7, 2, 8, 4],
        [2, 8, 7, 4, 1, 9, 6, 3, 5],
        [3, 4, 5, 2, 8, 6, 1, 7, 9]
    ],
    [ # Tablero 2
        [2, 9, 5, 7, 4, 3, 8, 6, 1],
        [4, 3, 1, 8, 6, 5, 9, 2, 7],
        [8, 7, 6, 1, 9, 2, 5, 4, 3],
        [3, 8, 7, 4, 5, 9, 2, 1, 6],
        [6, 1, 2, 3, 8, 7, 4, 9, 5],
        [5, 4, 9, 2, 1, 6, 7, 3, 8],
        [7, 6, 3, 5, 2, 4, 1, 8, 9],
        [9, 2, 8, 6, 7, 1, 3, 5, 4],
        [1, 5, 4, 9, 3, 8, 6, 7, 2]
    ],
    [ # Tablero 3
        [1, 2, 3, 6, 7, 8, 9, 4, 5],
        [5, 8, 4, 2, 3, 9, 7, 6, 1],
        [9, 6, 7, 1, 4, 5, 3, 2, 8],
        [3, 7, 2, 4, 6, 1, 5, 8, 9],
        [6, 9, 1, 5, 8, 3, 2, 7, 4],
        [4, 5, 8, 7, 9, 2, 6, 1, 3],
        [8, 3, 6, 9, 2, 4, 1, 5, 7],
        [2, 1, 9, 8, 5, 7, 4, 3, 6],
        [7, 4, 5, 3, 1, 6, 8, 9, 2]
    ],
    [ # Tablero 4
        [7, 8, 1, 4, 3, 9, 5, 2, 6],
        [5, 9, 3, 2, 6, 1, 8, 7, 4],
        [4, 6, 2, 8, 7, 5, 1, 3, 9],
        [9, 3, 7, 1, 5, 4, 6, 8, 2],
        [1, 5, 8, 6, 2, 3, 9, 4, 7],
        [6, 2, 4, 9, 8, 7, 3, 5, 1],
        [8, 7, 9, 5, 1, 2, 4, 6, 3],
        [2, 1, 6, 3, 4, 8, 7, 9, 5],
        [3, 4, 5, 7, 9, 6, 2, 1, 8]
    ],
    [ # Tablero 5
        [3, 1, 6, 5, 7, 8, 4, 9, 2],
        [5, 2, 9, 1, 3, 4, 7, 6, 8],
        [4, 8, 7, 6, 2, 9, 5, 3, 1],
        [2, 6, 3, 4, 1, 5, 9, 8, 7],
        [9, 7, 4, 8, 6, 3, 1, 2, 5],
        [8, 5, 1, 7, 9, 2, 6, 4, 3],
        [1, 3, 8, 9, 4, 7, 2, 5, 6],
        [6, 9, 2, 3, 5, 1, 8, 7, 4],
        [7, 4, 5, 2, 8, 6, 3, 1, 9]
    ]
]


class SudokuGUI:
    def __init__(self, root):
        self.root = root
        self.root.title("Sudoku (25 Juegos)")
        self.vidas = 5

        # 1. Preguntar la dificultad
        self.dificultad = self.preguntar_dificultad()
        if not self.dificultad:
            self.root.destroy()
            return

        # 2. Generar el tablero
        # --- ¡MODIFICADO! ---
        # Elegimos uno de los 5 tableros al azar
        self.tablero_resuelto = random.choice(LISTA_TABLEROS_BASE)
        self.tablero_puzzle = self.crear_puzzle(self.dificultad)

        # 3. Crear la GUI
        self.celdas_entry = {} # Diccionario para guardar los widgets Entry
        self.frame_grid = tk.Frame(self.root)
        self.frame_grid.pack(padx=10, pady=10)
        self.crear_grid() # Esta función dibuja el tablero

        # 4. Mostrar vidas y botón
        self.label_vidas = tk.Label(self.root, text=f"Vidas: {self.vidas}", font=('Arial', 16, 'bold'), fg="blue")
        self.label_vidas.pack(pady=5)

        self.btn_revisar = tk.Button(self.root, text="Revisar Solución", font=('Arial', 14), command=self.revisar_solucion)
        self.btn_revisar.pack(pady=10)

    def preguntar_dificultad(self):
        """Muestra un diálogo para elegir la dificultad."""
        niveles = list(RANGOS_DIFICULTAD.keys())
        prompt = f"Elige un nivel de dificultad:\n{', '.join(niveles)}"
        
        while True:
            nivel = simpledialog.askstring("Dificultad", prompt, parent=self.root)
            if nivel and nivel.upper() in niveles:
                return nivel.upper()
            elif nivel is None: # Si el usuario cierra la ventana
                return None
            messagebox.showwarning("Error", "Nivel no válido. Intenta de nuevo.")

    def crear_puzzle(self, dificultad_key):
        """Crea un puzzle quitando números del tablero resuelto."""
        min_pistas, max_pistas = RANGOS_DIFICULTAD[dificultad_key]
        num_pistas = random.randint(min_pistas, max_pistas)
        
        # Crea una copia para el puzzle
        puzzle = [row[:] for row in self.tablero_resuelto]
        
        celdas_a_quitar = 81 - num_pistas
        
        quitadas = 0
        while quitadas < celdas_a_quitar:
            fila = random.randint(0, 8)
            col = random.randint(0, 8)
            if puzzle[fila][col] != 0:
                puzzle[fila][col] = 0
                quitadas += 1
                
        return puzzle

    def crear_grid(self):
        """
        Dibuja el tablero 9x9 en la ventana.
        ESTA ES LA FUNCIÓN CLAVE QUE CONECTA LOS DATOS CON LA GUI.
        """
        for fila in range(9):
            for col in range(9):
                # Define el color de fondo para los sub-cuadrados 3x3
                bg_color = "#DDEEFF" if (fila // 3 + col // 3) % 2 == 0 else "#FFFFFF"
                
                # Define bordes más gruesos para los sub-cuadrados
                pad_x = (5 if col % 3 == 0 else 1)
                pad_y = (5 if fila % 3 == 0 else 1)
                
                celda_frame = tk.Frame(self.frame_grid, borderwidth=1, relief="solid")
                celda_frame.grid(row=fila, column=col, sticky="nsew", padx=(pad_x, 0), pady=(pad_y, 0))

                # --- ¡AQUÍ ESTÁ LA LÓGICA! ---
                # 1. Mira el valor en nuestro tablero de datos
                valor = self.tablero_puzzle[fila][col]

                if valor != 0:
                    # 2. Si el valor NO es 0, es una PISTA.
                    # Creamos una 'Label' (etiqueta) que no se puede editar.
                    label = tk.Label(celda_frame, text=str(valor), 
                                     font=('Arial', 20, 'bold'), 
                                     width=2, height=1, bg=bg_color, fg="#333333")
                    label.pack(padx=5, pady=5)
                else:
                    # 3. Si el valor ES 0, es un HUECO.
                    # Creamos una 'Entry' (campo de texto) para que el usuario escriba.
                    entry = tk.Entry(celda_frame, width=2, 
                                     font=('Arial', 20), 
                                     justify='center', 
                                     relief="flat", bg=bg_color)
                    entry.pack(padx=5, pady=5)
                    
                    # 4. Guardamos la referencia a la celda de entrada
                    # para poder leerla después al "Revisar".
                    self.celdas_entry[(fila, col)] = entry
        
        # Hacemos que las celdas se expandan si se cambia el tamaño de la ventana
        for i in range(9):
            self.frame_grid.grid_rowconfigure(i, weight=1)
            self.frame_grid.grid_columnconfigure(i, weight=1)


    def revisar_solucion(self):
        """Comprueba las respuestas del usuario contra la solución."""
        errores = 0
        celdas_llenas = 0
        
        # Itera sobre las celdas que guardamos en el diccionario
        for (fila, col), entry in self.celdas_entry.items():
            valor_usuario = entry.get()
            
            if valor_usuario:
                celdas_llenas += 1
                try:
                    num_usuario = int(valor_usuario)
                    # Compara el valor del usuario con la solución real
                    if num_usuario == self.tablero_resuelto[fila][col]:
                        entry.config(fg="green") # Correcto
                    else:
                        entry.config(fg="red") # Incorrecto
                        errores += 1
                except ValueError:
                    entry.config(fg="red") # No es un número
                    errores += 1

        if errores > 0:
            self.vidas -= 1
            self.label_vidas.config(text=f"Vidas: {self.vidas}")
            messagebox.showwarning("Incorrecto", f"Tienes {errores} errores. ¡Pierdes una vida!")
            
            if self.vidas <= 0:
                messagebox.showerror("Game Over", "¡Te has quedado sin vidas! Fin del juego.")
                self.root.destroy()
        else:
            if celdas_llenas == len(self.celdas_entry):
                messagebox.showinfo("¡Felicidades!", "¡Has resuelto el Sudoku!")
                self.root.destroy()
            else:
                messagebox.showinfo("¡Vas bien!", "Todas las respuestas son correctas, pero aún no terminas.")


# --- Ejecución principal ---
if __name__ == "__main__":
    root = tk.Tk()
    app = SudokuGUI(root)
    root.mainloop()