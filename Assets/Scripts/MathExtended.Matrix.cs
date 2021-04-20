using System.Diagnostics;
using System.Text;

namespace MathExtended.Matrices
{
    [DebuggerDisplay("Matrix = {ToString()}")]
    public partial class Matrix
    {
        private double[,] _matrix;

        public int Rows { get { return _matrix.GetLength(0); } }
        public int Columns { get { return _matrix.GetLength(1); } }

        public bool IsSquare => (Rows == Columns);

        public double this[int row, int column]
        {
            get
            {
                return _matrix[row - 1, column - 1];
            }

            set
            {
                _matrix[row - 1, column - 1] = value;
            }
        }

        public Matrix(int Rows, int Columns)
        {
            _matrix = new double[Rows, Columns];
        }

        /// <summary>
        /// Square Matrix constructor; Creates matrox of size m*x
        /// </summary>
        /// <param name="m"></param>
        public Matrix(int m) : this(m, m) { }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    sb.Append($"{_matrix[row, col]:N2}").Append(" ");
                }
                sb.Append(";");
            }
            sb.Append("]");
            return sb.ToString();
        }

    }
}
