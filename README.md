# Dynamic-K-Partioning-Problem

A Simple example of how to solve the K Partioning Problem. This includes a method to determin the cost of a group, that 
goes like this:

![Error Loading Image](https://9am7hg.db.files.1drv.com/y4mYmz0eNw95zOblFLAfC0-8dHjZn0AQVR1gcUEQDCFSyvVR8HpxX3PcFzrQj_wlME8RTL0D3AVW3crGjirrq98u-8bs0TNHk2JqAtvQX8XUruaCPQdqxsD62gs9mJbjSIMgiMhyOsbKcWhyJHXXL0a8h4S-U1U76hNKxaoAt_45tJDcutclg8WUxLUNrTmrkMqTcG9l4jjWM_w1Knmmo29Mw?width=710&height=272&cropmode=none)

Where the program can display the best group, as well as the cost of the group:

![Error Loading Image](https://amodpg.db.files.1drv.com/y4mr8MAfHKZPlx4KNWMApy1ex8jWsJ-jlL77u6EEwx2tH4hEC3hZY9csh3UHyk0vnWsbY4zte07iJLRNDwTfNplrjJKWI1CRoRitORVUdJCnr3L7L1rXT5bt4XF-WDjPI-Swgg1JZLwEUvDXKVH9inqglm8d93Iv6Uvono_NUFbgMJn6kQxic7M0EcZg0ko8vM0UOxQk5las8b-cB7SO5VD7Q?width=1162&height=387&cropmode=none)

Where the program rounds up, so that it only returns intergers as a cost.

## How it works

Run through all combinations, and comparing costs in a dynamic manner. That means that an example input of [4, 5, 7, 11, 21] in k = 3, we have the possible sets of:

1 : [ 4, ] [ 5, ] [ 7, 11, 21, ]  
2 : [ 4, ] [ 5, 7, ] [ 11, 21, ]  
3 : [ 4, ] [ 5, 7, 11, ] [ 21, ]  
4 : [ 4, 5, ] [ 7, ] [ 11, 21, ]  
5 : [ 4, 5, ] [ 7, 11, ] [ 21, ]  
6 : [ 4, 5, 7, ] [ 11, ] [ 21, ]  

Since the total cost is cost(G1) + cost(G2) + cost(G3), we can see an obvious optimization possibility. Consider the groups:

4 : [ 4, 5, ] [ 7, ] [ 11, 21, ]  
5 : [ 4, 5, ] [ 7, 11, ] [ 21, ]  

The first group [ 4, 5 ], if it is purely recursive, would be calculated twice, but since there are no change to the first group, then we would only need to calculate it once.

If we only calculate the groups once, then the total calculations would be:

1 : [ 4, ] [ 5, ] [ 7, 11, 21, ] = [ 4, = 0 ] [ 5, = 0 ] [ 7, 11, 21, = 104 ] <- first group, nothing is precalculated  
2 : [ 4, ] [ 5, 7, ] [ 11, 21, ] = [ 0, ] [ 5, 7, = 2 ] [ 11, 21, = 50 ] <- here we have the first group that have already been calculated [ 4 ], wich gives the cost of 0  
3 : [ 4, ] [ 5, 7, 11, ] [ 21, ] = [ 0, ] [ 5, 7, 11, = 18.66 ] [ 21, = 0 ]  
4 : [ 4, 5, ] [ 7, ] [ 11, 21, ] = [ 4, 5, = 0.5 ] [ 7, = 0 ] [ 50 ] <- here we have two groups thta have been precalculated, the first and last group  
5 : [ 4, 5, ] [ 7, 11, ] [ 21, ] = [ 0.5 ] [ 7, 11, = 8 ] [ 21, = 0 ]  
6 : [ 4, 5, 7, ] [ 11, ] [ 21, ] = [ 4, 5, 7, = 4.66 ] [ 0 ] [ 0 ]   

Where we can see the total costs of each group:

1 : [ 0 ] [ 0 ] [ 104 ]    = 104  
2 : [ 0 ] [ 2 ] [ 50 ]     = 52  
3 : [ 0 ] [ 18.66 ] [ 0 ]  = 18  
4 : [ 0.5 ] [ 0 ] [ 50 ]   = 50  
5 : [ 0.5 ] [ 8] [ 0 ]     = 8  
6 : [ 4.66 ] [ 0 ] [0 ]    = 4  

Where the last group is the best group.
With the dynamic caching, there is a total of 12 calculations being done, as opposed to without caching, that would have used 18 calculations.
Thats a big improvement, and the improvement gets even greater the larger the input group.

All the group options if found [this way.](https://github.com/kris701/Stirling-Numbers-of-2nd-Kind-Visualizer)
