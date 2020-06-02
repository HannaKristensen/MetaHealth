var NUMBER_ROWS = 12;
var NUMBER_COLS = 20;

var types1 = new Array("blue", "green", "red", "yellow");
var types2 = new Array("darkgreen", "blue", "yellow", "orange");
var types3 = new Array("maroon", "orange", "pink", "purple");

var currentBoard = "boardDetails12x20";

var grid;

function restartGame() {
    document.getElementById("finished").style.display = 'none';
    var a = document.getElementById("boardSize");
    var option = a.options[a.selectedIndex].value;
    if (option == "one") {
        NUMBER_COLS = 20;
        NUMBER_ROWS = 12;
        document.getElementById("boardDetails12x20").style.display = 'block';
        document.getElementById("boardDetails6x10").style.display = 'none';
        document.getElementById("boardDetails3x5").style.display = 'none';
        currentBoard = "boardDetails12x20";
    }
    else if (option == "two") {
        NUMBER_COLS = 10;
        NUMBER_ROWS = 6;
        document.getElementById("boardDetails6x10").style.display = 'block';
        document.getElementById("boardDetails12x20").style.display = 'none';
        document.getElementById("boardDetails3x5").style.display = 'none';
        currentBoard = "boardDetails6x10";
    }
    else if (option == "three") {
        NUMBER_COLS = 5;
        NUMBER_ROWS = 3;
        document.getElementById("boardDetails3x5").style.display = 'block';
        document.getElementById("boardDetails12x20").style.display = 'none';
        document.getElementById("boardDetails6x10").style.display = 'none';
        currentBoard = "boardDetails3x5";
    }

    var a = document.getElementById("color");
    var option = a.options[a.selectedIndex].value;
    var types;

    if (option == "one") {
        types = types1;
    }
    else if (option == "two") {
        types = types2;
    }
    else if (option == "three") {
        types = types3;
    }

    grid = new Array(NUMBER_COLS);
    for (var currentCol = 0; currentCol < NUMBER_COLS; currentCol++) {
        grid[currentCol] = new Array(NUMBER_ROWS);
    }
    populateGrid(grid, types);
}

$(document).ready(function () {
    $("img").click(function (event) {
        var bool = imageClicked(event, grid);
        if (bool == null) {
            sleep(1000);
        }
        var finished = gameFinished(bool);
        if (finished == false) {
            confetti();
            document.getElementById("finished").style.display = 'block';
        }
        return true;
    });

    grid = new Array(NUMBER_COLS);
    for (var currentCol = 0; currentCol < NUMBER_COLS; currentCol++) {
        grid[currentCol] = new Array(NUMBER_ROWS);
    }

    populateGrid(grid, types);

    var a = document.getElementById("color");
    var option = a.options[a.selectedIndex].value;
    var types;

    if (option == "one") {
        types = types1;
    }
    else if (option == "two") {
        types = types2;
    }
    else if (option == "three") {
        types = types3;
    }
    populateGrid(grid, types);
});

function gameFinished(variable) {
    for (var y = 0; y < NUMBER_ROWS; y++) {
        for (var x = 0; x < NUMBER_COLS; x++) {
            var topX = x;
            var topY = y + 1;
            var leftX = x - 1;
            var leftY = y;
            var rightX = x + 1;
            var rightY = y;
            var bottomX = x;
            var bottomY = y - 1;
            var colorCurrent = grid[y][x];

            if (colorCurrent != "blank") {
                if (topX > -1 && topX < NUMBER_ROWS) {
                    if (topY > - 1 && topY < NUMBER_COLS) {
                        var colorCheck = grid[topY][topX];
                        if (colorCheck == colorCurrent) {
                            return true;
                        }
                    }
                }

                if (leftX > -1 && leftX < NUMBER_ROWS) {
                    if (leftY > - 1 && leftY < NUMBER_COLS) {
                        var colorCheck = grid[leftY][leftX];
                        if (colorCheck == colorCurrent) {
                            return true;
                        }
                    }
                }

                if (rightX > -1 && rightX < NUMBER_ROWS) {
                    if (rightY > - 1 && rightY < NUMBER_COLS) {
                        var colorCheck = grid[rightY][rightX];
                        if (colorCheck == colorCurrent) {
                            return true;
                        }
                    }
                }

                if (bottomX > -1 && bottomX < NUMBER_ROWS) {
                    if (bottomY > - 1 && bottomY < NUMBER_COLS) {
                        var colorCheck = grid[bottomY][bottomX];
                        if (colorCheck == colorCurrent) {
                            return true;
                        }
                    }
                }
            }
        }
    }

    return false;
}

/**
 * Function is called when image is clicked
 * @param eventObject(object) Object contain details about image clicked
 * @param grid(Multidimensional array of strings) board state
 * @pre assumes that image has id in format slot[0-19]x[0-11]
 */
function imageClicked(event, grid) {
    var id = event.target.id;
    var x = parseInt(id.match(/\d+/));
    var y = parseInt(id.match(/\d+$/));
    slotClicked(x, y, grid);
    return true;
}

