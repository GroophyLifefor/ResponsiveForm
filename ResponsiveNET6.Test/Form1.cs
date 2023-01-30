using ScintillaNET;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Markdig;
using Responsive.Azyeb;

namespace ResponsiveNET6.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        ValueWatcher watcher;
        Size initSize;
        private void Form1_Load(object sender, EventArgs e)
        {
            var responsivePage = new ResponsivePage(this);
            responsivePage
                .CreateNewHeader(this, responsivePage, out var header)
                    .LoadButtons(this, minBtn, maxBtn, closeBtn)
                    .AddMoveHandler(this, moveableMenuBar, true)
                    .AddMoveHandler(this, leftMenuBar, true);
            
            responsivePage
                .CreateNewScaler(this, responsivePage, out var scaler)
                    .LoadResizeLimits(ResizeLimits.GenerateResizeLimitsByHeader(header))
                    .LoadMouseHook()
                    .DebugItemChanged += (s, name, value) => {watcher.AddOrUpdateItem(name, value.ToString());};

            responsivePage.ControlCustomBorder.LoadRoundedBorders(this);


            // --------

            watcher = new ValueWatcher();
            watcher.Show();
            initSize = this.Size;


            this.SizeChanged += Form1_SizeChanged;


            //MoveForm moveForm = new MoveForm(this, moveableMenuBar);
            //MoveForm moveFormLeft = new MoveForm(this, leftMenuBar);
            //moveForm.LoadButtons(minBtn, maxBtn, closeBtn);
            //moveForm.AutoColorBrightnessButtons();
            //MoveForm.ColorizePanelWhenHover(helpPanel);

            //ResponsiveNET6.Sizing sizing = new Sizing(this, menuBar.Height + statusBar.Height);
            //sizing.debugItemChanged += (s, name, value) =>
            //{
            //    watcher.AddOrUpdateItem(name, value.ToString());
            //};
            //sizing.log += (s, log) =>
            //{
            //    watcher.Log(log);
            //};

            //sizing.DEBUG_GetInitSize();
            //sizing.IgnoreControlWhenSizing(menuBar);
            //sizing.IgnoreControlWhenSizing(leftMenuBar);
            //sizing.IgnoreControlWhenSizing(statusBar);
            //sizing.FixedWidth(sideBar);
            ////sizing.CreateNewConnection(scintilla, menuBar, Sizing.MarginSection.Top);
            ////sizing.CreateNewConnection(scintilla, sideBar, Sizing.MarginSection.Left);
            ////sizing.CreateNewConnection(scintilla, hiddenBlock, Sizing.MarginSection.Bottom);
            ////sizing.CreateNewConnection(preview, menuBar, Sizing.MarginSection.Top);
            ////sizing.CreateNewConnection(preview, scintilla, Sizing.MarginSection.Left);
            ////sizing.CreateNewConnection(preview, Sizing.MarginSection.Right, Sizing.As.Size);
            //sizing.AddCustomSizing(sideBar, new Action<(Control owner, Size CalculatedSize)>(x =>
            //{
            //    sideBar.Size = new Size(
            //        sideBar.Size.Width,
            //        Height - statusBar.Height - menuBar.Height);
            //}));

            //Sizing sizingMenuBar = new Sizing(menuBar);
            //sizingMenuBar.IgnoreControlWhenSizing(createAPublicNotePanel);
            //sizingMenuBar.IgnoreControlWhenSizing(leftMenuBar);
            //sizingMenuBar.CreateNewConnection(createAPublicNotePanel, Sizing.MarginSection.Right, Sizing.As.Location);

            //Sizing sizingSideBar = new Sizing(sideBar, 0);
            //sizingSideBar.IgnoreControlWhenSizing(helpPanel);
            //sizingSideBar.CreateNewConnection(checkBox1, Sizing.MarginSection.Top, Sizing.As.Location);


            //resizer = new Resizer();

            //resizer.LoadMouseHook(this);
            //resizer.LoadRoundedBorders();
            //resizer.LoadResizeLimits(minWidth: 400);

            //resizer.DebugItemChanged += (s, name, value) =>
            //{
            //    watcher.AddOrUpdateItem(name, value.ToString());
            //};
            //resizer.GenerateResizeLimitsByMoveForm(this, moveForm);

            //resizer.LoadRoundedBorders(fintoryPanel);
            //resizer.LoadRoundedBorders(fintoryImage);
            //resizer.LoadRoundedBorders(createAPublicNotePanel);
            //resizer.LoadRoundedBorders(helpPanel);
            HotReload();
        }
        Resizer resizer;

        private async void HotReload()
        {
            string html = Markdown.ToHtml(scintilla.Text);
            // Border
            html = html.Insert(0, @"<style> 
#top, #bottom, #left, #right {
	background: #000000;
	position: fixed;
	}
	#left, #right {
		top: 0; bottom: 0;
		width: 2px;
		}
		#left { left: 0; }
		#right { right: 0; }
		
	#top, #bottom {
		left: 0; right: 0;
		height: 2px;
		}
		#top { top: 0; }
		#bottom { bottom: 0; }
</style>
<div id=""left""></div>
<div id=""right""></div>
<div id=""top""></div>
<div id=""bottom""></div>");
            await preview.EnsureCoreWebView2Async();
            preview.NavigateToString(html);
        }

        private int maxLineNumberCharLength;
        private void scintilla_TextChanged(object sender, EventArgs e)
        {
            HotReload();
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            this.maxLineNumberCharLength = maxLineNumberCharLength;
        }

        private float getBestFontSize(string text, Font Mainfont, Size size)
        {
            float fontSize = 1;
            for (; fontSize < 28; fontSize += 0.10f)
            {
                Font font = new Font(Mainfont.Name, fontSize);
                var labelSize = TextRenderer.MeasureText(text, font);
                if (Math.Abs(size.Width - labelSize.Width) < 15)
                    break;
            }
            watcher.AddOrUpdateItem("BestFontSize", fontSize);
            return fontSize;
        }

        private void Form1_SizeChanged(object? sender, EventArgs e)
        {
            watcher.AddOrUpdateItem("Width", Width.ToString());
            watcher.AddOrUpdateItem("Height", Height.ToString());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                resizer.EnableAutoRefresh();
            else
                resizer.DisableAutoRefresh();
        }
    }
}