//////////////////////////////////////////////////////////////////////////
//                                                                      //
//  Anybody can Use, Modify, Redistribute this code freely. If this     // 
//  module has been helpful to you then just leave a comment on Website //
// Name:        IsaiyiniKeyboard
// Purpose:	 Accessing the TACE keyboard easily
//
// Author:      Pitchaimuthu
//
// Created:     24/04/2019
// Copyright:   (c) Pitchaimuthu 2019
// Licence:     This work is licensed under a CCA 4.0 International License. 
//              http://creativecommons.org/licenses/by/4.0/
//                                                                      //
//////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using CaretPosition.Entity.ITRANS;

namespace CaretPosition
{
    public partial class frmTooltip : Form
    {

        public frmTooltip()
        {
            InitializeComponent();
            timer1.Start();  // Processing events from Hooks involves message queue complexities.
        }                    // Timer has been used just to avoid that Mouse and Keyboard hooking                           
        // and to keep things simple. 

        # region Data Members & Structures
        StringBuilder currentTextWord = new StringBuilder();
        StringBuilder sb = new StringBuilder();
        StringBuilder sbWholeWords = new StringBuilder();

        [StructLayout(LayoutKind.Sequential)]    // Required by user32.dll
        public struct RECT
        {
            public uint Left;
            public uint Top;
            public uint Right;
            public uint Bottom;
        };

        [StructLayout(LayoutKind.Sequential)]    // Required by user32.dll
        public struct GUITHREADINFO
        {
            public uint cbSize;
            public uint flags;
            public IntPtr hwndActive;
            public IntPtr hwndFocus;
            public IntPtr hwndCapture;
            public IntPtr hwndMenuOwner;
            public IntPtr hwndMoveSize;
            public IntPtr hwndCaret;
            public RECT rcCaret;
        };

        Point startPosition = new Point();       // Point required for ToolTip movement by Mouse
        GUITHREADINFO guiInfo;                     // To store GUI Thread Information
        Point caretPosition;                     // To store Caret Position  


        # endregion

        # region DllImports


        /*- Retrieves Title Information of the specified window -*/
        [DllImport("user32.dll")]
        static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        /*- Retrieves Id of the thread that created the specified window -*/
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, out uint lpdwProcessId);

        /*- Retrieves information about active window or any specific GUI thread -*/
        [DllImport("user32.dll", EntryPoint = "GetGUIThreadInfo")]
        public static extern bool GetGUIThreadInfo(uint tId, out GUITHREADINFO threadInfo);

