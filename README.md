# List-Removal-performance-benchmarks-.netCore

Some benchmarks around the list removal strategy in C# .netCore


## What do we bench and how ?

Here is a bench of 3 different ways of removing elements in a list in C# .NetCore, every bench is an average run time over 1000 run on the same list for everyone for every run

- RemoveAt : Native List.RemoveAt

- LinQ.Where : Recreate the cleaned list

- RemoveAtUnordered : Custom method if list doesn't need the order => swap the last element the the index we want to remove and shrink the list by removing the last element


## Benchmarks reuslts


### Small size
- Recreate correct list : `0,0018919 ms`

- Remove unordered : `0,0020857 ms`

- Remove At native : `0,0015852 ms`

- Remove All native : `0,0002715 ms`


### Small size +
- Recreate correct list : `0,0362409 ms`

- Remove unordered : `0,0218615 ms`

- Remove At native : `0,044515 ms`

- Remove All native : `0,0025464 ms`


### Medium size
- Recreate correct list : `0,1991805 ms`

- Remove unordered : `0,2179159 ms`

- Remove At native : `2,1869103 ms`

- Remove All native : `0,0264847 ms`

### Medium size+
- Recreate correct list : `1,9935933 ms`

- Remove unordered : `2,203854 ms`

- Remove At native : `276,1453138 ms`

- Remove All native : `0,2626365 ms`


### Large size
- Recreate correct list : `19,0728416 ms`

- Remove unordered : `24,9022341 ms`

- Remove At native : `TIMEOUT ???`

- Remove All native : `3,4090118 ms`


## Conclusions

Native RemoveAt is an O(n) operation and with large list and garbage collector it struggles a lot to remove elements, even on small lists it is not performant as it will be faster to create a new array from scratch than remove the element then shrink the array by creating a new one with the new capacity : Should never be used on List

LinQ Where has the advantage of producing immutable code and it performs really well

Custom RemoveAt with the swap technique (move the last element to the index you want to remove then shrink the list by 1 removing the last element) : on small lists (< 1000 more or less) it performs better than recreating a new array with a significant gain, it becomes useless on large lists /!\ it does not garantee the order only usable if you don't care about the order

RemoveAll is the way to go as it is up to 10 times faster than other methods, I tested it on different remove frequency (removing more or less objects) it seems to be always the best solution