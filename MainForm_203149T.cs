using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Media;
using System.Text.RegularExpressions;

namespace Calculator_203149T
{

    public partial class MainForm_203149T : Form
    {
        SpeechSynthesizer syn = new SpeechSynthesizer();
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        public MainForm_203149T()
        {
            InitializeComponent();
            // removes IBeam Cursor when txtResults(textbox) gets focus
            txtResults.GotFocus += new EventHandler(txtResults_getFocus);
            txtResults.ReadOnly = true;
            txtResults.GotFocus += txtResults_getFocus;
            txtResults.Cursor = Cursors.Arrow;
        }

        private void txtResults_getFocus(object sender, EventArgs e)
        {
            // removes highlighted selection to maintain design
            txtResults.SelectionStart = 0;
            txtResults.SelectionLength = 0;
            HideCaret(txtResults.Handle);
        }

        private void lblID_Click(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            Clipboard.SetText(attribute.Value.ToString());
        }
        private void MainForm_203149T_Load(object sender, EventArgs e)
        { // richtextbox text positioning
            txtEquation.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            string temp = txtResults.Text;
            if (temp == "0")
                temp = "";
            temp += '0';
            txtResults.Text = temp;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            string temp = txtResults.Text;
            if (temp == "0")
                temp = "";
            temp += '1';
            txtResults.Text = temp;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            string temp = txtResults.Text;
            if (temp == "0")
                temp = "";
            temp += '2';
            txtResults.Text = temp;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            string temp = txtResults.Text;
            if (temp == "0")
                temp = "";
            temp += '3';
            txtResults.Text = temp;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            string temp = txtResults.Text;
            if (temp == "0")
                temp = "";
            temp += '4';
            txtResults.Text = temp;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            string temp = txtResults.Text;
            if (temp == "0")
                temp = "";
            temp += '5';
            txtResults.Text = temp;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            string temp = txtResults.Text;
            if (temp == "0")
                temp = "";
            temp += '6';
            txtResults.Text = temp;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            string temp = txtResults.Text;
            if (temp == "0")
                temp = "";
            temp += '7';
            txtResults.Text = temp;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            string temp = txtResults.Text;
            if (temp == "0")
                temp = "";
            temp += '8';
            txtResults.Text = temp;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            string temp = txtResults.Text;
            if (temp == "0")
                temp = "";
            temp += '9';
            txtResults.Text = temp;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            string temp = txtResults.Text;
            if (!temp.Contains('.'))
            {
                temp += '.';
                txtResults.Text = temp;
            }
        }

        private void numPad_Click(object sender, EventArgs e)
        {
            if (calClear == 1)
            {
                txtEquation.Text = "";
                txtEquation2.Text = "";
                txtResults.Text = "";
                calClear = 0;
            }
            Button btn = (Button)sender;
            string num = btn.Text;
            string temp = txtResults.Text;
            // ensure input fits in screen
            if (txtResults.Text.Length > 14)
                temp = temp.Remove(txtResults.Text.Length - 1, 1);
            //clear display if operator is pressed
            if (flagOpPressed == true)
            {
                temp = "";
                flagOpPressed = false;
            }
            switch (num)
            {
                case ".":
                    if (!temp.Contains('.'))
                    {
                        temp += '.';
                    }
                    break;
                default:
                    if (temp == "0")
                        temp = "";
                    temp += num;
                    break;
            }
            // Adds comma to numeral input by using <Math> (1,000 etc.)
            double addComma = 0;
            if (Regex.IsMatch(txtResults.Text, @"^[0-9.,-]*$"))
            {
                int dotFreq = Regex.Matches(txtResults.Text, "[.]").Count;
                int dashFreq = Regex.Matches(txtResults.Text, "[-]").Count;
                if (dotFreq <= 1 && dashFreq <= 1)
                {
                    addComma = double.Parse(temp);
                }
                else
                {
                    btnCE.PerformClick();
                }
            }
            else
            {
                btnCE.PerformClick();
            }
            txtResults.Text = Math.Pow(addComma, 1).ToString("N10");
            txtResults.Text = txtResults.Text.TrimEnd('0').TrimEnd('.');
            txtResults.Focus();
        }

