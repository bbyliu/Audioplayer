using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace AudioPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            Pause.Visibility = Visibility.Hidden;
            Repeat.Visibility = Visibility.Hidden;
            Pause.Visibility = Visibility.Hidden;


        }
        private string[] files;
        private MediaPlayer mediaPlayer = new MediaPlayer();
        List<string> songsList = new List<string>();
        private void ChoseDir_Click(object sender, RoutedEventArgs e)
        {
            ChoiceDir();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Play_click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
        }

        private void Pause_click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }


        private void Forward_click(object sender, RoutedEventArgs e)
        {

        }

        private void Repeat_click(object sender, RoutedEventArgs e)
        {

        }

        private void RandomM_Click(object sender, RoutedEventArgs e)
        {

        }
        private void audioSlieder_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Position = new TimeSpan(Convert.ToInt64(audioSlider.Value));
        }

        private void soundSlieder_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        private void media_mediaOpened(object sender, RoutedEventArgs e)
        {
            Label2.Content = mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
            audioSlider.Maximum = mediaPlayer.NaturalDuration.TimeSpan.Ticks;
        }
        private void media_mediaEnded(object sender, RoutedEventArgs e)
        {

        }
        private void ChoiceDir()
        {
            Lsongs.Items.Clear();

            CommonOpenFileDialog dialog = new CommonOpenFileDialog() { IsFolderPicker = true };
            CommonFileDialogResult result = dialog.ShowDialog();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                files = Directory.GetFiles(dialog.FileName);
                foreach (var file in files)
                {
                    if (System.IO.Path.GetExtension(file) == ".mp3")
                    {
                        songsList.Add(file);
                        Lsongs.Items.Add(System.IO.Path.GetFileName(file));
                    }
                }
                mediaPlayer.Open(new Uri(openFileDialog.FileName));
                mediaPlayer.Play();

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();

            }
        }
        private void MPause()
        {
            Pause.Visibility = Visibility.Visible;
            Play.Visibility = Visibility.Hidden;
            if (mediaPlayer.CanPause)
            {
                mediaPlayer.Pause();
            }

        }
        private void MPlay()
        {
            Pause.Visibility = Visibility.Hidden;
            Play.Visibility = Visibility.Visible;


            /*if (media.Source != null)
            {
                if (media.Position == TimeSpan.Zero)
                {
                    // начинаем проигрывание с начала
                    media.Play();
                }
                else
                {
                    // продолжаем проигрывание с текущей позиции
                    media.Play();
                }*/

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    mediaPlayer.Open(new Uri(openFileDialog.FileName));
                    mediaPlayer.Play();

                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += timer_Tick;
                    timer.Start();
                }
            }
            void timer_Tick(object sender, EventArgs e)
            {
                if (mediaPlayer.Source != null)
                {
                    Label1.Content = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                }
                /*else
                {

                }*/

            }

    }
}


