using System;
using System.Collections.Generic;
using BlyncMorseCode.Configuration;
using BlyncMorseCode.Engine;
using BlyncMorseCode.Resource;

namespace BlyncMorseCode
{
    public static class MorseCodeEngineFactory
    {
        static readonly Dictionary<Languages, CreateEngine> EngineGenerators = new Dictionary<Languages,CreateEngine>();

        static MorseCodeEngineFactory()
        {
            EngineGenerators[Languages.American] = MakeAmericanEngine;
            EngineGenerators[Languages.Custom] = MakeCustomEngine;
        }

        private delegate IMorseCodeEngine CreateEngine(MorseCodeEngineConfiguration configuration);

        public static IMorseCodeEngine GenerateEngine(Languages languageChoice, MorseCodeEngineConfiguration configuration)
        {
            if(!EngineGenerators.ContainsKey(languageChoice)) throw new NotImplementedException();
            return EngineGenerators[languageChoice](configuration);
        }

        static IMorseCodeEngine MakeAmericanEngine(MorseCodeEngineConfiguration configuration)
        {
            return new MorseCodeEngine(configuration, new American(), BlyncEngine.Instance);
        }

        static IMorseCodeEngine MakeCustomEngine(MorseCodeEngineConfiguration configuration)
        {
            return new MorseCodeEngine(configuration, configuration.CustomCharacterSet, BlyncEngine.Instance);
        }

    }
}
