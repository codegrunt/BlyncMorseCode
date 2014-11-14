using System.Collections.Generic;
using System.Linq;

namespace BlyncMorseCode.Resource
{
    public static class DictionaryToCharacterSet
    {
        public static List<CharacterMapping> ProcessDictionaryToCharacterMappings(this Dictionary<char, string> dictionaryToProcess)
        {
            return dictionaryToProcess.ToList().Select(x => new CharacterMapping{Character = x.Key, Morse = x.Value}).ToList();
        }
    }
}
