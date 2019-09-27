using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace MicroService.Core.Web.Utilities
{
    public static class CheckCodeUtility
    {
        /// <summary>
        /// 动态生成指定数目的随机数或字母
        /// </summary>
        /// <param name="num">整数</param>
        /// <returns>返回验证码字符串</returns>
        public static string GenerateCheckCode(int num)
        {
            System.Threading.Thread.Sleep(3);
            long tick = DateTime.Now.Ticks;//1个以0.1纳秒为单位的时间戳，18位
            char[] constant = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(constant.Length);
            Random rd = new Random(unchecked((int)tick));
            for (int i = 0; i < num; i++)
            {
                newRandom.Append(constant[rd.Next(constant.Length)]);
            }
            return newRandom.ToString();
        }


        /// <summary>
        /// 根据验证码字符串生成验证码图片
        /// </summary>
        /// <param name="checkCode">验证码字符串</param>
        public static byte[] CreateCheckCodeImage(string checkCode)
        {

            if (checkCode == null || checkCode.Trim() == String.Empty) return null;
            // 引用System.Drawing类库
            Bitmap myImage = new Bitmap(60, 30);//生成一个指定大小的位图
            Graphics graphics = Graphics.FromImage(myImage); //从一个位图生成一个画布
            try
            {
                graphics.Clear(Color.White); //清除整个绘画面并以指定的背景色填充,这里是把背景色设为白色
                Random random = new Random(); //实例化一个伪随机数生成器

                //画图片的前景噪音点,这里有200个
                for (int i = 0; i < 200; i++)
                {
                    int x = random.Next(myImage.Width);
                    int y = random.Next(myImage.Height);
                    myImage.SetPixel(x, y, Color.FromArgb(random.Next()));//指定坐标为x,y处的像素的颜色
                }

                //画图片的背景噪音线,这里为5条
                //                for (int i = 0; i < 5; i++)
                //                {
                //                    int x1 = random.Next(myImage.Width);
                //                    int x2 = random.Next(myImage.Width);
                //                    int y1 = random.Next(myImage.Height);
                //                    int y2 = random.Next(myImage.Height);
                //                    //绘制一条坐标x1,y1到坐标x2,y2的指定颜色的线条，这里的线条为黑色
                //                    graphics.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
                //                }
                //定义特定的文本格式,这里的字体为Arial，大小为15,字体加粗
                Font font = new Font("Arial", 15, FontStyle.Italic);
                //根据矩形、起始颜色和结束颜色以及方向角度产生一个LinearGradientBrush实例---线性渐变
                System.Drawing.Drawing2D.LinearGradientBrush brush =
                    new System.Drawing.Drawing2D.LinearGradientBrush(
                        new Rectangle(0, 0, myImage.Width, myImage.Height),//在坐标0,0处实例化一个和myImage同样大小的矩形
                        Color.Blue, Color.Red, 1.2f, true);
                //绘制文本字符串
                graphics.DrawString(checkCode, font, brush, 2, 2);

                //绘制有坐标对、宽度和高度指定的矩形---画图片的边框线
                graphics.DrawRectangle(new Pen(Color.Silver), 0, 0, myImage.Width - 1, myImage.Height - 1);
                //创建其支持存储器为内存的流
                MemoryStream ms = new MemoryStream();
                //将此图像以指定格式保存到指定的流中
                myImage.Save(ms, System.Drawing.Imaging.ImageFormat.Gif); //这里是以gif的格式保存到内存中

                return ms.ToArray();
            }
            finally
            {
                //释放占用资源
                graphics.Dispose();
                myImage.Dispose();
            }
        }


      
    }
}
