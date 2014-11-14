using System;
using System.Collections.Generic;
using System.Linq;
using BlyncMorseCode.Configuration;
using BlyncMorseCode.Engine;
using BlyncMorseCode.Resource;

namespace BlyncMorseCode
{
    internal class MorseCodeEngine : IMorseCodeEngine
    {
        private MorseTimingConfiguration _configuration;
        private ICharacterSet MorseCharacterSet { get; set; }
        private IDisplayEngine DisplayEngine { get; set; }
        
        public MorseCodeEngine(MorseTimingConfiguration configuration, ICharacterSet morseCharacterSet, IDisplayEngine displayEngine)
        {
            _configuration = configuration;
            MorseCharacterSet = morseCharacterSet;
            DisplayEngine = displayEngine;
        }
        
        private Dictionary<char, List<int>> workingConverter;
        
        public bool RegisterCharacterSet(ICharacterSet characterSet)
        {
            MorseCharacterSet = characterSet;
            ProcessCharacterSet(characterSet);
            return true;
        }

        public bool UpdateConfiguration(MorseTimingConfiguration configuration)
        {
            _configuration = configuration;
            return true;
        }

        internal void ProcessCharacterSet(ICharacterSet characterSet)
        {
            workingConverter = new Dictionary<char, List<int>>();
            var DotDash = new Dictionary<char, int>
            {
                {'.', _configuration.DotLengthInMilliseconds},
                {'-', _configuration.DashLengthInMilliseconds}
            };
            MorseCharacterSet.CharacterList.ForEach(x =>
            {
                var morseCharArray = x.Morse.ToCharArray();
                List<int> parsedMorse = morseCharArray.ToList().Select(character =>
                {
                    if (!DotDash.ContainsKey(character)) throw new ArgumentOutOfRangeException("CharacterMapping", x, string.Format("CharacterMapping contains an invalid character of {0}", character));
                    return DotDash[character];
                }).ToList();
                workingConverter.Add(x.Character, parsedMorse);
            });
        }

        public bool ProcessString(string stringToProcess)
        {
            if (MorseCharacterSet == null) throw new ArgumentNullException("MorseCharacterSet", "If you use a custom IMorseCodeEngine you must initialize a ICharacterSet through IMorseCodeEngine.RegisterCharacterSet(ICharacterSet)");
            return DisplayEngine.ProcessString(stringToProcess, workingConverter, _configuration);
        }
    }
}
