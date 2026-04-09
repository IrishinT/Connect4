using Connect4.Core;
using Xunit;

namespace Connect4.Tests
{
    public class TimerTests
    {
        [Fact]
        public void Tick_DecrementsTimeLeft()
        {
            var engine = new GameEngine { TurnTimeLimit = 10 };
            engine.StartNewGame();
            engine.Tick();
            Assert.Equal(9, engine.TimeLeft);
        }

        [Fact]
        public void Tick_TimeoutEndsGame()
        {
            var engine = new GameEngine { TurnTimeLimit = 2 };
            engine.StartNewGame();

            bool timedOut = false;
            engine.GameFinished += (res) => timedOut = (res == GameResult.Timeout);

            engine.Tick(); // 1 сек
            engine.Tick(); // 0 сек -> Таймаут

            Assert.True(engine.IsGameOver);
            Assert.True(timedOut);
        }

        [Fact]
        public void Tick_NoEffectWhenGameOver()
        {
            var engine = new GameEngine { TurnTimeLimit = 1 };
            engine.StartNewGame();
            engine.Tick(); // Игра окончена
            int timeAtEnd = engine.TimeLeft;

            engine.Tick(); // Время не должно меняться
            Assert.Equal(timeAtEnd, engine.TimeLeft);
        }

        [Fact]
        public void ResetTimer_ResetsToLimit()
        {
            var engine = new GameEngine { TurnTimeLimit = 15 };
            engine.StartNewGame();
            engine.Tick();
            engine.Tick();
            Assert.Equal(13, engine.TimeLeft);

            engine.ResetTimer();
            Assert.Equal(15, engine.TimeLeft);
        }
    }
}