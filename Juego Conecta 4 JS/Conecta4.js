import readline from "readline";
import chalk from "chalk";

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
});

const rows = 6;
const cols = 7;

let board;
let currentPlayer;
const player1 = chalk.red("O");    // ficha roja
const player2 = chalk.yellow("O"); // ficha amarilla

function resetGame() {
  board = Array.from({ length: rows }, () => Array(cols).fill(" "));
  currentPlayer = player1;
}

function printBoard() {
  console.clear();
  console.log("\n   1   2   3   4   5   6   7");
  board.forEach((row) => {
    console.log(" | " + row.join(" | ") + " |");
  });
  console.log("‾".repeat(cols * 4 + 1));
}

function dropPiece(col) {
  for (let i = rows - 1; i >= 0; i--) {
    if (board[i][col] === " ") {
      board[i][col] = currentPlayer;
      return true;
    }
  }
  return false;
}

function checkWin() {
  for (let r = 0; r < rows; r++) {
    for (let c = 0; c < cols; c++) {
      if (
        board[r][c] !== " " &&
        (
          (c + 3 < cols &&
            board[r][c] === board[r][c + 1] &&
            board[r][c] === board[r][c + 2] &&
            board[r][c] === board[r][c + 3]) ||
          (r + 3 < rows &&
            board[r][c] === board[r + 1][c] &&
            board[r][c] === board[r + 2][c] &&
            board[r][c] === board[r + 3][c]) ||
          (r + 3 < rows &&
            c + 3 < cols &&
            board[r][c] === board[r + 1][c + 1] &&
            board[r][c] === board[r + 2][c + 2] &&
            board[r][c] === board[r + 3][c + 3]) ||
          (r + 3 < rows &&
            c - 3 >= 0 &&
            board[r][c] === board[r + 1][c - 1] &&
            board[r][c] === board[r + 2][c - 2] &&
            board[r][c] === board[r + 3][c - 3])
        )
      ) {
        return true;
      }
    }
  }
  return false;
}

function askMove(onGameEnd) {
  printBoard();
  rl.question(
    `Turno de ${currentPlayer === player1 ? "Jugador 1 (rojo)" : "Jugador 2 (amarillo)"} - Elige columna (1-7): `,
    (col) => {
      col = parseInt(col) - 1;
      if (col >= 0 && col < cols) {
        if (dropPiece(col)) {
          if (checkWin()) {
            printBoard();
            console.log(` ${currentPlayer === player1 ? "Jugador 1 (rojo)" : "Jugador 2 (amarillo)"} gana!`);
            onGameEnd();
            return;
          }
          currentPlayer = currentPlayer === player1 ? player2 : player1;
        } else {
          console.log("Columna llena, intenta de nuevo.");
        }
      } else {
        console.log("Opción inválida, elige entre 1 y 7.");
      }
      askMove(onGameEnd);
    }
  );
}
function startGameLoop() {
  resetGame();
  askMove(() => {
    rl.question("¿Quieres jugar otra vez? (s/n): ", (answer) => {
      if (answer.toLowerCase() === "s" || answer.toLowerCase() === "si") {
        startGameLoop(); // reinicia la partida
      } else {
        console.log("¡Gracias por jugar!");
        rl.close();
      }
    });
  });
}

startGameLoop();

