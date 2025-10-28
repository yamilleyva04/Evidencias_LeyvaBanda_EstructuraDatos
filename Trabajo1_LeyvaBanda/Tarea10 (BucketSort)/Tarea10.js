function insertionSort(bukt) {
  for (let j = 1; j < bukt.length; j++) {
    let val = bukt[j];
    let k = j - 1;
    while (k >= 0 && bukt[k] > val) {
      bukt[k + 1] = bukt[k];
      k--;
    }
    bukt[k + 1] = val;
  }
}

function bucketSort(arr) {
  let s = arr.length;
  let buckets = Array.from({ length: s }, () => []);

  for (let j of arr) {
    let bi = Math.floor(s * j);
    if (bi >= s) bi = s - 1;
    buckets[bi].push(j);
  }

  for (let bukt of buckets)
    insertionSort(bukt);

  let idx = 0;
  for (let bukt of buckets)
    for (let j of bukt)
      arr[idx++] = j;
}

let arr = [0.77, 0.16, 0.38, 0.25, 0.71, 0.93, 0.22, 0.11, 0.24, 0.67];
console.log("Arreglo antes de ordenar:");
console.log(arr.join(" "));

bucketSort(arr);

console.log("Arreglo después de ordenar:");
console.log(arr.join(" "));
