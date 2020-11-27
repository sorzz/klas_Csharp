using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Web;
using agi = HtmlAgilityPack;


namespace oss_project
{
    public partial class Form1 : Form
    {
        string strSessionID;
        bool CK;

        public Form1()
        { 
            InitializeComponent(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             
            

        }
 

        private void button1_Click(object sender, EventArgs e)
        {
            // 달력 폼을 띄울 버튼
            // 일단 로그인 버튼이라고 쳐보자

            strSessionID = doLogin("2017203086","qhanf3488");
            if (strSessionID.Length > 0)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show(strSessionID);  
            }

        }
        private string doLogin(string ID, string PW)
        {
            string url = "https://klas.kw.ac.kr/std/cmn/frame/SchdulStdList.do";
            // Web Header 분석기에서 구한 PostData 정보를 여기에 적는다
            string PostData = string.Format("loginId={0}&loginPwd={1}", ID.Trim(), PW.Trim());

            string strID;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            byte[] sendData = UTF8Encoding.UTF8.GetBytes(PostData);
            webRequest.Method = "POST";
            webRequest.UserAgent = "Mozilla/5.0";
            webRequest.ContentType = "application/json;charset=UTF-8";
            webRequest.KeepAlive = true;
            webRequest.AllowAutoRedirect = false;

            Stream dataStrem = webRequest.GetRequestStream();
            dataStrem.Write(sendData, 0, sendData.Length);
            dataStrem.Close();

            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            StreamReader readStream = new StreamReader(webResponse.GetResponseStream(), System.Text.Encoding.UTF8, true);
            string resResult = readStream.ReadToEnd().Trim();
            MessageBox.Show(resResult);  // 패스워드 정상 / 비정상 메시지를 확인한다.
            string cookie = webResponse.GetResponseHeader("Set-Cookie");
            MessageBox.Show(cookie);

            readStream.Close();
            webResponse.Close();

            CK = false;
            if (resResult.IndexOf("history.go(-1);//]]>") < 0)  // 웹페이지 메세지가 없으면
            {
                if (webResponse.GetResponseHeader("Set-Cookie").IndexOf("PHPSESSID=") != -1)
                {
                    CK = true;
                    strSessionID = SSplit(SSplit(webResponse.GetResponseHeader("Set-Cookie"), "PHPSESSID=", 1), ";", 0);
                    strID = "2017203086";
                    MessageBox.Show("11");
                }
                else
                {
                    //여기로 옴.
                    MessageBox.Show("221");
                    strID = string.Empty;
                }
            }
            else
            {
                    MessageBox.Show("33");
                strID = string.Empty;
            }
            return strID;
        }

        public static string SSplit(string _body, string _parseString, int index)
        {
            string varTemp = _body.Split(new string[] { _parseString }, StringSplitOptions.None)[index];
            return varTemp;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