        int index = 0;
        int calClear = 0;
        string opr = "";
        string oprSign = "";
        double operand = 0;
        double operand2 = 0;
        private void btnEqu_Click(object sender, EventArgs e)
        {
            // Regex to verify (min 0, max 1 dot) + digits from pasted results
            if (Regex.IsMatch(txtResults.Text, @"^[0-9.,-]*$"))
            {
                int dotFreq = Regex.Matches(txtResults.Text, "[.]").Count;
                int dashFreq = Regex.Matches(txtResults.Text, "[-]").Count;
                if (dotFreq <= 1 && dashFreq <= 1)
                {
                    // operand2 = 2nd numeral input before equal sign
                    operand2 = double.Parse(txtResults.Text);
                }
                else
                {
                    btnCE.PerformClick();
                }
            }
            else
            {
                btnCE.PerformClick();
            }
            switch (opr)
            {
                case "Add":
                    operand = storedValue + operand2;
                    txtResults.Text = operand.ToString("N10").TrimEnd('0').TrimEnd('.');
                    txtEquation.Text = (operand - operand2).ToString() + "+" + operand2 + "=";
                    txtEquation2.Text += operand2;
                    index = txtEquation.Text.IndexOf("+");
                    break;
                case "Sub":
                    operand = storedValue - operand2;
                    txtResults.Text = operand.ToString("N10").TrimEnd('0').TrimEnd('.');
                    txtEquation.Text = (operand + operand2).ToString() + "-" + operand2 + "=";
                    txtEquation2.Text += operand2;
                    index = txtEquation.Text.IndexOf("-", 1);
                    break;
                case "Div":
                    if (operand2 == 0)
                    {
                        txtResults.Text = " undefined";
                        txtEquation.Text = storedValue.ToString() + "÷" + "0" + "=";
                        txtEquation2.Text += "0";
                    }
                    else
                    {
                        operand = storedValue / operand2;
                        txtResults.Text = operand.ToString("N10").TrimEnd('0').TrimEnd('.');
                        txtEquation.Text = (operand * operand2).ToString() + "÷" + operand2 + "=";
                        txtEquation2.Text += operand2;
                        index = txtEquation.Text.IndexOf("÷");
                    }
                    break;
                case "Mul":
                    if (operand2 == 0)
                    {
                        txtResults.Text = "0";
                        txtEquation.Text = storedValue.ToString() + "×" + "0" + "=";
                        txtEquation2.Text += "0";
                    }
                    else
                    {
                        operand = storedValue * operand2;
                        txtResults.Text = operand.ToString("N10").TrimEnd('0').TrimEnd('.');
                        txtEquation.Text = (operand / operand2).ToString() + "×" + operand2 + "=";
                        txtEquation2.Text += operand2;
                        index = txtEquation.Text.IndexOf("×");
                    }
                    break;
                default:
                    break;
            }
            txtEquation.Select(index, 1);
            txtEquation.SelectionColor = Color.LightSalmon;
            histView.Items.Add(txtEquation.Text.ToString() + txtResults.Text.ToString());
            if (syn.State.ToString() != "Paused")
            {
                if (txtResults.Text.Contains("-"))
                {
                    syn.Speak("The answer is negative" + txtResults.Text);
                }
                else
                {
                    syn.Speak("The answer is" + txtResults.Text);
                }
            }
            opr = "";
            oprSign = "";
            calClear = 1;
        }

