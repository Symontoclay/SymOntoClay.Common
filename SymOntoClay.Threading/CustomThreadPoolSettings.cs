﻿using SymOntoClay.Common;
using SymOntoClay.Common.DebugHelpers;
using System.Text;

namespace SymOntoClay.Threading
{
    public class CustomThreadPoolSettings: IObjectToString
    {
        public int? MinThreadsCount { get; set; }
        public int? MaxThreadsCount { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return ToString(0u);
        }

        /// <inheritdoc/>
        public string ToString(uint n)
        {
            return this.GetDefaultToStringInformation(n);
        }

        /// <inheritdoc/>
        string IObjectToString.PropertiesToString(uint n)
        {
            var spaces = DisplayHelper.Spaces(n);
            var sb = new StringBuilder();
            sb.AppendLine($"{spaces}{nameof(MinThreadsCount)} = {MinThreadsCount}");
            sb.AppendLine($"{spaces}{nameof(MaxThreadsCount)} = {MaxThreadsCount}");
            return sb.ToString();
        }
    }
}
