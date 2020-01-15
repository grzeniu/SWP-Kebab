using System.Collections.Generic;

namespace AutomaticSpeechRecognition
{
    public static class Info
    {
        public static readonly IEnumerable<string> DefaultCommands = new List<string>(3)
        {
            "Help",
            "Stop",
            "Reset"
        };

        public static readonly IEnumerable<string> Sauce = new List<string>(4)
        {
            "łagodny",
            "ostry"
        };
        public static readonly IEnumerable<string> Kind = new List<string>(7)
        {
            "w bułce",
            "w cieście",
            "belgijskie",
            "standardowe"
        };

        public static readonly IEnumerable<string> Meal = new List<string>(6)
        {
            "kebaba",
            "frytki"
        };
    }
}
