using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TrippleNT.Utility
{
    public class LogWriter
    {
        private readonly IHostingEnvironment env;
      
        private string m_exePath = string.Empty;
        public LogWriter(string logMessage, IHostingEnvironment _env)
        {
            env = _env;
            LogWrite(logMessage);
        }
        public string LogWrite(string logMessage)
        {

            try
            {
                Directory.CreateDirectory(env.ContentRootPath + "~/ErrorLog/");
                m_exePath = env.ContentRootPath + "~/ErrorLog/" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
                if (!File.Exists(m_exePath))
                {
                    using (StreamWriter sw = File.CreateText(m_exePath))
                    {
                        Log(logMessage, sw);
                    }
                }
                else
                {
                    using (StreamWriter w = File.AppendText(m_exePath))
                    {
                        Log(logMessage, w);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
