function selection(a) {
    for (let i = 0; i < a.length; i++) {
        let small = i;
        for (let j = i + 1; j < a.length; j++) {
            if (a[small] > a[j]) {
                small = j;
            }
        }
        [a[i], a[small]] = [a[small], a[i]]; // swap
    }
}

function printArr(a) {
    console.log(a.join(" "));
}

let a = [65, 26, 13, 23, 12];

console.log("Arreglo antes de ser ordenado:");
printArr(a);

selection(a);

console.log("Arreglo despues de ser ordenado:");
printArr(a);
