# List-Removal-performance-benchmarks-.netCore
Some benchmarks around the list removal strategy in C# .netCore

## Benchmarks reuslts


### Small size
- Recreate correct list 0,0016527 ms

- Remove unordered 0,0026065 ms

- Remove At native 0,0021817 ms


### Small size +
- Recreate correct list 0,039476 ms

- Remove unordered 0,0282642 ms

- Remove At native 0,0488365 ms


### Medium size
- Recreate correct list 0,2047986 ms

- Remove unordered 0,2634362 ms

- Remove At native 2,5061822 ms


### Medium size+
- Recreate correct list 2,240202 ms

- Remove unordered 2,6004066 ms

- Remove At native 276,1453138 ms


### Large size
- Recreate correct list 21,2680889 ms

- Remove unordered 29,9195827 ms

- Remove At native TIMEOUT ???
