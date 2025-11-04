#include <iostream>
#include <vector>

using namespace std;

class ShellSort {
public:
    static void displayArr(const vector<int>& inputArr) {
        for (int k : inputArr) {
            cout << k << " ";
        }
        cout << endl;
    }

    void sort(vector<int>& inputArr) {
        int size = inputArr.size();
        
        for (int gapsize = size / 2; gapsize > 0; gapsize /= 2) {
            for (int j = gapsize; j < size; j++) {
                int val = inputArr[j];
                int k = j;
                while (k >= gapsize && inputArr[k - gapsize] > val) {
                    inputArr[k] = inputArr[k - gapsize];
                    k = k - gapsize;
                }
                inputArr[k] = val;
            }
        }
    }
};

int main() {
    vector<int> inputArr = { 36, 34, 43, 11, 15, 20, 28, 45 };
    cout << "Arreglo antes de ser ordenado: " << endl;
    ShellSort::displayArr(inputArr);

    ShellSort obj;
    obj.sort(inputArr);

    cout << "Arreglo después de ser ordenado: " << endl;
    ShellSort::displayArr(inputArr);

    return 0;
}