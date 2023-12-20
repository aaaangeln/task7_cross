using System;
namespace te.Models
{
    public class TicTacToeModel
    {
        public char[] Board { get; set; } // Игровое поле
        public int CurrentPlayer { get; set; } // Текущий игрок (1 или 2)
    }
}

