using System;

namespace MathExtended.Matrices
{
    public partial class Matrix
    {
        private void Zero()
        {
            for (int _row = 0; _row < _matrix.GetLength(0); _row++)
                for (int _col = 0; _col < _matrix.GetLength(1); _col++)
                {
                    _matrix[_row, _col] = 0.0;
                }
        }

        private void Identity()
        {
            if (!IsSquare)
                throw new InvalidOperationException("Identity matrix must be square.");
            Zero();
            for (int n = 0; n < Rows; n++)
                _matrix[n, n] = 1.0;
        }

        public static Matrix Zero(int size)
        {
            var _result = new Matrix(size);
            _result.Zero();
            return _result;
        }

        public static Matrix Zero(int rows, int cols)
        {
            var _result = new Matrix(rows, cols);
            _result.Zero();
            return _result;
        }

        public static Matrix Identity(int size)
        {
            var _result = new Matrix(size);
            _result.Identity();
            return _result;
        }

        public static Matrix Vandermonde(int rows, int cols)
        {
            var _result = new Matrix(rows, cols);
            for (int m = 1; m <= rows; m++)
                for (int n = 1; n <= cols; n++)
                {
                    _result[m, n] = Math.Pow(m, n - 1);
                }
            return _result;
        }
    }
}
