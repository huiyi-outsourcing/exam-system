using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExcelLibrary.SpreadSheet;

namespace ExamResult.utils {
    public class ExcelHelper2 {
        public ExcelHelper2() { }

        public void SaveToExcel(String filepath, IList<ExamSystem.entities.ExamResult> results) {
            Workbook workbook = new Workbook();
            Worksheet worksheet = new Worksheet("成绩");

            worksheet.Cells[0, 0] = new Cell("姓名");
            worksheet.Cells[0, 1] = new Cell("保障卡号");
            worksheet.Cells[0, 2] = new Cell("成绩");
            worksheet.Cells[0, 3] = new Cell("时间");

            for (int i = 1; i <= results.Count; ++i) {
                worksheet.Cells[i, 0] = new Cell(results[i - 1].User.Name);
                worksheet.Cells[i, 1] = new Cell(results[i - 1].User.SecurityCode);
                worksheet.Cells[i, 2] = new Cell((decimal)results[i - 1].Score);
                worksheet.Cells[i, 3] = new Cell(results[i-1].DateTime, @"YYYY\-MM\-DD HH:MM:SS");
            }
            worksheet.Cells.ColumnWidth[0, 1] = 4000;
            worksheet.Cells.ColumnWidth[1, 2] = 6000;
            worksheet.Cells.ColumnWidth[3, 4] = 10000;

            for (int i = results.Count + 1; i < results.Count + 100; ++i) {
                worksheet.Cells[i, 0] = new Cell("");
            }

            workbook.Worksheets.Add(worksheet);
            workbook.Save(filepath);
        }
    }
}