        bool flagOpPressed = false;
        double storedValue = 0;
        private void operator_Click(object sender, EventArgs e)
        {
            if (calClear == 1)
            {
                calClear = 0;
            }
            if (opr == "")
            {
                txtEquation2.Text = "";
            }
            // Regex to detect (min 0, max 1 dot) + digits from pasted results
            if (Regex.IsMatch(txtResults.Text, @"^[0-9.,-]*$"))
            {
                int dotFreq = Regex.Matches(txtResults.Text, "[.]").Count;
                int dashFreq = Regex.Matches(txtResults.Text, "[-]").Count;
                if (dotFreq <= 1 && dashFreq <= 1)
                {
                    // operand = 1st numeral input before operator sign
                    operand = double.Parse(txtResults.Text);
                }
                else
                {
                    btnCE.PerformClick();
                }
            }
            else
            {
                btnCE.PerformClick();
            }
            if (opr == "Add")
            {
                storedValue += operand;
            }
            else if (opr == "Sub")
            {
                storedValue -= operand;
            }
            else if (opr == "Mul")
            {
                storedValue *= operand;
            }
            else if (opr == "Div")
            {
                storedValue /= operand;
            }
            else
            {
                storedValue = operand;
            }
            storedValue = double.Parse(storedValue.ToString("N10").TrimEnd('0').TrimEnd('.'));
            Button btn = (Button)sender;
            opr = btn.Tag.ToString();
            if (opr == "Add")
            {
                oprSign = "+";
                txtEquation2.Text += operand + oprSign;
            }
            else if (opr == "Sub")
            {
                oprSign = "-";
                txtEquation2.Text += operand + oprSign;
            }
            else if (opr == "Mul")
            {
                oprSign = "×";
                txtEquation2.Text = txtEquation2.Text.Insert(0, "(");
                txtEquation2.Text += operand + ")" + oprSign;
            }
            else if (opr == "Div")
            {
                oprSign = "÷";
                txtEquation2.Text = txtEquation2.Text.Insert(0, "(");
                txtEquation2.Text += operand + ")" + oprSign;
            }
            txtEquation.Text = storedValue.ToString() + oprSign;
            txtEquation.Select(txtEquation.TextLength - 1, 1);
            txtEquation.SelectionColor = Color.LightSalmon;
            flagOpPressed = true;
        }

