using NAudio.Wave;
using Responsive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponsiveForm.Test
{
    public partial class MusicPlayer : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        AudioFileReader audioFileReader;
        IWavePlayer waveOutDevice;
        public MusicPlayer(string path)
        {
            if (string.IsNullOrEmpty(path)) path = @"C:\Users\GROOPHY\Downloads\Music\BuKalpSeniUnuturMu.mp3";
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            DebugLogs debug = new DebugLogs();

            ListDirectory(Songs, Path.GetDirectoryName(path));

            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader(path);

            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
            stopwatch.Start();

            trackBar1.Maximum = (int)audioFileReader.TotalTime.TotalSeconds;
            Thread thread = new Thread(UpdateValue);
            thread.Start();
        }

        private void UpdateValue()
        {
            for (; ; )
            {
                time.Text = stopwatch.Elapsed.ToString() + " / " + audioFileReader.TotalTime.ToString();
                trackBar1.Value = (int)stopwatch.Elapsed.TotalSeconds;
                label1.Text = 
$@"
SampleRate:             {waveOutDevice.OutputWaveFormat.SampleRate}
AverageBytesPerSecond:  {waveOutDevice.OutputWaveFormat.AverageBytesPerSecond}
BitsPerSample:          {waveOutDevice.OutputWaveFormat.BitsPerSample}
BlockAlign:             {waveOutDevice.OutputWaveFormat.BlockAlign}
Channels:               {waveOutDevice.OutputWaveFormat.Channels}
".TrimStart();
            }
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles("*.mp3"))
            {
                var node = new TreeNode(file.Name);
                node.Tag = file.FullName;
                directoryNode.Nodes.Add(node);
            }
            return directoryNode;
        }

        private void MusicPlayer_Load(object sender, EventArgs e)
        {
            MoveForm moveForm = new MoveForm(this, Menubar);
            moveForm.LoadButtons(minBtn, maxBtn, closeBtn);

            Resizer resizer = new Resizer();
            resizer.LoadMouseHook(this);
            resizer.LoadRoundedBorders();
            resizer.DisableAutoRefresh();
            resizer.LoadResizeLimits(this, new Resizer.ResizeLimits()
            {
                minWidth = 470,
                minHeight = 300
            });

            Sizing sizing = new Sizing(this);
            sizing.IgnoreControlWhenSizing(Menubar);
            sizing.CreateNewConnection(label1, panel1, Sizing.MarginSection.Left);
            sizing.CreateNewConnection(trackBar1, panel1, Sizing.MarginSection.Left);
            sizing.CreateNewConnection(time, panel1, Sizing.MarginSection.Left);

            int trackBarDistance    = this.Height - trackBar1.Location.Y;
            int timeLabelDistance   = this.Height - time.Location.Y;
            SizeChanged += (_, __) =>
            {
                trackBar1.Location  = new Point(trackBar1.Location.X, this.Height - trackBarDistance);
                time.Location       = new Point(time.Location.X, this.Height - timeLabelDistance);
            };
        }

        private void Songs_DoubleClick(object sender, EventArgs e)
        {
            if (Songs.SelectedNode.Tag is null) return;

            DebugLogs.AddOrUpdateItem("path", Songs.SelectedNode.Tag.ToString());

            waveOutDevice.Stop();
            stopwatch.Reset();
            waveOutDevice.Dispose();
            audioFileReader.Dispose();

            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader(Songs.SelectedNode.Tag.ToString());

            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
            stopwatch.Start();

            trackBar1.Maximum = (int)audioFileReader.TotalTime.TotalSeconds;
        }
    }
}
