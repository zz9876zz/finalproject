using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[] n = new int[9];
        PictureBox[] p = new PictureBox[9];
        PictureBox hitpic1, hitpic2;
        string t1, t2;
        bool isfirst = true;
        int timer1num1, timenum2;
        int level=2, num,k,A,B,j=1;
        int meme1, meme2;

        private void button3_Click(object sender, EventArgs e)
        {
            j = 2;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1num1 -= 1;
            label1.Text = "你可以檢視的時間還有" + Convert.ToString(timer1num1) + "秒";
            if(timer1num1==0)
            {
                timer1.Enabled = false;
                label1.Text = "";
                timer2.Enabled = true;
                for (int i = 1; i <= n.GetUpperBound(0); i++)
                {
                    p[i].Image = new Bitmap("q.jpg");
                    p[i].Enabled = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1num1 = 5;
            Gamestart();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timenum2 += 1;
            label1.Text = "遊戲時間:" + Convert.ToString(timenum2) + "秒";
            if (timenum2==30)
            {
                timer2.Enabled = false;
                MessageBox.Show("失敗");
                label1.Text = "請開始";
                label2.Text = "";
                for (int i = 1; i <= n.GetUpperBound(0); i++)
                {
                    p[i].Image = new Bitmap("q.jpg");
                    p[i].Enabled = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            j = 1;
        }

        void setrnd()
        {
            int[] ary = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            int max = n.GetUpperBound(0);
            Random rndObj = new Random();
            int rndNum;
            for (int i = 1; i <= n.GetUpperBound(0); i++)
            {
                rndNum = rndObj.Next(1, max + 1);
                n[i] = ary[rndNum];
                ary[rndNum] = ary[max];
                max--;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            label1.Text = "請按[開始]鈕進行遊戲";
            label2.Text = "";
            //指定timer1每一秒執行timerl_Tick事件處理函式一次
            timer1.Interval = 1000;
            //指定tmer2每一秒執行tmerl_Tick事件處理函式一次
            timer2.Interval = 1000;
            //分別將picl~pic8指定給p[1]~p[8]
            //表示p[1]~p[8]可以操作picl~pic8控制項
            p[1] = pictureBox1;
            p[2] = pictureBox2;
            p[3] = pictureBox3;
            p[4] = pictureBox4;
            p[5] = pictureBox5;
            p[6] = pictureBox6;
            p[7] = pictureBox7;
            p[8] = pictureBox8;
            for (int i = 1; i <= n.GetUpperBound(0); i++)
            {
                p[i].Image = new Bitmap("q.jpg");//使picl~pic8顯示qjpg
                //使圖片隨picl~pic8的大小做縮放
                p[i].SizeMode = PictureBoxSizeMode.StretchImage;
                //使picl~pic8的框線樣式以3D框線顯示
                p[i].BorderStyle = BorderStyle.Fixed3D;
                p[i].Enabled = false; //picl~pic8失效
                //使picl~pic8的Click事件被觸發時皆會執行PicClick事件處理函式
                p[i].Click += new EventHandler(PicClick);
            }
        }
        private void PicClick(object sender, EventArgs e)
        {

            if (isfirst) //第一次翻牌
            {
                hitpic1 = (PictureBox)sender; //將第一次翻牌的圖片方塊指定給hitPicl
                t1 = Convert.ToString(hitpic1.Tag); //將目前翻牌圖片的值指定給t1                  
                if (j == 1)
                {
                    hitpic1.Image = new Bitmap(Convert.ToInt32(hitpic1.Tag) + ".jpg");//顯示目前翻牌的圖示
                }
                if (j == 2)
                {
                    meme1= Convert.ToInt32(hitpic1.Tag)+8;
                    hitpic1.Image = new Bitmap(meme1 + ".jpg");//顯示目前翻牌的圖示
                }
                isfirst = false;
                A = Convert.ToInt32(hitpic1.Tag);
            }
            else  //第二次翻牌
            {
                //將第二次翻牌的嗣片方塊指定給hitPic
                hitpic2 = (PictureBox)sender;
                t2 = Convert.ToString(hitpic2.Tag); // 將目前翻牌圖片的值指定給t2  
                if (j == 1)
                {
                    hitpic2.Image = new Bitmap(Convert.ToInt32(hitpic2.Tag) + ".jpg"); //顯示目前翻牌的圖示
                }                
                if (j == 2)
                {
                    meme2 = Convert.ToInt32(hitpic2.Tag) + 8;
                    hitpic2.Image = new Bitmap(meme2 + ".jpg");//顯示目前翻牌的圖示
                }

                isfirst = true;//將isFirst設為tue表示目前已結束第二次翻牌
                //若t1等於,表示所翻牌兩個圖片的Tag屬性相同,即兩者的圖示相同
                B = Convert.ToInt32(hitpic2.Tag);

                if (A+4 == B || A == B+4)
                {
                    //使目前翻牌兩個圖片失效
                    hitpic1.Enabled = false;
                    hitpic2.Enabled = false;
                    num += 1;//答對組數加1 
                }
                else
                {
                    MessageBox.Show("答錯了");
                    // 將第一次和第二次翻牌的圖示以qjpg顯示  
                    hitpic1.Image = new Bitmap("q.jpg");
                    hitpic2.Image = new Bitmap("q.jpg");
                }
                if (num==4)
                {
                    MessageBox.Show("過關了");
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                }
                if (level==2)
                {
                    MessageBox.Show("過關了");
                }
            }
        }
        private void Gamestart()
        {
            level = timer1num1;
            //btn1鈕失效 
            button1.Enabled = false;
            //btn2鈕失效 
            button2.Enabled = false;
            //btn3鈕失效 
            button3.Enabled = false;
            //啟動timer1計時器 
            timer1.Enabled = true;
            //timer2To的計時遊戲時間 
            timenum2 = 0;
            //將t1第一次翻牌圖片所取得的值設為空白 
            t1 = "";
            //將2第二次翻牌圖片所取得的值設為空白 
            t2 ="";
            //將答對的組數設為0,若tot為4表示過關 
            num = 0;
            //將hitPic1第一次翻牌的圖片方塊設為null 
            hitpic1 = null;
            //將hitPic2第一次翻牌的圖片方塊設為null 
            hitpic2 = null;
            label1.Text ="你可以檢視的時間還有" + Convert.ToString(timer1num1) + "秒";
            label2.Text = "";
            setrnd();
            for (int i = 1; i <= n.GetUpperBound(0); i++)
            {
                //pic1~pic8的tag屬性設為n[1]~n[8]，表示圖示狀態
                p[i].Tag = n[i];                
                if (j == 1)
                {
                    p[i].Image = new Bitmap(Convert.ToString(n[i]) + ".jpg"); //顯示目前翻牌的圖示
                }
                if (j == 2)
                {
                    meme2 = Convert.ToInt32(n[i]) + 8;
                    p[i].Image = new Bitmap(meme2 + ".jpg");//顯示目前翻牌的圖示
                }
            }
        }
    }
}
