import sys



class Nodo:
    def __init__(self, key):
        self.key = key
        self.left = None
        self.right = None

class BinarySearchTree:
    def __init__(self):
        self.root = None

    def insert(self, key):
        if self.root is None:
            self.root = Nodo(key)
        else:
            self._insert_recursive(self.root, key)

    def _insert_recursive(self, current, key):
        if key < current.key:
            if current.left is None:
                current.left = Nodo(key)
            else:
                self._insert_recursive(current.left, key)
        elif key > current.key:
            if current.right is None:
                current.right = Nodo(key)
            else:
                self._insert_recursive(current.right, key)
        else:
            print(f"⚠ El valor {key} ya existe en el árbol.")

    def search(self, key):
        path = []
        found = self._search_recursive(self.root, key, path)
        if found:
            print(f"✅ Encontrado: {key}")
            print(f"📍 Ruta: {' -> '.join(map(str, path))}")
        else:
            print(f"❌ El valor {key} no se encuentra en el árbol.")
        return found

    def _search_recursive(self, current, key, path):
        if current is None:
            return False
        path.append(current.key)
        if key == current.key:
            return True
        elif key < current.key:
            return self._search_recursive(current.left, key, path)
        else:
            return self._search_recursive(current.right, key, path)

    def delete(self, key):
        self.root = self._delete_recursive(self.root, key)

    def _delete_recursive(self, current, key):
        if current is None:
            return current

        if key < current.key:
            current.left = self._delete_recursive(current.left, key)
        elif key > current.key:
            current.right = self._delete_recursive(current.right, key)
        else:
            
            if current.left is None:
                return current.right
            elif current.right is None:
                return current.left
            
            temp = self._min_value_node(current.right)
            current.key = temp.key
            current.right = self._delete_recursive(current.right, temp.key)
        
        return current

    def _min_value_node(self, node):
        current = node
        while current.left is not None:
            current = current.left
        return current

    def inorder(self):
        result = []
        self._inorder_recursive(self.root, result)
        return result

    def _inorder_recursive(self, node, result):
        if node:
            self._inorder_recursive(node.left, result)
            result.append(node.key)
            self._inorder_recursive(node.right, result)

    def preorder(self):
        result = []
        self._preorder_recursive(self.root, result)
        return result

    def _preorder_recursive(self, node, result):
        if node:
            result.append(node.key)
            self._preorder_recursive(node.left, result)
            self._preorder_recursive(node.right, result)

    def postorder(self):
        result = []
        self._postorder_recursive(self.root, result)
        return result

    def _postorder_recursive(self, node, result):
        if node:
            self._postorder_recursive(node.left, result)
            self._postorder_recursive(node.right, result)
            result.append(node.key)

    def height(self):
        return self._height_recursive(self.root)

    def _height_recursive(self, node):
        if node is None:
            return -1
        left_h = self._height_recursive(node.left)
        right_h = self._height_recursive(node.right)
        return max(left_h, right_h) + 1

    def size(self):
        return self._size_recursive(self.root)

    def _size_recursive(self, node):
        if node is None:
            return 0
        return 1 + self._size_recursive(node.left) + self._size_recursive(node.right)

    def export_inorder(self, filename):
        data = self.inorder()
        try:
            with open(filename, 'w') as f:
                f.write(" ".join(map(str, data)))
            print(f"💾 Árbol exportado (Inorden) exitosamente a: {filename}")
        except IOError as e:
            print(f"Error al escribir el archivo: {e}")


def main():
    bst = BinarySearchTree()
    print("=== Gestor de Árbol Binario de Búsqueda (Python) ===")
    print("Escribe 'help' para ver los comandos.")

    while True:
        try:
            line = input("BST> ").strip()
            if not line:
                continue
            
            parts = line.split()
            command = parts[0].lower()
            
            if command == "exit":
                print("Saliendo...")
                break
            
            elif command == "help":
                print("""
Comandos disponibles:
  insert <n>    : Insertar número
  search <n>    : Buscar número y mostrar ruta
  delete <n>    : Eliminar número
  inorder       : Mostrar recorrido inorden
  preorder      : Mostrar recorrido preorden
  postorder     : Mostrar recorrido posorden
  height        : Mostrar altura
  size          : Mostrar número de nodos
  export <file> : Guardar inorden en archivo
  exit          : Salir
                """)

            elif command == "test":
                print("Ejecutando caso de prueba del PDF...")
                test_values = [45, 15, 79, 90, 10, 55, 12, 20, 50]
                for v in test_values:
                    bst.insert(v)
                print(f"Datos insertados: {test_values}")
                print(f"Inorden actual: {bst.inorder()}")

            elif command in ["inorder", "preorder", "postorder"]:
                if command == "inorder":
                    print(f"Inorden: {bst.inorder()}")
                elif command == "preorder":
                    print(f"Preorden: {bst.preorder()}")
                else:
                    print(f"Posorden: {bst.postorder()}")

            elif command == "height":
                print(f"Altura del árbol: {bst.height()}")

            elif command == "size":
                print(f"Número de nodos: {bst.size()}")

            elif command == "export":
                filename = parts[1] if len(parts) > 1 else "bst_output.txt"
                bst.export_inorder(filename)

            elif command in ["insert", "search", "delete"]:
                if len(parts) < 2:
                    print(f"Uso: {command} <numero>")
                else:
                    try:
                        val = int(parts[1])
                        if command == "insert":
                            bst.insert(val)
                            print(f"Insertado: {val}")
                        elif command == "search":
                            bst.search(val)
                        elif command == "delete":
                            bst.delete(val)
                            print(f"Se ha solicitado eliminar: {val}")
                    except ValueError:
                        print("Error: El argumento debe ser un número entero.")
            else:
                print("Comando no reconocido. Escribe 'help'.")

        except (KeyboardInterrupt, EOFError):
            print("\nSaliendo...")
            break

if __name__ == "__main__":
    main()