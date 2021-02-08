using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Watch
{
    public partial class Watch : Form
    {
        private Label watch = new Label();
        private Timer timer = new Timer();
        private Button stopwatch = new Button();
        private Timer studyTime = new Timer();
        private Timer chillTime = new Timer();
        private SoundPlayer music = new SoundPlayer(Properties.Resources.Around);
        public Watch()
        {
            InitializeComponent();
            ConfigTimer();
            StudyTime();
            ChillTime();
            this.Load += new System.EventHandler(this.WatchLoad);
            BackgroundImage = Properties.Resources.background;
            BackgroundImageLayout = ImageLayout.Stretch;
            ImageAnimator.Animate(BackgroundImage, OnFrameChanged);
            CreateLabel();
            CreateButton();
            this.Focus();   //Залипушка
        }
        private void CreateLabel()
        {
            watch.Font = new Font("Microsoft Sans Serif", 72F);
            watch.AutoSize = false;
            watch.TextAlign = ContentAlignment.MiddleCenter;
            watch.Dock = DockStyle.Fill;
            watch.BackColor = Color.Transparent;
            watch.ForeColor = Color.Fuchsia;
            watch.Text = "00:00:00";
            this.Controls.Add(watch);
        }
        
        private void ConfigTimer()
        {
            timer.Interval = 1000;
            timer.Tick += Tic;

        }
        private void StudyTime()
        {
            studyTime.Interval = 20 * 60 * 1000;
            studyTime.Tick += PlayMusic;
        }

        private void StudyTic(object sender, EventArgs e)
        {
            music.Stop();
            chillTime.Stop();
            studyTime.Start();
        }
        private void ChillTime()
        {
            chillTime.Interval = 5 * 60 * 1000;
            chillTime.Tick += StudyTic;
        }
        private void PlayMusic(object sender, EventArgs e)
        {
            studyTime.Stop();
            chillTime.Start();
            music.Play();
        }
        private void CreateButton()
        {
            stopwatch.Font = new Font("Microsoft Sans Serif", 36F);
            stopwatch.Dock = DockStyle.Bottom;
            stopwatch.BackColor = Color.DarkViolet;
            stopwatch.FlatStyle = FlatStyle.Flat;
            stopwatch.ForeColor = Color.Indigo;
            stopwatch.AutoSize = true;
            stopwatch.Text = "Start learning";
            stopwatch.Click += StudyTic;
            this.Controls.Add(stopwatch);

        }
        private void OnFrameChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => OnFrameChanged(sender, e)));
                return;
            }
            ImageAnimator.UpdateFrames();
            Invalidate(false);
        }
        private void WatchLoad(object sender, EventArgs e)
        {
            timer.Start();
        }
        private void Tic(object sender, EventArgs e)
        {
            watch.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
