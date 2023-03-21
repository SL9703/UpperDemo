using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpperComputerDemo1
{
    public partial class UpperDemo : Form
    {
        public UpperDemo()
        {
            InitializeComponent();
        }

        private void UpperDemo_Load(object sender, EventArgs e)
        {
            label1.Text = "< " +
                serialPort1.BaudRate.ToString() + "," +
                serialPort1.DataBits.ToString() + "," +
                "1,ASCII" +
                " >"
                ;//显示波特率（BaudRate)，数据位(DataBits)
            SearchAndAddSerialToCombox(serialPort1, comboBox1);//将端口号自动添加到列表内
            toolStripStatusLabel2.Text = DateTime.Now.ToString("yyyy/MM/dd  HH:mm:ss");
            timer1.Start();

        }
        /// <summary>
        /// 自动添加串口号到列表内
        /// </summary>
        /// <param name="serialPort1"></param>
        /// <param name="comboBox1"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SearchAndAddSerialToCombox(SerialPort mySerialPort, ComboBox myComBoBox)
        {
            string[] ComputerPortName = SerialPort.GetPortNames();//获得本机串口列表
            myComBoBox.Items.Clear();//清空列表
            for (int i = 0; i < ComputerPortName.Length; i++)
            {
                try
                {
                    mySerialPort.PortName = ComputerPortName[i];
                    mySerialPort.Open();
                    myComBoBox.Items.Add(mySerialPort.PortName);
                    mySerialPort.Close();

                    if (comboBox1.Text == "")
                    {
                        comboBox1.Text = mySerialPort.PortName;
                    }
                }
                catch
                {

                }
            }
            if (comboBox1.Text == "")
            {
                textBox1.AppendText("[" + DateTime.Now.ToString("HH:mm:ss:fff") + "]" + "->");
                textBox1.AppendText("未检测到串口！\r\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)//扫描前检测串口是否打开状态
            {
                //如果是打开状态，则关闭串口
                serialPort1.Close();
                button2.Text = "打开串口";
                button2.Tag = "OFF";
                textBox1.AppendText("[" + DateTime.Now.ToString("HH:mm:ss:fff") + "]" + "->");
                textBox1.AppendText("扫描串口前关闭串口！\r\n");
            }
            SearchAndAddSerialToCombox(serialPort1, comboBox1);//手动扫描
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Tag.ToString() == "OFF")
            {
                //打开串口
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    button2.Text = "关闭串口";
                    button2.Tag = "NO";
                    textBox1.AppendText("[" + DateTime.Now.ToString("HH:mm:ss:fff") + "]" + "->");
                    textBox1.AppendText("串口打开成功！\r\n");
                }
                catch
                {
                    //打开失败
                    //关闭串口
                    serialPort1.Close();
                    button2.Text = "打开串口";
                    button2.Tag = "OFF";
                    textBox1.AppendText("[" + DateTime.Now.ToString("HH:mm:ss:fff") + "]" + "->");
                    textBox1.AppendText("串口打开失败！\r\n");
                }

            }
            else
            {
                //关闭串口
                serialPort1.Close();
                button2.Text = "打开串口";
                button2.Tag = "OFF";
                textBox1.AppendText("[" + DateTime.Now.ToString("HH:mm:ss:fff") + "]" + "->");
                textBox1.AppendText("关闭串口！\r\n");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString("yyyy/MM/dd  HH:mm:ss");
            //try
            //{
            //    if (this.IsHandleCreated)
            //    {
            //        this.Invoke(new Action(() =>
            //        {
            //            toolStripStatusLabel2.Text = DateTime.Now.ToString("yyyy/MM/dd  HH:mm:ss");
            //        }));
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }
    }
}
