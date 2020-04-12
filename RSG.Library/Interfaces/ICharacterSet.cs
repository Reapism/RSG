﻿using RSG.Core.Models;
using System.Collections.Generic;

namespace RSG.Core.Interfaces
{
    public interface ICharacterSet
    {
        IDictionary<string, CharacterSet> CharacterSets { get; set; }
    }
}
