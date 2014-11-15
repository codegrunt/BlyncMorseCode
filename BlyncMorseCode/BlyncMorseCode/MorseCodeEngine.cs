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
        private MorseCodeEngineConfiguration _configuration;
        private ICharacterSet MorseCharacterSet { get; set; }
        private IDisplayEngine DisplayEngine { get; set; }
        
        public MorseCodeEngine(MorseCodeEngineConfiguration configuration, ICharacterSet morseCharacterSet, IDisplayEngine displayEngine)
        {
            _configuration = configuration;
            MorseCharacterSet = morseCharacterSet;
            ProcessCharacterSet(MorseCharacterSet);
            DisplayEngine = displayEngine;
        }
        
        private Dictionary<char, List<int>> _workingConverter;
        
        public bool RegisterCharacterSet(ICharacterSet characterSet)
        {
            MorseCharacterSet = characterSet;
            ProcessCharacterSet(characterSet);
            return true;
        }

        public bool UpdateConfiguration(MorseCodeEngineConfiguration configuration)
        {
            _configuration = configuration;
            return true;
        }

        internal void ProcessCharacterSet(ICharacterSet characterSet)
        {
            _workingConverter = new Dictionary<char, List<int>>();
            var dotDash = new Dictionary<char, int>
            {
                {'.', _configuration.DotLengthInMilliseconds},
                {'-', _configuration.DashLengthInMilliseconds}
            };
            MorseCharacterSet.CharacterList.ForEach(x =>
            {
                var morseCharArray = x.Morse.ToCharArray();
                List<int> parsedMorse = morseCharArray.ToList().Select(character =>
                {
                    if (!dotDash.ContainsKey(character)) throw new ArgumentOutOfRangeException("CharacterMapping", x, string.Format("CharacterMapping contains an invalid character of {0}", character));
                    return dotDash[character];
                }).ToList();
                _workingConverter.Add(x.Character, parsedMorse);
            });
        }

        public bool ProcessString(string stringToProcess)
        {
            if (MorseCharacterSet == null) throw new ArgumentNullException("MorseCharacterSet", "If you use a custom IMorseCodeEngine you must initialize a ICharacterSet through IMorseCodeEngine.RegisterCharacterSet(ICharacterSet)");
            return DisplayEngine.ProcessString(stringToProcess.ToLower(), _workingConverter, _configuration);
        }
    }
}
