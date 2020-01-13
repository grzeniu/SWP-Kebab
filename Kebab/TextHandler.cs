using System;
using AutomaticSpeechRecognition;
using System.Collections.Generic;
using System.Linq;
using TextToSpeech;
using System.Windows.Threading;
using System.Windows;

namespace Kebab
{
    public partial class TextHandler : Window, Handler
    {
        private const double ConfidenceThreshold = 0.4;
        private bool _initiative = true;
        private readonly Order _order= new Order();
        private Speaker _speaker;
        private SpeechRecognition _speechRecognition;
        private MainWindow _mainWindow;

        public void Initialize(Speaker speaker, SpeechRecognition speechRecognition, MainWindow mainWindow)
        {
            _speaker = speaker;
            _speechRecognition = speechRecognition;
            _mainWindow = mainWindow;
        }

        public void Handle(RecognizedText result)
        {
                if (result.Confidence >= ConfidenceThreshold)
                {
                    Console.WriteLine(result.Text);
                    if (_initiative)
                    {
                        ProcessHelpMessages(result.TextList);
                        ProcessOrder(result.TextList);
                        FillKnownProperties(result.TextList);
                        _initiative = false;
                    }
                    else
                    {
                        FillKnownProperties(result.TextList);
                        ProcessHelpMessages(result.TextList);
                        ProcessOrder(result.TextList);

                    }

                    if (_order.OrderReady())
                    {
                        CalculateThePrice();
                        _mainWindow.SetLabels(_order);
                        _speaker.SpeakAsync($"Thank you for the order. It will be {_order.Price} dollars. Have a nice day!");
                        //_speechSynthesizer.SpeakAsync("Dziękuję za zamówienie");
                        _speechRecognition.StopSpeech();
                    }

                }
                else
                {
                    _speaker.SpeakAsync($"Sorry I didn't get that.");
                }
            }

        private void CalculateThePrice()
        {
            _order.Price = new Random().Next(20, 45).ToString();
        }

        private void ProcessOrder(IReadOnlyCollection<string> textList)
        {
            int noise = 0;
            if (textList.Contains("sauce"))
            {
                noise++;
            }

            if (textList.Contains("pizza"))
            {
                noise++;
            }

            if (textList.Contains("cake"))
            {
                noise++;
            }

            switch (textList.Count - noise)
            {
                case 1:
                    ProcessStepByStep();
                    break;
                case 2:
                    ProcessPartialOrder(textList);
                    break;
            }
        }

        private void ProcessPartialOrder(IReadOnlyCollection<string> textList)
        {
            if (textList.Select(el => el).Intersect(Info.Cakes).ToList().Count == 0)
            {
                _speaker.SpeakAsync("And what kind of cake do you prefer?");
                //_speechSynthesizer.SpeakAsync("Proszę podać rodzaj ciasta");
            }
            else if (textList.Select(el => el).Intersect(Info.Dipps).ToList().Count == 0)
            {
                _speaker.SpeakAsync("How about the sauce?");
                //_speechSynthesizer.SpeakAsync("Proszę podać sos");
            }
            else if (textList.Select(el => el).Intersect(Info.PizzaChoices).ToList().Count == 0)
            {
                _speaker.SpeakAsync("Which pizza? For now we have only hawaiian or peperoni");
                //_speechSynthesizer.SpeakAsync("Jaka będzie picca?");
            }
        }

        private void FillKnownProperties(IReadOnlyCollection<string> textList)
        {
            var cakes = textList.Select(el => el).Intersect(Info.Cakes).FirstOrDefault();
            var dips = textList.Select(el => el).Intersect(Info.Dipps).FirstOrDefault();
            var choices = textList.Select(el => el).Intersect(Info.PizzaChoices).FirstOrDefault();

            if (!string.IsNullOrEmpty(cakes))
            {
                _order.Cake = cakes;
            }

            if (!string.IsNullOrEmpty(dips))
            {
                _order.Dip = dips;
            }

            if (!string.IsNullOrEmpty(choices))
            {
                _order.Choice = choices;
            }

            _mainWindow.SetLabels(_order);
        }

        private void ProcessHelpMessages(IReadOnlyCollection<string> textList)
        {
            if (textList.Contains("Stop"))
            {
                _speechRecognition.StopSpeech();
                //_speechSynthesizer.SpeakAsync("Dziękuję za zamówienie!");
                CalculateThePrice();
                _mainWindow.SetLabels(_order);
                _speaker.SpeakAsync("It was a pleasure to serve you. Have a nice day!");
                return;
            }

            if (textList.Contains("Help"))
            {
                _speaker.SpeakAsync("Please order a pizza!");
                //_speechSynthesizer.SpeakAsync("Zamów piccce!");
                return;
            }

            if (textList.Contains("Reset"))
            {
                _speaker.SpeakAsync("Resetting the order! You can order new one now.");
                //_speechSynthesizer.SpeakAsync("Anulowano zamówienie!");
                _order.ResetPizza();
                _mainWindow.SetLabels(_order);
            }
        }

        private void ProcessStepByStep()
        {
            if (string.IsNullOrEmpty(_order.Cake))
            {
                _speaker.SpeakAsync("What kind of cake?");
                //_speechSynthesizer.SpeakAsync("Jakie ciasto?");
                return;
            }

            if (string.IsNullOrEmpty(_order.Dip))
            {
                //_speechSynthesizer.SpeakAsync("Jaki sos?");
                _speaker.SpeakAsync("Which sauce?");
                return;
            }

            if (string.IsNullOrEmpty(_order.Choice))
            {
                //_speechSynthesizer.SpeakAsync("Jaka pizza?");
                _speaker.SpeakAsync("Which pizza?");
            }

        }
    }
}
