using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;

namespace ExamSystem.utils {
    public class ExamHelper {
        #region Properties
        #endregion

        #region Constructor
        #endregion

        public static IList<ClinicalCase> RetrieveByCategory(String category, int number = 20) {
            IList<ClinicalCase> cases = new List<ClinicalCase>();

            IList<InjuredArea> areas = PersistenceHelper.RetrieveAll<InjuredArea>();
            int[] random = GenerateUniformlyRandomNumberArray(number, areas.Count);

            Random rnd = new Random();
            ISet<long> idSet = new HashSet<long>();
            for (int i = 0; i < areas.Count; ++i) {
                // get specified list under that category
                IList<ClinicalCase> tmp = new List<ClinicalCase>();
                foreach (ClinicalCase cc in areas[i].ClinicalCases) {
                    foreach (Category ct in cc.Categories) {
                        if (ct.Description.Equals(category)) {
                            tmp.Add(cc);
                            break;
                        }
                    } 
                }

                for (int j = 0; j < random[i]; ++j) {
                    int id;
                    do {
                        id = rnd.Next(0, tmp.Count);
                    } while (idSet.Contains(tmp[id].Id));

                    cases.Add(tmp[id]);
                    idSet.Add(tmp[id].Id);
                }
            }

            return cases;
        }

        public static IList<ClinicalCase> RetrieveByInjuredArea(int number = 20) {
            IList<ClinicalCase> cases = new List<ClinicalCase>();

            IList<InjuredArea> areas = PersistenceHelper.RetrieveAll<InjuredArea>();
            int[] random = GenerateUniformlyRandomNumberArray(number, areas.Count);

            Random rnd = new Random();
            ISet<long> idSet = new HashSet<long>();
            for (int i = 0; i < areas.Count; ++i) {
                ClinicalCase[] tmp = areas[i].ClinicalCases.ToArray<ClinicalCase>();
                shuffle(tmp);
                for (int j = 0; j < random[i]; ++j) {
                    int id;
                    do {
                        id = rnd.Next(0, tmp.Length);
                    } while (idSet.Contains(tmp[id].Id));

                    cases.Add(tmp[id]);
                    idSet.Add(tmp[id].Id);
                }
            }

            return cases;
        }

        private static int[] GenerateUniformlyRandomNumberArray(int number, int split) {
            int[] random = new int[split];
            while (number != 0) {
                random[number % split]++;
                number--;
            }

            shuffle(random);
            return random;
        }

        private static void shuffle(int[] random) {
            Random rnd = new Random();
            for (int i = 0; i < random.Length; ++i) {
                int r = i + (int)rnd.Next(0, random.Length - i);
                int swap = random[r];
                random[r] = random[i];
                random[i] = swap;
            }
        }

        public static void shuffle(object[] a) {
            Random rnd = new Random();
            for (int i = 0; i < a.Length; ++i) {
                int r = i + (int)rnd.Next(0, a.Length - i);
                object swap = a[r];
                a[r] = a[i];
                a[i] = swap;
            }
        }
    }
}
