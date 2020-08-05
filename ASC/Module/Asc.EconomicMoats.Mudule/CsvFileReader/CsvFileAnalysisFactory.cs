using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Asc
{
    public class CsvFileAnalysisFactory
    {
        private XmlDocument XmlDoc;

        /// <summary>
        /// If Aggregate Root Create Fails, return null.
        /// </summary>
        /// <returns></returns>
        public CsvFileAnalyzer CreateCsvFileReader(string ConfigPath)
        {
            CsvFileAnalyzer Cfr;
            try
            {
                // Read Config
                XmlDoc = new XmlDocument();
                XmlDoc.Load(ConfigPath);

                // Select Objects and Parameters
                Cfr = (CsvFileAnalyzer)GetInstance(GetThisNamespace() +
                    SelectSingleOption("CsvFileReader", "CsvFileReader"));
                Cfr.FilePath = SelectSingleOption("CsvFileReader", "FilePath");
                Cfr.Delimiters = SelectMultipleOptions("CsvFileReader", "Delimiters");

                // Dependency Management
                Cfr.Cfs = CreateCsvFileStructure();
                Cfr.Dal = CreateDatalineAnalysisLogic();
                return Cfr;
            }
            catch
            {
                return null;
            }
        }

        private CsvFileStructure CreateCsvFileStructure()
        {
            CsvFileStructure Cfs;
            // Select Objects and Parameters
            Cfs = (CsvFileStructure)GetInstance(GetThisNamespace() +
                SelectSingleOption("CsvFileStructure", "CsvFileStructure"));
            Cfs.HeaderLineStartAt = int.Parse(SelectSingleOption("CsvFileStructure", "HeaderLineStartAt"));
            Cfs.DataLinesStartAt = int.Parse(SelectSingleOption("CsvFileStructure", "DataLinesStartAt"));
            Cfs.FooterLinesCount = int.Parse(SelectSingleOption("CsvFileStructure", "FooterLinesCount"));

            // Dependency Management

            return Cfs;
        }

        private DatalineAnalysisLogic CreateDatalineAnalysisLogic()
        {
            DatalineAnalysisLogic Dal;

            // Select Objects and Parameters
            Dal = (DatalineAnalysisLogic)GetInstance(GetThisNamespace() +
                SelectSingleOption("DatalineAnalysisLogic", "DatalineAnalysisLogic"));

            // Dependency Management
            Dal.Def = CreateDatalineEntityFormat();

            return Dal;
        }

        private DatalineEntityAndFormat CreateDatalineEntityFormat()
        {
            DatalineEntityAndFormat Def;
            // Select Objects and Parameters
            Def = (DatalineEntityAndFormat)GetInstance(GetThisNamespace() +
                SelectSingleOption("DatalineEntityFormat", "DatalineEntityFormat"));

            // Dependency Management

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
