# Author - Ricardo Martens
# Day 2 - Advent of Code 2024

# Safe
# The levels are either all increasing or all decreasing.
# Any two adjacent levels differ by at least one and at most three.
# Single bad levels removed which fixes it to safe is also good.

def is_safe(levels):
    # Check if the levels are either all increasing or all decreasing
    is_increasing = True
    is_decreasing = True
    for i in range(len(levels) - 1):
        diff = abs(int(levels[i]) - int(levels[i + 1]))
        if diff < 1 or diff > 3:
            return False
        if int(levels[i]) < int(levels[i + 1]):
            is_decreasing = False
        if int(levels[i]) > int(levels[i + 1]):
            is_increasing = False
    
    return is_increasing or is_decreasing

def solve_part1(file):
    total_lines = 0
    unsafe_sum = 0
    for line in file:
        total_lines += 1
        levels = line.split()
        
        if is_safe(levels):
            print("Report is safe for levels: ", levels)
            continue
        
        print("The levels has a problem and is false in ", levels)
        unsafe_sum += 1

    safe_reports = total_lines - unsafe_sum
    print("Day 2 part 1 answer = ", safe_reports)

def solve_part2(file):
    total_lines = 0
    unsafe_sum = 0
    for line in file:
        total_lines += 1
        levels = line.split()

        if is_safe(levels):
            print("Report is safe for levels: ", levels)
            continue  
        
        print("Checking if we can make it safe with 1 removal on level ", levels)
        safe_with_one_removal = False
        for i in range(len(levels)):
            new_levels = levels[:i] + levels[i + 1:]  
            if is_safe(new_levels):
                safe_with_one_removal = True
                print("Report is now safe with removing 1 bad node from level", levels)
                break 

        if safe_with_one_removal:
            continue  
        
        print("Report can not be made safe, so it's an unsolvable report with levels ", levels)
        unsafe_sum += 1

    safe_reports = total_lines - unsafe_sum
    print("Day 2 part 2 answer =", safe_reports)

with open('input.txt', 'r') as file:
    solve_part1(file)

with open('input.txt', 'r') as file:
    solve_part2(file)
    
                