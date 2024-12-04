# Author - Ricardo Martens
# Day 4 - Advent of Code 2024

def is_valid_direction(x, y, len_x, len_y):
    return 0 <= x < len_x and 0 <= y < len_y

def found_word(grid, amount_rows, amount_columns, word, index,
               x, y, x_direction, y_direction):
    if index == len(word): 
        return True
    
    if is_valid_direction(x, y, amount_rows, amount_columns) and word[index] == grid[x][y]:
        return found_word(grid, amount_rows, amount_columns, word, index + 1,
                          x + x_direction, y + y_direction, x_direction, y_direction)
    
    return False

def search_word(grid, word):
    amount_rows = len(grid)
    amount_columns = len(grid[0])
    amount_words_found = 0  
    directions = [
        (-1, 0), # up
        (0, 1),  # right
        (1, 0),  # down
        (0, -1), # left
        (-1, 1), # up-right
        (1, 1),  # down-right
        (1, -1), # down-left
        (-1, -1) # up-left
    ]

    for i in range(amount_rows):
        for j in range(amount_columns):
            if grid[i][j] == word[0]: 
                for x_direction, y_direction in directions:
                    if found_word(grid, amount_rows, amount_columns, word, 0, 
                                  i, j, x_direction, y_direction):
                        amount_words_found += 1

    return amount_words_found

def search_x_mas(grid, word):
    amount_rows = len(grid)
    amount_columns = len(grid[0])
    amount_words_found = 0  
    directions = [
        (-1, 1), # up-right
        (1, 1),  # down-right
        (1, -1), # down-left
        (-1, -1) # up-left
    ]
    x_mas_possible = False
    last_letter = ""

    for i in range(amount_rows):
        for j in range(amount_columns):
            if grid[i][j] == word[1]: # = A
                if (
                    is_valid_direction(i + 1, j + 1, amount_rows, amount_columns) and  # down-right
                    is_valid_direction(i - 1, j + 1, amount_rows, amount_columns) and  # up-right
                    is_valid_direction(i + 1, j - 1, amount_rows, amount_columns) and  # down-left
                    is_valid_direction(i - 1, j - 1, amount_rows, amount_columns)  # up-left
                ):                
                    m_count = 0
                    s_count = 0
                    for x_dir, y_dir in directions:
                        if grid[i + x_dir][j + y_dir] == "M":
                            m_count += 1
                        elif grid[i + x_dir][j + y_dir] == "S":
                            s_count += 1

                        if s_count == 2 and m_count == 1 and last_letter == "M":
                            x_mas_possible = False
                            break
                        elif m_count == 2 and s_count == 1 and last_letter == "S":
                            x_mas_possible = False
                            break
                        else:
                            x_mas_possible = True
                        
                        last_letter = grid[i + x_dir][j + y_dir]

                    if s_count == 2 and m_count == 2 and x_mas_possible:
                        amount_words_found += 1 
    return amount_words_found

def define_grid(filename):
    grid = []
    with open(filename, 'r') as file:
        for line in file:
            row = list(line.strip())
            grid.append(row)
    return grid

def solve_part1():
    filename = 'input.txt'
    grid = define_grid(filename)
    word = "XMAS"

    amount_words_found = search_word(grid, word)

    print("Day 4 part 1 answer = ", amount_words_found)

def solve_part2():
    filename = 'input.txt'
    grid = define_grid(filename)
    word = "MAS"

    amount_words_found = search_x_mas(grid, word)

    print("Day 4 part 2 answer = ", amount_words_found)
    
solve_part1()
solve_part2()
