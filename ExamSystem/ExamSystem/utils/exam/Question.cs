using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;

namespace ExamSystem.utils.exam {
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

        public enum EXAMSTATUS { CLASSIFICATION, MEDICAL }
        #endregion

        #region Constructor
        public Question(ClinicalCase cli_case, EXAMSTATUS estatus, String occupation, int sc = 5) {
            description = cli_case.Description;
            manifestation = cli_case.Manifestation;
            score = sc;
            selectedOptions = new HashSet<int>();
            options = new List<Option>();
            if (estatus == EXAMSTATUS.CLASSIFICATION) { // if it's classification exam 
                foreach (ClassificationOption co in cli_case.COptions) {
                    if (!occupation.Equals(co.Occupation.Description))
                        continue;
                    Option option = new Option(co.Description, co.Correct);
                    options.Add(option);
                }
            } else {
                foreach (MedicalOption mo in cli_case.MOptions) {
                    if (!occupation.Equals(mo.Occupation.Description))
                        continue;
                    Option option = new Option(mo.Description, mo.Correct);
                    options.Add(option);
                }
            }
        }
        #endregion

        #region Public Methods
        public double GetScore() {
            int result = 0;

            for (int i = 0; i < options.Count; ++i) {
                if (selectedOptions.Contains(i)) {
                    if (options[i].Correct)
                        result += 1;
                    else
                        return 0.0;
                }
            }

            int count = 0;
            foreach (Option o in options) {
                if (o.Correct)
                    count++;
            }

            return result * 1.0 / count;
        }

        public bool IsCorrect() {
            for (int i = 0; i < options.Count; ++i) {
                if (options[i].Correct) {
                    if (!selectedOptions.Contains(i))
                        return false;
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
