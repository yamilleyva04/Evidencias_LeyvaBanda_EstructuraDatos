function merge(a, l, m, r) {
    let a1 = m - l + 1;
    let a2 = r - m;

    let L = new Array(a1);
    let R = new Array(a2);

    for (let i = 0; i < a1; i++)
        L[i] = a[l + i];
    for (let j = 0; j < a2; j++)
        R[j] = a[m + 1 + j];

    let i = 0, j = 0, k = l;

    while (i < a1 && j < a2) {
        if (L[i] <= R[j]) {
            a[k++] = L[i++];
        } else {
            a[k++] = R[j++];
        }
    }

    while (i < a1) a[k++] = L[i++];
    while (j < a2) a[k++] = R[j++];
}

function mergeSort(a, l, r) {
    if (l < r) {
        let m = l + Math.floor((r - l) / 2);
        mergeSort(a, l, m);
        mergeSort(a, m + 1, r);
        merge(a, l, m, r);
    }
}

let a = [39, 28, 44, 11];
console.log("Antes de ordenar el arreglo:");
console.log(a.join(" "));
mergeSort(a, 0, a.length - 1);
console.log("Después de ordenar el arreglo:");
console.log(a.join(" "));
