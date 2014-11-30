/*
Assignment 5

 
*/



function getGrid() {
    displayLocalStore(); // assignment 5 Local Storage

    // game functions // 
    var _gridSize = setLevel();
    var _gameArray = gameController(_gridSize);
    var _ggrid = document.getElementById("gameGrid");
    _ggrid.innerHTML = createGrid(_gridSize);


     addOnclick(_gameArray);

    //var _movement = document.getElementById("animantion"); // assignment 4 animation
   // _movement.innerHTML = animation(); // assignment 4 animation

}

// game code

function createGrid(_gridSize) {

    _cellNumber = 1;


    var html = "<table><tbody id='tgrid'>";

    for (i = 0; i < _gridSize; i++) {
        html += "<tr>";
        for (j = 0; j < _gridSize; j++) {
            html += "<td id = '" + _cellNumber + "' class = 'something'  onclick = 'displaybox()' >" + ' ' + "</td>";
            _cellNumber++;
        }// end j loop

        html += "</tr>";

    } // end i loop

    html += "</tbody></table>";
    return html;

} // end function


function setLevel() {

    var buttons = document.getElementById("gameLevel");
    for (h = 0; h < buttons.length; h++) {
        if (buttons[h].checked) {
            _gridSize = buttons[h].value;
        }
    }

    return _gridSize;

}


function gameController(_gridSize) {

    _boxes = (_gridSize * _gridSize) + 1;

    var _temporary = new Array(_boxes);;
    var count = 1;

    // create a two dimentional array
    var _gameArray = new Array();

    for (i = 0; i < _gridSize; i++) {
        _gameArray[i] = new Array();
        for (j = 0; j < _gridSize; j++) {
            _gameArray[i][j] = 0;
        }
    }


    // randomize a single dimentional array with bombs
    for (i = 0; i < _gridSize; i++) {

        _bombId = Math.floor((Math.random() * _boxes) + 1);

        if (_temporary[_bombId] == 'bomb') {
            i--;
        } else {
            _temporary[_bombId] = 'bomb';
        }
    }



    // populate game array with bombs
    for (i = 0; i < _gridSize; i++) {

        for (j = 0; j < _gridSize; j++) {

            _gameArray[i][j] = _temporary[count];
            count++;

        }

    }

    // calculate adjacent cell bomb counts
    for (i = 0; i < _gridSize; i++) {
        for (j = 0; j < _gridSize; j++) {
            _adjacent = 0;


            if ((i - 1) >= 0) {
                if ((j - 1) >= 0) {
                    if (_gameArray[i - 1][j - 1] === 'bomb') {
                        _adjacent++;
                    }
                }
                if (_gameArray[i - 1][j] === 'bomb') {
                    _adjacent++;
                }

                if ((j + 1) < _gameArray[j].length) {
                    if (_gameArray[i - 1][j + 1] === 'bomb') {
                        _adjacent++;
                    }
                }
            }



            if ((j - 1) >= 0) {
                if ((_gameArray[i][j - 1]) === 'bomb') {
                    _adjacent++;
                }
            }

            if ((j + 1) < _gameArray[j].length) {
                if ((_gameArray[i][j + 1]) === 'bomb') {
                    _adjacent++;
                }
            }




            if ((i + 1) < _gameArray[i].length) {
                if ((j + 1) < _gameArray[j].length) {
                    if (_gameArray[i + 1][j + 1] === 'bomb') {
                        _adjacent++;
                    }
                }
                if (_gameArray[i + 1][j] === 'bomb') {
                    _adjacent++;
                }

                if ((j - 1) >= 0) {
                    if (_gameArray[i + 1][j - 1] === 'bomb') {
                        _adjacent++;
                    }
                }
            }


            if (_gameArray[i][j] !== 'bomb') {
                _gameArray[i][j] = _adjacent;
            }



        } // end j loop
    } // end i loop

    return _gameArray;
}


