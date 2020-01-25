using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticSpeechRecognition;
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

        private void FormInterpretationAlgorithm(Form Form)
        {
            _currentForm = Form;
            if (Form.Field != null)
            {
                _speaker.SpeakAsync(Form.Field.Prompt.Message);
            }
            else
            {
                CalculateThePrice();
                _mainWindow.SetLabels(_order);
                _speaker.Speak(Form.Block.Prompt.Message);
                Environment.Exit(0);
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
                    FillKnownProperties(text);

                    String nextFormId = "default";
                    if (_order.Meal != "" && text.Meal != "")
                    {
                        nextFormId = _currentForm.Field.Filled.Execute(text.Meal);
                        _currentForm = _formList.Find(form => form.Id == nextFormId);
                    }
                    if (_order.Kind != "" && text.Kind != "")
                    {
                        nextFormId = _currentForm.Field.Filled.Execute(text.Kind);
                        _currentForm = _formList.Find(form => form.Id == nextFormId);
                    }
                    if (_order.Sauce != "" && text.Sauce != "")
                    {
                        nextFormId = _currentForm.Field.Filled.Execute(text.Sauce);
                        _currentForm = _formList.Find(form => form.Id == nextFormId);
                    }
                    FormInterpretationAlgorithm(_currentForm);
                }
            }
            else
            {
                _speaker.SpeakAsync($"Proszę powtórzyć");
            }
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
                    //reset
                    _order.ResetPizza();
                    _mainWindow.SetLabels(_order);
                    _currentForm = _formList.Find(f => f.Id.Equals("Main"));
                    FormInterpretationAlgorithm(_currentForm);
                }

            }
        }
    }
}
