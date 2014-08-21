using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using ExamSystem.entities;
using DataMigration.utils;
using System.Xml;

namespace DataMigration.utils {
    public class PropertyHelper {
        #region Properties
        private Dictionary<String, Category> categories = null;
        private Dictionary<String, InjuredArea> areas = null;
        private Dictionary<String, InjuredDegree> degrees = null;
        private Dictionary<String, Occupation> occupations = null;
        #endregion

        #region Constructor
        public PropertyHelper() {
            // read all default values into Dictionary
            categories = new Dictionary<String, Category>();
            IList<Category> categoryList = PersistenceHelper.RetrieveAll<Category>();
            foreach (Category category in categoryList) {
                categories.Add(category.Description, category);
            }

            areas = new Dictionary<string, InjuredArea>();
            foreach (InjuredArea area in PersistenceHelper.RetrieveAll<InjuredArea>()) {
                areas.Add(area.Area, area);
            }

            degrees = new Dictionary<string, InjuredDegree>();
            foreach (InjuredDegree degree in PersistenceHelper.RetrieveAll<InjuredDegree>()) {
                degrees.Add(degree.Degree, degree);
            }

            occupations = new Dictionary<string, Occupation>();
            foreach (Occupation occupation in PersistenceHelper.RetrieveAll<Occupation>()) {
                occupations.Add(occupation.Description, occupation);
            }
        }
        #endregion

        #region Private methods
        private void readCategory(ClinicalCase clinical_case, String str) {
            IList<Category> categoryList = new List<Category>();
            String[] categoriesString = str.Split('|');
            foreach (String categoryString in categoriesString) {
                if (!categories.ContainsKey(categoryString))
                    throw new Exception("组别中不存在: " + categoryString);
                categories[categoryString].ClinicalCases.Add(clinical_case);
                categoryList.Add(categories[categoryString]);
            }
            clinical_case.Categories = categoryList;
        }

        private void readInjuredAreas(ClinicalCase clinical_case, String str) {
            IList<InjuredArea> areaList = new List<InjuredArea>();
            String[] areasString = str.Split('|');
            foreach (String areaString in areasString) {
                if (!areas.ContainsKey(areaString))
                    throw new Exception("伤部中不存在: " + areaString);
                areaList.Add(areas[areaString]);
            }
            clinical_case.InjuredAreas = areaList;
        }

        private void readReason(ClinicalCase clinical_case, String str) {
            if (!(str.Equals("战伤") || str.Equals("外伤")))
                throw new Exception("伤势类型中不存在： " + str);
            clinical_case.Reason = str;
        }

        private void readInjuredDegrees(ClinicalCase clinical_case, String str) {
            String degreeString = str;
            if (!degrees.ContainsKey(degreeString))
                throw new Exception("受伤程度中不存在： " + degreeString);
            clinical_case.InjuredDegree = degrees[degreeString];
        }

        private void readDescription(ClinicalCase clinical_case, String str) {
            clinical_case.Description = str;
        }

        private void readManifestation(ClinicalCase clinical_case, String str) {
            clinical_case.Manifestation = str;
        }

        private IList<ClassificationOption> readClassificationOptions(ClinicalCase clinical_case, XmlNode root) {
            IList<ClassificationOption> coptions = new List<ClassificationOption>();
            XmlNode doctor = root.SelectSingleNode("军医");
            XmlNodeList options = doctor.SelectNodes("选项");
            foreach (XmlNode node in options) {
                ClassificationOption option = new ClassificationOption();
                option.Description = node.SelectSingleNode("描述").InnerText.Trim();
                String correct = node.SelectSingleNode("正确").InnerText.Trim();
                if (correct.Equals("对")) {
                    option.Correct = true;
                } else {
                    option.Correct = false;
                }
                option.ClinicalCase = clinical_case;
                option.Occupation = occupations["军医"];
                coptions.Add(option);
            }

            XmlNode nurse = root.SelectSingleNode("护士");
            options = nurse.SelectNodes("选项");
            foreach (XmlNode node in options) {
                ClassificationOption option = new ClassificationOption();
                option.Description = node.SelectSingleNode("描述").InnerText.Trim();
                String correct = node.SelectSingleNode("正确").InnerText.Trim();
                if (correct.Equals("对")) {
                    option.Correct = true;
                } else {
                    option.Correct = false;
                }
                option.ClinicalCase = clinical_case;
                option.Occupation = occupations["护士"];
                coptions.Add(option);
            }

            return coptions;
        }

        private IList<MedicalOption> readMedicalOption(ClinicalCase clinical_case, XmlNode root) {
            IList<MedicalOption> MOptions = new List<MedicalOption>();

            XmlNode doctor = root.SelectSingleNode("军医");
            XmlNodeList options = doctor.SelectNodes("选项");
            foreach (XmlNode node in options) {
                MedicalOption option = new MedicalOption();
                option.Description = node.SelectSingleNode("描述").InnerText.Trim();
                String correct = node.SelectSingleNode("正确").InnerText.Trim();
                if (correct.Equals("对")) {
                    option.Correct = true;
                } else {
                    option.Correct = false;
                }
                option.ClinicalCase = clinical_case;
                option.Occupation = occupations["军医"];
                MOptions.Add(option);
            }

            XmlNode nurse = root.SelectSingleNode("护士");
            options = nurse.SelectNodes("选项");
            foreach (XmlNode node in options) {
                MedicalOption option = new MedicalOption();
                option.Description = node.SelectSingleNode("描述").InnerText.Trim();
                String correct = node.SelectSingleNode("正确").InnerText.Trim();
                if (correct.Equals("对")) {
                    option.Correct = true;
                } else {
                    option.Correct = false;
                }
                option.ClinicalCase = clinical_case;
                option.Occupation = occupations["护士"];
                MOptions.Add(option);
            }

            return MOptions;
        }
        #endregion

        #region Public Methods
        public void readFile(String filePath) {
            ClinicalCase clinical_case = new ClinicalCase();

            XmlDocument xml = new XmlDocument();
            xml.Load(filePath);
            XmlElement root = xml.DocumentElement;

            // read Categories
            readCategory(clinical_case, root.SelectSingleNode("分组").InnerText.Trim());

            // read InjuredAreas
            readInjuredAreas(clinical_case, root.SelectSingleNode("伤部").InnerText.Trim());

            // read InjuredDegrees
            readInjuredDegrees(clinical_case, root.SelectSingleNode("受伤程度").InnerText.Trim());

            // read description
            readDescription(clinical_case, root.SelectSingleNode("受伤史").InnerText.Trim());

            // read manifestation
            readManifestation(clinical_case, root.SelectSingleNode("临床表现").InnerText.Trim());

            // read Reason
            readReason(clinical_case, root.SelectSingleNode("伤势类型").InnerText.Trim());
            PersistenceHelper.Save<ClinicalCase>(clinical_case);

            // read classification Option
            IList<ClassificationOption> coptions = readClassificationOptions(clinical_case, root.SelectSingleNode("分类处置"));
            foreach (ClassificationOption co in coptions) {
                PersistenceHelper.Save<ClassificationOption>(co);
            }

            // read Medical Option
            IList<MedicalOption> moptions = readMedicalOption(clinical_case, root.SelectSingleNode("医疗处置"));
            foreach (MedicalOption mo in moptions) {
                PersistenceHelper.Save<MedicalOption>(mo);
            }
        }
        #endregion
    }
}