        /*- Retrieves Handle to the ForeGroundWindow -*/
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /*- Converts window specific point to screen specific -*/
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, out Point position);

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();

        //include FindWindowEx
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent,
            IntPtr hwndChildAfter,
            string lpszClass,
            string lpszWindow);

        //include SendMessage
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);
        //public static extern int SendMessageW(IntPtr hWnd, int uMsg, int wParam, string lParam);
        public static extern int SendMessageW(IntPtr hWnd, int uMsg, int wParam, string lParam);

        # endregion

        #region Event Handlers
        string activeProcessName = "";
        Process activeProcess = null;

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Stop();
            // If Tooltip window is active window (Suppose user clicks on the Tooltip Window)
            if (GetForegroundWindow() == this.Handle)
            {
                // then do no processing
                return;
            }
            // Get Current active Process
            activeProcessName = GetActiveProcessName();
            activeProcess = GetActiveProcess();
            //activeProcessName = activeProcess.ProcessName;

            // If window explorer is active window (eg. user has opened any drive)
            // Or for any failure when activeProcess is nothing               
            if (
                (activeProcessName.ToLower().Contains("explorer")
                | activeProcessName.ToLower().Contains("devenv")
                | activeProcessName.ToLower().Contains("tace16tamilkeyboard")
                | (activeProcessName == string.Empty)))
            {
                // Dissappear Tooltip
                this.Visible = false;
            }
            else
            {


                // Otherwise Calculate Caret position
                EvaluateCaretPosition();

                // Adjust ToolTip according to the Caret
                AdjustUI();

                // Display current active Process on Tooltip
                lblCurrentApp.Text = " You are Currently inside : " + activeProcessName;
                this.Visible = true;

                //Got focus on the form
                //ActivateTargetApplication(this.Handle, "");
                //txtUserGivenWord.Focus();
            }
            //timer1.Start();
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            // Set the Mouse Cursor
            this.Cursor = Cursors.SizeAll;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            // If Left Button was pressed
            if (e.Button == MouseButtons.Left)
            {
                // then move the Tooltip
                this.Left += e.Location.X - startPosition.X;
                this.Top += e.Location.Y - startPosition.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            // Store start position of mouse when clicked down.
            // It will be used to calculate offset during movement.
            startPosition = e.Location;
        }


        #endregion

        #region Methods



        /// <summary>
        /// This function will adjust Tooltip position and
        /// will keep it always inside the screen area.
        /// </summary>
        private void AdjustUI()
        {
            // Get Current Screen Resolution
            Rectangle workingArea = SystemInformation.WorkingArea;

            // If current caret position throws Tooltip outside of screen area
            // then do some UI adjustment.
            if (caretPosition.X + this.Width > workingArea.Width)
            {
                caretPosition.X = caretPosition.X - this.Width - 50;
            }

            if (caretPosition.Y + this.Height > workingArea.Height)
            {
                caretPosition.Y = caretPosition.Y - this.Height - 50;
            }

            //this.Left = caretPosition.X;
            //this.Top = caretPosition.Y;
        }

        /// <summary>
        /// Evaluates Cursor Position with respect to client screen.
        /// </summary>
        private void EvaluateCaretPosition()
        {
            caretPosition = new Point();

            // Fetch GUITHREADINFO
            GetCaretPosition();

            caretPosition.X = (int)guiInfo.rcCaret.Left + 25;
            caretPosition.Y = (int)guiInfo.rcCaret.Bottom + 25;

            ClientToScreen(guiInfo.hwndCaret, out caretPosition);

            txtCaretX.Text = (caretPosition.X).ToString();
            txtCaretY.Text = caretPosition.Y.ToString();

        }

        /// <summary>
        /// Get the caret position
        /// </summary>
        public void GetCaretPosition()
        {
            guiInfo = new GUITHREADINFO();
            guiInfo.cbSize = (uint)Marshal.SizeOf(guiInfo);

            // Get GuiThreadInfo into guiInfo
            GetGUIThreadInfo(0, out guiInfo);
        }

        /// <summary>
        /// Retrieves name of active Process.
        /// </summary>
        /// <returns>Active Process Name</returns>
        private string GetActiveProcessName()
        {
            const int nChars = 256;
            int handle = 0;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = (int)GetForegroundWindow();

            // If Active window has some title info
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                uint lpdwProcessId;
                uint dwCaretID = GetWindowThreadProcessId(handle, out lpdwProcessId);
                uint dwCurrentID = (uint)Thread.CurrentThread.ManagedThreadId;
                return Process.GetProcessById((int)lpdwProcessId).ProcessName;
            }
            // Otherwise either error or non client region
            return String.Empty;
        }

        private string GetActiveProcessName(IntPtr handle)
        {
            const int nChars = 256;
            //int handle = 0;
            StringBuilder Buff = new StringBuilder(nChars);
            //handle = (int)GetForegroundWindow();

            // If Active window has some title info
            if (GetWindowText((int)handle, Buff, nChars) > 0)
            {
                uint lpdwProcessId;
                uint dwCaretID = GetWindowThreadProcessId((int)handle, out lpdwProcessId);
                uint dwCurrentID = (uint)Thread.CurrentThread.ManagedThreadId;
                return Process.GetProcessById((int)lpdwProcessId).ProcessName;
            }
            // Otherwise either error or non client region
            return String.Empty;
        }

        /// <summary>
        /// Retrieves name of active Process.
        /// </summary>
        /// <returns>Active Process Name</returns>
        private Process GetActiveProcess()
        {
            const int nChars = 256;
            int handle = 0;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = (int)GetForegroundWindow();

            // If Active window has some title info
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                uint lpdwProcessId;
                uint dwCaretID = GetWindowThreadProcessId(handle, out lpdwProcessId);
                uint dwCurrentID = (uint)Thread.CurrentThread.ManagedThreadId;
                return Process.GetProcessById((int)lpdwProcessId);//.ProcessName;
            }
            // Otherwise either error or non client region
            return new Process();
        }


        [DllImport("User32.dll")]
        public static extern int SetForegroundWindow(IntPtr point);

        public void ActivateTargetApplication(IntPtr h, string givenText)
        {
            //IntPtr h = this.Handle;
            //SetForegroundWindow(h);

            timer1.Stop();
            //IntPtr h = activeProcess.MainWindowHandle;
            SetForegroundWindow(h);
            Thread.Sleep(100);
            //this.TopMost = true;
            SendKeys.SendWait(givenText);
            timer1.Start();

            //ActivateTargetApplication(this.Handle, "");
            SetForegroundWindow(this.Handle);
            //this.TopMost = true;
            //this.txtUserGivenWord.Focus();
            if (givenText != "CaretPosition.Entity.ITRANS.IDictionaryTamil ")
            {
                objTamilWordNLP.saveUserDictionary(txtUserGivenWord.Text,
                    givenText);
            }
        }
        private void txtUserGivenWord_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtUserGivenWord.Text.Length > 50)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Space
                || e.KeyCode == Keys.Enter)
            {
                //Process p = Process
                //    .GetProcessesByName(activeProcess.ProcessName)
                //    .FirstOrDefault();
                //timer1.Stop();
                IDictionaryTamil givenText0 = getlstBoxSelectedItem();
                //string givenText = givenText0.taWordTace;
                if (activeProcess != null)
                {

                    if (givenText0 != null)
                    {
                        //ThreadProc("");
                        ActivateTargetApplication(activeProcess.MainWindowHandle,
                            givenText0);
                    }
                    //else
                    //{
                    //    //if (objTamilWordNLP.taEncoding == TaEncodingE.TACE)
                    //    //{
                    //    ActivateTargetApplication(activeProcess.MainWindowHandle,
                    //            getlstBoxSelectedItemV2());
                    //    //}
                    //    //else
                    //    //{
                    //    //    ActivateTargetApplication(activeProcess.MainWindowHandle,
                    //    //                givenText0.taWordUnicode);
                    //    //}
                    //}
                }
                //timer1.Start();
                txtUserGivenWord.Text = "";
                lstBoxSuggestedWords.Items.Clear();

            }
            else if (e.KeyCode == Keys.Down)
            {
                int listLength = lstBoxSuggestedWords.Items.Count;
                if (listLength > 0)
                {
                    int SelectedIndex = lstBoxSuggestedWords.SelectedIndex + 1;
                    int maxIndex = lstBoxSuggestedWords.Items.Count - 1;
                    if (SelectedIndex > maxIndex)
                    {
                        SelectedIndex = maxIndex;
                    }

                    lstBoxSuggestedWords.SelectedIndex = SelectedIndex;

                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                int listLength = lstBoxSuggestedWords.Items.Count;
                if (listLength > 0)
                {
                    int SelectedIndex = lstBoxSuggestedWords.SelectedIndex - 1;
                    int maxIndex = 0;// lstBoxSuggestedWords.Items.Count - 1;
                    if (SelectedIndex < maxIndex)
                    {
                        SelectedIndex = maxIndex;
                    }

                    lstBoxSuggestedWords.SelectedIndex = SelectedIndex;

                }
            }
            else
            {
                appendSuggestedWords(txtUserGivenWord.Text);
            }

        }

        public void ActivateTargetApplication(IntPtr h, IDictionaryTamil givenText)
        {
            //IntPtr h = this.Handle;
            //SetForegroundWindow(h);

            timer1.Stop();
            //IntPtr h = activeProcess.MainWindowHandle;
            SetForegroundWindow(h);
            Thread.Sleep(100);
            //this.TopMost = true;
            SendKeys.SendWait(givenText.taWordTace);
            timer1.Start();

            //ActivateTargetApplication(this.Handle, "");
            SetForegroundWindow(this.Handle);
            //this.TopMost = true;
            //this.txtUserGivenWord.Focus();
            if (givenText.taWordTace != null
                && givenText.engWord == null
                && givenText.taWordUnicode == null)
            {
                objTamilWordNLP.saveUserDictionary(txtUserGivenWord.Text,
                    givenText.taWordTace);
            }
        }

        private void ThreadProc(object statInfo)
        {

            //ActivateTargetApplication(activeProcess.ProcessName);                    
            //ThreadPool.QueueUserWorkItem(ThreadProc);


        }

        private void appendSuggestedWords(string userTypedWord)
        {
            lstBoxSuggestedWords.Items.Clear();
            lookUpWords.Clear();
            lookUpWords = objTamilWordNLP.loadEnglishTranslitDict("Assets/data/user_dictionary.txt");


            if (objTamilWordNLP.taEncoding == TaEncodingE.TACE)
            {
                //lstBoxSuggestedWords.Items.AddRange(
                //lookUpWords
                //.Where(
                //    f => f.engWord.StartsWith(txtUserGivenWord.Text)
                //    )
                //    .Take(5)
                //    .Select(
                //    s => s.taWordTace
                //    )                    
                //    .ToArray()
                //    );

                lstBoxSuggestedWords.Items.AddRange(
                lookUpWords
                .Where(
                    f => f.engWord.StartsWith(txtUserGivenWord.Text)
                    )
                    .Take(5)
                    .ToArray()
                    );

                lstBoxSuggestedWords.DisplayMember = "IDictionaryTamil.taWordTace";
            }
            else
            {
                //lstBoxSuggestedWords.Items.AddRange(
                //   lookUpWords.FindAll(
                //       f => f.engWord.StartsWith(txtUserGivenWord.Text)
                //       )
                //       .Select(
                //       s => s.taWordUnicode
                //       )
                //       .Take(5)
                //       .ToArray()
                //       );

                lstBoxSuggestedWords.Items.AddRange(
                lookUpWords
                .Where(
                    f => f.engWord.StartsWith(txtUserGivenWord.Text)
                    )
                    .Take(5)
                    .ToArray()
                    );

                lstBoxSuggestedWords.DisplayMember = "IDictionaryTamil.taWordUnicode";
            }

            //lstBoxSuggestedWords
            //    .Items
            //    .Add(objTamilWordNLP
            //    .getEnglishTransliterationByUSerDict(
            //    txtUserGivenWord.Text)
            //    );

            lstBoxSuggestedWords
                .Items
                .Add(objTamilWordNLP
                .getEnglishTransliteration(
                txtUserGivenWord.Text)
                );

            lstBoxSuggestedWords.Items.Add(txtUserGivenWord.Text);

            lstBoxSuggestedWords.SelectedIndex = 0;

        }

        //private void appendSuggestedWords(string userTypedWord)
        //{
        //    lstBoxSuggestedWords.Items.Clear();

        //    lstBoxSuggestedWords.Items.AddRange(
        //    lookUpWords.FindAll(
        //        f => f.engWord.StartsWith(txtUserGivenWord.Text)
        //        )
        //        .Select(
        //        s => s.taWordUnicode
        //        )
        //        .Take(5)
        //        .ToArray()
        //        );

        //    lstBoxSuggestedWords.Items.Add(txtUserGivenWord.Text);

        //    lstBoxSuggestedWords.SelectedIndex = 0;

        //}
        private void SuggestTheWords(KeyEventArgs e)
        {
            currentTextWord.Append(txtUserGivenWord.Text);
            //if (lstBoxSuggestedWords.Items.Count > 0)
            //{
            //    lstBoxSuggestedWords.Items.Clear();
            //}
            if (e.KeyCode == Keys.Space)
            {
                //sendText2Application(activeProcessName, getlstBoxSelectedItem());
                sendText2ApplicationV3(activeProcess, getlstBoxSelectedItem().taWordTace);
                lstBoxSuggestedWords.Items.Clear();
            }
            else
            {
                //setListedWordsV2(currentTextWord.ToString());
                setListedWordsV2(txtUserGivenWord.Text);
            }

            if (lstBoxSuggestedWords.Items.Count > 0)
            {
                lstBoxSuggestedWords.SelectedIndex = lstBoxSuggestedWords.Items.Count - 1;
            }

            //currentTextWord = new StringBuilder();
        }



        private IDictionaryTamil getlstBoxSelectedItem()
        {
            string currentword = "";
            if (lstBoxSuggestedWords.Items.Count > 0)
            {
                //currentword = lstBoxSuggestedWords.SelectedItem.ToString() + " ";
                if (lstBoxSuggestedWords.SelectedItem.GetType() == typeof(IDictionaryTamil))
                {
                    return (IDictionaryTamil)lstBoxSuggestedWords.SelectedItem;
                }
                else
                {
                    return new IDictionaryTamil()
                    {
                        taWordTace = lstBoxSuggestedWords.SelectedItem.ToString()
                    };
                }
            }
            return null;
        }

        //private string getlstBoxSelectedItemV2()
        //{
        //    string currentword = "";
        //    if (lstBoxSuggestedWords.Items.Count > 0)
        //    {
        //        currentword = lstBoxSuggestedWords.SelectedItem.ToString() + " ";
        //        //return (IDictionaryTamil)lstBoxSuggestedWords.SelectedItem;

        //    }
        //    return currentword;
        //}

        private void sendText2ApplicationV3
            (Process activeProcess,
            string textWord2Notepad)
        {

            IntPtr handle = activeProcess.Handle;
            //Process[] notepads = Process.GetProcessesByName("notepad");
            Process[] notepads = Process.GetProcessesByName(activeProcess.ProcessName);
            if (notepads.Length == 0) return;
            if (notepads[0] != null)
            {
                IntPtr child = FindWindowEx(notepads[0].MainWindowHandle,
                    new IntPtr(0),
                    "Edit", null);

                sbWholeWords.Append(textWord2Notepad + " ");

                //SendMessage(child, 0x000C, 0, sbWholeWords.ToString());
                SendMessageW(child, 0x000C, 0, sbWholeWords.ToString());
                setListedWordsV2(textWord2Notepad);


                txtUserGivenWord.Text = "";
                //lstBoxSuggestedWords.Items.Clear();
            }


            //Process[] 
            notepads = Process.GetProcessesByName("notepad++");
            if (notepads.Length == 0) return;
            if (notepads[0] != null)
            {
                IntPtr child = FindWindowEx(notepads[0].MainWindowHandle,
                    new IntPtr(0),
                    "Scintilla",
                    null);
                SendMessageW(child, 0x000C, 0, sbWholeWords.ToString());
            }
        }

        private void sendText2Application(string applicationName,
            string textWord2Notepad)
        {
            IntPtr handle = GetActiveWindow();
            //Process[] notepads = Process.GetProcessesByName("notepad");
            Process[] notepads = Process.GetProcessesByName(applicationName);
            if (notepads.Length == 0) return;
            if (notepads[0] != null)
            {
                IntPtr child = FindWindowEx(notepads[0].MainWindowHandle,
                    new IntPtr(0),
                    "Edit", null);

                sbWholeWords.Append(textWord2Notepad + " ");

                //SendMessage(child, 0x000C, 0, sbWholeWords.ToString());
                SendMessageW(child, 0x000C, 0, sbWholeWords.ToString());
                setListedWordsV2(textWord2Notepad);


                txtUserGivenWord.Text = "";
                //lstBoxSuggestedWords.Items.Clear();
            }


            //Process[] 
            notepads = Process.GetProcessesByName("notepad++");
            if (notepads.Length == 0) return;
            if (notepads[0] != null)
            {
                IntPtr child = FindWindowEx(notepads[0].MainWindowHandle,
                    new IntPtr(0),
                    "Scintilla",
                    null);
                SendMessageW(child, 0x000C, 0, sbWholeWords.ToString());
            }
        }

        public void setListedWordsV2(string givenCharacter)
        {
            //if (givenCharacter == Keys.Enter.ToString())
            //{

            //}
            if (givenCharacter == Keys.Tab.ToString()
                || givenCharacter == Keys.Space.ToString()
                || givenCharacter == Keys.Enter.ToString())
            {
                sb = new StringBuilder();
                lstBoxSuggestedWords.Items.Clear();
            }
            else
            {
                sb = new StringBuilder();
                sb.Append(givenCharacter);
                lstBoxSuggestedWords.Items.AddRange(objTamilWordNLP.getSuggestedWordslistV2(sb).ToArray());
            }
        }


        public void setListedWords(string givenCharacter)
        {

            if (givenCharacter == Keys.Enter.ToString())
            {

            }
            if (givenCharacter == Keys.Tab.ToString()
                || givenCharacter == Keys.Space.ToString()
                | givenCharacter == Keys.Enter.ToString())
            {
                sb = new StringBuilder();
                lstBoxSuggestedWords.Items.Clear();
            }
            else
            {
                sb.Append(givenCharacter);
                lstBoxSuggestedWords.Items.AddRange(objTamilWordNLP.getSuggestedWordslistV2(sb).ToArray());
            }

        }

        TamilWordNLP objTamilWordNLP = new TamilWordNLP();

        #endregion

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmAboutBox objAboutBox = new frmAboutBox())
            {
                timer1.Stop();
                objAboutBox.TopMost = true;
                objAboutBox.ShowDialog();
                timer1.Start();
            }
        }

        private void tamilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Start();
            lblLang.Text = "Tamil";
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            lblLang.Text = "English";
        }

        private void taceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objTamilWordNLP.taEncoding = TaEncodingE.TACE;
            displayEncoding();
        }

        private void unicodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objTamilWordNLP.taEncoding = TaEncodingE.UTF;
            displayEncoding();
        }

        private void frmTooltip_Load(object sender, EventArgs e)
        {
            //timer1.Stop();
            objTamilWordNLP.taEncoding = TaEncodingE.TACE;
            displayEncoding();

            int x = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Location = new Point(x, y);

            //lookUpWords = objTamilWordNLP.loadEnglishTranslitDict("Assets/data/lookups_dictionary.txt");
            //lookUpWords = objTamilWordNLP.loadEnglishTranslitDict("Assets/data/segment_mappings.txt");
            //            lookUpWords = objTamilWordNLP.loadEnglishTranslitDict("Assets/data/user_dictionary.txt");
            //lookUpWords = objTamilWordNLP.loadEnglishTranslitDict("Assets/data/lookups_dictionary.v3.txt");
        }
        List<IDictionaryTamil> lookUpWords = new List<IDictionaryTamil>();
        private void displayEncoding()
        {
            lblEncoding.Text = objTamilWordNLP.taEncoding.ToString();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void languageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txtUserGivenWord_Enter(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void txtUserGivenWord_Leave(object sender, EventArgs e)
        {

        }
    }
}