function addOnclick(_gameArray) {
    var location = document.getElementsByTagName("td");
    for (i = 0; i < location.length; i++) {
        location[i].onclick = function displayBox() {
            var col = this.cellIndex;
            var row = this.parentNode.rowIndex;
            if (_gameArray[col][row] === 'bomb') {
                this.innerHTML = "<img src='bomb.png'>"
            } else {
                this.innerHTML = _gameArray[col][row];
            }
        };

        // assigment 4  display onclick // 


        //function displayBox() {
        //var col = this.cellIndex;
        //var row = this.parentNode.rowIndex;
        //this.className = 'clicked'
        //this.innerHTML = '[' + col + "," + row + ']';


        //var _tgrid = document.getElementById("tgrid");
        //cell = _tgrid.rows[row].cells[col];
        //var _coordinates = document.getElementById("coordiantes");
        //_coordinates.innerHTML = displayCoordinates(cell);

    //};// end function

    }// end for loop

}
//********************************************* Assignment 5 ***************************************
function validate() {

    var _userName = document.getElementById('UserName');
    _userName = _userName.value;
    var _password = document.getElementById('Password');
    _password = _password.value;

    var data = "userName=" + _userName + "&password=" + _password;

    var ajax; 
        if (window.XMLHttpRequest) {
            ajax = new XMLHttpRequest();
        } else if (window.ActiveXObject) { // Older IE.
            ajax = new ActiveXObject('MSXML2.XMLHTTP.3.0');
        }

        ajax.open('POST', 'http://universe.tc.uvu.edu/cs2550/assignments/PasswordCheck/check.php?', false)
        ajax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        ajax.send(data)
        
        var _response = JSON.parse(ajax.responseText)

        var _results = _response['userName'] + " " + _response['timestamp'];
        
        if (_response['result'] === 'valid') {
            localStorage.setItem('cs2550timestamp', _results);
            window.open("ProgramGrid.html", "_self")
        } else {
           
            var _invalid = document.getElementById('validated');
            _invalid.innerHTML = "Invalid User Name or Password";
        }

    }

function displayLocalStore() {
        
        var _localStore = document.getElementById('localStore');

    if (localStorage.length) {
        var _localStoreHTML = localStorage['cs2550timestamp'];
        _localStore.innerHTML = _localStoreHTML + "<br> <input type = 'button' value = 'clear local Storage' onclick = 'clearLocal()' />";
    } else {
        _localStore.innerHTML = "The local storage is empty. <br> <input type = 'button' value = 'clear local Storage' onclick = 'clearLocal()' />"
    }


}
    
function clearLocal() {
    localStorage.clear();
    displayLocalStore();
}

//******************************************************************************************************************


//***************************************    Assignment 4  *********************************************************

//function displayCoordinates(cell)
//{

//    var _coordinates = document.getElementById("coordiantes");
//    var _coordianteHTML = "<textarea> The corrdinates of the cell previously cliked are: ";
//    if (cell == undefined) {
//        _coordianteHTML += "</textarea>";
//    } else {
//        _coordianteHTML += '[' + cell.parentNode.rowIndex + "," + cell.cellIndex + ']';
//        _coordianteHTML += "</textarea>";
//    }
//    return _coordianteHTML;
//}

//function animation() {
//    var _movement = document.getElementById("animantion");
//    var _animantionHTML = "<p id='flying'> Do you want to see the car race across the screen?</p> <p> If this was your dream car what would it be?  <select> <option value='ford'>Ford</option> <option value='buick'>Buick</option> <option value='chevy'>Chevy</option><option value='wagon'>Wagon</option></select> </br> </p></br><p> Milliseconds Per Frame:  <input id='milliseconds' type='text' size='5' value='10'> Distance Moved: <input id='distance' type='text' size='5' value='20'></p> </br> <button type='button' onclick = 'start()'> start </button></p>"

//    return _animantionHTML;
//}


//function start() {
//    _carimg = document.getElementById("carimg");
    
//    var _milliseconds = document.getElementById("milliseconds");
//    milliperframe = _milliseconds.value;

//    var _cardiv = document.getElementById("cardiv");
    
//    _carDivWidth = _cardiv.offsetWidth;
//    carLeft = 0;
//    setTimeout(moveCar, milliperframe);
//}

//function moveCar() {
//    var _distance = document.getElementById("distance");
//    distMoved = parseInt(_distance.value);
//    carLeft += distMoved;
//    _carimg.style.left = carLeft + "px";

//    if (carLeft < _carDivWidth - _carimg.width) {
//        setTimeout(moveCar, milliperframe);
//    }
//}

//******************************************************************************************************************



// I will need a function that places flags
function setFlags(_gameArray) {

}

//I will need a function that counts down the bombs based on flags placed
function bombCount(_flagCount) {

}

//I will need a function to count the time that the user is playing the game
function clockCount() {

}