/**
 * Function sets the contents of a slot
 * @param x(int) represents the xth horizontal slot
 * @param y(int) represents the yth vertical slot
 * @param type(string) type of item to fill the space with
 * @pre   x is between 0-19 inclusive and y is between 0-11 inclusive
 * @pre   type is either "blank", "blue", "green", "red", "yellow"
 */
function setSlotType(x, y, type, board) {
    var elementId = board + "slot" + x + "x" + y;
    var img = document.getElementById(elementId);
    if (type == "blank") {
        img.src = 'https://i.imgur.com/UMIZ0d0.png';
    } else if (type == "red") {
        img.src = 'https://i.imgur.com/7WeXiuv.png';
    } else if (type == "yellow") {
        img.src = 'https://i.imgur.com/EmjfFnJ.png';
    } else if (type == "blue") {
        img.src = 'https://i.imgur.com/hVkRu5R.png';
    } else if (type == "green") {
        img.src = 'https://i.imgur.com/P9F1H3S.png';
    } else if (type == "orange") {
        img.src = 'https://i.imgur.com/udrVRla.png';
    } else if (type == "purple") {
        img.src = 'https://i.imgur.com/JiHUV6U.png';
    } else if (type == "pink") {
        img.src = 'https://i.imgur.com/WyrqQit.png';
    } else if (type == "darkgreen") {
        img.src = 'https://i.imgur.com/KzshRz8.png';
    } else if (type == "maroon") {
        img.src = 'https://i.imgur.com/tcuoLlW.png';
    }
}

function boardSize() {
    var board;
    if (currentBoard == "boardDetails12x20") {
        board = "L";
    }
    else if (currentBoard == "boardDetails6x10") {
        board = "M";
    }
    else if (currentBoard == "boardDetails3x5") {
        board = "S";
    }

    return board;
}

function populateGrid(grid, types) {
    var a = document.getElementById("color");
    var option = a.options[a.selectedIndex].value;

    if (option == "one") {
        types = types1;
    }
    else if (option == "two") {
        types = types2;
    }
    else if (option == "three") {
        types = types3;
    }

    var board = boardSize();

    for (var currentCol = 0; currentCol < NUMBER_COLS; currentCol++) {
        for (var currentRow = 0; currentRow < NUMBER_ROWS; currentRow++) {
            var type = types[Math.floor(Math.random() * types.length)];
            grid[currentCol][currentRow] = type;
            setSlotType(currentCol, currentRow, type, board);
        }
    }
}

/**
 * Function is called when slot on play field is selected
 * @param x(int) represents the xth horizontal slot
 * @param y(int) represents the yth vertical slot
 * @param grid(Multidimensional array of strings) board state
 * @pre   x is between 0-19 inclusive and y is between 0-11 inclusive
 */
function slotClicked(x, y, grid) {
    var type = grid[x][y];
    var blocksCleared = checkNeighbor(x, y, type, grid);
    var board = boardSize();
    if (blocksCleared == 1) {
        setSlotType(x, y, type, board);
        grid[x][y] = type;
        return;
    }

    collapseDown(grid);
    collapseAcross(grid);
}

function checkNeighbor(x, y, type, grid) {
    var numberMatches = 1;
    var board = boardSize();
    setSlotType(x, y, "blank", board);
    grid[x][y] = "blank";

    if (isOfType(x - 1, y, type, grid) == true) {
        numberMatches += checkNeighbor(x - 1, y, type, grid);
    }

    if (isOfType(x + 1, y, type, grid) == true) {
        numberMatches += checkNeighbor(x + 1, y, type, grid);
    }

    if (isOfType(x, y - 1, type, grid) == true) {
        numberMatches += checkNeighbor(x, y - 1, type, grid);
    }

    if (isOfType(x, y + 1, type, grid) == true) {
        numberMatches += checkNeighbor(x, y + 1, type, grid);
    }
    return numberMatches;
}

function isOfType(x, y, type, grid) {
    if (x < 0 || x >= NUMBER_COLS ||
        y < 0 || y >= NUMBER_ROWS) {
        return false;
    }
    return grid[x][y] == type;
}

function collapseDown(grid) {
    var board = boardSize();
    for (var x = 0; x < NUMBER_COLS; x++) {
        var numberEmptySpaces = 0;
        for (var y = 0; y < NUMBER_ROWS; y++) {
            if (grid[x][y] == "blank") {
                numberEmptySpaces = numberEmptySpaces + 1;
            } else if (numberEmptySpaces > 0) {
                grid[x][y - numberEmptySpaces] = grid[x][y];
                setSlotType(x, y - numberEmptySpaces, grid[x][y], board);
                grid[x][y] = "blank";
                setSlotType(x, y, "blank", board);
            }
        }
    }
}
function collapseAcross(grid) {
    var blankColumns = 0;
    var board = boardSize();
    for (var x = 0; x < NUMBER_COLS; x++) {
        var isBlank = true;
        for (var y = 0; y < NUMBER_ROWS; y++) {
            if (grid[x][y] != "blank") {
                isBlank = false;
            }
            var gridStatus = grid[x][y];
            grid[x][y] = "blank";
            setSlotType(x, y, "blank", board);
            grid[x - blankColumns][y] = gridStatus;
            setSlotType(x - blankColumns, y, gridStatus, board);
        }
        if (isBlank == true) {
            blankColumns = blankColumns + 1;
        }
    }
}