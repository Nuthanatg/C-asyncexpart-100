using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

//part 100
namespace AsynExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private int CountCharacters()
        {
            int count = 0;
            using(StreamReader reader = new StreamReader("d:\\Sample Files C#\\Data.txt"))
            {
                string content = reader.ReadToEnd();
                count = content.Length;
                Thread.Sleep(5000);
            }

            return count;
        }

        //here by clicking file button we have two problems
        //1.it will not display the status message processing file please wait and
        //2.the application also becomes unresponsive while its busy processing the file will not be able to move the windows form around and resize the form
        //once it has finished processing we can move it and resize
        //so below is the Blocking example code
        //let see how to make application responsive by using the async and await keyword below is the only method to change basically async is telling we can call this method asynchronously 
        //and then within the method we are going create a Task - as a unit of work to do ,Task class is present in a diffrent namespace System.Threading.Task
        int charactercount = 0;
        private void btnProcessFile_Click(object sender, EventArgs e)
        //private async void btnProcessFile_Click(object sender, EventArgs e)
        {
            //Task<int> task = new Task<int> (CountCharacters);
            //task.Start();
            //lblCount.Text = "Processing File. Please wait...";
            ////int count = CountCharacters();
            //int count = await task;
            //lblCount.Text = count.ToString() + " characters in file";

            //part-102 using THread
            
            Thread thread = new Thread(()=> 
            {
                charactercount = CountCharacters();
                //lblCount.Text += count.ToString() + " characters in file";
                Action action = () => lblCount.Text += charactercount.ToString() + " characters in file";
                this.BeginInvoke(action);
            });
            thread.Start();

            lblCount.Text = "Processing File. Please wait...";
            //int count = CountCharacters();
            //int count = await task;
            //lblCount.Text = count.ToString() + " characters in file"; // here this message is overriding the message not 
            //thread.Join();
            //lblCount.Text += count.ToString() + " characters in file"; //here clrt+x this line and paste inside thread
        }

        private void SetLabelTextProperty()
        {
            lblCount.Text += charactercount.ToString() + " characters in file";

        }
    }
}
