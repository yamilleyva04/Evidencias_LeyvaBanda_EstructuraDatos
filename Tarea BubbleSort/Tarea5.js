function bubbleSort(arr) {
    let n = arr.length;
    for (let i = 0; i < n; i++) {
        let intercambio = false;
        for (let j = 0; j < n - i - 1; j++) {
            if (arr[j] > arr[j + 1]) {
                [arr[j], arr[j + 1]] = [arr[j + 1], arr[j]];
                intercambio = true;
            }
        }
        if (!intercambio) break;
    }
    return arr;
}

let arr = [56, 32, 17, 99, 23, 10, 78];
console.log("Arreglo original:", arr);

bubbleSort(arr);

console.log("Arreglo ordenado:", arr);
