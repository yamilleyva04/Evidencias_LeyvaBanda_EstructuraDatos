function swap(arr, i, j) {
    let temp = arr[i];
    arr[i] = arr[j];
    arr[j] = temp;
}

function partition(arr, l, h) {
    let pivot = arr[h];
    let j = l - 1;
    for (let k = l; k < h; k++) {
        if (arr[k] < pivot) {
            j++;
            swap(arr, j, k);
        }
    }
    swap(arr, j + 1, h);
    return j + 1;
}

function quickSort(arr, l, h) {
    if (l < h) {
        let pi = partition(arr, l, h);
        quickSort(arr, l, pi - 1);
        quickSort(arr, pi + 1, h);
    }
}

let arr = [10, 7, 8, 9, 1, 5];

console.log("Antes:", arr.join(" "));

quickSort(arr, 0, arr.length - 1);

console.log("Después:", arr.join(" "));
