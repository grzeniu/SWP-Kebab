﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab
{
    public class Order
    {
        public string Choice { get; set; } = "";
        public string Dip { get; set; } = "";
        public string Cake { get; set; } = "";
        public string AdditionalIngredience { get; set; } = "";
        public string Price { get; set; }

        public void ResetPizza()
        {
            Choice = "";
            Dip = "";
            Cake = "";
            AdditionalIngredience = "";
            Price = "";
        }

        public bool OrderReady()
        {
            return !string.IsNullOrEmpty(Choice) && !string.IsNullOrEmpty(Dip) && !string.IsNullOrEmpty(Cake);
        }
    }
}
