using System.ComponentModel;
using AutomaticSpeechRecognition;
using Kebab.Services;
using TextToSpeech;

namespace Kebab
{
    /// <summary>
    ///     Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private readonly ISpeechRecognition _speechRecognitionEngine;
        private readonly ITextAnalyzer _textHandler;
        private readonly IKebabManager _kebabManager;

        public MainWindow()
        {
            ISpeaker speaker = new Speaker();
            _speechRecognitionEngine = new SpeechRecognition();
            _textHandler = new TextHandler(speaker, _speechRecognitionEngine);
            _kebabManager = new KebabManager(_speechRecognitionEngine, _textHandler);

            InitializeComponent();
            _worker.DoWork += Run;
            _worker.RunWorkerAsync();
        }

        private void Run(object sender, DoWorkEventArgs e)
        {
            _speechRecognitionEngine.Initialize(_kebabManager.ManageKebab);
            _textHandler.ConnectToWindow(this);
        }
        public void SetLabels(Order order)
        {
            Dispatcher?.Invoke(() =>
            {
                orderMeal.Text = order.Meal;
                orderSauce.Text = order.Sauce;
                orderKind.Text = order.Kind;
                orderPrice.Text = order.Price;
            });
        }
    }
}