        double value;
        double newvalue;
        private void u_operatorClick(object sender, EventArgs e)
        {
            if (opr == "")
            {
                txtEquation2.Text = "";
            }
            Button btn = (Button)sender;
            string u_opr = btn.Tag.ToString();
            if (Regex.IsMatch(txtResults.Text, @"^[0-9.,-]*$"))
            {
                int dotFreq = Regex.Matches(txtResults.Text, "[.]").Count;
                int dashFreq = Regex.Matches(txtResults.Text, "[-]").Count;
                if (dotFreq <= 1 && dashFreq <= 1)
                {
                    value = double.Parse(txtResults.Text);
                }
                else
                {
                    btnCE.PerformClick();
                }
            }
            else
            {
                btnCE.PerformClick();
            }
            string results;
            switch (u_opr)
            {
                case "Sqrt":
                    results = Math.Sqrt(value).ToString("N10");
                    txtResults.Text = results.TrimEnd('0').TrimEnd('.');
                    if (value < 0)
                    {
                        txtResults.Text = " not real number";
                    }
                    txtEquation.Text = "√" + "(" + value + ")";
                    txtEquation.Select(0, 2);
                    txtEquation.SelectionColor = Color.FromArgb(38, 230, 196);
                    break;
                case "Sqr":
                    results = Math.Pow(value, 2).ToString("N10");
                    txtResults.Text = results.TrimEnd('0').TrimEnd('.');
                    txtEquation.Text = "sqr" + "(" + value + ")";
                    txtEquation.Select(0, 4);
                    txtEquation.SelectionColor = Color.FromArgb(38, 230, 196);
                    break;
                case "Inv":
                    results = Math.Pow(value, -1).ToString("N10");
                    txtResults.Text = results.TrimEnd('0').TrimEnd('.');
                    if (value == 0)
                    {
                        txtResults.Text = " undefined";
                    }
                    txtEquation.Text = "1" + "/" + "(" + value + ")";
                    txtEquation.Select(0, 3);
                    txtEquation.SelectionColor = Color.FromArgb(38, 230, 196);
                    break;
                case "Sin":
                    if (angleUnit == 1)
                    {
                        newvalue = value * Math.PI / 180;
                    }
                    else
                    {
                        newvalue = value;
                    }
                    results = Math.Sin(newvalue).ToString("N10");
                    txtResults.Text = results.TrimEnd('0').TrimEnd('.');
                    txtEquation.Text = "sin" + "(" + value + ")";
                    txtEquation.Select(0, 4);
                    txtEquation.SelectionColor = Color.FromArgb(38, 230, 196);
                    break;
                case "Cos":
                    if (angleUnit == 1)
                    {
                        newvalue = value * Math.PI / 180;
                    }
                    else
                    {
                        newvalue = value;
                    }
                    results = Math.Cos(newvalue).ToString("N10");
                    txtResults.Text = results.TrimEnd('0').TrimEnd('.');
                    txtEquation.Text = "cos" + "(" + value + ")";
                    txtEquation.Select(0, 4);
                    txtEquation.SelectionColor = Color.FromArgb(38, 230, 196);
                    break;
                case "Tan":
                    if (angleUnit == 1)
                    {
                        newvalue = value * Math.PI / 180;
                        if (value == 90)
                        {
                            txtResults.Text = " undefined";
                        }
                        else
                        {
                            results = Math.Tan(newvalue).ToString("N10");
                            txtResults.Text = results.TrimEnd('0').TrimEnd('.');
                        }
                    }
                    else
                    {
                        newvalue = value;
                        results = Math.Tan(newvalue).ToString("N10");
                        txtResults.Text = results.TrimEnd('0').TrimEnd('.');
                    }
                    txtEquation.Text = "tan" + "(" + value + ")";
                    txtEquation.Select(0, 4);
                    txtEquation.SelectionColor = Color.FromArgb(38, 230, 196);
                    
                    break;
                case "Log":
                    results = Math.Log10(value).ToString("N10");
                    txtResults.Text = results.TrimEnd('0').TrimEnd('.');
                    if (value < 0)
                    {
                        txtResults.Text = " undefined";
                    }
                    txtEquation.Text = "log" + "(" + value + ")";
                    txtEquation.Select(0, 4);
                    txtEquation.SelectionColor = Color.FromArgb(38, 230, 196);
                    break;
                case "Nat":
                    results = Math.Log(value).ToString("N10");
                    txtResults.Text = results.TrimEnd('0').TrimEnd('.');
                    if (value < 0)
                    {
                        txtResults.Text = " undefined";
                    }
                    txtEquation.Text = "ln" + "(" + value + ")";
                    txtEquation.Select(0, 3);
                    txtEquation.SelectionColor = Color.FromArgb(38, 230, 196);
                    break;
                case "ePx":
                    results = Math.Pow(Math.E, value).ToString("N10");
                    txtResults.Text = results.TrimEnd('0').TrimEnd('.');
                    txtEquation.Text = "e" + "^" + "(" + value + ")";
                    txtEquation.Select(0, 3);
                    txtEquation.SelectionColor = Color.FromArgb(38, 230, 196);
                    break;
                case "10Px":
                    results = Math.Pow(10, value).ToString("N10");
                    txtResults.Text = results.TrimEnd('0').TrimEnd('.');
                    txtEquation.Text = "10" + "^" + "(" + value + ")";
                    txtEquation.Select(0, 4);
                    txtEquation.SelectionColor = Color.FromArgb(38, 230, 196);
                    break;
            }
            txtEquation.Select(txtEquation.Text.Length - 1, 1);
            txtEquation.SelectionColor = Color.FromArgb(38, 230, 196);
            histView.Items.Add(txtEquation.Text.ToString() + "=" + txtResults.Text.ToString());
            if (syn.State.ToString() != "Paused")
            {
                if (txtResults.Text.Contains("-"))
                {
                    syn.Speak("The answer is negative" + txtResults.Text);
                }
                else
                {
                    syn.Speak("The answer is" + txtResults.Text);
                }
            }
        }

        private void btnChangeSign_Click(object sender, EventArgs e)
        {
            // useful for calculating -6 * -6 etc.
            if (Regex.IsMatch(txtResults.Text, @"^[0-9.,-]*$"))
            {
                int dotFreq = Regex.Matches(txtResults.Text, "[.]").Count;
                int dashFreq = Regex.Matches(txtResults.Text, "[-]").Count;
                if (dotFreq <= 1 && dashFreq <= 1)
                {
                    // operand = 1st numeral input before operator sign
                    value = double.Parse(txtResults.Text);
                    value *= -1;
                    txtResults.Text = Math.Pow(value, 1).ToString("N10");
                    txtResults.Text = txtResults.Text.TrimEnd('0').TrimEnd('.');
                    label1.Focus();
                }
                else
                {
                    btnCE.PerformClick();
                }
            }
            else
            {
                btnCE.PerformClick();
            }
        }
        private void btnCE_Click(object sender, EventArgs e)
        {
            if (soundfx == 1)
            {
                SoundPlayer s = new SoundPlayer(@"C:\Users\nunez\source\repos\Calculator_203149Tv2\click_sound.wav");
                s.Play();
            }
            txtResults.Text = "0";
            flagOpPressed = false;
        }
        private void btnC_Click(object sender, EventArgs e)
        {
            if (soundfx == 1)
            {
                SoundPlayer s = new SoundPlayer(@"C:\Users\nunez\source\repos\Calculator_203149Tv2\click_sound.wav");
                s.Play();
            }
            opr = "";
            operand = 0;
            flagOpPressed = false;
            txtResults.Text = "0";
            txtEquation.Text = "";
            txtEquation2.Text = "";
        }
        private void btnMode_Click(object sender, EventArgs e)
        {
            if (SCIBotPanel.Visible == true)
            {
                SCITopPanel.Visible = false;
                SCIBotPanel.Visible = false;
                btnMode.Text = "STD";
                btnMode.ForeColor = Color.FromArgb(38, 230, 196);
            }
            else
            {
                SCITopPanel.Visible = true;
                SCIBotPanel.Visible = true;
                btnModePanel.Text = "SCI";
                btnModePanel.ForeColor = Color.LightSalmon;
            }
            label1.Focus();
        }

