document.addEventListener('DOMContentLoaded', () => {
    const canvas = document.getElementById('tetris');
    const context = canvas.getContext('2d');

    // Настройка масштаба отрисовываемых фигур
    context.scale(20, 20);

    // Уничтожаем полностью застроенные строки арены
    function arenaSweep() {
        let rowCount = 0;
        outer: for (let y = arena.length - 1; y > 0; --y) {
            for (let x = 0; x < arena[y].length; ++x) {
                if (arena[y][x] === 0) {
                    continue outer;
                }
            }

            const row = arena.splice(y, 1)[0].fill(0);
            arena.unshift(row);
            ++y; ++rowCount;
        }

        player.score += points[rowCount];
        player.count += rowCount;
        player.level = 1 + Math.floor(player.count / countNewLevel);
        dropInterval = Math.pow(0.75, player.level) * 1333;
    }

    // Проверка на то, можно ли сдвинуть фигуру 
    // (влево, вправо или вниз)
    function collide(arena, player) {
        const [m, o] = [player.matrix, player.pos];
        for (let y = 0; y < m.length; ++y) {
            for (let x = 0; x < m[y].length; ++x) {
                if (m[y][x] !== 0 &&
                    (arena[y + o.y] &&
                        arena[y + o.y][x + o.x]) !== 0) {
                    return true;
                }
            }
        }
        return false;
    }

    // Создание двумерного массива размерности h x w
    // h - число строк
    // w - число столбцов
    function createMatrix(w, h) {
        const matrix = [];
        while (h--) {
            matrix.push(new Array(w).fill(0));
        }
        return matrix;
    }

    // Создание арены для нашей игры 
    arena = createMatrix(12, 20);

    // Создание одной из семи возможных тетро фигурок
    function createPiece(type) {
        if (type === 'T') {
            return [
                [0, 0, 0],
                [1, 1, 1],
                [0, 1, 0]
            ];
        } else if (type === 'O') {
            return [
                [2, 2],
                [2, 2]
            ];
        } else if (type === 'L') {
            return [
                [0, 3, 0],
                [0, 3, 0],
                [0, 3, 3]
            ];
        } else if (type === 'J') {
            return [
                [0, 4, 0],
                [0, 4, 0],
                [4, 4, 0]
            ];
        } else if (type === 'I') {
            return [
                [0, 5, 0, 0],
                [0, 5, 0, 0],
                [0, 5, 0, 0],
                [0, 5, 0, 0]
            ];
        } else if (type === 'S') {
            return [
                [0, 6, 6],
                [6, 6, 0],
                [0, 0, 0]
            ];
        } else if (type === 'Z') {
            return [
                [7, 7, 0],
                [0, 7, 7],
                [0, 0, 0]
            ];
        }
    }

    // Отрисовка
    function draw() {
        context.fillStyle = '#000';
        context.fillRect(0, 0, canvas.width, canvas.height);

        drawMatrix(arena, { x: 0, y: 0 });
        drawMatrix(player.matrix, player.pos);
    }

    // Отрисовка матрицы - в зависимости от значений, каждая
    // ячейка закрашивается в определенный цвет
    function drawMatrix(matrix, offset) {
        matrix.forEach((row, y) => {
            row.forEach((value, x) => {
                if (value != 0) {
                    context.fillStyle = colors[value];
                    context.fillRect(x + offset.x, y + offset.y, 1, 1);
                }
            });
        });
    }

    // Наложение на арену (массив нулей) тетро-фигур, 
    // заполнив соответствующие клетки какими-то ненулевыми числами
    function merge(arena, player) {
        player.matrix.forEach((row, y) => {
            row.forEach((value, x) => {
                if (value !== 0) {
                    arena[y + player.pos.y][x + player.pos.x] = value
                }
            })
        })
    }

    // Тетро фигурка падает вниз, пока может, после чего происходит обновление очков
    function playerDrop() {
        player.pos.y++;
        if (collide(arena, player)) {
            player.pos.y--;
            merge(arena, player);
            playerReset();
            arenaSweep();
            updateScore();
        }
        dropCounter = 0;
    }

    // Сдвиг тетро фигуры на dir единиц вправо
    function playerMove(dir) {
        player.pos.x += dir;
        if (collide(arena, player)) {
            player.pos.x -= dir;
        }
    }

    // Генерация новой тетро фигурки 
    function playerReset() {
        const pieces = 'ILJOTSZ';
        player.matrix = createPiece(pieces[pieces.length * Math.random() | 0]);
        player.pos.y = 0;
        player.pos.x = (arena[0].length / 2 | 0) - (player.matrix[0].length / 2 | 0);

        // Если превысли верхнюю границу, заканчиваем игру, очищаем арену
        if (collide(arena, player)) {
            result();
            arena.forEach(row => row.fill(0));
            player.score = 0;
            player.count = 0;
            player.level = 1;
            dropInterval = 1000;
            updateScore();
        }
    }

    // Метод поворота, вызываемый пользователем с проверкой
    // возможности поворота при помощи функции collide
    function playerRotate(dir) {
        const pos = player.pos.x;
        let offset = 1;
        rotate(player.matrix, dir);
        while (collide(arena, player)) {
            player.pos.x += offset;
            offset = -(offset + (offset > 0 ? 1 : -1));
            if (offset > player.matrix[0].length) {
                rotate(player.matrix, -dir);
                player.pos.x = pos;
                return;
            }
        }
    }

    // Поворот матрицы: сначала транспонирование,
    // потом реверс каждой из строк матрицы 
    function rotate(matrix, dir) {
        for (let y = 0; y < matrix.length; ++y) {
            for (let x = 0; x < y; ++x) {
                [
                    matrix[x][y],
                    matrix[y][x]
                ] = [
                        matrix[y][x],
                        matrix[x][y]
                    ];
            }
        }

        if (dir > 0) {
            matrix.forEach(row => row.reverse());
        } else {
            matrix.reverse();
        }
    }

    let dropCounter = 0;
    let dropInterval = 1000;

    let countNewLevel = 10;

    let lastTime = 0

    function update(time = 0) {
        const deltaTime = time - lastTime;
        lastTime = time;
        dropCounter += deltaTime;
        if (dropCounter > dropInterval) {
            playerDrop()
        }

        draw();
        requestAnimationFrame(update);
    }

    // Обновление счета - обращение к html файлу 
    function updateScore() {
        document.getElementById('score').innerText = player.score;
        document.getElementById('count').innerText = player.count;
        document.getElementById('level').innerText = player.level;
    }

    // Настройка цветов тетро фигурок в зависимости от их индекса в функции createPeace
    const colors = [
        null,
        '#FF0D72',
        '#0DC2FF',
        '#0DFF72',
        '#F538FF',
        '#FF8E0D',
        '#FFE138',
        '#3877FF'
    ];

    // Очки увеличиваются в зависимости от количества уничтоженных строк одновременно
    const points = [
        0,
        10,
        30,
        60,
        100
    ]

    // Игрок 
    player = {
        pos: { x: 0, y: 0 }, // стартовая позиция фигур
        matrix: null, // следующая фигура
        score: 0, // счет
        count: 0, // кол-во уничтоженных строк
        level: 1 // уровень
    }

    // Обработка событий нажатия кнопок
    document.addEventListener('keydown', event => {
        if (event.code == 'ArrowLeft') {
            playerMove(-1); // <- сдвиг на единицу влево
        } else if (event.code == 'ArrowRight') {
            playerMove(1); // -> сдвиг на единицу вправо
        } else if (event.code == 'ArrowDown') {
            playerDrop(); // сдвиг на единицу вниз
        } else if (event.code == 'KeyQ') {
            playerRotate(-1); // поворот (против часовой стрелки)
        } else if (event.code == 'KeyW') {
            playerRotate(1);  // поворот (по часовой стрелке)
        }
    });

    playerReset();
    updateScore();
    update();

    function result() {
        $.post('Result', { score: player.score });
    }
})