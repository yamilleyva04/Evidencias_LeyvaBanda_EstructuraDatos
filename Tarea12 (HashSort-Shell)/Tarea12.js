class ShellSort {
    static displayArr(arr) {
        console.log(arr.join(" "));
    }

    sort(arr) {
        let n = arr.length;
        for (let gap = Math.floor(n / 2); gap > 0; gap = Math.floor(gap / 2)) {
            for (let i = gap; i < n; i++) {
                let temp = arr[i];
                let j = i;
                while (j >= gap && arr[j - gap] > temp) {
                    arr[j] = arr[j - gap];
                    j -= gap;
                }
                arr[j] = temp;
            }
        }
    }
}

let arr = [36, 34, 43, 11, 15, 20, 28, 45];
console.log("Arreglo antes de ser ordenado:");
ShellSort.displayArr(arr);
let sorter = new ShellSort();
sorter.sort(arr);
console.log("Arreglo después de ser ordenado:");
ShellSort.displayArr(arr);
