using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Maze
{
    private int row_numbers;
    private int col_numbers;
    private short[,] grid;

    private short UNVISITED = -1;
    private short VISITED = 1;
    private short WALL = 0;
    private short NORTH = 0;
    private short EAST = 1;
    private short SOUTH = 2;
    private short WEST = 3;
    private short toggle = -1;

	public Maze(int row, int columns){
        row_numbers = row;
        col_numbers = columns;
        grid = new short[row_numbers, col_numbers];
        setGrid();
	}


    public void setHauntandKill() {
        int current_row;
        int current_col;
        do {
            current_row = Random.Range(1, row_numbers - 1);
            current_col = Random.Range(1, col_numbers - 1);
        } while (grid[current_row, current_col] != UNVISITED);
        

  //      grid[current_row, current_col] = VISITED;

        int[] aux;

        while (current_row != -1 && current_col != -1){
            walk(current_row, current_col);
            aux = hunt();
            current_row = aux[0];
            current_col = aux[1];
        }

    }
    //Build maze with all walls
    public void setGrid() {
        for(int i = 0; i < row_numbers; i++) {
            for(int j = 0; j < col_numbers; j++) {
                //build maze wall
                if(i == 0 || i == row_numbers-1 || j == 0 || j == col_numbers-1 || i%2 == 0) {
                    grid[i, j] = WALL;
                }
                else {
                //build free spots
                    grid[i, j] = toggle;
                    toggle = (toggle == -1) ? (short)0 : (short)-1;
                }
            }
            toggle = 1;
        }
    }

    public short[,] getGrid() {
        return grid;
    }
    
    public void walk(int row, int col) {
        int[,] unvisited_neighbors;
        unvisited_neighbors = find_neighbor(row, col, true);
        

        //Takes a random unvisited neighbor
        int[] neighbor = new int[2];
        while (!is_empty(unvisited_neighbors)) {
            neighbor = choice(unvisited_neighbors);
            grid[neighbor[0], neighbor[1]] = VISITED;
            grid[(neighbor[0] + row) / 2, (neighbor[1] + col) / 2] = VISITED;
            row = neighbor[0];
            col = neighbor[1];
            unvisited_neighbors = find_neighbor(row, col, true);
        }

    }

    private int[] hunt() {
        int row, col;
        row = 1;
        col = 1;
        int count = 0;
        int[] neighbor = new int[2];
        int[,] visited_neighbor = new int[4,2];
        bool found = false;
        while (true) {
            if (grid[row, col] == UNVISITED) {
                visited_neighbor = find_neighbor(row, col, false);
                if (!is_empty(visited_neighbor)) {
                    break;
                }
            }
            col += 1;
            if(col > col_numbers - 2) {
                col = 1;
                row += 1;
                if (row > row_numbers - 2) {
                    neighbor[0] = -1;
                    neighbor[1] = -1;

                    return neighbor;
                }
            }
            count++;
        }
        neighbor = choice(visited_neighbor);
        grid[row, col] = VISITED; //necessary?
        grid[(neighbor[0] + row) / 2, (neighbor[1] + col) / 2] = VISITED;
        return neighbor;
    }

    private int[] choice(int[,] unvisited_neighbors) {
        int aux;
        do {
            aux = Random.Range(0, 4);
        } while (unvisited_neighbors[aux, 0] == -1);
        int[] neighbor = new int[2];
        neighbor[0] = unvisited_neighbors[aux, 0];
        neighbor[1] = unvisited_neighbors[aux, 1];
        return neighbor;
    }

    private bool is_empty(int [,] unvisited_neighbors) {
        bool empty = true;
        for(int i = 0; i < 4; i++) {
            for(int j = 0; j < 2; j++) {
                if(unvisited_neighbors[i,j] != -1) {
                    empty = false;
                }
            }
        }
    return empty;
    }


    private int[,] find_neighbor(int row, int col, bool unvisited) {
        int[,] unvisited_neighbors = new int[4, 2];
        int rowN = row - 2;
        int colN = col;

        int rowE = row;
        int colE = col + 2;

        int rowS = row + 2;
        int colS = col;

        int rowW = row;
        int colW = col - 2;

        //Initialize all elements with -1
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 2; j++) {
                unvisited_neighbors[i, j] = -1;
            }
        }

        //Calculate northern neighbor, if there exists
        if (rowN > 0) {
            if (unvisited) {
                if (grid[rowN, colN] == UNVISITED) {
                    unvisited_neighbors[NORTH, 0] = rowN;
                    unvisited_neighbors[NORTH, 1] = colN;
                }
            }
            else {
                if (grid[rowN, colN] == VISITED) {
                    unvisited_neighbors[NORTH, 0] = rowN;
                    unvisited_neighbors[NORTH, 1] = colN;
                }
            }
        }
        //Calculate eastern neighbor, if there exists
        if (colE < col_numbers - 1) {
            if (unvisited) {
                if (grid[rowE, colE] == UNVISITED) {
                    unvisited_neighbors[EAST, 0] = rowE;
                    unvisited_neighbors[EAST, 1] = colE;
                }
            }
            else {
                if (grid[rowE, colE] == VISITED) {
                    unvisited_neighbors[EAST, 0] = rowE;
                    unvisited_neighbors[EAST, 1] = colE;
                }
            }

        }
        //Calculate southern neighbor, if there exists
        if (rowS < row_numbers - 1) {
            if (unvisited) {
                if (grid[rowS, colS] == UNVISITED) {
                    unvisited_neighbors[SOUTH, 0] = rowS;
                    unvisited_neighbors[SOUTH, 1] = colS;
                }
            }
            else {
                if (grid[rowS, colS] == VISITED) {
                    unvisited_neighbors[SOUTH, 0] = rowS;
                    unvisited_neighbors[SOUTH, 1] = colS;
                }
            }

        }
        //Calculate western neihbor, if there exists
        if (colW > 0) {
            if (unvisited) {
                if (grid[rowW, colW] == UNVISITED) {
                    unvisited_neighbors[WEST, 0] = rowW;
                    unvisited_neighbors[WEST, 1] = colW;
                }
            }
            else {
                if (grid[rowW, colW] == VISITED) {
                    unvisited_neighbors[WEST, 0] = rowW;
                    unvisited_neighbors[WEST, 1] = colW;
                }
            }

        }
        return unvisited_neighbors;
    }
    
    public void setBall_position(){
        int x, y;
        do {
            x = Random.Range(1, row_numbers - 1);
            y = Random.Range(1, col_numbers - 1);
        } while (grid[x, y] != 1);
        grid[x, y] = 2;
    }

    public void setPickUps(int num) {
        int x, y;
        for(int i = 0; i<num; i++) {
            do {
                x = Random.Range(1, row_numbers - 1);
                y = Random.Range(1, col_numbers - 1);
            } while (grid[x, y] != 1);
            grid[x, y] = 3;
        }
        
    }
}

