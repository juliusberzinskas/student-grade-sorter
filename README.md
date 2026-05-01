# Student Grade Sorter

Two implementations of the same algorithm: reads a list of students with homework
grades and an exam score, calculates a weighted final result, and splits them into
passing and failing groups.

Built as a two-part systems programming course assignment — first in C++ (CLI),
then rewritten with a C# WinForms GUI.

## Part 1 — C++ (CLI)

Located in `part1-cpp/`

**Language:** C++  
**How it works:**
- Reads student data from a `.txt` file
- Calculates final result: `0.4 × homework average + 0.6 × exam score`
- Splits students into two output files: `Kietiakai.txt` (pass) and `Vargsiukai.txt` (fail)

**Run:**
```bash
g++ programa.cpp biblioteka.cpp -o sorter
./sorter