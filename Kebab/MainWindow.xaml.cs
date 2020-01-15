using System.ComponentModel;
using AutomaticSpeechRecognition;
using TextToSpeech;

namespace Kebab
{
    /// <summary>
    ///     Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private readonly ISpeaker _speaker;
        private readonly ISpeechRecognition _speechRecognitionEngine;
        private readonly ITextAnalyzer _textHandler;
        private readonly IKebabManager _kebabManager;

        public MainWindow()
        {
            _speaker = new Speaker();
            _speechRecognitionEngine = new SpeechRecognition();
            _textHandler = new TextHandler(_speaker, _speechRecognitionEngine);
            _kebabManager = new KebabManager(_speechRecognitionEngine, _textHandler);

            InitializeComponent();
            _worker.DoWork += Run;
            _worker.RunWorkerAsync();
        }

        private void Run(object sender, DoWorkEventArgs e)
        {
            _speechRecognitionEngine.Initialize(_kebabManager.ManageKebab);
            _textHandler.ConnectToWindow(this);

            //TODO extarct method for this ? Or move to handler ? Text fetch from db
            _speaker.Speak("Welcome in intergalactic Pizzeria. Which pizza do you prefer?");
        }
        public void SetLabels(Order order)
        {
            Dispatcher?.Invoke(() =>
            {
                pizzaNumber.Text = order.Choice;
                pizzaCake.Text = order.Cake;
                pizzaDip.Text = order.Dip;
                pizzaPrice.Text = order.Price;
            });
        }
    }
}