        private void btnHist_Click(object sender, EventArgs e)
        {
            if (histView.Visible == false)
            {
                histView.Visible = true;
                btnHistClear.Visible = true;
                this.Size = new Size(455, 415);
                btnHist.ForeColor = Color.FromArgb(38, 230, 196);
                btnHistPanel.ForeColor = Color.FromArgb(38, 230, 196);
            }
            else
            {
                this.Size = new Size(303, 415);
                histView.Visible = false;
                btnHistClear.Visible = false;
                btnHist.ForeColor = Color.LightSalmon;
                btnHistPanel.ForeColor = Color.LightSalmon;
            }
            label1.Focus();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtResults.Text != "")
            {
                Clipboard.SetText(txtResults.Text);
            }
            MessageBox.Show("Answer Copied to Clipboard");
            label1.Focus();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (soundfx == 1)
            {
                SoundPlayer s = new SoundPlayer(@"C:\Users\nunez\source\repos\Calculator_203149Tv2\click_sound.wav");
                s.Play();
            }
            if (txtResults.Text.Length > 0)
            {
                txtResults.Text = txtResults.Text.Remove(txtResults.Text.Length - 1);
            }
            if (txtResults.Text == "")
                txtResults.Text = "0";
            flagOpPressed = false;
            label1.Focus();
        }

