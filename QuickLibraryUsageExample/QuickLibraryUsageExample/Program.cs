using BlyncMorseCode;
using BlyncMorseCode.Configuration;

namespace QuickLibraryUsageExample

{
    class Program
    {
        static void Main(string[] args)
        {
            var blinkConfig = new MorseCodeEngineConfiguration
            {
                BreakPauseInMilliseconds = 100,
                DashLengthInMilliseconds = 400,
                DotLengthInMilliseconds = 100,
                EndOfStringPauseInMilliseconds = 1000,
                LetterPauseInMilliseconds = 300,
                MissingCharacterDisplayInMilliseconds = 400,
                WordPauseInMilliseconds = 300,
                EndOfStringFlicker = false
            };

            var morseEngine = MorseCodeEngineFactory.GenerateEngine(Languages.American, blinkConfig);
            morseEngine.ProcessString("Hello World!");
        }
    }
}
