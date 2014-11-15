using BlyncMorseCode.Configuration;
using BlyncMorseCode.Resource;

namespace BlyncMorseCode
{
    public interface IMorseCodeEngine
    {
        bool RegisterCharacterSet(ICharacterSet characterSet);
        bool UpdateConfiguration(MorseCodeEngineConfiguration configuration);
        bool ProcessString(string stringToProcess);
    }
}