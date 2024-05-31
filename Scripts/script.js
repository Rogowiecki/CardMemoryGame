
var selectedvalue = 0;    // Используется для хранения параметра URL
var actual_index = 0;     // Количество карт в игре, используемое в качестве идентификатора для каждой карты
var row_selector = 0;     // Количество строк
var column_selector = 0;  // Количество столбцов
var count_twocards = 0;   // Количество перевернутых карт
var imgsrc_twocards = []; // Используется для хранения источников изображений двух перевернутых карточек
var id_twocards = [];     // Используется для хранения идентификаторов двух перевернутых карт
var interval = null;      // Идентификатор используемого таймера
var correct_flipcount = 0;// Количество правильно идентифицированных карт
var flipcount = 0;        // Общее количество сделанных переворотов
var timeleft;            // Количество секунд до конца
var initial_time;         // Общее время, отведенное на игру

/**
 * Функция, которая обрабатывает таймер
 */
function mytimer(){

    if(timeleft == 0){

        // Выполняется по истечении таймера
        document.getElementById("time-remain").innerHTML = timeleft;
        clearInterval(interval);
        var game_over = document.getElementById("gameover");
        game_over.classList.add("visible");
    }
    else{

        // Уменьшаем таймер каждую секунду
        timeleft = timeleft - 1;
        document.getElementById("time-remain").innerHTML = timeleft;
    }
}

/**
 * Функция отображения инструкций в начале игры
 */
function startinstruction(){

    // Делает инструкцию видимой
    var instruction = document.getElementById("instruction");
    instruction.classList.add("visible");
}

/**
 * Функция создания карточек для игры в зависимости от выбора пользователя
 */
function myFunction(){

    // Скрываем инструкцию
    var instruction = document.getElementById("instruction");
    instruction.classList.remove("visible");
    
    // Получить выбранный номер по URL
    const urlString = window.location.search;
    const urlParams = new URLSearchParams(urlString);
    const selector = urlParams.get("cards");
    selectedvalue = selector;
    console.log(selector);
    
    // Инициализируем таймер на основе выбранного значения
    switch(selector){
        case "3": timeleft = 30;break;
        case "4": timeleft = 45;break;
        case "5": timeleft = 60;break;
    }
    document.getElementById("time-remain").innerHTML = timeleft;
    initial_time = timeleft;
    interval = setInterval(mytimer,1000);
    
    var grid = document.createElement("div");
    grid.className="grid-container";

    // Инициализируем количество строк и столбцов
    if(selector==5){
        row_selector = 4;
        column_selector = selector;
    }   
    else{
        row_selector = selector;
        column_selector = 4; 
    }

    // Получаем случайный список изображений
    images_list = get_images(selector*2);
          
    for(var row=0;row<row_selector;row++){

        // Создать строку
        var row_div = document.createElement("div");
        row_div.className="grid-row";
        
        for(var column=0;column<column_selector;column++){

            // Создаём столбец и карточку
            var column_div = document.createElement("div");
            var card_front = document.createElement("div");
            var card_back = document.createElement("div");
            
            column_div.id = actual_index.toString() + "_main";

            // Привязываем изображение к карточке
            current_index = Math.floor(Math.random() * images_list.length);
            card_back.style.backgroundImage = 'url(../images/' + images_list[current_index] + ')';
            card_back.style.backgroundSize = "100% 100%";
            
            // удаляем назначенное изображение из списка
            images_list.splice(current_index,1);
            card_back.classList.add("card","card_back");
            card_back.id = actual_index.toString();
            actual_index += 1;
            // Назначаем фоновое изображение карте
            card_front.style.backgroundImage = "url('../images/card_back_side.jpg')";
            card_front.style.backgroundSize = "100% 100%";
            card_front.classList.add("card","card_front");
            
            // Добавляем прослушиватель событий на карту, чтобы перевернуть их
            column_div.addEventListener("click",flipcard);
            column_div.classList.add("grid-column");

            column_div.appendChild(card_front);
            column_div.appendChild(card_back);
            row_div.appendChild(column_div);
        }
        grid.appendChild(row_div);
    }
    document.getElementById("dummygrid").appendChild(grid);
}


