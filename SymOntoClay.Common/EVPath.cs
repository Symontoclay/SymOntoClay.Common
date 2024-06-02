/*MIT License

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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SymOntoClay.Common
{
    public static class EVPath
    {
        private static Regex _normalizeMatch = new Regex("(%(\\w|\\(|\\))+%)");
        private static Regex _normalizeMatch2 = new Regex("(\\w|\\(|\\))+");

        static EVPath()
        {
            RegVar("APPDIR", Directory.GetCurrentDirectory());
        }

        public static string Normalize(string sourcePath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath))
            {
                return string.Empty;
            }

            if (sourcePath.Contains(@":/") || sourcePath.Contains(@":\"))
            {
                return sourcePath;
            }

            var match = _normalizeMatch.Match(sourcePath);

            if (match.Success)
            {
                var targetValue = match.Value;

                var match2 = _normalizeMatch2.Match(targetValue);

                if (match2.Success)
                {
                    var variableName = match2.Value;

                    var variableValue = string.Empty;

                    if (_additionalVariablesDict.ContainsKey(variableName))
                    {
                        variableValue = _additionalVariablesDict[variableName];
                    }
                    else
                    {
                        variableValue = Environment.GetEnvironmentVariable(variableName);
                    }

                    if (!string.IsNullOrWhiteSpace(variableValue))
                    {
                        sourcePath = sourcePath.Replace(targetValue, variableValue);
                    }
                }
            }

            var fullPath = Path.GetFullPath(sourcePath);

            var colonPos = fullPath.IndexOf(":", 5);

            if (colonPos == -1)
            {
                return fullPath;
            }

            var backSlashPos = DetectBackSlachPos(fullPath, colonPos);

            return fullPath.Substring(backSlashPos + 1);
        }

        private static int DetectBackSlachPos(string value, int colonPos)
        {
            for (var i = colonPos; i >= 0; i--)
            {
                var ch = value[i];

                if (ch == '/' || ch == '\\')
                {
                    return i;
                }
            }

            return 0;
        }

        public static void RegVar(string varName, string varValue)
        {
            _additionalVariablesDict[varName] = varValue;
        }

        private static readonly Dictionary<string, string> _additionalVariablesDict = new Dictionary<string, string>();
    }
}
