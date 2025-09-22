function insertionSort(arr) {
    for (let i = 1; i < arr.length; i++) {
        let temp = arr[i];
        let j = i - 1;
        while (j >= 0 && arr[j] > temp) {
            arr[j + 1] = arr[j];
            j--;
        }
        arr[j + 1] = temp;
    }
}

function printArr(arr) {
    console.log(arr.join(" "));
}

let arr = [70, 15, 2, 51, 60];

console.log("Antes de ordenar: ");
printArr(arr);

insertionSort(arr);

console.log("Después de ordenar: ");
printArr(arr);
