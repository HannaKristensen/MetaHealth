var NUMBER_ROWS;
var NUMBER_COLS;

var types1 = new Array("blue", "green", "red", "yellow");
var types2 = new Array("darkgreen", "blue", "yellow", "orange");
var types3 = new Array("maroon", "orange", "pink", "purple");

//console.log(NUMBER_ROWS + "," + NUMBER_COLS);
var grid;

function newBoard() {

    var b = document.getElementById("boardSize");
    var choice = b.options[b.selectedIndex].value;

    //console.log(choice);
    if (choice == "one") {
        NUMBER_ROWS = 12;
        NUMBER_COLS = 20;
    }
    else if (choice == "two") {
        NUMBER_ROWS = 6;
        NUMBER_COLS = 10;
    }
    else if (choice == "three") {
        NUMBER_ROWS = 3;
        NUMBER_COLS = 5;
    }


}

function restartGame() {

    newBoard();

   for (var y = NUMBER_ROWS - 1; y > -1; y--) {
        for (var x = 0; x < NUMBER_COLS; x++) {
            document.getElementById("board").innerHTML = ("<img id=\"slot" + x + "x" + y + "\" width=\"30px\" height=\"30px\"  style=\"backgroundColor: white\" />");
        }
        document.getElementById("board").innerHTML = ("<br />")
    }

    //document.getElementById("finished").style.display = 'none';
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
    newBoard();
    grid = new Array(NUMBER_COLS);
    for (var currentCol = 0; currentCol < NUMBER_COLS; currentCol++) {
        grid[currentCol] = new Array(NUMBER_ROWS);
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
    populateGrid(grid, types);
});

function gameFinished(variable) {

    newBoard();
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
function setSlotType(x, y, type) {
    var elementId = "slot" + x + "x" + y;
    var img = document.getElementById(elementId);
    if (type == "blank") {
        img.src = 'https://color-hex.org/colors/e9e6d4.png';
    } else if (type == "red") {
        img.src = 'https://i.imgur.com/HsghsKC.jpg';
    } else if (type == "yellow") {
        img.src = 'https://www.enasco.com/medias/9723819D-main-530Wx530H?context=bWFzdGVyfHJvb3R8NDU2OXxpbWFnZS9qcGVnfGgyZS9oYjMvODg0NDM5NDU2MTU2Ni5qcGd8NzM0YjgzZTk4MjYwMWU2MDNkZTc5NGQ1OTA5ZTM1MjAzNjNmNDZmNzk5MWU2NWZjY2NlYWNmZWMzOTI1NzdhNA';
    } else if (type == "blue") {
        img.src = 'https://www.nafc.uhi.ac.uk/t4-media/one-web/nafc/research/document/marine-spatial-planning/dark-blue-box.png';
    } else if (type == "green") {
        img.src = 'https://img.buzzfeed.com/buzzfeed-static/static/2020-02/26/18/enhanced/63fa524113ec/enhanced-1674-1582742675-2.png?downsize=600:*&output-format=auto&output-quality=auto';
    } else if (type == "orange") {
        img.src = 'http://m.sweetclipart.com/wp-content/uploads/Ffc000-Color-Square-Orange-Clip-Art.png';
    } else if (type == "purple") {
        img.src = 'https://www.americasfinestlabels.com/includes/work/image_cache/907b65fdc3cc8579cc07b49c65317dda.thumb.jpg';
    } else if (type == "pink") {
        img.src = 'https://matthewelonis.com/wp-content/uploads/2018/07/same-coloured-paper-l-light-pink-13.8-inch-34.5-cm-squared-10-sheets-c209o-9694-p.jpg';
    } else if (type == "darkgreen") {
        img.src = 'https://i0.wp.com/avenuesixty.com/wp-content/uploads/2012/12/color-green-square.jpg?fit=2000%2C2118&ssl=1';
    } else if (type == "maroon") {
        img.src = 'https://static.bhphoto.com/images/images500x500/Rosco_110084014812_26_26_Light_Red_Fluorescent_1252489597_107135.jpg';
    } 
}



function populateGrid(grid, types) {
    newBoard();

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
    newBoard();
    for (var currentCol = 0; currentCol < NUMBER_COLS; currentCol++) {
        for (var currentRow = 0; currentRow < NUMBER_ROWS; currentRow++) {
            var type = types[Math.floor(Math.random() * types.length)];
            grid[currentCol][currentRow] = type;
            setSlotType(currentCol, currentRow, type);
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
    if (blocksCleared == 1) {
        setSlotType(x, y, type);
        grid[x][y] = type;
        return;
    }

    collapseDown(grid);
    collapseAcross(grid);
}

function checkNeighbor(x, y, type, grid) {
    var numberMatches = 1;
    setSlotType(x, y, "blank");
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
    newBoard();

    if (x < 0 || x >= NUMBER_COLS ||
        y < 0 || y >= NUMBER_ROWS) {
        return false;
    }
    return grid[x][y] == type;
}

function collapseDown(grid) {
    newBoard();
    for (var x = 0; x < NUMBER_COLS; x++) {
        var numberEmptySpaces = 0;
        for (var y = 0; y < NUMBER_ROWS; y++) {
            if (grid[x][y] == "blank") {
                numberEmptySpaces = numberEmptySpaces + 1;
            } else if (numberEmptySpaces > 0) {
                grid[x][y - numberEmptySpaces] = grid[x][y];
                setSlotType(x, y - numberEmptySpaces, grid[x][y]);
                grid[x][y] = "blank";
                setSlotType(x, y, "blank");
            }
        }
    }
}

function collapseAcross(grid) {
    newBoard();
    var blankColumns = 0;
    for (var x = 0; x < NUMBER_COLS; x++) {
        var isBlank = true;
        for (var y = 0; y < NUMBER_ROWS; y++) {
            if (grid[x][y] != "blank") {
                isBlank = false;
            }
            var gridStatus = grid[x][y];
            grid[x][y] = "blank";
            setSlotType(x, y, "blank");
            grid[x - blankColumns][y] = gridStatus;
            setSlotType(x - blankColumns, y, gridStatus);
        }
        if (isBlank == true) {
            blankColumns = blankColumns + 1;
        }
    }
}