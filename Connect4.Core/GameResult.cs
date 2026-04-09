namespace Connect4.Core
{
    public enum GameResult
    {
        None,           // Игра идёт
        InvalidMove,    // Колонка заполнена / неверный ввод
        RedWins,
        YellowWins,
        Draw,
        Timeout         // Время вышло у текущего игрока
    }
}
