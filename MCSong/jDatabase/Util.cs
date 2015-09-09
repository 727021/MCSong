﻿/*
 *  Copyright 2013 Jonny Li (jonnyli1125)
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a copy
 *  of this software and associated documentation files (the "Software"), to deal
 *  in the Software without restriction, including without limitation the rights
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  copies of the Software, and to permit persons to whom the Software is
 *  furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 *  THE SOFTWARE.
 *
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace jDatabase
{
    /// <summary>
    /// Class that contains commonly-used methods.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Gets the the file name with the file extension removed.
        /// </summary>
        /// <param name="fileName">The name of the file (with or without an extension).</param>
        /// <returns>The file name with the file extension removed.</returns>
        public static string RemoveFileExtension(string fileName) { return (fileName.Contains(".") ? fileName.Remove(fileName.LastIndexOf(".")) : fileName); }

        /// <summary>
        /// Gets the file path with only the last directory.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <returns>The file path with only the last directory.</returns>
        public static string LastDirectory(string path) { return (!String.IsNullOrEmpty(path) && (path.Contains(Path.DirectorySeparatorChar) || path.Contains(Path.AltDirectorySeparatorChar))) ? path.TrimEnd(Path.DirectorySeparatorChar).TrimEnd(Path.AltDirectorySeparatorChar).Substring(Math.Max(path.LastIndexOf(Path.DirectorySeparatorChar), path.LastIndexOf(Path.AltDirectorySeparatorChar)) + 1) : path; }

        /// <summary>
        /// Returns a list with all empty strings within it removed.
        /// </summary>
        /// <param name="list">The list to apply changes to.</param>
        /// <returns>A list with all empty strings within it removed.</returns>
        public static List<string> RemoveEmptyStrings(IEnumerable<string> list) { return list.Where(s => !String.IsNullOrEmpty(s)).Distinct().ToList(); }
    }
}
