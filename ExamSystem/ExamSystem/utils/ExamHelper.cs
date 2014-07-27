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

        public static IList<ClinicalCase> RetrieveByInjuredArea(int number = 20) {
            IList<ClinicalCase> cases = new List<ClinicalCase>();

            IList<InjuredArea> areas = PersistenceHelper.RetrieveAll<InjuredArea>();
            int[] random = GenerateUniformlyRandomNumberArray(number, areas.Count);

            Random rnd = new Random();
            ISet<long> idSet = new HashSet<long>();
            for (int i = 0; i < areas.Count; ++i) {
                for (int j = 0; j < random[i]; ++j) {
                    IList<ClinicalCase> tmp = areas[i].ClinicalCases;
                    
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
    }
}
