using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MatchingGame
{
    
    public partial class Form1 : Form
    {
        System.Timers.Timer t=new System.Timers.Timer();
        int s = 29;
        int ms=100;
        // firstClicked points to the first Label control  
        // that the player clicks, but it will be null  
        // if the player hasn't clicked a label yet.
        Label firstClicked = null;

        // secondClicked points to the second Label control  
        // that the player clicks.
        Label secondClicked = null;
        
        // Use this Random object to choose random icons for the squares.
        Random random = new Random();

        // Each of these letters is an interesting icon 
        // in the Webdings font, 
        // and each icon appears twice in this list.
        List<string> icons = new List<string>() 
        { 
            "!", "!", "N", "N", ",", ",", "k", "k","e","e","f","f","%","%","j","j","P","P",
            "b", "b", "v", "v", "w", "w", "$", "$","G","G","J","J","l","l","@","@","~","~"
        };

        /// <summary> 
        /// Assign each icon from the list of icons to a random square 
        /// </summary> 
        private void AssignIconsToSquares()
        {
            // The TableLayoutPanel has 16 labels, 
            // and the icon list has 16 icons, 
            // so an icon is pulled at random from the list 
            // and added to each label.
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
        } 


        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        /// <summary> 
        /// Every label's Click event is handled by this event handler.
        /// </summary> 
        /// <param name="sender">The label that was clicked.</param>
        /// <param name="e"></param>
        private void label_Click(object sender, EventArgs e)
        {
            // The timer is only on after two non-matching  
            // icons have been shown to the player,  
            // so ignore any clicks if the timer is running 
            if (timer1.Enabled == true)
                return; 
            
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // If the clicked label is black, the player clicked 
                // an icon that's already been revealed -- 
                // ignore the click.
                if (clickedLabel.ForeColor == Color.Black)
                    // All done - leave the if statements.
                    return;

                // If firstClicked is null, this is the first icon  
                // in the pair that the player clicked, 
                // so set firstClicked to the label that the player  
                // clicked, change its color to black, and return. 
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    // All done - leave the if statements.
                    return;
                }

                // If the player gets this far, the timer isn't 
                // running and firstClicked isn't null, 
                // so this must be the second icon the player clicked 
                // Set its color to black.
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Check to see if the player won.
                CheckForWinner();

                // If the player clicked two matching icons, keep them  
                // black and reset firstClicked and secondClicked  
                // so the player can click another icon. 
                if (firstClicked.Text == secondClicked.Text)
                {
                    if (radioButton1.Checked == true)
                    {
                        label49.Text = (int.Parse(label49.Text) + 1).ToString();
                        firstClicked.BackColor = Color.Red;
                        secondClicked.BackColor = Color.Red;
                    }
                    else
                    {
                        label50.Text = (int.Parse(label50.Text) + 1).ToString();
                        firstClicked.BackColor = Color.Yellow;
                        secondClicked.BackColor = Color.Yellow;
                    }
                    firstClicked = null;
                    secondClicked = null;
                    Thread.Sleep(1000);
                    s = 29;
                    t.Start();
                    return;
                }
                
                // If the player gets this far, the player  
                // clicked two different icons, so start the  
                // timer (which will wait three quarters of  
                // a second, and then hide the icons).
                timer1.Start();
            }
        }

        /// <summary> 
        /// This timer is started when the player clicks  
        /// two icons that don't match, 
        /// so it counts three quarters of a second  
        /// and then turns itself off and hides both icons.
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Stop the timer.
            timer1.Stop();

            // Hide both icons.
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Reset firstClicked and secondClicked  
            // so the next time a label is 
            // clicked, the program knows it's the first click.
            firstClicked = null;
            secondClicked = null;
            if (radioButton1.Checked == true)
            {
                lblT1.Text = (int.Parse(lblT1.Text) - 1).ToString();
                if (int.Parse(lblT2.Text) > 0)
                { 
                radioButton2.Checked = true;
                }
            }
            else
            {
                lblT2.Text = (int.Parse(lblT2.Text) - 1).ToString();
                if (int.Parse(lblT1.Text) > 0)
                {
                    radioButton1.Checked = true;
                }
                
            }
            Thread.Sleep(1000);
            s = 29;
            t.Start();
        }

        /// <summary> 
        /// Check every icon to see if it is matched, by  
        /// comparing its foreground color to its background color.  
        /// If all of the icons are matched, the player wins. 
        /// </summary> 
        

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label49.Text = (int.Parse(label49.Text) + 1).ToString();
            }
            else
            {
                label50.Text = (int.Parse(label50.Text) + 1).ToString();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label49.Text = (int.Parse(label49.Text) - 1).ToString();
            }
            else
            {
                label50.Text = (int.Parse(label50.Text) - 1).ToString();
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t.Interval = 10;
            t.Elapsed += OnTimeEvent;
        }

        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            
            Invoke(new Action(() => 
            {
                if (ms == 0)
                {
                    ms = 100;
                    s -= 1;
                }
                ms -= 1;
                if ((s == 0) && (ms == 0))
                {
                    t.Stop();
                }
                label52.Text = string.Format("{0} : {1}", s.ToString().PadLeft(2, '0'), ms.ToString().PadLeft(2, '0'));
            }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;









































































            t.Start();
            
        }

        private void CheckForWinner()
        {
            // Go through all of the labels in the TableLayoutPanel,  
            // checking each one to see if its icon is matched.
            

            if ((int.Parse(lblT1.Text)!=0)&&(int.Parse(lblT2.Text) != 0))
            {
                return;
            }
            // If the loop didn’t return, it didn't find 
            // any unmatched icons. 
            // That means the user won. Show a message and close the form.
            if (int.Parse(label49.Text) == int.Parse(label50.Text))
            { 
                MessageBox.Show("Its a tie!", "Game Over!");
                
            }
            else if (int.Parse(label49.Text) > int.Parse(label50.Text))
            {
                MessageBox.Show("Team 1 wins!!", "Congratulations!");
                
            }
            else
            {
                MessageBox.Show("Team 2 wins!!", "Congratulations!");
                
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (s == 0)
            {
                if (radioButton1.Checked == true)
                {
                    lblT2.Text = (int.Parse(lblT2.Text) - 1).ToString();
                }
                else
                {
                    lblT1.Text = (int.Parse(lblT1.Text) - 1).ToString();
                }
                Thread.Sleep(1000);
                s = 29;
                t.Start();
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (s == 0)
            {
                if (radioButton1.Checked == true)
                {
                    lblT2.Text = (int.Parse(lblT2.Text) - 1).ToString();
                }
                else
                {
                    lblT1.Text = (int.Parse(lblT1.Text) - 1).ToString();
                }
                Thread.Sleep(1000);
                s = 29;
                t.Start();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void label49_Click(object sender, EventArgs e)
        {

        }

        private void label50_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void lblT1_Click(object sender, EventArgs e)
        {

        }

        private void lblT2_Click(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void label52_Click(object sender, EventArgs e)
        {

        }
    }
}
