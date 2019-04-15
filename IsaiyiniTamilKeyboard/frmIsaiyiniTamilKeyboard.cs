
//////////////////////////////////////////////////////////////////////////
//                                                                      //
//  Anybody can Use, Modify, Redistribute this code freely. If this     // 
//  module has been helpful to you then just leave a comment on Website //
//                                                                      //
//////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

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


        //include FindWindowEx
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        //include SendMessage
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        # endregion

        #region Event Handlers


        private void timer1_Tick(object sender, EventArgs e)
        {
            // If Tooltip window is active window (Suppose user clicks on the Tooltip Window)
            if (GetForegroundWindow() == this.Handle)
            {
                // then do no processing
                return;
            }

            // Get Current active Process
            string activeProcess = GetActiveProcess();

            // If window explorer is active window (eg. user has opened any drive)
            // Or for any failure when activeProcess is nothing               
            if (
                (activeProcess.ToLower().Contains("explorer")
                | activeProcess.ToLower().Contains("devenv")
                | activeProcess.ToLower().Contains("tace16tamilkeyboard")
                | (activeProcess == string.Empty)))
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
                lblCurrentApp.Text = " You are Currently inside : " + activeProcess;
                this.Visible = true;

                //Got focus on the form
                ActivateTargetApplication(this.Name);
            }
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

            this.Left = caretPosition.X;
            this.Top = caretPosition.Y;
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
        private string GetActiveProcess()
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

        [DllImport("User32.dll")]
        public static extern int SetForegroundWindow(IntPtr point);

        public void ActivateTargetApplication(string processName)
        {
            Process[] prco = Process.GetProcessesByName(processName);

            //Process p = Process.Start(this.Name);
            //p.WaitForInputIdle();
            IntPtr h = this.Handle;
            SetForegroundWindow(h);
            ////SendKeys.SendWait("");
            //IntPtr processFoundWindow = p.MainWindowHandle;
            txtUserGivenWord.Focus();
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            currentTextWord.Append(txtUserGivenWord.Text);
            //if (lstBoxSuggestedWords.Items.Count > 0)
            //{
            //    lstBoxSuggestedWords.Items.Clear();
            //}
            if (e.KeyCode == Keys.Space)
            {
                sendText2Notepad(getlstBoxSelectedItem());
                lstBoxSuggestedWords.Items.Clear();
            }
            else
            {
                setListedWordsV2(currentTextWord.ToString());
            }

            if (lstBoxSuggestedWords.Items.Count > 0)
            {
                lstBoxSuggestedWords.SelectedIndex = lstBoxSuggestedWords.Items.Count - 1;
            }

            currentTextWord = new StringBuilder();
        }

        private string getlstBoxSelectedItem()
        {
            string currentword = "";
            if (lstBoxSuggestedWords.Items.Count > 0)
            {
                currentword = lstBoxSuggestedWords.SelectedItem.ToString();
            }
            return currentword;
        }

        private void sendText2Notepad(string textWord2Notepad)
        {

            Process[] notepads = Process.GetProcessesByName("notepad");
            if (notepads.Length == 0) return;
            if (notepads[0] != null)
            {
                IntPtr child = FindWindowEx(notepads[0].MainWindowHandle, new IntPtr(0), "Edit", null);
                
                sbWholeWords.Append(textWord2Notepad + " ");

                SendMessage(child, 0x000C, 0, sbWholeWords.ToString());
                setListedWordsV2(textWord2Notepad);
                

                txtUserGivenWord.Text = "";
                //lstBoxSuggestedWords.Items.Clear();
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
                lstBoxSuggestedWords.Items.AddRange(objTamilWordNLP.getSuggestedWordslist(sb));
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
                lstBoxSuggestedWords.Items.AddRange(objTamilWordNLP.getSuggestedWordslist(sb));
            }

        }

        TamilWordNLP objTamilWordNLP = new TamilWordNLP();

        #endregion

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox objAboutBox = new AboutBox())
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
            objTamilWordNLP.TaEncoding = TaEncoding.TACE;
            displayEncoding();
        }

        private void unicodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objTamilWordNLP.TaEncoding = TaEncoding.UTF;
            displayEncoding();
        }

        private void frmTooltip_Load(object sender, EventArgs e)
        {
            objTamilWordNLP.TaEncoding = TaEncoding.TACE;
            displayEncoding();
        }

        private void displayEncoding()
        {
            label1.Text = objTamilWordNLP.TaEncoding.ToString();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


    }
}
