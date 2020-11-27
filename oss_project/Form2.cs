using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace oss_project
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
 
        public class CLSList
        {
            public string TYPE { get; set; }
            public string NAME { get; set; }
            public string IMGURL { get; set; }
            public string ID { get; set; }
            public string SITEURL { get; set; }
            public string DATE { get; set; }
        }

        // 제이슨을 요청해서 콘솔이나 메시지박스로 먼저 찍어 보시고,
        // 필요한 정보를 클래스 형태로 정의해주세요.
        private void Form2_Load(object sender, EventArgs e)
        {
            string json = Request_Json();
            MessageBox.Show(json);  
            //aaa();
          //  this.ParseJson(json);
        }

        private string Request_Json()
        {
            string result = null;
            try
            {
                //가져오는데에 성공한 네이버 url
                //string callUrl = "https://comic.naver.com/xml/mainTopXml.nhn?order=user&null";
                
                String callUrl = "https://klas.kw.ac.kr/std/cmn/frame/LctrumSchdulInfo.do";
                String[] data = new String[2];
                data[0] = "2017203086";         // id
                data[1] = "qhanf348";          // pw
                String postData = String.Format("loginToken={0}", "5L1670DMgBNKfnKaeKCwD6dYxD1DsZCeEdHPQpqLwNlxiqbdnGYMxn3kCF50gOm57f0BD6kXKb+2yMGP+OvwAtoTeDy/pKdWe4H3ecmFlA5jRWlh5k3N1ll9GIMy+A9WyLb7ucyd0lVrj1ca8g6guEFQfiqFSUMxYfFKt4oqfKD6fsZBNr6e03zjR0gz5VQVS9gRSRZJhPvZkSn60RFjiLPMciE3VacTF16dd4Vh7ib85U8OjF2UXeV0J9hAB2Vh1IkgmpIdYpMRiGGw6N9vR8Vk3mXESgVrvMlRFCCFovCKNQAF2weCl6O9t1CRtEU1EN9lz976ZnsRa9q11rvlaQ==");
                //String postData = String.Format("selectSubj={0}&selectYearhakgi={1}", "5L1670DMgBNKfnKaeKCwD6dYxD1DsZCeEdHPQpqLwNlxiqbdnGYMxn3kCF50gOm57f0BD6kXKb+2yMGP+OvwAtoTeDy/pKdWe4H3ecmFlA5jRWlh5k3N1ll9GIMy+A9WyLb7ucyd0lVrj1ca8g6guEFQfiqFSUMxYfFKt4oqfKD6fsZBNr6e03zjR0gz5VQVS9gRSRZJhPvZkSn60RFjiLPMciE3VacTF16dd4Vh7ib85U8OjF2UXeV0J9hAB2Vh1IkgmpIdYpMRiGGw6N9vR8Vk3mXESgVrvMlRFCCFovCKNQAF2weCl6O9t1CRtEU1EN9lz976ZnsRa9q11rvlaQ==", "2020, 2");

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(callUrl);
                
                // 인코딩 UTF-8
                byte[] sendData = UTF8Encoding.UTF8.GetBytes(postData);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = sendData.Length;

                httpWebRequest.ContentType = "application/json;charset=UTF-8";
                httpWebRequest.UserAgent = "Mozilla/5.0";
                //httpWebRequest.Connection = "Keep-Alive";

                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(sendData, 0, sendData.Length);
                requestStream.Close();
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                string re = streamReader.ReadToEnd();
                streamReader.Close();
                httpWebResponse.Close();
                textBox1.Text = re;


            }
            catch (Exception ex)
            {
            }
            return result;  
        }

        private void ParseJson(String json)
        {
            System.Net.WebClient wcClient = new System.Net.WebClient(); 
            List<CLSList> issues = new List<CLSList>();
            JArray jsonArray = JArray.Parse(json); // 에러 발생
            CLSList issue = new CLSList();

            MessageBox.Show(json.Substring(5, 10));
        }

        private void aaa()
        {
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(
              "https://klas.kw.ac.kr/std/cmn/frame/LctrumSchdulInfo.do");
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;


            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                textBox1.Text=responseFromServer;
            }

            // Close the response.
            response.Close();
        }

    }
}
