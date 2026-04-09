using Connect4.Core;
using Xunit;

namespace Connect4.Tests
{
    public class BoardTests
    {
        [Fact]
        public void Drop_PlaceChipInEmptyColumn_ReturnsBottomRow()
        {
            var board = new Board();
            // Фишка должна упасть в самый низ (ряд 5)
            int row = board.Drop(0, 1);
            Assert.Equal(5, row);
            Assert.Equal(1, board.Get(5, 0));
        }

        [Fact]
        public void Drop_StackChips_ReturnsCorrectRows()
        {
            var board = new Board();
            board.Drop(0, 1);
            // Вторая фишка должна лечь сверху (ряд 4)
            int row = board.Drop(0, 2);
            Assert.Equal(4, row);
            Assert.Equal(2, board.Get(4, 0));
        }

        [Fact]
        public void Drop_FullColumn_ReturnsInvalid()
        {
            var board = new Board();
            // Заполняем колонку полностью (6 фишек)
            for (int i = 0; i < 6; i++) board.Drop(0, 1);
            // Следующая попытка должна вернуть ошибку
            Assert.Equal(-1, board.Drop(0, 2));
        }

        [Fact]
        public void IsFull_EmptyBoard_ReturnsFalse()
        {
            Assert.False(new Board().IsFull());
        }

        [Fact]
        public void IsFull_CompletelyFilledBoard_ReturnsTrue()
        {
            var board = new Board();
            // Забиваем всё поле
            for (int c = 0; c < 7; c++)
                for (int r = 0; r < 6; r++)
                    board.Drop(c, 1);
            Assert.True(board.IsFull());
        }
    }
}