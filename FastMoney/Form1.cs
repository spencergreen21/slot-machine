using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastMoney
{
    public partial class frmSlot : Form
    {
        //This will create a random number so the windows can stop spinning
        Random myrngStop = new Random();

        //These will tell me which windows won
        bool myblnWinner1 = false;
        bool myblnWinner2 = false;
        bool myblnWinner3 = false;

        //These are my Money Variables
        double mydblWinnings = 0;
        double mydblCredits = 100;
        double mydblBet = 10;

        private void Bank()
        {
            //need to make sure we are not betting more than we have
            if (mydblBet > mydblCredits)
            {
                mydblBet = mydblCredits;
            }

            if (mydblCredits <= 0)
            {
                MessageBox.Show("You have lost your money!", "Sorry Charlie");
            }


            //This will show the money on the screen
            lblWinnings.Text = mydblWinnings.ToString();
            lblCurrentBet.Text = mydblBet.ToString();
            lblCreditsRemaining.Text = mydblCredits.ToString("C");
        }

        // If the random number is 4, then the spinner stops
        private bool StopSpin()
        {
            //This will return true if the spinner should stop
            bool blnStop = false;

            int intStop = myrngStop.Next(10);
            if (intStop == 4)
            {
                blnStop = true;
            }

            return blnStop;
        }

        public frmSlot()
        {
            InitializeComponent();
        }


        // Changes the picture in a loop 1-7 
        private void PictureChange(Label lblWindowX, PictureBox picWindowX)
        {
            int intNext = int.Parse(lblWindowX.Text);
            intNext++;
            if (intNext > 7)
            {
                intNext = 1;
            }
            lblWindowX.Text = intNext.ToString();
            PictureBox picFound = (PictureBox)this.Controls.Find(
                    "picSlot" + intNext.ToString(), true)[0];
            picWindowX.BackgroundImage = picFound.BackgroundImage;

        }
        private void tmrWindow1_Tick(object sender, EventArgs e)
        {
            PictureChange(lblWindow1, picWindow1);
            if (StopSpin())
            {
                tmrWindow1.Enabled = false;
            }
        }


        private void btnArm_Click(object sender, EventArgs e)
        // checks to see if credits is above the bet
        {
            if (mydblCredits >= mydblBet)

            // Also, there must be more than 0 credits to spin the 

            {
                if (mydblCredits > 0)
                {
                    mydblCredits -= mydblBet;

                    tmrWindow1.Enabled = true;

                    tmrWindow2.Enabled = true;

                    tmrWindow3.Enabled = true;
                }
            }
        }








        private void tmrWindow2_Tick(object sender, EventArgs e)
        {
            PictureChange(lblWindow2, picWindow2);
            if (!tmrWindow1.Enabled && StopSpin())
            {
                tmrWindow2.Enabled = false;
            }
        }

        private void tmrWindow3_Tick(object sender, EventArgs e)
        {
            //spins by itself by waiting for the tmrWindow2 to stop 
            PictureChange(lblWindow3, picWindow3);
            if (!tmrWindow2.Enabled && StopSpin())
            {
                tmrWindow3.Enabled = false;
                Win();
            }
        }
        private void Win()
        {
            //Assume that we lost
            myblnWinner1 = false;
            myblnWinner2 = false;
            myblnWinner3 = false;
            mydblWinnings = 0;


            //This will check if we won and set the winning windows
            if (lblWindow1.Text == lblWindow2.Text &&
                lblWindow2.Text == lblWindow3.Text)
            {
                //Big Winner winner chicken dinner
                myblnWinner1 = true;
                myblnWinner2 = true;
                myblnWinner3 = true;

                mydblWinnings = mydblBet * 10;
            }
            else if (lblWindow1.Text == lblWindow2.Text)
            {
                //Medium winner
                myblnWinner1 = true;
                myblnWinner2 = true;
                mydblWinnings = mydblBet * 1.85;
            }
            else if (lblWindow2.Text == lblWindow3.Text)
            {
                //Medium Winner
                myblnWinner2 = true;
                myblnWinner3 = true;
                mydblWinnings = mydblBet * 1.85;

            }
            else if (lblWindow1.Text == lblWindow3.Text)
            {
                //Medium Winner
                myblnWinner1 = true;
                myblnWinner3 = true;
                mydblWinnings = mydblBet * 1.85;

            }
            mydblCredits += mydblWinnings;

            Bank();
            trmWinner.Enabled = true;
        }

        // changes the colors for a winner
        private void trmWinner_Tick(object sender, EventArgs e)
        {
            if (picWinner1.BackColor == Color.Fuchsia)
            {
                picWinner1.BackColor = Color.MediumSlateBlue;
            }
            else if (picWinner1.BackColor == Color.MediumSlateBlue)
            {
                picWinner1.BackColor = Color.MediumSeaGreen;
            }
            else
            {
                picWinner1.BackColor = Color.Fuchsia;
            }
            picWinner2.BackColor = picWinner1.BackColor;
            picWinner3.BackColor = picWinner1.BackColor;

            //Stop the winning lights and turn them all off
            if (StopSpin())
            {
                trmWinner.Enabled = false;
                myblnWinner1 = false;
                myblnWinner2 = false;
                myblnWinner3 = false;
            }

            picWinner1.Visible = myblnWinner1;
            picWinner2.Visible = myblnWinner2;
            picWinner3.Visible = myblnWinner3;



        }

        private void frmSlot_Load(object sender, EventArgs e)
        {
            Bank();
        }

        private void btnBet50_Click(object sender, EventArgs e)
        {
            mydblBet = 50;
            Bank();
        }

        private void btnBet25_Click(object sender, EventArgs e)
        {
            mydblBet = 25;
            Bank();
        }

        private void btnBetAll_Click(object sender, EventArgs e)
        {
            mydblBet = mydblCredits;
            Bank();
        }

        private void Reset()
        {
            mydblCredits = 100;
            mydblBet = 10;
            mydblWinnings = 0;
            Bank();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }


        // Custom Bet price 
        private void btnSubmitPrice_Click(object sender, EventArgs e)
        // Must be an integer to bet 
        {
            int input = int.Parse(txtUniqueBet.Text);
            mydblBet = input;
            Bank();





        }
    }

}
