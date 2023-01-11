# ResponsiveForm

In many reason winforms are not responsive, so i felt compelled to do this.

# Sizing (Responsive/Sizing.cs)

## Without Initalized.

![withoutSizing](https://user-images.githubusercontent.com/77299279/211275690-49898961-a110-4300-b15f-efb024cc7004.gif)

Actually, really bad
I don't use any dock because dock too not a good way I think.

## When you call just Sizing Class like
```cs
private void Form1_Load(object sender, EventArgs e)
{
    Sizing sizing = new Sizing(this);
}
```

![JustCallSizing](https://user-images.githubusercontent.com/77299279/211275746-1a0bc2a6-fcdd-433c-88ba-6ce3ea0b3c35.gif)

Yes, that's much worse, I must to change something,
or? adding some feature?

## If I able to connect controls to controls as side by side? like
```cs
private void Form1_Load(object sender, EventArgs e)
{
    Sizing sizing = new Sizing(this);
    sizing.CreateNewConnection(richTextBox1, menuStrip1, Sizing.MarginSection.Top);
    sizing.CreateNewConnection(button1, richTextBox1, Sizing.MarginSection.Top);
    sizing.CreateNewConnection(button2, button1, Sizing.MarginSection.Top);
    sizing.CreateNewConnection(button3, button2, Sizing.MarginSection.Top);
    sizing.CreateNewConnection(button4, richTextBox1, Sizing.MarginSection.Top);
    sizing.CreateNewConnection(button4, button1, Sizing.MarginSection.Left);
    sizing.CreateNewConnection(numericUpDown1, richTextBox1, Sizing.MarginSection.Top);
    sizing.CreateNewConnection(numericUpDown1, button3, Sizing.MarginSection.Left);
    sizing.CreateNewConnection(treeView1, richTextBox1, Sizing.MarginSection.Left);
    sizing.CreateNewConnection(treeView1, Sizing.MarginSection.Right);
    sizing.CreateNewConnection(richTextBox1, Sizing.MarginSection.Bottom);
    sizing.CreateNewConnection(button1, Sizing.MarginSection.Bottom);
    sizing.CreateNewConnection(button2, Sizing.MarginSection.Bottom);
    sizing.CreateNewConnection(button3, Sizing.MarginSection.Bottom);
    sizing.CreateNewConnection(button4, Sizing.MarginSection.Bottom);
}
 ```

![connections](https://user-images.githubusercontent.com/77299279/211275798-4bb9d297-b29c-4284-aa20-0f82d8307674.png)

![WithConnections](https://user-images.githubusercontent.com/77299279/211275846-d889b205-3dbf-4a06-91c2-3945df87c377.gif)

Yeah, Really better

# MoveForm (Responsive/MoveForm.cs)

WinForm gives you a MenuBar which you can `move, put title, minimalize, maximize, close`.
But not so effective.
(I was inspired by Spotify UI)
![spotify](https://user-images.githubusercontent.com/77299279/211364584-311e3831-e08e-4a2f-b55a-84c4b6b7f82d.png)

## Without Initalized.

![Withoutmoveform](https://user-images.githubusercontent.com/77299279/211368466-dd160200-1dcb-4ad2-aeb5-8489ae529564.gif)

Lets try something
You can do everything which WinForm gives you
and you can do more.

## Steps
 - Create Panel which will be custom MenuBar

```cs
MoveForm moveForm = new MoveForm(this, panel1);
 ```
![Withmoveform](https://user-images.githubusercontent.com/77299279/211370003-8e37e6d3-bb74-4a59-b3c3-ef3ded580bd4.gif)
Maybe you see laggy reason of you see in a gif, but really smooth

 - **If you want** customize your MenuBar
 - Put three button which will be `minimalize, maximize, close` 
 
 ```cs
 LoadButtons(
    Form MainForm,                         // Your Form usually 'this'
    Control MinimalizeBtn,                 // Your Minimalize Control(usually Button)
    Control SizingChangeBtn,               // Your Maximize/Normalize Control(usually Button)
    Control CloseBtn,                      // Your Close Control(usually Button)
    bool JustHideFormWhenClose = false)    // true  -> Hide Form when tried Close
                                           // false -> Close all of application
 ```
Actually I don't wanted use `Form` but I must use Form because I need `WindowState` property
```cs
MoveForm moveForm = new MoveForm(this, panel1);
moveForm.LoadButtons(this, minBtn, maxBtn, closeBtn);
 ```
![Withbuttons](https://user-images.githubusercontent.com/77299279/211490430-72fb97f2-b93c-430e-9894-f58398a55f09.gif)

I record in setted values so you don't see full screen, But maximize works fine.

Really going better!

# Resizer (Responsive/Resizer.cs)

## Without Initalized.

![withoutResizer](https://user-images.githubusercontent.com/77299279/211490742-ff35a918-b830-4444-8e60-99c10d70a3f6.gif)

WinForm give you resize your form but when `FormBorderStyle = None`, You can not resize your form as you can see top.

## With Initalize

Actually do not do anything with just initalize.
You must to call some methots

## LoadMouseHook

```cs
Resizer resizer = new Resizer();
resizer.LoadMouseHook(this);
 ```

![withmousehook](https://user-images.githubusercontent.com/77299279/211505617-08118124-d26b-409b-b7e7-90ffe135a7fb.gif)

Your project's interface may not be able to support smaller or larger than an amount. So you can set Resize limit.

## ResizeLimit

```cs
resizer.LoadResizeLimits(this, new Resizer.ResizeLimits()
            {
                minWidth = 600,
                minHeight = 500,
                maxWidth = 1200,
                maxHeight = 800
            });
 ```

![withreizelimit](https://user-images.githubusercontent.com/77299279/211505638-d6faf916-449e-4710-ae0e-f35ddbbfa2d1.gif)

Some distortions occur and these distortions are corrected with minor resizes, my goal is to fix them completely.


# Final Project

![final](https://user-images.githubusercontent.com/77299279/211506900-f64ce7bb-f489-4622-9d88-00c71ff59e1f.gif)

# Spotify Demo UI

Made for just Responsive UI not able to play music or else.
Some info about demo
 -  minHeight = 680,
    minWidth = 735
 - Two Menubar reason of two color
 - Two Sizing which `Form` and `PlaylistPanel` as (picture, state, name, author, time)

![finalspotify](https://user-images.githubusercontent.com/77299279/211758455-5dd65520-1d17-4a5e-9b7e-8d8614c72f9b.gif)