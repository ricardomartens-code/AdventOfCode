import re

def solve_part1():
    total_sum = 0
    regex = r"W*(mul)\W*\((\d+)\,(\d+)\)"
    with open('input.txt', 'r') as file:
        for line in file:
            match = re.findall(regex, line)
            if match != None:
                for i in range(len(match)):
                    total_sum += int(match[i][1]) * int(match[i][2])
        print("Day 3 part 1 answer = ", total_sum)
        
def solve_part2():
    regex = re.compile(r"W*(mul)\W*\((\d+)\,(\d+)\)|do\(\)|don't\(\)")
    enabled = True
    total_sum = 0
    with open('input.txt', 'r') as file:
        for line in file:
            for match in regex.finditer(line):
                match_text = match.group(0)
                enabled = True if "do()" in match_text else False if "don't()" in match_text else enabled
                total_sum += int(re.findall(r"\d+", match_text)[0]) * int(re.findall(r"\d+", match_text)[1]) if enabled and "mul" in match_text else 0
    print("Day 3 part 2 answer = ", total_sum)

solve_part1()
solve_part2()