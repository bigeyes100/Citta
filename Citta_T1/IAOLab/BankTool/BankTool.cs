﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace C2.IAOLab.BankTool
{
    public class BankTool
    {
        private static BankTool instance;
        public static BankTool GetInstance()
        {
            if (instance == null)
                instance = new BankTool();
            return instance;
        }
        public string BankToolSearch(string input)
        {
            string location = GetBankTool(input);
            //location = string.Join("", location.Split('{', '}', '"'));
            //StringBuilder bankCardLocationStringBuilder = new StringBuilder();
            //string bankCardLocation = input + "\t" + location + "\n";
            //bankCardLocationStringBuilder.Append(bankCardLocation);
            //string bankCardLocationString = bankCardLocationStringBuilder.ToString();
            //return bankCardLocationString;
            //location = string.Join("", location.Split('"'));
            return string.Format("{0}{1}{2}{3}", input, "\t", location, "\n");
        }

        public string GetBankTool(string bankCard)
        {
            string strURL = "http://www.teldata2018.com/cha/kapost.php?ka="+ bankCard;
            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式  
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";

            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
            }
            if (writer == null)
            {
                return "网络连接失败";
            }
            //将请求参数写入流
            //writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流
                           // String strValue = "";//strValue为http响应所返回的字符流
            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }
            Stream s = response.GetResponseStream();
            //  Stream postData = Request.InputStream;
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();
            postContent = string.Join("", postContent.Split('\r','\n','\t'));
            postContent = postContent.Replace("<br />", "\t");
            String[] postContentArry = postContent.Split('\t','?');
            if (postContentArry.Length == 9)
            {
                string fullpostContent = postContentArry[1] + "\t" + postContentArry[3] + "\t" + postContentArry[5];
                return fullpostContent;
            }
            else
            {
                string fullpostContent = "银行卡格式不正确";
                return fullpostContent;
            }
        }
    }
   
}