        private void histView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (histView.SelectedItems.Count > 0)
            {
                int index = histView.SelectedItems[0].Index;
                btnHistCopy.Location = new Point(histView.Items[index].Position.X + 380, histView.Items[index].Position.Y + 6);
                btnHistDel.Location = new Point(histView.Items[index].Position.X + 380, histView.Items[index].Position.Y + 24);
                btnHistCopy.Visible = true;
                btnHistDel.Visible = true;
            }
            else
            {
                btnHistCopy.Visible = false;
                btnHistDel.Visible = false;
            }
        }

        private void btnHistClear_Click(object sender, EventArgs e)
        {
            // (execute before loop runs; defines condition of i; execute everytime during for
            // loop)
            for (int i = histView.Items.Count - 1; i >= 0; i --)
            {
                histView.Items[i].Remove();
            }
            label1.Focus();
        }

        string s = "";
        private void btnHistCopy_Click(object sender, EventArgs e)
        {
            if (histView.SelectedItems.Count > 0)
            {
                s = histView.SelectedItems[0].Text;
                if (!s.Contains('='))
                {
                    Clipboard.SetText(s);
                    btnHistCopy.Visible = false;
                    btnHistDel.Visible = false;
                }
                else
                {
                    char[] charsToTrim = { '=' }; // copies result
                    s = histView.SelectedItems[0].Text;
                    string result = s.Substring(s.IndexOf("="));
                    result = result.Trim(charsToTrim);
                    Clipboard.SetText(result);
                    btnHistCopy.Visible = false;
                    btnHistDel.Visible = false;
                }
            }
            label1.Focus();
        }

        private void btnHistDel_Click(object sender, EventArgs e)
        {
            if (histView.SelectedItems.Count > 0)
            {
                histView.SelectedItems[0].Remove();
            }
            label1.Focus();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            // retrieve previously calculated results
            if (histView.SelectedItems.Count > 0)
            {
                string s = histView.SelectedItems[0].Text;
                if (!s.Contains('='))
                {
                    txtEquation.Text = "";
                    txtEquation2.Text = "";
                    txtResults.Text = s;
                }
                else
                {
                    string answer = s.Substring(s.IndexOf("="));
                    txtEquation.Text = s.Substring(0, s.IndexOf("=")) + "=";
                    char[] charsToTrim = { '=' };
                    txtResults.Text = answer.Trim(charsToTrim);
                    txtEquation2.Text = "";
                }
            }
        }
        private void btnSpeak_Click(object sender, EventArgs e)
        {
            if (syn.State.ToString() == "Paused")
            {
                syn.Resume();
                btnSpeak.ForeColor = Color.FromArgb(38, 230, 196);
                btnSPKPanel.ForeColor = Color.FromArgb(38, 230, 196);
            }
            else
            {
                syn.Pause();
                btnSpeak.ForeColor = Color.LightSalmon;
                btnSPKPanel.ForeColor = Color.LightSalmon;
            }
            label1.Focus();
        }

        int soundfx = 1;
        private void btnSFX_Click(object sender, EventArgs e)
        {
            // Declared soundfx as an integer as a way to state whether sfx is On/Off
            if (btnSFX.ForeColor == Color.FromArgb(38, 230, 196))
            {
                soundfx = 0;
                btnSFX.ForeColor = Color.LightSalmon;
                btnSFXPanel.ForeColor = Color.LightSalmon;
            }
            else
            {
                soundfx = 1;
                btnSFX.ForeColor = Color.FromArgb(38, 230, 196);
                btnSFXPanel.ForeColor = Color.FromArgb(38, 230, 196);
            }
            label1.Focus();
        }

        private void Form_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar.ToString())
            {
                case "0":
                    btn0.PerformClick();
                    break;
                case "1":
                    btn1.PerformClick();
                    break;
                case "2":
                    btn2.PerformClick();
                    break;
                case "3":
                    btn3.PerformClick();
                    break;
                case "4":
                    btn4.PerformClick();
                    break;
                case "5":
                    btn5.PerformClick();
                    break;
                case "6":
                    btn6.PerformClick();
                    break;
                case "7":
                    btn7.PerformClick();
                    break;
                case "8":
                    btn8.PerformClick();
                    break;
                case "9":
                    btn9.PerformClick();
                    break;
                case ".":
                    btnDot.PerformClick();
                    break;
                case "+":
                    btnAdd.PerformClick();
                    break;
                case "-":
                    btnMinus.PerformClick();
                    break;
                case "*":
                    btnMultiply.PerformClick();
                    break;
                case "/":
                    btnDivide.PerformClick();
                    break;
                case "=":
                    btnEqu.PerformClick();
                    break;
                case " ": // Space Bar
                    btnEqu.PerformClick();
                    break;
                case "\r": // Enter key
                    btnEqu.PerformClick();
                    break;
                case "\b": // Backspace key
                    btnDel.PerformClick();
                    break;
                default:
                    break;
            }
        }

        int angleUnit = 1;
        private void btnAngUnit_Click(object sender, EventArgs e)
        {
            if (btnAngUnit.ForeColor == Color.FromArgb(38, 230, 196))
            {
                angleUnit = 0;  // unit indicator:  0 - radians, 1 - degrees 
                btnAngUnit.ForeColor = Color.LightSalmon;
                btnAngUnit.Text = "RAD";
            }
            else
            {
                angleUnit = 1;
                btnAngUnit.ForeColor = Color.FromArgb(38, 230, 196);
                btnAngUnit.Text = "DEG";
            }
            label1.Focus();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.V) // Ctrl V
            {
                if (Clipboard.ContainsText())
                {
                    txtResults.Text += Clipboard.GetText();
                }
            }
            else if (e.Control == true && e.KeyCode == Keys.C) // Ctrl C
            {
                if (txtResults.Text != "")
                {
                    Clipboard.SetText(txtResults.Text);
                }
            }
        }

        private void txtEquation2_TextChanged(object sender, EventArgs e)
        {
            txtEquation2.SelectionStart = txtEquation2.Text.Length;
            // scroll it automatically
            txtEquation2.ScrollToCaret();
        }
    }
}
