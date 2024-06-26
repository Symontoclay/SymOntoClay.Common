﻿/*MIT License

Copyright (c) 2020 - 2024 Sergiy Tolkachov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/

namespace SymOntoClay.Common
{
    /// <summary>
    /// Provides methods for helping in generating debug string image.
    /// </summary>
    public interface IObjectToShortString
    {
        /// <summary>
        /// Returns a string that represents the current instance in short way.
        /// </summary>
        /// <returns>A string that represents the current instance in short way.</returns>
        string ToShortString();

        /// <summary>
        /// Returns a string that represents the current instance in short way.
        /// </summary>
        /// <param name="n">Count of spaces in the string for more comfortable representation.</param>
        /// <returns>A string that represents the current instance in short way.</returns>
        string ToShortString(uint n);

        /// <summary>
        /// Internal method which returns a string that represents the current instance in short way without additional information, only pair name of property - value.
        /// </summary>
        /// <param name="n">Count of spaces in the string for more comfortable representation.</param>
        /// <returns>A string that represents the current instance in short way without additional information, only pair name of property - value.</returns>
        string PropertiesToShortString(uint n);
    }
}

/*
        /// <inheritdoc/>
        public string ToShortString()
        {
            return ToShortString(0u);
        }

        /// <inheritdoc/>
        public string ToShortString(uint n)
        {
            return this.GetDefaultToShortStringInformation(n);
        }

        /// <inheritdoc/>
        string IObjectToShortString.PropertiesToShortString(uint n)
        {
            var spaces = DisplayHelper.Spaces(n);
            var sb = new StringBuilder();
            return sb.ToString();
        }
*/