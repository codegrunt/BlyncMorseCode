namespace BlyncMorseCode.Configuration
{
    public class MorseTimingConfiguration
    {
        public int WordPauseInMilliseconds { get; set; }
        public int LetterPauseInMilliseconds { get; set; }
        public int BreakPauseInMilliseconds { get; set; }
        public int MissingCharacterDisplayInMilliseconds { get; set; }
        public int DotLengthInMilliseconds { get; set; }
        public int DashLengthInMilliseconds { get; set; }
        public bool EndOfStringFlicker { get; set; }
        public int EndOfStringPauseInMilliseconds { get; set; }
    }
}
