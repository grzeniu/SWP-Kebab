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
        private readonly Speaker _speaker = new Speaker();
        private readonly TextHandler _textHandler;

        public MainWindow()
        {
            _textHandler = new TextHandler();
            InitializeComponent();
            _worker.DoWork += Run;
            _worker.RunWorkerAsync();
        }

        private void Run(object sender, DoWorkEventArgs e)
        {

            var speechRecognition = new SpeechRecognition(_textHandler);
            _textHandler.Initialize(_speaker, speechRecognition, this);

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
