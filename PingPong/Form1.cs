using PingPongLib;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPong
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        Bitmap bitmap;
        Graphics screen;
        Graphics g;
        Paddle paddle;
        Ball ball;
        int a = 0;
        float deltaX = 0f;
        float speed = 5f;

        int frames = 0;
        int seconds = 0;
        float fps = 0;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(playGround.Width, playGround.Height);
            g = Graphics.FromImage(bitmap);
            screen = playGround.CreateGraphics();
            paddle = new Paddle(playGround.Width / 2 - 100, playGround.Height - 30, 200, 25);
            ball = new Ball(100, 200, 50, 50, -1, -1);
        }

        private void adjustPlayGround()
        {
            playGround.Size = this.Size;
            screen = playGround.CreateGraphics();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                while (!ball.OutOfBounds)
                {
                    g.Clear(Color.White);
                    paddle.MoveX(deltaX * speed);
                    g.FillRectangle(Brushes.Black, paddle.getRectengle());
                    ball.MoveBall(paddle.Location, paddle.Size, playGround.Size);
                    g.DrawString("Speed on ball: " + ball.GetUserFriendlySpeed(), new Font("arial", 10), Brushes.Black, new PointF(0, 0));
                    g.FillEllipse(Brushes.Black, ball.GetRectengle());

                    frames++;
                    g.DrawString("FPS: " + fps, new Font("arial", 10), Brushes.Black, new PointF(this.Width - 100, 0));
                    screen.DrawImage(bitmap, 0, 0, playGround.Width, playGround.Height);      
                }
                timer.Enabled = false;
                var byeMessage = ball.GetUserFriendlySpeed() < 5 ? "Could be better -_-" : "Not bad, not bad at all !";
                MessageBox.Show("You lost!\nSpeed of the ball was: " + ball.GetUserFriendlySpeed() + "\n" + byeMessage, "Looser", MessageBoxButtons.OK);
                Application.Exit();    
            });
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }



        private void Form1_Resize(object sender, EventArgs e)
        {
            adjustPlayGround();
        }

        //controlls
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            Console.WriteLine("constructor fired" + ++a);
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left: if (paddle.Location.X >= 0) deltaX = -1; else deltaX = 0; break;

                case Keys.D:
                case Keys.Right: if (paddle.Location.X <= playGround.Width - paddle.Size.Width) deltaX = 1; else deltaX = 0; break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left: if (deltaX == -1) deltaX = 0; break;

                case Keys.D:
                case Keys.Right: if (deltaX == 1) deltaX = 0; break;
            }
        }

        private void playGround_Click(object sender, EventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            fps = frames / ++seconds;
        }
    }
}

