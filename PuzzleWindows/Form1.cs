using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleWindows
{
    public partial class Form1 : Form
    {
        //puzzleGameEngine 생성하자
        PuzzleGameEngine pge;
        List<Image> imgList = new List<Image>();
        private int puzzleSize = 16;
        int imgWidth = 100;
        int imgHeight = 100;
        private Font theFont;
        private Brush theBrush;
        private int theTick;
        private int theGameTick;
        private Pen thePen;
        private Brush fill;

        public Form1()
        {
            InitializeComponent();
            //image를 가져오자
            //image를 list에 넣자
            for(int i=0; i<puzzleSize; i++)
            {
                string fileName = "pic_" + (char)('a'+i) + ".png";
                //string fileName = string.Format("pic_{0}.png",(char)('a'+i));
                Image tmpI = Image.FromFile("../../images/"+fileName);
                imgList.Add(tmpI);
            }
            pge = new PuzzleGameEngine();

            theFont = new Font("굴림", 15);
            theBrush = new SolidBrush(Color.Green);
            fill = new SolidBrush(Color.Red);
            thePen = new Pen(Color.Red);
            
            theTick = 0;
            theGameTick = 0;
            timer1.Start();
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int time = 100-(theTick-theGameTick)/(1000/timer1.Interval);
            string timeString = string.Format("Time : {0:D3}", time);
            //time 표시
           // e.Graphics.DrawString(timeString, theFont, theBrush, 0, 10);
            e.Graphics.DrawRectangle(thePen, 0, 0, time,10);
            e.Graphics.FillRectangle(fill, 0, 0, time, 10);
            //time이 0이면 게임오버
            if (time == -1)
            {
                timer1.Stop();
                MessageBox.Show("게임오버");
                return;
            }

            //그릴 때, 이미지를 4*4fh 그리자
            int h = -1;
            for (int i = 0; i < 16; i++)
            {
                if (i % 4 == 0) h++;
                if (pge.GetViewIndex(i) != puzzleSize - 1)
                {
                    e.Graphics.DrawImage(imgList[pge.GetViewIndex(i)], i % 4 * 100+50, h * 100+50, imgWidth, imgHeight);
                }
            }
        }

            //클릭했을 때, 클릭한 index와 빈 칸 index를 교체하자
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int tmpX = e.X;
            int tmpY = e.Y;
            if (!(50 <= tmpX && tmpX <= 4 * imgHeight + 50
                && 50 <= tmpY && tmpY<= 4 * imgHeight + 50)) return;
            //MessageBox.Show(tmpX + "," + tmpY);
            //x,y 좌표를 구역으로 바꾸자
            //50~150
            tmpX -= 50;
            tmpY -= 50;
            tmpX /= imgWidth;
            tmpY /= imgHeight;
            

            int index = tmpX + tmpY * 4;
            //MessageBox.Show(index + " ");
            pge.Change(index);
            Invalidate();

            //다 맞췄으면 축하~~
            //창 닫자
            if (pge.isEnd())
            {
                MessageBox.Show("ㅊㅋㅊㅋ");
                Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            theTick++;
            Invalidate();
        }
    }
}
