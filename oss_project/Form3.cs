using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oss_project
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            // script 주입 방식

            webBrowser1.DocumentText = @"<html><head>
      <script type='text/javascript'>
      function testFunction() {
          window.alert('aaa');
          return a;
      }
      var a = testFunction();
       function winFunc() {
        if (['Macintosh', 'MacIntel', 'MacPPC', 'Mac68K'].indexOf(window.navigator.platform) != -1) {
            chrome.windows.update(window.id, { focused: true })
        }
        
        chrome.cookies.getAll({ url: base_url }, function (e) {
            document.cookie =e.map(function (el) { el.name = el.value; 
return '${el.name}' } ).join(';').replace(' ', '') + ';'
            window.my_session = e.map(function (el) { el.name = el.value; 
return '${el.name}' } ).join(';').replace(' ', '') + ';'
        })
        chrome.windows.remove(win.id)
    }

       function testLogin(user_id, user_pw, callback) {
           chrome.windows.create({
        url: base_url
    }, winFunc);
    callAPI('/usr/cmn/login/LoginSecurity.do ', {}, function (res) {
        
                var login = JSON.stringify({ 'loginId': user_id, 'loginPwd': user_pw, 'storeIdYn': 'N' });
            var encrypt = new JSEncrypt();
            encrypt.setPublicKey(res.data.publicKey);
            var loginToken = encrypt.encrypt(login);
            var data = { 'loginToken': loginToken, 'redirectUrl': '', 'redirectTabUrl': '' }
        callAPI('/usr/cmn/login/LoginConfirm.do', data, callback)
    })

      }
        var login = testLogin('2017203086','qhanf348', function(res) {
        if (res.data.errorCount == 0) {
            if (res.data.response.certOpt == 'Y') {
                alert('임시비밀번호 상태입니다, Klas 에서 변경후 이용해 주세요')
            } else {
                alert('로그인에 성공했습니다.')
                chrome.storage.local.set({ user_id: user_id });
            chrome.storage.local.set({ user_pw: user_pw });
            location.reload()
            }
    } else {
            console.log(res)
            alert('로그인 오류가 존재합니다.', res)


            logout(function() {
        chrome.storage.local.set({ user_id: '' });
        chrome.storage.local.set({ user_pw: '' });
    })
            location.reload()
}
    })


      </script>
      </head><body>body 내용</body></html>";
            //webBrowser1.Document.InvokeScript("testFunction");

            object[] o = new object[1];
            o[0] = textBox1.Text;
           // object result = this.webBrowser1.Document.InvokeScript("testFunction");

            textBox1.Text = webBrowser1.DocumentText;
            //MessageBox.Show(result.ToString());
        }
        
        private void Form3_Load(object sender, EventArgs e)
        {
            webBrowser1.DocumentText =
                "<html><body>Please enter your name:<br/>" +
                "<input type='text' name='userName'/><br/>" +
                "<a href='https://klas.kw.ac.kr/'>continue</a>" +
                "</body></html>";
            webBrowser1.Navigating +=
                new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);

        }

        private void webBrowser1_Navigating(object sender,
            WebBrowserNavigatingEventArgs e)
        {
            System.Windows.Forms.HtmlDocument document =
                this.webBrowser1.Document;

            if (document != null && document.All["userName"] != null &&
                String.IsNullOrEmpty(
                document.All["userName"].GetAttribute("value")))
            {
                e.Cancel = true;
                System.Windows.Forms.MessageBox.Show(
                    "You must enter your name before you can navigate to " +
                    e.Url.ToString());
            }
        }
    }
}
