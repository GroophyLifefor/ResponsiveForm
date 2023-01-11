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
            MoveForm moveForm1 = new MoveForm(this, RightMenuBar);
            MoveForm moveForm2 = new MoveForm(this, LeftMenuBar);

            Resizer resizer = new Resizer();
            resizer.LoadMouseHook(this);
            resizer.LoadResizeLimits(this, new Resizer.ResizeLimits()
            {
                minHeight = 680,
                minWidth = 735
            });

            Sizing sizing = new Sizing(this);
            sizing.IgnoreControlWhenSizing(RightMenuBar);
            sizing.IgnoreControlWhenSizing(LeftMenuBar);
            sizing.IgnoreControlWhenSizing(SideBar);
            sizing.CreateNewConnection(PlayBar, Sizing.MarginSection.Bottom);
            sizing.CreateNewConnection(PlaylistSongs, PlaylistPreviewPanel, Sizing.MarginSection.Top);
            sizing.CreateNewConnection(PlaylistSongs, Sizing.MarginSection.Right);
            sizing.CreateNewConnection(PlayBar, Sizing.MarginSection.Right);
            sizing.CreateNewConnection(PlaylistPreviewPanel, Sizing.MarginSection.Right);
            sizing.CreateNewConnection(PlayBar, PlaylistSongs, Sizing.MarginSection.Top);
            this.SizeChanged += (s, _) =>
            {
                // For some reasons I'm not able to handle this two control
                SideBar.Height = this.Height - LeftMenuBar.Height;
                menubarButtons.Location = new Point(this.Width - menubarButtons.Size.Width + 2, 0);
            };

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
        }
    }
}
