using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace Matching_Game
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        List<string> icons = new List<string>
        {
            //"!", "!", "N", "N", ",", ",", "k", "k",
            //"b", "b", "v", "v", "w", "w", "z", "z", 
          "A","A","B","B","b","b", "z", "z",
           "C","C","D","D","d","d","p","p",
           "E","E","e","e","F","F","f","f",
           "G","G","H","H","h","h","o","o",
           "I","I","i","i","J","J","j","j",
           "K","K","k","k","L","L","l","l",
           "M","M","m","m","N","N","O","O",
           "!","!",",",",","v","v","w","w",
        };

        Label firstClicked = null;
        Label secondClicked = null;

        int timeCount;

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquare();
        }

        public void AssignIconsToSquare()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);

                }
            }

            timer2.Enabled = true;
            timeCount = 0;
            elapsedTime.Text = timeCount + " seconds";
            timer2.Start();
        }

        /// <summary> 
        /// Every label's Click event is handled by this event handler 
        /// </summary> 
        /// <param name="sender">The label that was clicked</param>
        /// <param name="e"></param>
        private void label_click(object sender, EventArgs e)
        {
            //this will timer when user choice two non-matching terms. 
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                //A click will be ignored if it is already black. 
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // this will deal with the second click. 
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Check to see if the player won
                CheckForTheWinner();

               // if a macthis found. this will reset the cliked varables.  
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    SystemSounds.Beep.Play();
                    return;
                }

                // if the player picks two that are not correct
                //that it will wait 3/4 of a second the clear them. 
                timer1.Start();
            }
        }

        /// <summary> 
        /// if the player picks two that are not correct
        /// that it will wait 3/4 of a second the clear them. 
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //stop the timer
            timer1.Stop();

            //hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // resets so the next cliks can be recorded. 
            firstClicked = null;
            secondClicked = null;
        }

        /// <summary> 
        /// to check for a winner it will check if all are unhide. 
        /// </summary>
        private void CheckForTheWinner()
        {
            // go thought it all a see if they are all matched.
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }
            timer2.Stop();
            timer2.Enabled = false;

            // if nothing is found that all have been match and user wins. 
            MessageBox.Show("You matched all the icons in " + timeCount + " seconds!", "Congratulations");
            Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timeCount++;
            elapsedTime.Text = timeCount + " seconds";
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
