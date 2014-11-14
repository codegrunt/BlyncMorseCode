using System.Collections.Generic;

namespace BlyncMorseCode.Resource
{
    public interface ICharacterSet
    {
        List<CharacterMapping> CharacterList { get; }
    }
}