function flipcard() {
    
    // Получаем идентификатор выбранной карты
    var child_id = this.childNodes; 

    // Проверяем, выбрана ли карта уже
    if(!id_twocards.includes(this.id)){

        // Переворачиваем карту и увеличиваем счетчик переворотов
        this.classList.add('is-flipped');
        document.getElementById("filps").innerHTML=++flipcount;
        id_twocards.push(this.id);
        imgsrc_twocards[count_twocards] = child_id[1].style.backgroundImage;
        count_twocards += 1;;

        // Когда перевернуты две карты
        if(count_twocards == 2){
            count_twocards = 0;

            // Когда две выбранные карты не равны
            if(imgsrc_twocards[0] != imgsrc_twocards[1]){
                document.getElementById("blocker").classList.add("dummygridblocker");

                // Устанавливаем интервал в 700 миллисекунд, прежде чем перевернуть карты обратно
                setTimeout(function(){    
                    
                    // Переворачиваем две карты назад и удаляем из списка
                    document.getElementById(id_twocards[0]).classList.remove('is-flipped');
                    document.getElementById(id_twocards[1]).classList.remove('is-flipped');
                    id_twocards.pop();
                    id_twocards.pop();
                    document.getElementById("blocker").classList.remove("dummygridblocker");
                },700);
            }

                // когда две карты равны
            else{

                // Удалить события указателя для карточек и удалить из списка
                document.getElementById(id_twocards[0]).style.pointerEvents = "none";
                document.getElementById(id_twocards[1]).style.pointerEvents = "none";
                id_twocards.pop();
                id_twocards.pop();

                // Увеличиваем количество правильных переворотов
                correct_flipcount += 1;

                // Проверяем, все ли карты найдены
                if(correct_flipcount == selectedvalue*2){
                    clearTimeout(interval);
                    var victory = document.getElementById("victory");
                    victory.classList.add("visible");
                    score_calculator();
                }
            }
        }
    }
}

/**
 * Эта функция возвращает список случайных пар изображений.
 * @param {int} selector Необходимое количество пар карт 
 */
function get_images(selector) {

    // Список доступных изображений
    var images = ['2_of_clubs.png','2_of_diamonds.png','2_of_hearts.png','2_of_spades.png',
                  '3_of_clubs.png','3_of_diamonds.png','3_of_hearts.png','3_of_spades.png',
                  '4_of_clubs.png','4_of_diamonds.png','4_of_hearts.png','4_of_spades.png',
                  '5_of_clubs.png','5_of_diamonds.png','5_of_hearts.png','5_of_spades.png',
                  '6_of_clubs.png','6_of_diamonds.png','6_of_hearts.png','6_of_spades.png',
                  '7_of_clubs.png','7_of_diamonds.png','7_of_hearts.png','7_of_spades.png',
                  '8_of_clubs.png','8_of_diamonds.png','8_of_hearts.png','8_of_spades.png',
                  '9_of_clubs.png','9_of_diamonds.png','9_of_hearts.png','9_of_spades.png',
                  '10_of_clubs.png','10_of_diamonds.png','10_of_hearts.png','10_of_spades.png',
                  'ace_of_clubs.png','ace_of_diamonds.png','ace_of_hearts.png','ace_of_spades2.png',
                  'jack_of_clubs2.png','jack_of_diamonds2.png','jack_of_hearts2.png',
                  'jack_of_spades2.png','king_of_clubs2.png','king_of_diamonds2.png',
                  'king_of_hearts2.png','king_of_spades2.png','queen_of_clubs2.png',
                  'queen_of_diamonds2.png','queen_of_hearts2.png','queen_of_spades2.png',
                  'red_joker.png'];
    var rand_image = [];
    written = 0;
    while(written < selector){
        var x = Math.floor(Math.random() * images.length);
        
        // Проверяем, есть ли изображение уже в списке
        if(!(rand_image.includes(images[x]))){

            // Add images in pair
            rand_image.push(images[x]);
            rand_image.push(images[x]);
            written ++;
        }
      }
      console.log(rand_image);

    return rand_image;
}

/**
 * Функция расчета очков
 * счет переворота = количество карт
 * -------------------
 * Количество сделанных флипов
 * 
 * счет времени = Оставшееся время
 * ------------
 *               Общее время
 * 
* окончательный результат = 70 % от результата подбрасывания + 30 % от результата за время.
 */
function score_calculator() {

    // Подсчет очков
    var flip_score = (1 / flipcount) * (selectedvalue * 4) * 100;
    var time_score = timeleft / initial_time * 100;
    var final_score = parseInt((0.7 * flip_score) + (0.3 * time_score));

    // Отображение счета
    document.getElementById("UserScore").innerHTML = "Score: " + final_score;
    document.getElementById("UserScoreHidden").value = final_score;
    
}

function display_message(msg) {

    console.log("function");
    alert("This feature is disabled\n" + String(msg));
}
