﻿using System.Collections.Generic;

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

        public static readonly IEnumerable<string> PizzaChoices = new List<string>(4)
        {
            "peperoni",
            "hawai",
            "hawaiian",
            "hawaiian pizza",
        };
        public static readonly IEnumerable<string> Dipps = new List<string>(7)
        {
            "garlic",
            "garlic sauce",
            "maxican",
            "maxican sauce",
            "arabian",
            "arabian sauce",
            "no sauce"
        };

        public static readonly IEnumerable<string> Cakes = new List<string>(6)
        {
            "thick",
            "thick cake",
            "thin",
            "thin cake",
            "medium",
            "medium cake"
        };
    }
}
