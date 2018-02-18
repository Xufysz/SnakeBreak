using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeBreak
{
    public partial class Form1 : Form
    {
        private int SnakeLength { get; set; }
        private List<Rectangle> SnakeOnePoints { get; set; }
        private int SnakeOneDirection { get; set; }
        private bool SnakeBroken { get; set; }
        private Rectangle currentPos;
        private Rectangle foodPos;
        private GamePanel gamePanel;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.gamePanel = new GamePanel();
            this.gamePanel.BackColor = Color.Black;
            this.gamePanel.Dock = DockStyle.Fill;

            this.gamePanel.Paint += this.Panel_Paint;

            this.Controls.Add(this.gamePanel);

            this.SetupGame();
        }

        private void SetupGame()
        {
            this.SnakeLength = 5;
            this.SnakeOnePoints = new List<Rectangle>();
            this.SnakeOneDirection = Program.RandomNumber(3);

            this.currentPos = new Rectangle(this.Size.Width / 2, this.Size.Height / 2, 10, 10);
            this.foodPos = new Rectangle(Program.RandomNumber(this.Size.Width), Program.RandomNumber(this.Size.Height), 10, 10);

            this.SnakeBroken = false;

            Timer t = new Timer() { Interval = 60 };
            t.Tick += T_Tick;
            t.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            this.gamePanel.Invalidate();

            //Check snake length
            if (this.SnakeOnePoints.Count > this.SnakeLength)
            {
                this.SnakeOnePoints.RemoveAt(0);
            }

            //Split snake
            //if (this.SnakeBroken && this.SnakeTwoPoints.Count > )

            if (this.IsSnakeTouching())
            {
                this.SnakeLength++;
                this.foodPos = new Rectangle(Program.RandomNumber(this.Size.Width), Program.RandomNumber(this.Size.Height), 10, 10);
            }

            switch (this.SnakeOneDirection)
            {
                case 0: //left
                    currentPos.X-=5;
                    break;

                case 1: //up
                    currentPos.Y-=5;
                    break;

                case 2: //right
                    currentPos.X+=5;
                    break;

                case 3: //down
                    currentPos.Y+=5;
                    break;
            }

            this.SnakeOnePoints.Add(new Rectangle(this.currentPos.X, this.currentPos.Y, 10, 10));
        }

        private bool IsSnakeTouching()
        {
            //Touching food
            if (!Rectangle.Intersect(this.currentPos, this.foodPos).IsEmpty)
                return true;

            return false;
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        { 
            Graphics graphic = e.Graphics;
            graphic.Clear(Color.Black);
            foreach (Rectangle r in this.SnakeOnePoints)
            {
                graphic.FillRectangle(Brushes.White, r);
            }
            graphic.FillRectangle(Brushes.Yellow, this.foodPos);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    this.SnakeOneDirection = 0;
                    break;

                case Keys.Up:
                    this.SnakeOneDirection = 1;
                    break;

                case Keys.Right:
                    this.SnakeOneDirection = 2;
                    break;

                case Keys.Down:
                    this.SnakeOneDirection = 3;
                    break;

                case Keys.Space:
                    this.SnakeBroken = !this.SnakeBroken;
                    break;
            }
        }
    }
}
