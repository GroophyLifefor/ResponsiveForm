using Responsive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Responsive.Resizer;

namespace ResponsiveForm.Test
{
    public partial class Spotify : Form
    {
        public Spotify()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Spotify_Load(object sender, EventArgs e)
        {
            DebugLogs debug = new DebugLogs();
            Program.logs = debug;

            MoveForm moveForm1 = new MoveForm(this, RightMenuBar);
            MoveForm moveForm2 = new MoveForm(this, LeftMenuBar);

            Resizer resizer = new Resizer();
            resizer.LoadMouseHook(this);
            resizer.LoadResizeLimits(this, new Resizer.ResizeLimits()
            {
                minHeight = 680,
                minWidth = 735
            });
            //resizer.LoadRoundedBorders();
            //resizer.DisableAutoRefresh();

            Sizing sizing = new Sizing(this);
            sizing.IgnoreControlWhenSizing(RightMenuBar);
            sizing.IgnoreControlWhenSizing(LeftMenuBar);
            sizing.CreateNewConnection(PlayBar, Sizing.MarginSection.Bottom);
            sizing.CreateNewConnection(PlaylistSongs, PlaylistPreviewPanel, Sizing.MarginSection.Top);
            sizing.CreateNewConnection(PlaylistSongs, Sizing.MarginSection.Right);
            sizing.CreateNewConnection(PlayBar, Sizing.MarginSection.Right);
            sizing.CreateNewConnection(PlaylistPreviewPanel, Sizing.MarginSection.Right);
            sizing.CreateNewConnection(PlayBar, PlaylistSongs, Sizing.MarginSection.Top);
            sizing.CreateNewConnection(SideBar, RightMenuBar, Sizing.MarginSection.Top);
            sizing.FixedWidth(SideBar);

            Sizing playlistPreviewSizing = new Sizing(PlaylistPreviewPanel);
            playlistPreviewSizing.CreateNewConnection(PlaylistTimePanel, playlistMainPanel, Sizing.MarginSection.Top);
            playlistPreviewSizing.CreateNewConnection(playlistMainPanel, PlaylistPicture, Sizing.MarginSection.Left);
            playlistPreviewSizing.CreateNewConnection(PlaylistTimePanel, PlaylistPicture, Sizing.MarginSection.Left);

            MainPage.MouseHover += (s, p) =>
            {
                MainPage.Image = SpotifySource.mainpageHover;
            };
            MainPage.MouseLeave += (s, p) =>
            {
                MainPage.Image = SpotifySource.mainpage;
            };

            Search.MouseHover += (s, p) =>
            {
                Search.Image = SpotifySource.searchHover;
            };
            Search.MouseLeave += (s, p) =>
            {
                Search.Image = SpotifySource.search;
            };

            Library.MouseHover += (s, p) =>
            {
                Library.Image = SpotifySource.libHover;
            };
            Library.MouseLeave += (s, p) =>
            {
                Library.Image = SpotifySource.lib;
            };

            //MouseHandle.addRule(delegate (Point lpPoint)
            //{
            //    bool isHorizontal = lpPoint.X > Location.X + Size.Width - 8 &&
            //            lpPoint.X < Location.X + Size.Width + 2 &&
            //            lpPoint.Y > Location.Y &&
            //            lpPoint.Y < Location.Y + Size.Height - 28;
            //    bool isVertical = lpPoint.X > Location.X &&
            //            lpPoint.X < Location.X + Size.Width - 28 &&
            //            lpPoint.Y > Location.Y + Size.Height - 8 &&
            //            lpPoint.Y < Location.Y + Size.Height + 2;
            //    bool isBoth = lpPoint.X > Location.X + Size.Width - 28 &&
            //            lpPoint.X < Location.X + Size.Width &&
            //            lpPoint.Y > Location.Y + Size.Height - 28 &&
            //            lpPoint.Y < Location.Y + Size.Height;
            //    DebugLogs.AddOrUpdateItem("isHorizontal", isHorizontal.ToString());
            //    DebugLogs.AddOrUpdateItem("isVertical", isVertical.ToString());
            //    DebugLogs.AddOrUpdateItem("isBoth", isBoth.ToString());
            //    DebugLogs.AddOrUpdateItem("Cursor", GetCursor().ToString());
            //    DebugLogs.AddOrUpdateItem("FrmSize", Size.ToString());
            //    DebugLogs.AddOrUpdateItem("sw", resizer.controlsVisibilityTimer.Elapsed.TotalMilliseconds.ToString());
            //});
        }
    }
}
