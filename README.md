# List-Removal-performance-benchmarks-.netCore

Some benchmarks around the list removal strategy in C# .netCore

## What do we bench and how ?

Here is a bench of 3 different ways of removing elements in a list in C# .NetCore, every bench is an average run time over 1000 run on the same list for everyone for every run

- RemoveAt : Native List.RemoveAt

- LinQ.Where : Recreate the cleaned list

- RemoveAtUnordered : Custom method if list doesn't need the order => swap the last element the the index we want to remove and shrink the list by removing the last element


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


## Conclusions

Native RemoveAt is an O(n) operation and with large list and garbage collector it struggles a lot to remove elements, even on small lists it is not performant as it will be faster to create a new array from scratch than remove the element then shrink the array by creating a new one with the new capacity : Should never be used on List

LinQ Where has the advantage of producing imutable code and it performs really well

Custom RemoveAt with the swap technique (move the last element to the index you want to remove then shrink the list by 1 removing the last element) : on small lists (< 1000 more or less) it performs better than recreating a new array with a significant gain, it becomes useless on large lists /!\ it does not garantee the order only usable if you don't care about the order
