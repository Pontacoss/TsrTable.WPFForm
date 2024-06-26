﻿using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace TsrTable.TsrElement
{
    [JsonDerivedType(typeof(TsrSentence), nameof(TsrSentence))]
    public interface ITsrBlock
    {
        Collection<ITsrElement> Children { get; }
    }
}
