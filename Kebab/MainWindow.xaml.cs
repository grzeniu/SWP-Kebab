using System.ComponentModel;
using System.Windows;
using AutomaticSpeechRecognition;
using TextToSpeech;

namespace Kebab
{
    /// <summary>
    ///     Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private readonly TextHandler _handler;

        public MainWindow()
        {
            _handler = new TextHandler();
            InitializeComponent();
            _worker.DoWork += Run;
            _worker.RunWorkerAsync();
        }

        private void Run(object sender, DoWorkEventArgs e)
        {
            var speaker = new Speaker();
            speaker.Initialize();

            var speechRecognition = new SpeechRecognition();
            speechRecognition.Initialize(_handler);

            _handler.Initialize(speaker, speechRecognition, this);

            //TODO extarct method for this ? Or move to handler ? Text fetch from db
            speaker.Speak("Welcome in intergalactic Pizzeria. Which pizza do you prefer?");
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
