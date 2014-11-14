using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using BlyncLightSDK;
using BlyncMorseCode.Configuration;

namespace BlyncMorseCode.Engine
{
    public class BlyncEngine : IDisplayEngine
    {
        //Blync Items
        private readonly BlyncLightController _blyncLightController = new BlyncLightController();
        private int _devices;

        //Object to make Threadsafe
        private static readonly object SynLock = new object();

        //Singleton Instance
        public static volatile BlyncEngine instance;

        public static BlyncEngine Instance 
        {
            get
            {
                if (instance == null)
                {
                    lock (SynLock)
                    {
                        if (instance == null)
                        {
                            instance = new BlyncEngine();
                        }
                    }
                }
                return instance;
            }
        }

        private BlyncEngine()
        {
            _devices = _blyncLightController.InitBlyncDevices();
        }

        public bool ProcessString(string inputString, Dictionary<char, List<int>> characterMappingDictionary, MorseTimingConfiguration configuration)
        {
            try
            {
                var replaceMultiSpace = new Regex(" +");
                inputString = replaceMultiSpace.Replace(inputString, " ");
                var inputCharArray = inputString.ToCharArray();
                inputCharArray.ToList().ForEach(character =>
                {
                    if (character == char.Parse(" "))
                    {
                        Blink(BlyncLightController.Color.White, configuration.MissingCharacterDisplayInMilliseconds, configuration.BreakPauseInMilliseconds);
                        Thread.Sleep(configuration.WordPauseInMilliseconds);
                    }
                    if (characterMappingDictionary.ContainsKey(character))
                    {
                        characterMappingDictionary[character].ForEach( blinkLength => Blink(BlyncLightController.Color.Green, blinkLength, configuration.BreakPauseInMilliseconds));
                    }
                    else
                    {
                        Blink(BlyncLightController.Color.Red,configuration.MissingCharacterDisplayInMilliseconds, configuration.BreakPauseInMilliseconds);
                    }
                });
                if(!configuration.EndOfStringFlicker) return true;
                var colors = new List<int> { 6, 4, 5, 3, 1, 8 };
                for (int i = 0; i < 50; i++)
                {
                    colors.ForEach(x => Blink((BlyncLightController.Color) x,10,0));
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Fired: {0}", ex.Message);
                throw;
            }
        }


        private void Blink(BlyncLightController.Color color, int secondsToDisplay, int secondsNotDisplaying)
        {
            Parallel.For(0, _devices, (i) => _blyncLightController.Display(color, i));
            Thread.Sleep(secondsToDisplay);
            Parallel.For(0, _devices, (i) => _blyncLightController.Display(BlyncLightController.Color.Off, i));
            Thread.Sleep(secondsNotDisplaying);
        }
        
        public void ReinitializeDevice()
        {
            _devices = _blyncLightController.InitBlyncDevices();
        }
    }
}
