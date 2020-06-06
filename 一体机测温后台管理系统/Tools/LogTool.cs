using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FaceServer.Tools {
    public class LogTool {
        public static void Write(string Msg) {
            string strPath;
            DateTime dt = DateTime.Now;
            try {
                strPath = AppDomain.CurrentDomain.BaseDirectory + "\\Log";
                if (Directory.Exists(strPath) == false) {
                    Directory.CreateDirectory(strPath);
                }

                DeleteLog(strPath);

                strPath = strPath + "\\" + dt.ToString("yyyyMMdd") + ".txt";
                StreamWriter FileWriter = new StreamWriter(strPath, true);
                FileWriter.WriteLine(dt.ToString("HH:mm:ss") + Msg);
                FileWriter.Close();
            }
            catch (Exception ex) {
                string str = ex.Message.ToString();
            }
        }
        public static void WriteError(string Msg) {
            string strPath;
            DateTime dt = DateTime.Now;
            try {
                strPath = AppDomain.CurrentDomain.BaseDirectory + "\\Log" + "\\Error";
                if (Directory.Exists(strPath) == false) {
                    Directory.CreateDirectory(strPath);
                }

                DeleteLog(strPath);

                strPath = strPath + "\\" + dt.ToString("yyyyMMdd") + "Error" + ".txt";
                StreamWriter FileWriter = new StreamWriter(strPath, true);
                FileWriter.WriteLine(dt.ToString("HH:mm:ss") + " " + Msg);
                FileWriter.Close();
            }
            catch (Exception ex) {
                string str = ex.Message.ToString();
            }
        }
        public static void WriteData(string Msg) {
            string strPath;
            DateTime dt = DateTime.Now;
            try {
                strPath = AppDomain.CurrentDomain.BaseDirectory + "\\Log" + "\\Data";
                if (Directory.Exists(strPath) == false) {
                    Directory.CreateDirectory(strPath);
                }

                DeleteLog(strPath);

                strPath = strPath + "\\" + dt.ToString("yyyyMMdd") + "Data" + ".txt";
                StreamWriter FileWriter = new StreamWriter(strPath, true);
                FileWriter.WriteLine(dt.ToString("HH:mm:ss") + " " + Msg);
                FileWriter.Close();
            }
            catch (Exception ex) {
                string str = ex.Message.ToString();
            }
        }
        public static void DeleteLog(string sLogPath) {
            //string strFolderPath = @"E:\Zhao\我的代码\f福建中医药大学附属人民医院\定制化服务\bin\Debug\logs";

            DirectoryInfo dyInfo = new DirectoryInfo(sLogPath);
            //获取文件夹下所有的文件
            foreach (FileInfo feInfo in dyInfo.GetFiles()) {
                //判断文件日期是否小于指定日期，是则删除
                if (feInfo.CreationTime < DateTime.Now.AddDays(-15))
                    feInfo.Delete();
            }

        }
    }
}