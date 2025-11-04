class ShellSort:
    @staticmethod
    def displayArr(inputArr):
        for k in inputArr:
            print(k, end=" ")
        print()

    def sort(self, inputArr):
        size = len(inputArr)
        gapsize = size // 2

        while gapsize > 0:
            for j in range(gapsize, size):
                val = inputArr[j]
                k = j
                while k >= gapsize and inputArr[k - gapsize] > val:
                    inputArr[k] = inputArr[k - gapsize]
                    k = k - gapsize
                inputArr[k] = val
            gapsize = gapsize // 2

if __name__ == "__main__":
    inputArr = [36, 34, 43, 11, 15, 20, 28, 45]
    print("Arreglo antes de ser ordenado: ")
    ShellSort.displayArr(inputArr)

    obj = ShellSort()
    obj.sort(inputArr)

    print("Arreglo después de ser ordenado: ")
    ShellSort.displayArr(inputArr)