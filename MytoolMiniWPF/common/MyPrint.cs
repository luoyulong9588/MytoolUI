using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace MytoolMiniWPF.common
{
    class DocumentPaginatorForDouble : DocumentPaginator
    {
        private int _pageCount;
        private Size _pageSize;
        private Patient _patient;
        private int x;
        private int y;

        public override bool IsPageCountValid
        {
            get
            {
                return true;
            }
        }

        public override int PageCount
        {
            get
            {
                return _pageCount;
            }
        }

        public override Size PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        public override IDocumentPaginatorSource Source
        {
            get
            {
                return null;
            }
        }
        public DocumentPaginatorForDouble(Patient patient, int x, int y)
        {
            _patient = patient;

            //这个数据可以根据你要打印的内容来计算
            _pageCount = 1;

            //我们使用A4纸张大小
            var pageMediaSize = LocalPrintServer.GetDefaultPrintQueue()
                              .GetPrintCapabilities()
                              .PageMediaSizeCapability
                              .FirstOrDefault(i => i.PageMediaSizeName == PageMediaSizeName.ISOA4);

            if (pageMediaSize != null)
            {
                _pageSize = new Size((double)pageMediaSize.Width, (double)pageMediaSize.Height);
            }

            this.x = x;
            this.y = y;
        }

        public override DocumentPage GetPage(int pageNumber) {
            const int range_y = 50;

            const int point_x_1 = 150;
            const int point_x_2 = 300;
            const int point_x_3 = 470;
            const int point_x_4 = 560;
            const int point_x_5 = 630;


            int line1Y = 190;

            int line2Y = line1Y + range_y;

            int line3Y = line1Y + range_y*2;

          //  int line4Y = 880;

            int line4Y = line1Y + range_y*3;
            int lin5Y = 0;// 第5行没有内容
            int line6Y = line1Y + range_y*5;

            int line7Y = line1Y + range_y*6;
            int line8Y = line1Y + range_y * 7 -5;

            int line9Y = 1050;

            DateTime inDay = DateTime.Parse(_patient.InDay);
            DateTime outDay = DateTime.Parse(_patient.OutDay);
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext dc = visual.RenderOpen())
            {
                var pen = new Pen(Brushes.Black, 1);
                dc.DrawText(FormatText(_patient.Name), new Point(x + point_x_1, line1Y + y));
                dc.DrawText(FormatText(_patient.Id), new Point(x + point_x_2, line1Y + y));

                dc.DrawText(FormatText(inDay.ToString("yyyy")), new Point(point_x_3 + x, line1Y + y));

                dc.DrawText(FormatText(inDay.ToString("MM")), new Point(point_x_4 + x, line1Y + y));
                dc.DrawText(FormatText(inDay.ToString("dd")), new Point(point_x_5 + x, line1Y + y));


                dc.DrawText(FormatText(_patient.Gender), new Point(x + point_x_1, line2Y + y));
                dc.DrawText(FormatText(_patient.Age), new Point(x + point_x_2, line2Y + y));


                dc.DrawText(FormatText("——"), new Point(point_x_3 + x, line2Y + y));
                dc.DrawText(FormatText("—"), new Point(point_x_4 + x, line2Y + y));
                dc.DrawText(FormatText("—"), new Point(point_x_5 + x, line2Y + y));

                dc.DrawText(FormatText(_patient.MainDiagnose), new Point(point_x_1 + x, line3Y + y));
                dc.DrawText(FormatText("——"), new Point(point_x_3 + x, line3Y + y));

                dc.DrawText(FormatText("——"), new Point(x + point_x_1 + 20, line4Y + y));
                dc.DrawText(FormatText("——"), new Point(x + point_x_2 -10, line4Y + y));

                dc.DrawText(FormatText("——"), new Point(x + point_x_2, line6Y + y));

                dc.DrawText(FormatText("——"), new Point(x + point_x_1, line7Y + y));
                dc.DrawText(FormatText("——"), new Point(x + point_x_2, line7Y + y));


                dc.DrawText(FormatText("普内科"), new Point(x + point_x_1, line8Y + y));
                dc.DrawText(FormatText(_patient.DoctorName), new Point(x + point_x_2, line8Y + y));

                dc.DrawText(FormatText(outDay.ToString("yyyy")), new Point(x + point_x_3 + 40, line9Y + y));
                dc.DrawText(FormatText(outDay.ToString("MM")), new Point(x + point_x_4 + 40 + x, line9Y + y));
                dc.DrawText(FormatText(outDay.ToString("dd")), new Point( x + point_x_5 +50, line9Y + y));
            }
            return new DocumentPage(visual, _pageSize, new Rect(_pageSize), new Rect(_pageSize));
        }

        public FormattedText FormatText(string txt)
        {
            var formattedTxt = new FormattedText(txt, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("司马彦简行修正版"), 14, Brushes.Black);

            return formattedTxt;
        }
    }
}
