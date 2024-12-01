# Author - Ricardo Martens
# Day 1 - Advent of Code 2024

compareA = []
compareB = []
sumPartOne = 0
sumPartTwo = 0
timesFound = 0

# Part 1
with open('input.txt', 'r') as file:
    for line in file:
        numbers = line.split()
        compareA.append(numbers[0])
        compareB.append(numbers[1])
    compareA = sorted(compareA)
    compareB = sorted(compareB)

    for i in range(len(compareA)):
        print("The distance of A = ", compareA[i], "The distance of B = ", compareB[i], "their difference is ", abs(int(compareA[i]) - int(compareB[i])))
        sumPartOne += abs(int(compareA[i]) - int(compareB[i]))
        # Part 2
        for j in range(len(compareB)):
            if (compareA[i] in compareB[j]):
                print("Found value in left that occurs in right namely", compareB[j])
                timesFound += 1
        print("amount of times was found on the right is ", timesFound)
        sumPartTwo += int(compareA[i]) * timesFound 
        timesFound = 0
            
    print("The solution for day 1 part 1 = ", sumPartOne)
    print("The solution for day 1 part 2 = ", sumPartTwo)

    
