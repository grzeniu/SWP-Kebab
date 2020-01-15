using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticSpeechRecognition;
using TextToSpeech;

namespace Kebab
{
    public class TextHandler : ITextAnalyzer
    {
        private const double ConfidenceThreshold = 0.4;
        private bool _initiative = true;
        private readonly Order _order = new Order();
        private readonly ISpeaker _speaker;
        private readonly ISpeechRecognition _speechRecognition;
        private MainWindow _mainWindow;

        public TextHandler(ISpeaker speaker, ISpeechRecognition speechRecognition)
        {
            _speaker = speaker;
            _speechRecognition = speechRecognition;
        }
        public void ConnectToWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void AnalyzeText(RecognizedText text)
        {
            if (text.Confidence >= ConfidenceThreshold)
            {
                if (_initiative)
                {
                    //ProcessHelpMessages(text.TextList);
                    //ProcessOrder(text.TextList);
                    FillKnownProperties(text);
                    _initiative = false;
                }
                else
                {
                    FillKnownProperties(text);
                    //ProcessHelpMessages(text.TextList);
                    //ProcessOrder(text);

                }

                if (_order.OrderReady())
                {
                    CalculateThePrice();
                    _mainWindow.SetLabels(_order);
                    _speaker.SpeakAsync($"Thank you for the order. It will be {_order.Price} dollars. Have a nice day!");
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
            if (textList.Select(el => el).Intersect(Info.Meal).ToList().Count == 0)
            {
                _speaker.SpeakAsync("And what kind of cake do you prefer?");
            }
            else if (textList.Select(el => el).Intersect(Info.Kind).ToList().Count == 0)
            {
                _speaker.SpeakAsync("How about the sauce?");
            }
            else if (textList.Select(el => el).Intersect(Info.Sauce).ToList().Count == 0)
            {
                _speaker.SpeakAsync("Which pizza? For now we have only hawaiian or peperoni");
            }
        }

        private void FillKnownProperties(RecognizedText recognizedText)
        {
            if (!string.IsNullOrEmpty(recognizedText.Meal))
            {
                _order.Cake = recognizedText.Meal;
            }

            if (!string.IsNullOrEmpty(recognizedText.Kind))
            {
                _order.Dip = recognizedText.Kind;
            }

            if (!string.IsNullOrEmpty(recognizedText.Sauce))
            {
                _order.Choice = recognizedText.Sauce;
            }

            _mainWindow.SetLabels(_order);
        }

        private string ListContainString(string text, IEnumerable<string> list)
        {
            foreach (string x in list)
            {
                if (text.Contains(x))
                {
                    return x;
                }
            }
            return null;
        }

        private void ProcessHelpMessages(IReadOnlyCollection<string> textList)
        {
            if (textList.Contains("Stop"))
            {
                _speechRecognition.StopSpeech();
                CalculateThePrice();
                _mainWindow.SetLabels(_order);
                _speaker.SpeakAsync("It was a pleasure to serve you. Have a nice day!");
                return;
            }

            if (textList.Contains("Help"))
            {
                _speaker.SpeakAsync("Please order a pizza!");
                return;
            }

            if (textList.Contains("Reset"))
            {
                _speaker.SpeakAsync("Resetting the order! You can order new one now.");
                _order.ResetPizza();
                _mainWindow.SetLabels(_order);
            }
        }

        private void ProcessStepByStep()
        {
            if (string.IsNullOrEmpty(_order.Cake))
            {
                _speaker.SpeakAsync("What kind of cake?");
                return;
            }

            if (string.IsNullOrEmpty(_order.Dip))
            {
                _speaker.SpeakAsync("Which sauce?");
                return;
            }

            if (string.IsNullOrEmpty(_order.Choice))
            {
                _speaker.SpeakAsync("Which pizza?");
            }

        }

    }
}
