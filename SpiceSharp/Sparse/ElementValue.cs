﻿using System.Numerics;
using System.Runtime.InteropServices;

namespace SpiceSharp.Sparse
{
    /// <summary>
    /// A value for a matrix element
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ElementValue
    {
        /// <summary>
        /// The real value
        /// </summary>
        [FieldOffset(0)]
        public double Real;

        /// <summary>
        /// The imaginary value
        /// </summary>
        [FieldOffset(4)]
        public double Imag;

        /// <summary>
        /// The complex representation
        /// </summary>
        [FieldOffset(0)]
        public Complex Cplx;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="re">Real part</param>
        /// <param name="im">Imaginary part</param>
        public ElementValue(double re, double im) : this()
        {
            Real = re;
            Imag = im;
        }

        /// <summary>
        /// Convert value implicitely to a double
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator double(ElementValue value) => value.Real;

        /// <summary>
        /// Convert value implicitely to a complex value
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Complex(ElementValue value) => value.Cplx;
    }
}
