using System;

namespace MathExtended.Matrices
{
    public partial class Matrix
    {
        /// <summary>
        /// Creates rotation matrix for 2D rotation
        /// </summary>
        /// <param name="angle">Rotation Angle in Degrees</param>
        /// <returns>Rotation Matrix</returns>
        public static Matrix Rotation2D(double angle)
        {
            double _rad = Math.PI * angle / 180.0;
            var _result = new Matrix(2);
            _result[1, 1] = Math.Cos(_rad);
            _result[1, 2] = -Math.Sin(_rad);
            _result[2, 1] = Math.Sin(_rad);
            _result[2, 2] = Math.Cos(_rad);
            return _result;
        }

        /// <summary>
        /// Creates roration matrix for 3D rotation around X axis
        /// </summary>
        /// <param name="angle">Rotation Angle in Degrees</param>
        /// <returns>Rotation Matrix</returns>
        public static Matrix Rotation3DX(double angle)
        {
            double _rad = Math.PI * angle / 180.0;
            var _result = new Matrix(3);
            _result[1, 1] = 1.0;
            _result[1, 2] = 0.0;
            _result[1, 3] = 0.0;
            //
            _result[2, 1] = 0.0;
            _result[2, 2] = Math.Cos(_rad);
            _result[2, 3] = -Math.Sin(_rad);
            //
            _result[3, 1] = 0.0;
            _result[3, 2] = Math.Sin(_rad);
            _result[3, 3] = Math.Cos(_rad);
            return _result;
        }

        /// <summary>
        /// Creates roration matrix for 3D rotation around Y axis
        /// </summary>
        /// <param name="angle">Rotation Angle in Degrees</param>
        /// <returns>Rotation Matrix</returns>
        public static Matrix Rotation3DY(double angle)
        {
            double _rad = Math.PI * angle / 180.0;
            var _result = new Matrix(3);
            _result[1, 1] = Math.Cos(_rad);
            _result[1, 2] = 0.0;
            _result[1, 3] = Math.Sin(_rad);
            //
            _result[2, 1] = 0.0;
            _result[2, 2] = 1.0;
            _result[2, 3] = 0.0;
            //
            _result[3, 1] = -Math.Sin(_rad);
            _result[3, 2] = 0.0;
            _result[3, 3] = Math.Cos(_rad);
            return _result;
        }

        /// <summary>
        /// Creates roration matrix for 3D rotation around Z axis
        /// </summary>
        /// <param name="angle">Rotation Angle in Degrees</param>
        /// <returns>Rotation Matrix</returns>
        public static Matrix Rotation3DZ(double angle)
        {
            double _rad = Math.PI * angle / 180.0;
            var _result = new Matrix(3);
            _result[1, 1] = Math.Cos(_rad);
            _result[1, 2] = -Math.Sin(_rad);
            _result[1, 3] = 0.0;
            //
            _result[2, 1] = Math.Sin(_rad);
            _result[2, 2] = Math.Cos(_rad);
            _result[2, 3] = 0.0;
            //
            _result[3, 1] = 0.0;
            _result[3, 2] = 0.0;
            _result[3, 3] = 1.0;
            return _result;
        }

        public static Matrix Scaling(double factor)
        {
            return Scaling(factor, factor, factor);
        }

        public static Matrix Scaling(double factorX, double factorY, double factorZ)
        {
            var _result = new Matrix(3);
            _result[1, 1] = factorX;
            _result[1, 2] = 0.0;
            _result[1, 3] = 0.0;
            //
            _result[2, 1] = 0.0;
            _result[2, 2] = factorY;
            _result[2, 3] = 0.0;
            //
            _result[3, 1] = 0.0;
            _result[3, 2] = 0.0;
            _result[3, 3] = factorZ;
            return _result;
        }

        public static Matrix Translation(double moveX, double moveY, double moveZ)
        {
            var _result = new Matrix(4);
            _result[1, 1] = 1.0;
            _result[1, 2] = 0.0;
            _result[1, 3] = 0.0;
            _result[1, 3] = moveX;
            //
            _result[2, 1] = 0.0;
            _result[2, 2] = 1.0;
            _result[2, 3] = 0.0;
            _result[2, 4] = moveY;
            //
            _result[3, 1] = 0.0;
            _result[3, 2] = 0.0;
            _result[3, 3] = 1.0;
            _result[3, 4] = moveZ;
            //
            _result[4, 1] = 0.0;
            _result[4, 2] = 0.0;
            _result[4, 3] = 0.0;
            _result[4, 4] = 1.0;
            return _result;
        }

    }
}
