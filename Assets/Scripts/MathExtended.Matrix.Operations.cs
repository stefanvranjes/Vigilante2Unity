using System;

namespace MathExtended.Matrices
{
    public partial class Matrix
    {
        public void Multiply(Matrix Matrix)
        {
            if (Columns != Matrix.Rows)
                throw new InvalidOperationException("Cannot multiply matrices of different sizes.");
            var _result = new double[Rows, Matrix.Columns];
            for (int _row = 0; _row < Rows; _row++)
                for (int _col = 0; _col < Matrix.Columns; _col++)
                {
                    double _sum = 0;
                    for (int i = 0; i < Columns; i++)
                        _sum += _matrix[_row, i] * Matrix[i + 1, _col + 1];
                    _result[_row, _col] = _sum;
                }
            _matrix = _result;
        }

        public void HadamardProduct(Matrix Matrix)
        {
            if (Columns != Matrix.Columns || Rows != Matrix.Rows)
                throw new InvalidOperationException("Cannot multiply matrices of different sizes.");
            for (int _row = 0; _row < Rows; _row++)
                for (int _col = 0; _col < Columns; _col++)
                {
                    _matrix[_row, _col] *= Matrix[_row + 1, _col + 1];
                }
        }

        public void Multiply(double Scalar)
        {
            for (int _row = 0; _row < _matrix.GetLength(0); _row++)
                for (int _col = 0; _col < _matrix.GetLength(1); _col++)
                {
                    _matrix[_row, _col] *= Scalar;
                }
        }

        public void Add(Matrix Matrix)
        {
            if (Rows != Matrix.Rows || Columns != Matrix.Columns)
                throw new InvalidOperationException("Cannot add matrices of different sizes.");
            for (int _row = 0; _row < _matrix.GetLength(0); _row++)
                for (int _col = 0; _col < _matrix.GetLength(1); _col++)
                {
                    _matrix[_row, _col] += Matrix[_row + 1, _col + 1];
                }
        }

        public void Sub(Matrix Matrix)
        {
            if (Rows != Matrix.Rows || Columns != Matrix.Columns)
                throw new InvalidOperationException("Cannot sub matrices of different sizes.");
            for (int _row = 0; _row < _matrix.GetLength(0); _row++)
                for (int _col = 0; _col < _matrix.GetLength(1); _col++)
                {
                    _matrix[_row, _col] -= Matrix[_row + 1, _col + 1];
                }
        }

        public void Inverse()
        {
            if (!IsSquare)
                throw new InvalidOperationException("Only square matrices can be inverted.");
            int dimension = Rows;
            var result = _matrix.Clone() as double[,];
            var identity = _matrix.Clone() as double[,];
            //make identity matrix
            for (int _row = 0; _row < dimension; _row++)
                for (int _col = 0; _col < dimension; _col++)
                {
                    identity[_row, _col] = (_row == _col) ? 1.0 : 0.0;
                }
            //invert
            for (int i = 0; i < dimension; i++)
            {
                double temporary = result[i, i];
                for (int j = 0; j < dimension; j++)
                {
                    result[i, j] = result[i, j] / temporary;
                    identity[i, j] = identity[i, j] / temporary;
                }
                for (int k = 0; k < dimension; k++)
                {
                    if (i != k)
                    {
                        temporary = result[k, i];
                        for (int n = 0; n < dimension; n++)
                        {
                            result[k, n] = result[k, n] - temporary * result[i, n];
                            identity[k, n] = identity[k, n] - temporary * identity[i, n];
                        }
                    }
                }
            }
            _matrix = identity;
        }

        public void Transpose()
        {
            var _result = new double[Columns, Rows];
            for (int _row = 0; _row < _matrix.GetLength(0); _row++)
                for (int _col = 0; _col < _matrix.GetLength(1); _col++)
                {
                    _result[_col, _row] = _matrix[_row, _col];
                }
            _matrix = _result;
        }

        public void Map(Func<double, double> func)
        {
            for (int _row = 0; _row < _matrix.GetLength(0); _row++)
                for (int _col = 0; _col < _matrix.GetLength(1); _col++)
                {
                    _matrix[_row, _col] = func(_matrix[_row, _col]);
                }
        }

        public void Randomize(double lowest, double highest)
        {
            Random _random = new Random();
            double _diff = highest - lowest;
            for (int _row = 0; _row < _matrix.GetLength(0); _row++)
                for (int _col = 0; _col < _matrix.GetLength(1); _col++)
                {
                    _matrix[_row, _col] = (_random.NextDouble() * _diff) + lowest;
                }
        }

        public void Randomize()
        {
            Randomize(0.0, 1.0);
        }

        public Matrix Duplicate()
        {
            var _result = new Matrix(Rows, Columns);
            for (int row = 0; row < Rows; row++)
                for (int col = 0; col < Columns; col++)
                {
                    _result[row + 1, col + 1] = _matrix[row, col];
                }
            return _result;
        }
    }
}
