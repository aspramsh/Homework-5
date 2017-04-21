using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Logging
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// A queue for keeping logs
        /// </summary>
        public static Queue<Log> Logs;
        /// <summary>
        /// A filter instance
        /// </summary>
        public static Filter filter;
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// A method to get IPs
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
        /// <summary>
        /// A button that creates a log in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logInButton_Click(object sender, EventArgs e)
        {
            // A list that keeps textboxes for manipulating user input data
            List<TextBox> texts = new List<TextBox>()
            { textBox1, textBox2, textBox3, textBox4, textBox3, textBox2 };
            foreach (TextBox textbox in texts)
            {
                if (textbox.Text == "")
                {
                    logButton.Enabled = false;
                }
            }
            if (logButton.Enabled == true)
            {
                Logs = new Queue<Log>();
                // An instance of a log that is initialized from UI
                Log log = new Log();
                log.Username = textBox1.Text;
                log.Password = textBox2.Text;
                log.Severity = textBox3.Text;
                log.LogTime = DateTime.Now;
                log.SourceIP = GetLocalIPAddress();
                log.Message = textBox4.Text;
                Logs.Enqueue(log);
                foreach (TextBox textbox in texts)
                {
                    textbox.Text = "";
                }
                Logger.Reset();
                Logger.AddLog();
            }
            else
            {
                MessageBox.Show("Please fill all fields.");
                logButton.Enabled = true;
            }
        }
        /// <summary>
        /// A start button that initializes filters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void enterDataButton_Click(object sender, EventArgs e)
        {
            filter = new Filter();
            filter.InitializeFilters();
            panel1.Enabled = true;
        }
        /// <summary>
        /// A button that gets filtered logs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queryButton_Click(object sender, EventArgs e)
        {
            QueryGenerator.GenerateConditionQuery();
            dataGridView1.DataSource = Logger.GetLog();
        }
        /// <summary>
        /// A button for creating filters' lists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateFilterButton_Click(object sender, EventArgs e)
        {
            string[] values = richTextBox1.Lines;
            filter.StartDate = dateTimePicker1.Value;
            filter.EndDate = dateTimePicker2.Value;
            string caseSwitch = comboBox1.Text;
            switch (caseSwitch)
            {
                case "Username":
                    foreach (string line in values)
                    {
                        filter.Usernames.Add(line);
                    }
                    break;
                case "Password":
                    foreach (string line in values)
                        filter.Passwords.Add(line);
                    break;
                case "Severity":
                    foreach (string line in values)
                        filter.Severities.Add(line);
                    break;
                case "IP Address":
                    foreach (string line in values)
                        filter.SourceIps.Add(line);
                    break;
                case "Message":
                    foreach (string line in values)
                        filter.Messages.Add(line);
                    break;
                case "ID":
                    foreach (string line in values)
                        filter.IDs.Add(Int32.Parse(line));
                    break;
            }
            richTextBox1.Lines = new string[0];
        }
        /// <summary>
        /// A button that ensures thread safety of the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void attackButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; ++i)
            {
                Thread thread = new Thread(() =>
                {
                    Log log = new Log("jane", "2425", "error", DateTime.Now,
                        "192.168.1.7", "Hi");
                    Logs = new Queue<Log>();
                    Logs.Enqueue(log);
                    Logger.AddLog();
                });
                thread.Start();
            }
        }
    }
}
