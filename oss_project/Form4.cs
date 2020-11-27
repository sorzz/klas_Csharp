using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;


namespace oss_project
{


    public partial class Form4 : Form
    {

        public Form4() { 
            InitializeComponent();
            POST();
          //  GET();
        }


        private void test()
        {

            //String callUrl = "https://klas.kw.ac.kr/std/lis/sport/d052b8f845784c639f036b102fdc3023/BoardStdList.do";
            //String[] data = new String[2];
            //data[0] = "2017203086";         // id
            //data[1] = "qhanf348";          // pw
            //String postData = String.Format("id={0}&pw={1}", data[0], data[1]);

            //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(callUrl);
            //// 인코딩 UTF-8
            //byte[] sendData = UTF8Encoding.UTF8.GetBytes(postData);
            //httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //httpWebRequest.Method = "POST";

            //httpWebRequest.ContentLength = sendData.Length;
            //Stream requestStream = httpWebRequest.GetRequestStream();
            //requestStream.Write(sendData, 0, sendData.Length);
            //requestStream.Close();
            //HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            //string re = streamReader.ReadToEnd();
            //streamReader.Close();
            //httpWebResponse.Close();

            //textBox1.Text = re;

             
        }

        void POST()
        {
            string jsonContent = "{loginId: \"2017203086\", loginPwd: \"qhanf348\"}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://klas.kw.ac.kr/usr/cmn/login/Logout.do");
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            long length = 0;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            length = response.ContentLength;
            

            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                textBox1.Text = reader.ReadToEnd();

                //return reader.ReadToEnd();
            }

        }

        string GET()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://klas.kw.ac.kr/std/lis/evltn/SelectOnlineCntntsStdList.do");
            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                textBox1.Text = reader.ReadToEnd();

                return reader.ReadToEnd();

            }
        }


        private string Request_Json()
        {
            string result = null;
            string url = "https://klas.kw.ac.kr/std/lis/sport/d052b8f845784c639f036b102fdc3023/BoardStdList.do";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "Application/json;charset=utf-8";

                //전달할 파라메타    
                string sendData = "param1=2017203086&param2=aaa";
                //string sendData = "";

                byte[] buffer;
                buffer = Encoding.Default.GetBytes(sendData);
                request.ContentLength = buffer.Length;

                Stream sendStream = request.GetRequestStream();
                sendStream.Write(buffer, 0, buffer.Length);
                sendStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream respPostStream = response.GetResponseStream();
                StreamReader readerPost = new StreamReader(respPostStream, Encoding.UTF8);

                result = readerPost.ReadToEnd();
                //JsonParser(result);
                textBox1.Text = result;
//                MessageBox.Show(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }


        private void JsonParser(String json)
        {
            JObject obj = JObject.Parse(json);
            JArray array = JArray.Parse(obj["d"].ToString());

            foreach (JObject itemObj in array)
            {
                //textBox1.Text += " ID : " + itemObj["Id"].ToString();
                //textBox1.Text += " --- ";
                //textBox1.Text += " Name : " + itemObj["Name"].ToString();
                //textBox1.Text += "\r\n";
            }
        }
    }
}