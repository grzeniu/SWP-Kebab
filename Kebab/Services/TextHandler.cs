using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticSpeechRecognition;
using Kebab.Database;
using TextToSpeech;

namespace Kebab.Services
{
    public class TextHandler : ITextAnalyzer
    {
        private const double ConfidenceThreshold = 0.4;
        private readonly Order _order = new Order();
        private readonly ISpeaker _speaker;
        private readonly ISpeechRecognition _speechRecognition;
        private MainWindow _mainWindow;
        private readonly List<Form> _formList;
        private Form _currentForm;

        public TextHandler(ISpeaker speaker, ISpeechRecognition speechRecognition)
        {
            _speaker = speaker;
            _speechRecognition = speechRecognition;
            var parser = new Parser();
            _formList = parser.ParseDocument();
        }
        public void ConnectToWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _currentForm = _formList[0];
            FormInterpretationAlgorithm(_currentForm);
        }

        private void FormInterpretationAlgorithm(Form form)
        {
            _currentForm = form;
            if (form.Field != null)
            {
                _speaker.SpeakAsync(form.Field.Prompt.Message);
            }
            else
            {
                if (form.Id.Equals("GoodEnd"))
                {
                    CalculateThePrice();
                    _mainWindow.SetLabels(_order);
                    DatabaseRepository.AddOrder(_order);
                    _speaker.Speak(form.Block.Prompt.Message);
                    Environment.Exit(0);
                }
                if (form.Id.Equals("Reset"))
                {
                    ResetOrder();
                }
            }
        }

        private void CalculateThePrice()
        {
            _order.Price = new Random().Next(20, 45).ToString();
        }

        public void AnalyzeText(RecognizedText text)
        {
            if (text.Confidence >= ConfidenceThreshold)
            {
                if (text.Default != "")
                {
                    ProcessHelpMessages(text);
                }

                else
                {
                    if (_order.Meal == "" && text.Meal != "")
                    {
                        SetNewCurrentForm(text.Meal);
                    }
                    if (_order.Kind == "" && text.Kind != "")
                    {
                        SetNewCurrentForm(text.Kind);
                        if(_order.Sauce != "")
                        {
                            SetNewCurrentForm(_order.Sauce);
                        }
                    }
                    if (_order.Sauce == "" && text.Sauce != "")
                    {
                        SetNewCurrentForm(text.Sauce);
                    }
                    FillKnownProperties(text);

                    if (_order.OrderReady() && text.Confirmation != "")
                    {
                        SetNewCurrentForm(text.Confirmation);
                    }
                    FormInterpretationAlgorithm(_currentForm);
                }
            }
            else
            {
                _speaker.SpeakAsync($"Proszę powtórzyć");
            }
        }

        private void SetNewCurrentForm(string key)
        {
            string nextFormId = _currentForm.Field.Filled.Execute(key);
            _currentForm = _formList.Find(form => form.Id == nextFormId);
        }

        private void FillKnownProperties(RecognizedText recognizedText)
        {
            if (!string.IsNullOrEmpty(recognizedText.Meal))
            {
                _order.Meal = recognizedText.Meal;
            }

            if (!string.IsNullOrEmpty(recognizedText.Kind))
            {
                _order.Kind = recognizedText.Kind;
            }

            if (!string.IsNullOrEmpty(recognizedText.Sauce))
            {
                _order.Sauce = recognizedText.Sauce;
            }

            _mainWindow.SetLabels(_order);
        }

        private void ProcessHelpMessages(RecognizedText recognizedText)
        {
            if (_formList.Any(a => (a.IdEqualsIgnoreCase(recognizedText.Default))))
            {
                Form form = _formList.First(a => (a.IdEqualsIgnoreCase(recognizedText.Default)));
                if (form.Id.Equals("Stop"))
                {
                    _speechRecognition.StopSpeech();
                    _mainWindow.SetLabels(_order);
                    _speaker.Speak(form.Block.Prompt.Message);
                    Environment.Exit(0);
                }
                else
                {
                    ResetOrder();
                }

            }
        }

        private void ResetOrder()
        {
            _order.ResetOrder();
            _mainWindow.SetLabels(_order);
            _currentForm = _formList.Find(f => f.Id.Equals("Main"));
            _speaker.Speak(_currentForm.Field.Prompt.Message);
        }
    }
}
