using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;

namespace ExamSystem.utils.exam2 {
    public class Question {
        #region Properties
        private String description;
        private String manifestation;
        private IList<Option> options;
        private double score;
        private ISet<int> selectedOptions;

        public ISet<int> SelectedOptions {
            get { return selectedOptions; }
            set { selectedOptions = value; }
        }

        public double Score {
            get { return score; }
            set { score = value; }
        }

        public IList<Option> Options {
            get { return options; }
            set { options = value; }
        }

        public String Manifestation {
            get { return manifestation; }
            set { manifestation = value; }
        }

        public String Description {
            get { return description; }
            set { description = value; }
        }
        #endregion

        #region Constructor
        public Question(ClinicalCase cli_case, String category, String occupation, double sc = 5) {
            description = cli_case.Description;
            manifestation = cli_case.Manifestation;
            score = sc;
            selectedOptions = new HashSet<int>();
            options = new List<Option>();
            char tmp = 'A';
            int index = 0;
            if (category.Equals("分类组")) { // if it's classification exam 
                for (int i = 0; i < cli_case.COptions.Count; ++i) {
                    ClassificationOption co = cli_case.COptions[i];
                    if (!occupation.Equals(co.Occupation.Description))
                        continue;
                    Option option = new Option((char)(tmp + index++) + ". " + co.Description, co.Correct);
                    options.Add(option);
                }
            } else {
                for (int i = 0; i < cli_case.MOptions.Count; ++i) {
                    MedicalOption mo = cli_case.MOptions[i];
                    if (!occupation.Equals(mo.Occupation.Description))
                        continue;
                    //Option option = new Option((char)(tmp + index++) + ". " + mo.Description, mo.Correct);
                    Option option = new Option(mo.Description, mo.Correct);
                    options.Add(option);
                }
            }
        }
        #endregion

        #region Public Methods
        public double GetScore() {
            int correctOption = 0;
            foreach (Option o in options) {
                if (o.Correct)
                    correctOption++;
            }

            if (selectedOptions.Count > correctOption) {
                int c = 0;
                int w = 0;
                for (int i = 0; i < selectedOptions.Count; ++i) {
                    if (options[selectedOptions.ElementAt(i)].Correct) {
                        c++;
                    } else {
                        w++;
                    }
                }
                if (w > c) {
                    return 0;
                } else {
                    return (c - w) * 1.0 / correctOption;
                }
            } else {
                int c = 0;
                for (int i = 0; i < selectedOptions.Count; ++i) {
                    if (options[selectedOptions.ElementAt(i)].Correct) {
                        c++;
                    }
                }
                return c * 1.0 / correctOption;
            }
        }

        public bool IsCorrect() {
            for (int i = 0; i < options.Count; ++i) {
                if (options[i].Correct) {
                    if (!selectedOptions.Contains(i))
                        return false;
                } else {
                    if (selectedOptions.Contains(i)) {
                        return false;
                    }
                }
            }

            return true;
        }

        public String GetAnswers() {
            char op = 'A';
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < options.Count; ++i) {
                if (options[i].Correct) {
                    char tmp = (char)(op + i);
                    builder.Append(tmp);
                    builder.Append(' ');
                }
            }

            return builder.ToString();
        }
        #endregion
    }
}
