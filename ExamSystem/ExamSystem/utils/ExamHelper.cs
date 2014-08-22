using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;
using log4net;

namespace ExamSystem.utils {
    public class ExamHelper {
        #region Properties
        private static readonly ILog log = LogManager.GetLogger(typeof(ExamHelper));
        #endregion

        #region Constructor
        #endregion

        public static IList<ClinicalCase> RetrieveByCategory(String category, String reason, int number = 20) {
            IList<ClinicalCase> cases = new List<ClinicalCase>();
            Category ct = PersistenceHelper.RetrieveByProperty<Category>("Description", category)[0];
            IList<ClinicalCase> tmp = new List<ClinicalCase>();
            foreach (ClinicalCase cc in ct.ClinicalCases) { 
                if (cc.Reason.Equals(reason))
                    tmp.Add(cc);
            }


            if (tmp.Count <= 20) {
                return tmp;
            } else {
                ClinicalCase[] cca = tmp.ToArray<ClinicalCase>();
                shuffle(cca);
                for (int i = 0; i < 20; ++i) {
                    cases.Add(cca[i]);
                }
                return cases;
            }

            //IList<InjuredArea> areas = PersistenceHelper.RetrieveAll<InjuredArea>();

            //bool redo = false;

            //do {
            //    int[] random = GenerateUniformlyRandomNumberArray(number, areas.Count);
            //    cases.Clear();
            //    redo = false;

            //    ISet<long> idSet = new HashSet<long>();
            //    for (int i = 0; i < areas.Count; ++i) {
            //        // get specified list under that category
            //        IList<ClinicalCase> tmp = new List<ClinicalCase>();
            //        foreach (ClinicalCase cc in areas[i].ClinicalCases) {
            //            if (cc.Reason.Equals(reason)) {
            //                foreach (Category ct in cc.Categories) {
            //                    if (ct.Description.Equals(category)) {
            //                        tmp.Add(cc);
            //                        break;
            //                    }
            //                }
            //            }
            //        }

            //        ClinicalCase[] cca = tmp.ToArray<ClinicalCase>();
            //        shuffle(cca);

            //        int cnt = 0;
            //        for (int j = 0; j < cca.Length; ++j) {
            //            if (!idSet.Contains(cca[j].Id)) {
            //                cases.Add(cca[j]);
            //                idSet.Add(cca[j].Id);
            //                cnt++;

            //                if (cnt == random[i])
            //                    break;
            //            }
            //        }

            //        if (cnt < random[i]) {
            //            redo = true;
            //            break;
            //        }
            //    }
            //} while (redo);
        }

        public static IList<ClinicalCase> RetrieveByInjuredArea(int number = 20) {
            IList<ClinicalCase> cases = new List<ClinicalCase>();

            IList<InjuredArea> areas = PersistenceHelper.RetrieveAll<InjuredArea>();
            

            bool redo = false;
            do {
                int[] random = GenerateUniformlyRandomNumberArray(number, areas.Count);
                cases.Clear();
                redo = false;
                Random rnd = new Random();
                ISet<long> idSet = new HashSet<long>();
                for (int i = 0; i < areas.Count && !redo; ++i) {
                    ClinicalCase[] tmp = areas[i].ClinicalCases.ToArray<ClinicalCase>();
                    shuffle(tmp);

                    int cnt = 0;
                    for (int j = 0; j < tmp.Length; ++j) {
                        if (!idSet.Contains(tmp[j].Id)) {
                            cases.Add(tmp[j]);
                            idSet.Add(tmp[j].Id);
                            cnt++;

                            if (cnt == random[i])
                                break;
                        }
                    }
                }
            } while (redo);

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
