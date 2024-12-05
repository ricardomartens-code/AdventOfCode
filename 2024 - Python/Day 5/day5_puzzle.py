# Author - Ricardo Martens
# Day 5 - Advent of Code 2024

def recursive_search(sequence, rules):
    modified = False
    for i in range(len(sequence)):
        for j in range(len(rules)):
            if rules[j][0] in sequence[i] and rules[j][1] in sequence[i]:
                first_index = sequence[i].index(rules[j][0])
                second_index = sequence[i].index(rules[j][1])
                if first_index > second_index:
                    sequence[i][first_index], sequence[i][second_index] = sequence[i][second_index], sequence[i][first_index]
                    modified = True 

    if not modified:
        return sequence
    
    return recursive_search(sequence, rules)

def solve_part1():
    new_section = False
    with open('input.txt', 'r') as file:
        page_ordering_rules = []
        page_number_update = []
        correct_sequences = []
        sum = 0
        for line in file:

            if len(line.strip()) == 0:
                new_section = True
            
            line = line.strip("\n")
            if line != "":
                if new_section:
                    page_number_update.append(line.split(",")) 
                else:
                    page_ordering_rules.append(line.split("|"))

        for i in range(len(page_number_update)):
            incorrect_found = False
            for j in range(len(page_ordering_rules)):
                if page_ordering_rules[j][0] in page_number_update[i] and page_ordering_rules[j][1] in page_number_update[i]:
                    if page_number_update[i].index(page_ordering_rules[j][0]) < page_number_update[i].index(page_ordering_rules[j][1]):
                        incorrect_found = False
                    else:
                        incorrect_found = True
                        break

            if not incorrect_found:
                correct_sequences.append(page_number_update[i])

        for sequence in correct_sequences:
            middle_index = int((len(sequence) - 1)/2)
            sum += int(sequence[middle_index])
        print(f"Day 5 part 1 answer = {sum}")

def solve_part2():
    new_section = False
    with open('input.txt', 'r') as file:
        page_ordering_rules = []
        page_number_update = []
        incorrect_sequences = []
        sum = 0
        for line in file:

            if len(line.strip()) == 0:
                new_section = True
            
            line = line.strip("\n")
            if line != "":
                if new_section:
                    page_number_update.append(line.split(",")) 
                else:
                    page_ordering_rules.append(line.split("|"))

        for i in range(len(page_number_update)):
            incorrect_found = False
            for j in range(len(page_ordering_rules)):
                if page_ordering_rules[j][0] in page_number_update[i] and page_ordering_rules[j][1] in page_number_update[i]:
                    if page_number_update[i].index(page_ordering_rules[j][0]) < page_number_update[i].index(page_ordering_rules[j][1]):
                        incorrect_found = False
                    else:
                        incorrect_found = True
                        break

            if incorrect_found:
                incorrect_sequences.append(page_number_update[i])
        
        corrected_sequence = recursive_search(incorrect_sequences, page_ordering_rules)

        for sequence in corrected_sequence:
            middle_index = int((len(sequence) - 1)/2)
            sum += int(sequence[middle_index])
        print(f"Day 5 part 2 answer = {sum}")

solve_part1()
solve_part2()