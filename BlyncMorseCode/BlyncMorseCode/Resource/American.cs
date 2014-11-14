using System.Collections.Generic;

namespace BlyncMorseCode.Resource
{
    internal class American : ICharacterSet
    {
        private List<CharacterMapping> _charcterList;
        public List<CharacterMapping> CharacterList
        {
            get
            {
                if (_charcterList == null)
                {
                    var characterMapping = new Dictionary<char, string>
                    {
                        {'a', ".-"},
                        {'b', "-..."},
                        {'c', "-.-."},
                        {'d', "-.."},
                        {'e', "."},
                        {'f', "..-."},
                        {'g', "--."},
                        {'h', "...."},
                        {'i', ".."},
                        {'j', ".---"},
                        {'k', "-.-"},
                        {'l', ".-.."},
                        {'m', "--"},
                        {'n', "-."},
                        {'o', "---"},
                        {'p', ".--."},
                        {'q', "--.-"},
                        {'r', ".-."},
                        {'s', "..."},
                        {'t', "-"},
                        {'u', "..-"},
                        {'v', "...-"},
                        {'w', ".--"},
                        {'x', "-..-"},
                        {'y', "-.--"},
                        {'z', "--.."},
                        {'0', "-----"},
                        {'1', ".----"},
                        {'2', "..----"},
                        {'3', "...--"},
                        {'4', "....-"},
                        {'5', "....."},
                        {'6', "-...."},
                        {'7', "--..."},
                        {'8', "---.."},
                        {'9', "----."},
                        {'.',".-.-.-"},
                        {',',"--..--"},
                        {':',"---..."},
                        {'?',"..--.."},
                        {char.Parse(@"'"),".----."},
                        {'-',"-....-"},
                        {'/',"-..-."},
                        {'(',"-.--.-"},
                        {')',"-.--.-"},
                        {'[',"-.--.-"},
                        {']',"-.--.-"},
                        {'"',".-..-."},
                        {'@',".--.-."},
                        {'=',"-...-"},
                    };
                    _charcterList = characterMapping.ProcessDictionaryToCharacterMappings();
                }
                return _charcterList;
            }
        }
    }
}
