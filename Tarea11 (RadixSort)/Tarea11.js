function countingSort(arr, exp) {
    let s = arr.length;
    let outputArray = new Array(s).fill(0);
    let countArray = new Array(10).fill(0);

    for (let j = 0; j < s; j++) {
        let idx = Math.floor(arr[j] / exp) % 10;
        countArray[idx]++;
    }

    for (let j = 1; j < 10; j++)
        countArray[j] += countArray[j - 1];

    for (let j = s - 1; j >= 0; j--) {
        let idx = Math.floor(arr[j] / exp) % 10;
        outputArray[countArray[idx] - 1] = arr[j];
        countArray[idx]--;
    }

    for (let j = 0; j < s; j++)
        arr[j] = outputArray[j];
}

function radixSort(arr) {
    let max1 = Math.max(...arr);
    for (let exp = 1; Math.floor(max1 / exp) > 0; exp *= 10)
        countingSort(arr, exp);
}

let arr = [171, 46, 76, 91, 803, 25, 3, 67];
console.log("Arreglo antes de ordenar:");
console.log(arr.join(" "));

radixSort(arr);

console.log("Después de ordenar el arreglo:");
console.log(arr.join(" "));
