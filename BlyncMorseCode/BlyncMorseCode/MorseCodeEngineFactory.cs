using System;
using System.Collections.Generic;
using BlyncMorseCode.Configuration;
using BlyncMorseCode.Engine;
using BlyncMorseCode.Resource;

namespace BlyncMorseCode
{
    public class MorseCodeEngineFactory
    {
        private readonly Dictionary<Languages, CreateEngine> _engineGenerators = new Dictionary<Languages,CreateEngine>();

        public MorseCodeEngineFactory()
        {
            _engineGenerators[Languages.American] = makeAmericanEngine;
        }

        private delegate IMorseCodeEngine CreateEngine(MorseTimingConfiguration configuration);

        public IMorseCodeEngine GenerateEngine(Languages languageChoice, MorseTimingConfiguration configuration)
        {
            if(!_engineGenerators.ContainsKey(languageChoice)) throw new NotImplementedException();
            return _engineGenerators[languageChoice](configuration);
        }

        private IMorseCodeEngine makeAmericanEngine(MorseTimingConfiguration configuration)
        {
            return new MorseCodeEngine(configuration, new American(), BlyncEngine.Instance);
        }
    }
}
