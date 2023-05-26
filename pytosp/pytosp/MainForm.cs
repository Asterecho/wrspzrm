/*
 * 由SharpDevelop创建。
 * 用户： ifwz
 * 日期: 2023/5/25
 * 时间: 19:41
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.International.Converters.PinYinConverter;
using NPinyin;

namespace pytosp
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			 
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button1Click(object sender, EventArgs e)
		{
			string[] t=File.ReadAllLines("dict.txt");
			for (int i = 0; i < t.Length; i++) {
				if (t[i]!="") {
					string hz=t[i].Split('=')[0];
					string fzm=t[i].Split('=')[1];
					string sp=PingYinHelper.Getsp(hz);
					t[i]=sp+fzm+"="+"0,"+hz;
				}
			}
			File.WriteAllLines("new.txt",t);
		}
		
		//https://www.cnblogs.com/shikyoh/p/6270026.html
		public static class PingYinHelper
    {
        private static Encoding gb2312 = Encoding.GetEncoding("GB2312");

        
        public static string Getsp(string hanzi){
        	Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("v", "y");
			dic.Add("iong", "s");
			dic.Add("ong", "s");
			dic.Add("iang", "d");
			dic.Add("uang", "d");
			dic.Add("uan", "r");
			dic.Add("uai", "y");
			dic.Add("eng", "g");
			dic.Add("ang", "h");
			dic.Add("iao", "c");
			dic.Add("ian", "m");
			dic.Add("ing", @";");
			dic.Add("iu", "q");
			dic.Add("ia", "w");
			dic.Add("ua", "w");
			dic.Add("er", "r");
			dic.Add("sh", "u");
			dic.Add("ch", "i");
			dic.Add("uo", "o");
			dic.Add("un", "p");
			dic.Add("en", "f");
			dic.Add("an", "j");
			dic.Add("ao", "k");
			dic.Add("ai", "l");
			dic.Add("ei", "z");
			dic.Add("ie", "x");
			dic.Add("zh", "v");
			dic.Add("ui", "v");
			dic.Add("ve", "v");
			dic.Add("ou", "b");
			dic.Add("in", "n");
			dic.Add("ue", "t");
			
			
			
			string t=PingYinHelper.ConvertToAllSpell(hanzi);
			try {
				foreach(KeyValuePair<string,string>kvp in dic)
				{
					t=t.Replace(kvp.Key, kvp.Value);
					if (t.Length==1) {
						t="o"+t;
					}
				}
			} catch (Exception) {
				
				 Console.WriteLine("转化出错！");
			}
			return t;
        }
        /// <summary>
        /// 汉字转全拼
        /// </summary>
        /// <param name="strChinese"></param>
        /// <returns></returns>
        public static string ConvertToAllSpell(string strChinese)
        {
            try
            {
                if (strChinese.Length != 0)
                {
                    StringBuilder fullSpell = new StringBuilder();
                    for (int i = 0; i < strChinese.Length; i++)
                    {
                        var chr = strChinese[i];
                        fullSpell.Append(GetSpell(chr));
                    }

                    return fullSpell.ToString().ToLower();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("全拼转化出错！" + e.Message);
            }

            return string.Empty;
        }

        

        private static string GetSpell(char chr)
        {
            var coverchr = NPinyin.Pinyin.GetPinyin(chr);

            bool isChineses = ChineseChar.IsValidChar(coverchr[0]);
            if (isChineses)
            {
                ChineseChar chineseChar = new ChineseChar(coverchr[0]);
                foreach (string value in chineseChar.Pinyins)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value.Remove(value.Length - 1, 1);
                    }
                }
            }

            return coverchr;

        }
    }
	}
}
