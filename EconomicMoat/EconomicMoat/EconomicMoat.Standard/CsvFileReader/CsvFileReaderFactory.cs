using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace EconomicMoat.Standard
{
    public class CsvFileReaderFactory
    {
        private XmlDocument XmlDoc;

        /// <summary>
        /// Dependency Management
        /// </summary>
        /// <returns></returns>
        public CsvFileReader CreateCsvFileReader(string ConfigPath)
        {
            XmlDoc = new XmlDocument();
            XmlDoc.Load(ConfigPath);

            CsvFileReader Cfr;
            try
            {
                // Try by Config Management
                Cfr = (CsvFileReader)GetInstance(GetThisNamespace() +
                    SelectSingleOption("CsvFileReader", "CsvFileReader"));
                Cfr.FilePath = SelectSingleOption("CsvFileReader", "FilePath");
                Cfr.Delimiters = SelectMultipleOptions("CsvFileReader", "Delimiters");
            }
            catch
            {
                // Catch by Default Management
                Cfr = new CsvFileReader();
                Cfr.FilePath = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg\TG_STAID000001.txt";
                Cfr.Delimiters = new Char[] { ',', ' ' };
            }
            Cfr.Cfs = CreateCsvFileStructure();
            Cfr.Dal = CreateDatalineAnalysisLogic();
            return Cfr;
        }

        private CsvFileStructure CreateCsvFileStructure()
        {
            CsvFileStructure Cfs;
            try
            {
                // Try by Config Management
                Cfs = (CsvFileStructure)GetInstance(GetThisNamespace() +
                    SelectSingleOption("CsvFileStructure", "CsvFileStructure"));
                Cfs.HeaderLineStartAt = int.Parse(SelectSingleOption("CsvFileStructure", "HeaderLineStartAt"));
                Cfs.DataLinesStartAt = int.Parse(SelectSingleOption("CsvFileStructure", "DataLinesStartAt"));
                Cfs.FooterLinesCount = int.Parse(SelectSingleOption("CsvFileStructure", "FooterLinesCount"));
            }
            catch
            {
                // Catch by Default Management
                Cfs = new CsvFileStructure();
                Cfs.HeaderLineStartAt = 1;
                Cfs.DataLinesStartAt = 2;
                Cfs.FooterLinesCount = 0;
                // TODO: NLog: "[This Method] [Try by Config] fails and [Catch by Default]."
            }
            return Cfs;
        }

        private DatalineAnalysisLogic CreateDatalineAnalysisLogic()
        {
            DatalineAnalysisLogic Dal;
            try
            {
                // Try by Config Management
                Dal = (DatalineAnalysisLogic)GetInstance(GetThisNamespace() +
                    SelectSingleOption("DatalineAnalysisLogic", "DatalineAnalysisLogic"));
            }
            catch
            {
                // Catch by Default Management
                Dal = new DatalineAnalysisLogic();
            }
            Dal.Def = CreateDatalineEntityFormat();
            return Dal;
        }

        private DatalineEntityFormat CreateDatalineEntityFormat()
        {
            DatalineEntityFormat Def;
            try
            {
                // Try by Config Management
                Def = (DatalineEntityFormat)GetInstance(GetThisNamespace() +
                    SelectSingleOption("DatalineEntityFormat", "DatalineEntityFormat"));
            }
            catch
            {
                // Catch by Default Management
                Def = new DatalineEntityFormat();
            }
            return Def;
        }

        private string SelectSingleOption(string ClassName, string SelectName)
        {
            XmlNode nodeObj = XmlDoc.SelectSingleNode("/Module" +
                "/Class[@Name='" + ClassName + "']" +
                "/Select[@Name='" + SelectName + "']");

            string Content = "";
            XmlNodeList xnList = nodeObj.SelectNodes("Option");
            foreach (XmlNode xn in xnList)
            {
                if (xn.Attributes["Selected"].Value == "Y")
                {
                    Content = xn.Attributes["Content"].Value;
                    break;
                }
            }
            return Content;
        }

        /// <summary>
        /// return Char[], e.g., Delimiters.
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="SelectName"></param>
        /// <returns></returns>
        private char[] SelectMultipleOptions(string ClassName, string SelectName)
        {
            XmlNode nodeObj = XmlDoc.SelectSingleNode("/Module" +
                "/Class[@Name='" + ClassName + "']" +
                "/Select[@Name='" + SelectName + "']");

            string Content = "";
            XmlNodeList xnList = nodeObj.SelectNodes("Option");
            foreach (XmlNode xn in xnList)
            {
                if (xn.Attributes["Selected"].Value == "Y")
                {
                    Content += xn.Attributes["Content"].Value;
                }
            }
            return Content.ToCharArray();
        }

        private object GetInstance(string FullyQualifiedName)
        {
            Type t = Type.GetType(FullyQualifiedName);
            return Activator.CreateInstance(t);
        }

        private string GetThisNamespace()
        {
            return MethodInfo.GetCurrentMethod().ReflectedType.Namespace + ".";
        }
    }
}
