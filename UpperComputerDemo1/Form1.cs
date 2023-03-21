using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpperComputerDemo1
{
    /// <summary>
    /// 编程易学，思想难教
    /// </summary>
    public partial class Form1 : Form
    {
        UInt16 Timer_Value = 0;//定时值，分钟*60+秒钟 = Time_value
        UInt16 Timer_Count = 0;//定时器计数
        Byte Timer_Status = 0;//定时状态
        // 0-停止状态 1-定时状态 2-暂停状态

        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            int i;
            for (i = 0; i < 60; i++)
            {
                comboBox1.Items.Add(i.ToString());
                comboBox2.Items.Add(i.ToString());
            }
            comboBox1.Text = "0";
            comboBox2.Text = "59";
            button2.Enabled = false;
            progressBar1.Value = 0;
        }
        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            Timer_Count++;//每1秒 +1 开始从零计数 timer设置为1秒
            textBox1.Text = (Timer_Value - Timer_Count).ToString() + " 秒";//更新定时时间
            progressBar1.Value = Timer_Count;//更新进度条
            if (Timer_Count == Timer_Value)
            {
                timer1.Stop();
                Timer_Count = 0;
                Timer_Status = 0;//状态设置为0
                button1.Text = "计时结束";
                MessageBox.Show("倒计时结束", "Tips");
                button1.Text = "开始计时";
                textBox1.Text =string.Empty;
                comboBox1.Enabled = true;
                comboBox2 .Enabled = true;
                Form1_Load(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 停止事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Timer_Count = 0;//停止计数，设置为0
            Timer_Status = 0;//状态设置为0
            Form1_Load(sender, e);
            button1.Text = "开始计时";
            textBox1.Text = string.Empty;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            Form1_Load(this, EventArgs.Empty);
        }
        /// <summary>
        /// 开始计时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            switch (Timer_Status)
            {
                // 0-停止状态 1-定时状态 2-暂停状态
                case 0:
                    Timer_Value = (ushort)((Convert.ToUInt16(comboBox1.Text,10) * 60)+Convert.ToUInt16(comboBox2.Text,10));//获取选框内的内容 分钟*60+秒钟
                    if (Timer_Value >0)
                    {
                        //开始计时
                        textBox1.Text = Timer_Value.ToString()+" 秒";//文本框内显示倒计时数
                        button1.Text = "暂停计时";
                        button2.Enabled = true;
                        comboBox1.Enabled = false;
                        comboBox2.Enabled = false;
                        Timer_Status = 1;//状态为1，定时状态
                        //设置进度条
                        progressBar1.Value = 0;//初始值设置为0
                        progressBar1.Maximum = Timer_Value;//设置最大值为多少
                        timer1.Start();//启动定时

                    }
                    else
                    {
                        MessageBox.Show("定时时间不能为0", "TIps");
                    }
                    break;
                case 1:
                    timer1.Stop();
                    button1.Text = "继续计时";
                    Timer_Status = 2;//状态设置为2，
                    break;
                case 2:
                    timer1.Start();
                    button1.Text = "暂停计时";
                    Timer_Status = 1;//计时状态
                    break;
                default:
                    break;
            }
        }
    }
}
