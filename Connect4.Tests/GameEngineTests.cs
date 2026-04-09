using Connect4.Core;
using Xunit;

namespace Connect4.Tests
{
    public class GameEngineTests
    {
        private readonly GameEngine _engine;

        public GameEngineTests()
        {
            _engine = new GameEngine();
            _engine.StartNewGame();
        }

        [Fact]
        public void TryDrop_InvalidColumn_ReturnsInvalidMove()
        {
            // Проверка выхода за границы поля
            Assert.Equal(GameResult.InvalidMove, _engine.TryDrop(-1));
            Assert.Equal(GameResult.InvalidMove, _engine.TryDrop(7));
        }

        [Fact]
        public void TryDrop_FullColumn_ReturnsInvalidMove()
        {
            // Заполняем колонку и пытаемся добавить еще
            for (int i = 0; i < 6; i++) _engine.TryDrop(0);
            Assert.Equal(GameResult.InvalidMove, _engine.TryDrop(0));
        }

        [Fact]
        public void CheckWin_HorizontalWin_Detected()
        {
            // P1 выстраивает 4 в ряд по горизонтали внизу
            _engine.TryDrop(0); _engine.TryDrop(6); // P1, P2
            _engine.TryDrop(1); _engine.TryDrop(6); // P1, P2
            _engine.TryDrop(2); _engine.TryDrop(6); // P1, P2
            var res = _engine.TryDrop(3); // P1 -> Победа

            Assert.Equal(GameResult.RedWins, res);
            Assert.True(_engine.IsGameOver);
        }

        [Fact]
        public void CheckWin_VerticalWin_Detected()
        {
            // P1 выстраивает 4 в ряд по вертикали в колонке 0
            _engine.TryDrop(0); _engine.TryDrop(1); // P1, P2
            _engine.TryDrop(0); _engine.TryDrop(1); // P1, P2
            _engine.TryDrop(0); _engine.TryDrop(1); // P1, P2
            var res = _engine.TryDrop(0); // P1 -> Победа

            Assert.Equal(GameResult.RedWins, res);
        }

        [Fact]
        public void CheckWin_DiagonalWin_Detected()
        {
            // P1 выстраивает диагональ: [5,0], [4,1], [3,2], [2,3]
            var eng = new GameEngine();
            eng.StartNewGame();

            eng.TryDrop(0); // P1 [5,0]
            eng.TryDrop(1); // P2 [5,1] (фундамент)
            eng.TryDrop(1); // P1 [4,1]
            eng.TryDrop(2); // P2 [5,2] (фундамент)
            eng.TryDrop(6); // P1 (мусорный ход)
            eng.TryDrop(2); // P2 [4,2] (фундамент)
            eng.TryDrop(2); // P1 [3,2]
            eng.TryDrop(3); // P2 [5,3] (фундамент)
            eng.TryDrop(6); // P1 (мусор)
            eng.TryDrop(3); // P2 [4,3] (фундамент)
            eng.TryDrop(6); // P1 (мусор)
            eng.TryDrop(3); // P2 [3,3] (фундамент)

            var res = eng.TryDrop(3); // P1 [2,3] -> Победа

            Assert.Equal(GameResult.RedWins, res);
            Assert.True(eng.IsGameOver);
        }

        [Fact]
        public void GameOver_NoMovesAfterWin()
        {
            // После победы любые ходы должны игнорироваться
            _engine.TryDrop(0); _engine.TryDrop(6);
            _engine.TryDrop(1); _engine.TryDrop(6);
            _engine.TryDrop(2); _engine.TryDrop(6);
            _engine.TryDrop(3); // Win

            Assert.Equal(GameResult.None, _engine.TryDrop(4));
        }
    }
}