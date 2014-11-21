using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grappr.Tests.Square8
{
    public class Square : IState
    {
        private readonly int[] _square = new int[9];
        private readonly string[] _moves = new[] { "Left", "Right", "Up", "Down" };
        public Square(int[] square)
        {
            _square = square;
            IsTerminal = CalculateGoal(_square);
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; private set; }

        private bool CalculateGoal(int[] square)
        {
            for (int i = 0; i < square.Length; i++)
                if (i != square[i])
                    return false;
            return true;
        }
        public IEnumerable<ISuccessor> GetSuccessors()
        {
            for (int i = 0; i < 4; i++)
            {
                var move = new SquareMove(_moves[i], _square);
                if (move.IsLegal)
                    yield return move;
            }
        }

        public bool IsTerminal
        {
            get;
            private set;
        }

        public bool IsEqualTo(IState state)
        {
            if (state == null) return false;
            if (!(state is Square)) return false;
            Square square = (Square)state;
            for (int i = 0; i < _square.Length; i++)
                if (_square[i] != square._square[i])
                    return false;
            return true;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _square.Length; i++)
            {
                sb.Append(string.Format("  {0} ", _square[i] == 0 ? "_" : _square[i].ToString()));
                if ((i + 1) % 3 == 0) sb.Append("\n");
            }
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            return IsEqualTo(obj as IState);
        }

        public override int GetHashCode()
        {
            return _square.GetHashCode();
        }
    }

    public class SquareMove : ISuccessor
    {
        public SquareMove(string action, int[] square)
        {
            Action = action;
            IsLegal = true;
            CalculateState(square);
        }

        private void CalculateState(int[] square)
        {
            int idx = Array.IndexOf(square, 0);
            if (Action == "Left" && idx % 3 != 0)
                State = Swap(idx - 1, idx, square);
            else if (Action == "Right" && (idx + 1) % 3 != 0)
                State = Swap(idx + 1, idx, square);
            else if (Action == "Up" && idx > 2)
                State = Swap(idx - 3, idx, square);
            else if (Action == "Down" && idx < 6)
                State = Swap(idx + 3, idx, square);
            else
                IsLegal = false;
        }

        private static IState Swap(int a, int b, int[] square)
        {
            var newSquare = (int[])square.Clone();
            var t = newSquare[a];
            newSquare[a] = newSquare[b];
            newSquare[b] = t;
            return new Square(newSquare);
        }

        public double Cost { get { return 1; } }
        public string Action { get; private set; }
        public IState State { get; private set; }
        public bool IsLegal { get; private set; }
    }
}
