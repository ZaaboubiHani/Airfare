using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Excel = Microsoft.Office.Interop.Excel;
using _Word = Microsoft.Office.Interop.Word;
using System.Drawing;
using Airfare.Models;
using System.Windows.Forms;
using System.Windows;
using Microsoft.Office.Core;
using HandyControl.Controls;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Airfare.Servies
{
    public class ExportationServices : BaseServices
    {
        private async Task<_Word.Application> _getHostPaymentWordFile(HostModel host)
        {
            var word = new _Word.Application();
            await Task.Run(async() =>
            {
                try
                {
                    word.WindowState = _Word.WdWindowState.wdWindowStateNormal;
                    _Word.Document document = word.Documents.Add();
                    document.PageSetup.Orientation = _Word.WdOrientation.wdOrientLandscape;
                    document.Sections.PageSetup.TextColumns.SetCount(2);
                    document.Paragraphs.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    document.Paragraphs.Format.LineSpacingRule = _Word.WdLineSpacing.wdLineSpaceAtLeast;
                    document.Paragraphs.Format.LineSpacing = 35;
                    document.Paragraphs.Format.SpaceBefore = 35;
                    document.Paragraphs.Format.SpaceAfter = 35;
                    document.Paragraphs.LineSpacingRule = _Word.WdLineSpacing.wdLineSpaceAtLeast;
                    document.Paragraphs.LineSpacing = 35;
                    document.Paragraphs.SpaceBefore = 35;
                    document.Paragraphs.SpaceAfter = 35;
                    object oMissing = System.Reflection.Missing.Value;
                    object oEndOfDoc = "\\endofdoc";

                    EnvironmentServices environmentServices = new();
                    var Environment = await environmentServices.GetEnvironment();
                    foreach (_Word.Section section in document.Sections)
                    {
                        if (!string.IsNullOrEmpty(Environment.HeaderSource))
                        {
                            _Word.HeaderFooter header = section.Headers[_Word.WdHeaderFooterIndex.wdHeaderFooterPrimary];

                            section.PageSetup.HeaderDistance = 1.2f;
                            _Word.Shape hshape = header.Shapes.AddPicture(Environment.HeaderSource);
                        }
                        if (!string.IsNullOrEmpty(Environment.FooterSource))
                        {
                            _Word.HeaderFooter footer = section.Footers[_Word.WdHeaderFooterIndex.wdHeaderFooterPrimary];
                            section.PageSetup.FooterDistance = 1.2f;
                            _Word.Shape fshape = footer.Shapes.AddPicture(Environment.FooterSource, Type.Missing, Type.Missing, 0, -20);
                        }
                    }


                    _Word.Paragraph hostIdPara;
                    hostIdPara = document.Content.Paragraphs.Add(ref oMissing);
                    hostIdPara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    hostIdPara.Range.Font.SizeBi = 22;
                    var hostId = "وصل إيداع رقم : " + host.Id.ToString() + host.Client.Id.ToString() + host.Payments[host.Payments.Count-1].Id.ToString();
                    hostIdPara.Range.Text = hostId;
                    _Word.Range hostIdRange = document.Range(hostIdPara.Range.Start, hostIdPara.Range.Start + hostId.IndexOf(":"));
                    hostIdRange.Underline = _Word.WdUnderline.wdUnderlineSingle;
                    hostIdRange.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    hostIdPara.Range.InsertParagraphAfter();

                    _Word.Paragraph hostPricePara;
                    hostPricePara = document.Content.Paragraphs.Add(ref oMissing);
                    hostPricePara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    hostPricePara.Range.Font.SizeBi = 22;
                    var hostPrice = "السعر : " + host.FullPrice.ToString("c", new CultureInfo("ar-DZ"));
                    hostPricePara.Range.Text = hostPrice;
                    _Word.Range hostPriceRange = document.Range(hostPricePara.Range.Start, hostPricePara.Range.Start + hostPrice.IndexOf(":"));
                    hostPriceRange.Underline = _Word.WdUnderline.wdUnderlineSingle;
                    hostPriceRange.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    hostPricePara.Range.InsertParagraphAfter();

                    _Word.Paragraph hostRemainPara;
                    hostRemainPara = document.Content.Paragraphs.Add(ref oMissing);
                    hostRemainPara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    hostRemainPara.Range.Font.SizeBi = 22;
                    var hostRemain = "الباقي : " + host.RemainingPrice.ToString("c", new CultureInfo("ar-DZ"));
                    hostRemainPara.Range.Text = hostRemain;
                    _Word.Range hostRemainRange = document.Range(hostRemainPara.Range.Start, hostRemainPara.Range.Start + hostRemain.IndexOf(":"));
                    hostRemainRange.Underline = _Word.WdUnderline.wdUnderlineSingle;
                    hostRemainRange.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    hostRemainPara.Range.InsertParagraphAfter();


                    _Word.Paragraph hostRoomPara;
                    hostRoomPara = document.Content.Paragraphs.Add(ref oMissing);
                    hostRoomPara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    hostRoomPara.Range.Font.SizeBi = 22;
                    var hostRoom = "نوع الغرفة : " + host.HotelRoom.Room.ToString();
                    hostRoomPara.Range.Text = hostRoom;
                    _Word.Range hostRoomRange = document.Range(hostRoomPara.Range.Start, hostRoomPara.Range.Start + hostRoom.IndexOf(":"));
                    hostRoomRange.Underline = _Word.WdUnderline.wdUnderlineSingle;
                    hostRoomRange.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    hostRoomPara.Range.InsertParagraphAfter();

                    _Word.Paragraph hostHotelPara;
                    hostHotelPara = document.Content.Paragraphs.Add(ref oMissing);
                    hostHotelPara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    hostHotelPara.Range.Font.SizeBi = 22;
                    var hostHotel = "الفندق : " + host.HotelRoom.FlightHotel.Hotel.ToString() +"\v\v";
                    hostHotelPara.Range.Text = hostHotel;
                    _Word.Range hostHotelRange = document.Range(hostHotelPara.Range.Start, hostHotelPara.Range.Start + hostHotel.IndexOf(":"));
                    hostHotelRange.Underline = _Word.WdUnderline.wdUnderlineSingle;
                    hostHotelRange.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    hostHotelPara.Range.InsertParagraphAfter();


                    _Word.Paragraph hostNamePara;
                    hostNamePara = document.Content.Paragraphs.Add(ref oMissing);
                    hostNamePara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    hostNamePara.Range.Font.SizeBi = 22;
                    var hostName = "الإسم و اللقب : " + host.Client.FirstName + " " + host.Client.LastName;
                    hostNamePara.Range.Text = hostName;
                    _Word.Range hostNameRange = document.Range(hostNamePara.Range.Start, hostNamePara.Range.Start + hostName.IndexOf(":"));
                    hostNameRange.Underline = _Word.WdUnderline.wdUnderlineSingle;
                    hostNameRange.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    hostNamePara.Range.InsertParagraphAfter();

                    _Word.Paragraph hostPhonePara;
                    hostPhonePara = document.Content.Paragraphs.Add(ref oMissing);
                    hostPhonePara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    hostPhonePara.Range.Font.SizeBi = 22;
                    hostPhonePara.Range.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    var phone = (host.Client.Phones.Count > 0) ? host.Client.Phones[0].Number : "";
                    var hostPhone = "رقم الهاتف : " + phone;
                    hostPhonePara.Range.Text = hostPhone;
                    _Word.Range hostPhoneRange = document.Range(hostPhonePara.Range.Start, hostPhonePara.Range.Start + hostPhone.IndexOf(":"));
                    hostPhoneRange.Underline = _Word.WdUnderline.wdUnderlineSingle;
                    hostPhonePara.Range.InsertParagraphAfter();

                    _Word.Paragraph hostFlightPara;
                    hostFlightPara = document.Content.Paragraphs.Add(ref oMissing);
                    hostFlightPara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderLtr;
                    hostFlightPara.Range.Font.Size = 22;
                    hostFlightPara.Range.Font.SizeBi = 22;
                    hostFlightPara.Range.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    var hostFlight = "نوع العمرة : " + host.HotelRoom.FlightHotel.Flight.ToString() ;
                    hostFlightPara.Range.Text = hostFlight;
                    _Word.Range hostFlightRange = document.Range(hostFlightPara.Range.Start, hostFlightPara.Range.Start + hostFlight.IndexOf(":"));
                    hostFlightRange.Underline = _Word.WdUnderline.wdUnderlineSingle;
                    hostFlightPara.Range.InsertParagraphAfter();
                    

                    _Word.Paragraph hostPaymentPara;
                    hostPaymentPara = document.Content.Paragraphs.Add(ref oMissing);
                    hostPaymentPara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    hostPaymentPara.Range.Font.SizeBi = 22;
                    var hostPayment = "الدفع : " + host.Payments[host.Payments.Count-1].Amount.ToString("c", new CultureInfo("ar-DZ"));
                    hostPaymentPara.Range.Text = hostPayment;
                    _Word.Range hostPaymentRange = document.Range(hostPaymentPara.Range.Start, hostPaymentPara.Range.Start + hostPayment.IndexOf(":"));
                    hostPaymentRange.Underline = _Word.WdUnderline.wdUnderlineSingle;
                    hostPaymentRange.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    hostPaymentPara.Range.InsertParagraphAfter();


                    _Word.Paragraph hostDatePara;
                    hostDatePara = document.Content.Paragraphs.Add(ref oMissing);
                    hostDatePara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    hostDatePara.Range.Font.SizeBi = 22;
                    var hostdate = "التاريخ : " + host.Payments[host.Payments.Count - 1].Date.ToString("dd_MM_yyyy");
                    hostDatePara.Range.Text = hostdate;
                    _Word.Range hostdateRange = document.Range(hostDatePara.Range.Start, hostDatePara.Range.Start + hostdate.IndexOf(":"));
                    hostdateRange.Underline = _Word.WdUnderline.wdUnderlineSingle;
                    hostdateRange.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    hostDatePara.Range.InsertParagraphAfter();


                    _Word.Paragraph hostPassportPara;
                    hostPassportPara = document.Content.Paragraphs.Add(ref oMissing);
                    hostPassportPara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    hostPassportPara.Range.Font.SizeBi = 22;
                    var hostPassport = "رقم الجواز : " + host.Client.PassportNumber;
                    hostPassportPara.Range.Text = hostPassport;
                    _Word.Range hostPassportRange = document.Range(hostPassportPara.Range.Start, hostPassportPara.Range.Start + hostPassport.IndexOf(":"));
                    hostPassportRange.Underline = _Word.WdUnderline.wdUnderlineSingle;
                    hostPassportRange.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    hostPassportPara.Range.InsertParagraphAfter();

                }
                catch (Exception e)
                {
                    Growl.Error(e.Message);
                }
            });
            return word;
        }

        public async Task ExportHostPaymentData(HostModel host)
        {
            try
            {
                Error = false;
                var word = await _getHostPaymentWordFile(host);

                SaveFileDialog saveFileDialog = new();
                saveFileDialog.Filter = "Word files (*.docx)|*.docx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = DateTime.Now.ToString("dd_MM_yyyy") + "_دفع_المعتمر" + "_" + host.Client.FirstName;
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.Title = "Export Word File To";
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.CreatePrompt = false;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    word.ActiveDocument.SaveAs(saveFileDialog.FileName);
                    word.ActiveDocument.Saved = true;
                    word.ActiveDocument.Close();
                    word.Quit();
                    Growl.Success("تم طباعة دفع المعتمر بنجاح");
                }
                else
                {
                    word.ActiveDocument.Close(_Word.WdSaveOptions.wdDoNotSaveChanges, null);
                    word.Quit();
                    Growl.Info("تم إلغاء طباعة دفع المعتمر");
                }

                word = null;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
            }
        }


        private async Task<_Word.Application> _getCompanyWordFile(CompanyModel company, List<CompanyContractModel> contracts)
        {
            var word = new _Word.Application();
            await Task.Run(() =>
            {
                try
                {
                    word.WindowState = _Word.WdWindowState.wdWindowStateNormal;
                    _Word.Document document = word.Documents.Add();
                    object oMissing = System.Reflection.Missing.Value;
                    object oEndOfDoc = "\\endofdoc";
                    _Word.Paragraph companyNamePara;
                    companyNamePara = document.Content.Paragraphs.Add(ref oMissing);
                    companyNamePara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    companyNamePara.Range.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    var text = "إسم المتعامل : " + company.Name;
                    companyNamePara.Range.Text = text;
                    _Word.Range underlinedRange = document.Range(companyNamePara.Range.Start, companyNamePara.Range.Start + +text.IndexOf(":"));
                    underlinedRange.Underline = _Word.WdUnderline.wdUnderlineSingle;
                    underlinedRange.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    companyNamePara.Range.InsertParagraphAfter();

                    _Word.Paragraph hostsTitlePara;
                    hostsTitlePara = document.Content.Paragraphs.Add();
                    hostsTitlePara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    hostsTitlePara.Range.Text = ": لائحة المعتمرين";
                    hostsTitlePara.Range.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    hostsTitlePara.Range.InsertParagraphAfter();

                    _Word.Table companyHostsTable;
                    _Word.Range wrdRng = document.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    companyHostsTable = document.Content.Tables.Add(companyNamePara.Range, company.Hosts.Count+1, 5);
                    companyHostsTable.Borders.InsideLineStyle = _Word.WdLineStyle.wdLineStyleSingle;
                    companyHostsTable.Borders.OutsideLineStyle = _Word.WdLineStyle.wdLineStyleSingle;
                    List<string> hostsTableTitle = new() { "الرحلة", "المبلغ المدفوع", "المبلغ المستحق", "الإسم", "اللقب" };
                   
                    for (int i = 1; i <= hostsTableTitle.Count; i++)
                    {
                        companyHostsTable.Cell(1, i).Range.Text = hostsTableTitle[i-1];
                        companyHostsTable.Cell(1, i).Range.BoldBi = 1;
                    }

                    for(int i = 1; i <= company.Hosts.Count; i++)
                    {
                        companyHostsTable.Cell(i + 1, 1).Range.Text = company.Hosts[i - 1].HotelRoom.FlightHotel.Flight.ToString();
                        companyHostsTable.Cell(i + 1, 2).Range.Text = company.Hosts[i - 1].PaidPrice + " دج";
                        companyHostsTable.Cell(i + 1, 3).Range.Text = company.Hosts[i - 1].FullPrice + " دج";
                        companyHostsTable.Cell(i + 1, 4).Range.Text = company.Hosts[i - 1].Client.LastName;
                        companyHostsTable.Cell(i + 1, 5).Range.Text = company.Hosts[i - 1].Client.FirstName;
                    }

                    _Word.Paragraph paymentsTitlePara;
                    paymentsTitlePara = document.Content.Paragraphs.Add();
                    paymentsTitlePara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    paymentsTitlePara.Range.Text = ": لائحة الفواتير ";
                    paymentsTitlePara.Range.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    paymentsTitlePara.Range.InsertParagraphAfter();

                    _Word.Table companyPaymentsTable;
                    companyPaymentsTable = document.Content.Tables.Add(companyNamePara.Range, company.Payments.Count + 1, 3);
                    companyPaymentsTable.Borders.InsideLineStyle = _Word.WdLineStyle.wdLineStyleSingle;
                    companyPaymentsTable.Borders.OutsideLineStyle = _Word.WdLineStyle.wdLineStyleSingle;
                    List<string> paymentsTableTitle = new() { "الرحلة", "التاريخ", "المبلغ" };

                    for (int i = 1; i <= paymentsTableTitle.Count; i++)
                    {
                        companyPaymentsTable.Cell(1, i).Range.Text = paymentsTableTitle[i - 1];
                        companyPaymentsTable.Cell(1, i).Range.BoldBi = 1;
                    }

                    for (int i = 1; i <= company.Payments.Count; i++)
                    {
                        companyPaymentsTable.Cell(i + 1, 1).Range.Text = company.Payments[i - 1].Flight.ToString();
                        companyPaymentsTable.Cell(i + 1, 2).Range.Text = company.Payments[i - 1].Date.ToString();
                        companyPaymentsTable.Cell(i + 1, 3).Range.Text = company.Payments[i - 1].Amount + " دج";
                    }

                    _Word.Paragraph ContractTitlePara;
                    ContractTitlePara = document.Content.Paragraphs.Add();
                    ContractTitlePara.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                    ContractTitlePara.Range.Text = ": لائحة العقود ";
                    ContractTitlePara.Range.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    ContractTitlePara.Range.InsertParagraphAfter();

                    _Word.Table companyContractsTable;
                    companyContractsTable = document.Content.Tables.Add(companyNamePara.Range, contracts.Count + 1, 6);
                    companyContractsTable.Borders.InsideLineStyle = _Word.WdLineStyle.wdLineStyleSingle;
                    companyContractsTable.Borders.OutsideLineStyle = _Word.WdLineStyle.wdLineStyleSingle;
                    List<string> contractsTableTitle = new() { "الرحلة", "المبلغ المستحق", "سعر الفرد", "عدد المدفوعين", "عدد الأفراد", "نوع الغرفة" };

                    for (int i = 1; i <= contractsTableTitle.Count; i++)
                    {
                        companyContractsTable.Cell(1, i).Range.Text = contractsTableTitle[i - 1];
                        companyContractsTable.Cell(1, i).Range.BoldBi = 1;
                    }

                    for (int i = 1; i <= contracts.Count; i++)
                    {
                        companyContractsTable.Cell(i + 1, 1).Range.Text = contracts[i - 1].HotelRoom.FlightHotel.Flight.ToString();
                        companyContractsTable.Cell(i + 1, 2).Range.Text = contracts[i - 1].Price.ToString() + " دج";
                        companyContractsTable.Cell(i + 1, 3).Range.Text = contracts[i - 1].HotelRoom.Price + " دج";
                        companyContractsTable.Cell(i + 1, 4).Range.Text = contracts[i - 1].PaidNumber.ToString();
                        companyContractsTable.Cell(i + 1, 5).Range.Text = contracts[i - 1].RoomsNumber.ToString();
                        companyContractsTable.Cell(i + 1, 6).Range.Text = contracts[i - 1].HotelRoom.Room.ToString();
                    }
                }
                catch (Exception e)
                {
                    Growl.Error(e.Message);
                }
            });
            return word;
        }
        public async Task ExportCompanyWordData(CompanyModel company,List<CompanyContractModel> contracts)
        {
            try
            {
                Error = false;
                var word = await _getCompanyWordFile(company, contracts);

                SaveFileDialog saveFileDialog = new();
                saveFileDialog.Filter = "Word files (*.docx)|*.docx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = DateTime.Now.ToString("dd_MM_yyyy") + "_بيانات_المتعامل" + "_" + company.Name ;
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.Title = "Export Word File To";
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.CreatePrompt = false;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    word.ActiveDocument.SaveAs(saveFileDialog.FileName);
                    word.ActiveDocument.Saved = true;
                    word.ActiveDocument.Close();
                    word.Quit();
                    Growl.Success("تم طباعة عقد بيانات المتعامل بنجاح");
                }
                else
                {
                    word.ActiveDocument.Close(_Word.WdSaveOptions.wdDoNotSaveChanges, null);
                    word.Quit();
                    Growl.Info("تم إلغاء طباعة بيانات المتعامل");
                }

                word = null;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
            }
        }


        private async Task<_Excel.Application> _createReservationExelFile(List<SpotModel> spots, List<GroupModel> groups,List<RoomModel> rooms,List<int> roomsNumbers,List<int> clientsNumbers)
        {
            _Excel.Application excel = new _Excel.Application();

            await Task.Run(() =>
            {
                try
                {
                    var workbook = excel.Application.Workbooks.Add(Type.Missing);
                  
                    var worksheet1 = (_Excel.Worksheet)workbook.Worksheets.get_Item(1);
                    worksheet1.Name = "قائمة التسكين";
                    int tableDepth = 2;
                    int newTableDepth = 0;
                    int tableDivider = 2;
                    for(int i = 0; i < spots.Count; i++)
                    {   
                        if(spots[i].Hosts.Count + tableDepth + 3 > newTableDepth)
                            newTableDepth = spots[i].Hosts.Count + tableDepth + 3;

                        _Excel.Range tableRange = worksheet1.Range[worksheet1.Cells[tableDepth, tableDivider], worksheet1.Cells[spots[i].Hosts.Count + tableDepth + 1, tableDivider]];
                        tableRange.Borders.Color = Color.Black.ToArgb();
                        tableRange.Cells.HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
                        _Excel.Font tableFont = tableRange.Font;
                        tableFont.Size = 13;
                        worksheet1.Cells[tableDepth, tableDivider] = "رقم الغرفة : ";
                        worksheet1.Cells[tableDepth+1, tableDivider] =   "غرفة " + spots[i].Hosts[0].HotelRoom.Room.Type;
                        _Excel.Range titleRange = worksheet1.Range[worksheet1.Cells[tableDepth, tableDivider], worksheet1.Cells[tableDepth + 1, tableDivider]];
                        _Excel.Font titleFont = titleRange.Font;
                        titleFont.Bold = true;
                        _Excel.Range roomRange = worksheet1.Range[worksheet1.Cells[tableDepth+1, tableDivider], worksheet1.Cells[tableDepth + 1, tableDivider]];
                        roomRange.Interior.Color = ColorTranslator.ToOle(ColorTranslator.FromHtml(spots[i].Color));
                        for(int j = 0; j < spots[i].Hosts.Count; j++)
                        {
                            worksheet1.Cells[tableDepth + 2 + j, tableDivider] = spots[i].Hosts[j].Client.FirstName +" " + spots[i].Hosts[j].Client.LastName;
                        }
                        tableDivider += 2;
                        if(i%4 == 0 && i != 0)
                        {
                            tableDepth = newTableDepth;
                            tableDivider = 2;
                        }
                    }
                    tableDivider = 2;
                    tableDepth = newTableDepth != 0 ? newTableDepth: tableDepth;
                    for (int x = 0; x < groups.Count; x++)
                    {
                        System.Windows.Point pos1 = new System.Windows.Point(1, 1);
                        System.Windows.Point pos2 = new System.Windows.Point(1, 1);
                        if (x != 0)
                        {
                            if(groups[x-1].Spots.Count + groups[x].Spots.Count > 5)
                            {
                                tableDepth = newTableDepth;
                                tableDivider = 2;
                            }
                        }

                        for (int i = 0; i < groups[x].Spots.Count; i++)
                        {
                            if (groups[x].Spots[i].Hosts.Count + tableDepth + 3 > newTableDepth)
                                newTableDepth = groups[x].Spots[i].Hosts.Count + tableDepth + 3;

                            if (i == 0)
                            {
                                pos1.X = tableDepth;
                                pos1.Y = tableDivider;
                            }
                            if(i == groups[x].Spots.Count - 1)
                            {
                                pos2.X = tableDepth + groups[x].Spots[i].Hosts.Count + 1;
                                if (tableDivider - 1 > pos2.Y)
                                    pos2.Y = tableDivider;
                              
                            }
                            _Excel.Range tableRange = worksheet1.Range[worksheet1.Cells[tableDepth, tableDivider], worksheet1.Cells[groups[x].Spots[i].Hosts.Count + tableDepth + 1, tableDivider]];
                            
                            tableRange.Borders.Color = Color.Black.ToArgb();
                            tableRange.Cells.HorizontalAlignment = _Excel.XlHAlign.xlHAlignCenter;
                            _Excel.Font tableFont = tableRange.Font;
                            tableFont.Size = 13;
                            worksheet1.Cells[tableDepth, tableDivider] = "رقم الغرفة : ";
                            worksheet1.Cells[tableDepth + 1, tableDivider] = "غرفة " + groups[x].Spots[i].Hosts[0].HotelRoom.Room.Type;
                            _Excel.Range titleRange = worksheet1.Range[worksheet1.Cells[tableDepth, tableDivider], worksheet1.Cells[tableDepth + 1, tableDivider]];
                            _Excel.Font titleFont = titleRange.Font;
                            titleFont.Bold = true;
                            _Excel.Range roomRange = worksheet1.Range[worksheet1.Cells[tableDepth + 1, tableDivider], worksheet1.Cells[tableDepth + 1, tableDivider]];
                            roomRange.Interior.Color = ColorTranslator.ToOle(ColorTranslator.FromHtml(groups[x].Spots[i].Color));
                            for (int j = 0; j < groups[x].Spots[i].Hosts.Count; j++)
                            {
                                worksheet1.Cells[tableDepth + 2 + j, tableDivider] = groups[x].Spots[i].Hosts[j].Client.FirstName + " " + groups[x].Spots[i].Hosts[j].Client.LastName;
                            }
                            tableDivider += 2;
                            if (i % 4 == 0 && i != 0)
                            {
                                tableDepth = newTableDepth;
                                if(tableDivider - 2 > pos2.Y )
                                    pos2.Y = tableDivider - 2;
                                tableDivider = 2;
                            }
                        }
                        _Excel.Range borderRange = worksheet1.Range[worksheet1.Cells[pos1.X, pos1.Y], worksheet1.Cells[pos2.X, pos2.Y]];
                        borderRange.Borders[_Excel.XlBordersIndex.xlEdgeBottom].Color = ColorTranslator.ToOle(ColorTranslator.FromHtml(groups[x].Color));
                        borderRange.Borders[_Excel.XlBordersIndex.xlEdgeBottom].Weight = _Excel.XlBorderWeight.xlThick;
                        borderRange.Borders[_Excel.XlBordersIndex.xlEdgeTop].Color = ColorTranslator.ToOle(ColorTranslator.FromHtml(groups[x].Color));
                        borderRange.Borders[_Excel.XlBordersIndex.xlEdgeTop].Weight = _Excel.XlBorderWeight.xlThick;
                        borderRange.Borders[_Excel.XlBordersIndex.xlEdgeLeft].Color = ColorTranslator.ToOle(ColorTranslator.FromHtml(groups[x].Color));
                        borderRange.Borders[_Excel.XlBordersIndex.xlEdgeLeft].Weight = _Excel.XlBorderWeight.xlThick;
                        borderRange.Borders[_Excel.XlBordersIndex.xlEdgeRight].Color = ColorTranslator.ToOle(ColorTranslator.FromHtml(groups[x].Color));
                        borderRange.Borders[_Excel.XlBordersIndex.xlEdgeRight].Weight = _Excel.XlBorderWeight.xlThick;
                      
                    }
                    tableDivider = 5;
                    tableDepth = newTableDepth != 0 ? newTableDepth : tableDepth;

                    _Excel.Range statRange = worksheet1.Range[worksheet1.Cells[tableDepth, tableDivider], worksheet1.Cells[tableDepth +2 , roomsNumbers.Count + tableDivider + 1]];
                    _Excel.Font stateFont = statRange.Font;
                    stateFont.Size = 13;
                    statRange.Borders.Color = Color.Black.ToArgb();
                    worksheet1.Cells[tableDepth, tableDivider] = "المجموع";
                    ((_Excel.Range)worksheet1.Cells[tableDepth, tableDivider]).Font.Bold = true;
                    worksheet1.Cells[tableDepth+1, tableDivider] = roomsNumbers.Sum();
                    worksheet1.Cells[tableDepth+2, tableDivider] = clientsNumbers.Sum();
                    rooms.Reverse();
                    roomsNumbers.Reverse();
                    clientsNumbers.Reverse();
                    for (int i = 0; i < roomsNumbers.Count; i++)
                    {
                        worksheet1.Cells[tableDepth, tableDivider + 1 + i] = rooms[i].Type;
                        ((_Excel.Range)worksheet1.Cells[tableDepth, tableDivider + 1 + i]).Interior.Color = ColorTranslator.ToOle(ColorTranslator.FromHtml(rooms[i].Color));
                        ((_Excel.Range)worksheet1.Cells[tableDepth, tableDivider + 1 + i]).Font.Bold = true;
                        worksheet1.Cells[tableDepth+1, tableDivider + 1 + i] = roomsNumbers[i];
                        worksheet1.Cells[tableDepth+2, tableDivider + 1 + i] = clientsNumbers[i];
                    }
                    worksheet1.Cells[tableDepth, roomsNumbers.Count + tableDivider + 1] = "نوع الغرف";
                    ((_Excel.Range)worksheet1.Cells[tableDepth, roomsNumbers.Count + tableDivider + 1]).Font.Bold = true;
                    worksheet1.Cells[tableDepth+1, roomsNumbers.Count + tableDivider + 1] = "عدد الغرف";
                    ((_Excel.Range)worksheet1.Cells[tableDepth + 1, roomsNumbers.Count + tableDivider + 1]).Font.Bold = true;
                    worksheet1.Cells[tableDepth+2, roomsNumbers.Count + tableDivider + 1] = "عدد المعتمرين";
                    ((_Excel.Range)worksheet1.Cells[tableDepth + 2, roomsNumbers.Count + tableDivider + 1]).Font.Bold = true;
                    worksheet1.Columns.AutoFit();
                 
                    worksheet1.Activate();

                }
                catch (Exception e)
                {
                    Growl.Error(e.Message);
                }
            });
            return excel;
        }

        public async Task ExportReservationExcelData(List<SpotModel> spots, List<GroupModel> groups,List<RoomModel> rooms, List<int> roomsNumbers, List<int> clientsNumbers,HotelModel hotel,FlightModel flight)
        {
            try
            {
                Error = false;
                var excel = await _createReservationExelFile(spots, groups,rooms,roomsNumbers,clientsNumbers);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl files (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = DateTime.Now.ToString("dd_MM_yyyy")  +"_" + hotel.Name+ "_" + flight.DepartName + "_" + flight.ReturnName+ "_بيانات_التسكين";
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.Title = "Export Excel File To";
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.CreatePrompt = false;
                System.Windows.Window win = System.Windows.Application.Current.MainWindow;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    excel.ActiveWorkbook.SaveCopyAs(saveFileDialog.FileName);
                    excel.ActiveWorkbook.Saved = true;
                    excel.ActiveWorkbook.Close();
                    excel.Quit();
                    Growl.Success("تم طباعة بيانات التسكين بنجاح");
                }
                else
                {
                    excel.ActiveWorkbook.Close(false, saveFileDialog.FileName);
                    excel.Quit();
                    Growl.Info("تم إلغاء طباعة بيانات التسكين");
                }


            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
            }
        }

        public async Task<List<HostModel>?> ImportFlightExcelData(int flightId)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Execl files (*.xlsx)|*.xlsx";
                openFileDialog.Multiselect = false;
                openFileDialog.Title = "Import Excel File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _Excel.Application excel = new _Excel.Application();
                    _Excel.Workbook workbook;
                    _Excel.Worksheet worksheet;
                    workbook = excel.Workbooks.Open(openFileDialog.FileName);
                    worksheet = workbook.Worksheets[1];
                    bool TableEnd = true;
                    int i = 2;
                    List<HostModel> hosts = new List<HostModel>();
                    var companies = await new CompanyServices().GetAllCompanies();
                    var hotels = await new HotelServices().GetAllHotels();
                    var hotelRooms = await new HotelRoomServices().GetHotelsRoomsOfFlight(flightId);
                    int flightHotelId = 0, roomId = 0 ;
                    while (TableEnd)
                    {
                        for (int j = 17; j > 1; j--)
                        {
                            string cellValue = ReadCell(worksheet, i, j);
                            if(j == 17 && string.IsNullOrEmpty(cellValue))
                            {
                                TableEnd = false;
                                break;
                            }
                            switch (j)
                            {
                                case 17:
                                    hosts.Add(new() { Client = new()});
                                    break;
                                case 16:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        hosts[i - 2].CompanyId = companies.Where(c => c.Name == cellValue).Select(c => c.Id).FirstOrDefault();
                                    }
                                    break;
                                case 15:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        hosts[i - 2].Client.FirstName = cellValue; 
                                    }
                                    break;
                                case 14:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        hosts[i - 2].Client.LastName = cellValue;
                                    }
                                    break;
                                case 13:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        hosts[i - 2].Client.PassportNumber = cellValue;
                                    }
                                    break;
                                case 12:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        hosts[i - 2].Client.BirthDate = DateTime.Parse(cellValue);
                                    }
                                    break;
                                case 11:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        if(cellValue == "ذكر")
                                        {
                                            hosts[i - 2].Client.Gender = false;
                                        }
                                        else
                                        {
                                            hosts[i - 2].Client.Gender = true;
                                        }
                                    }
                                    break;
                                case 10:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        hosts[i - 2].Client.Phones = new();
                                        hosts[i - 2].Client.Phones.Add(new() { Number = cellValue });
                                    }
                                    break;
                                case 9:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        flightHotelId = hotelRooms.Where(hr => hr.FlightHotel.Hotel.Name == cellValue).Select(hr => hr.FlightHotelId).FirstOrDefault();

                                        
                                    }
                                    break;
                                case 8:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        roomId = hotelRooms.Where(hr => hr.Room.Type == cellValue).Select(hr => hr.RoomId).FirstOrDefault();
                                        hosts[i - 2].HotelRoomId = hotelRooms.Where(hr => (hr.RoomId == roomId && hr.FlightHotelId == flightHotelId)).FirstOrDefault().Id;
                                    }
                                    break;
                                case 7:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        if (float.Parse(cellValue) == 0)
                                        {
                                            hosts[i - 2].Client.Feed = false;
                                        }
                                        else
                                        {
                                            hosts[i - 2].Client.Feed = true;
                                        }
                                    }
                                    break;
                                case 6:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        hosts[i - 2].FullPrice = float.Parse(cellValue);
                                    }
                                    break;
                                case 5:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        hosts[i - 2].PaidPrice = float.Parse(cellValue);
                                    }
                                    break;
                                case 4:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        hosts[i - 2].Discount = float.Parse(cellValue);
                                    }
                                    break;
                                case 3:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        hosts[i - 2].RemainingPrice = float.Parse(cellValue);
                                    }
                                    break;
                                case 2:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        hosts[i - 2].Client.HealthStatus= cellValue;
                                    }
                                    break;
                                case 1:
                                    if (!string.IsNullOrEmpty(cellValue))
                                    {
                                        hosts[i - 2].Client.Description = cellValue;
                                    }
                                    break;
                            }
                        }
                        i++;
                    }
                    excel.Quit();

                    return hosts;
                }
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
            }
            return null;
        }

        private string ReadCell(_Excel.Worksheet worksheet, int i, int j)
        {
            if (worksheet.Cells[i, j].Value2 != null)
            {

                var format = worksheet.Cells[i, j].NumberFormat;
                if(format == "dd/mm/yyyy")
                {
                    var dt = DateTime.FromOADate((double)(worksheet.Cells[i, j].Value2));
                    return dt.ToLongDateString();

                }
                var value = worksheet.Cells[i, j].Value2;
                return value.ToString();
            }
            else
            {
                return "";
            }
        }

        private async Task<_Excel.Application> _createFlightExelFile(List<HostModel> hosts, FlightModel flight, int hostsTotal, int adultHostsTotal, int iNFHostsTotal, int cHDHostsTotal)
        {
            _Excel.Application excel = new _Excel.Application();
          
            await Task.Run(() =>
            {
                try
                {
                    var workbook = excel.Application.Workbooks.Add(Type.Missing);
                    // Hosts Data
                    var worksheet1 = (_Excel.Worksheet)workbook.Worksheets.get_Item(1);
                    worksheet1.Name = "قائمة المعتمرين";
                    // Hosts Table Titles 
                    string[] titles = {"الرقم", "المتعامل", "اللقب", "الاسم", "رقم جواز السفر", "تاريخ الميلاد", "الجنس", "أرقام الهواتف", "الفندق", "نوع الغرفة", "الاعاشة", "المبلغ المطلوب", "المبلغ المدفوع", "التخفيض", "المبلغ المتبقي", "الحالة الصحية", "الملاحظة" };
                    titles = titles.Reverse().ToArray();
                    for (int i = 1; i < titles.Length + 1; i++)
                    {
                        worksheet1.Cells[1, i] = titles[i - 1];
                    }
                    _Excel.Range titleRange = worksheet1.Range[worksheet1.Cells[1, 1], worksheet1.Cells[1, titles.Length]];
                    titleRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                    _Excel.Font titleFont = titleRange.Font;
                    titleFont.Bold = true;
                    titleFont.Size = 15;

                    // Hosts Table Data
                    for (int i = 2; i < hosts.Count + 2; i++)
                    {
                        worksheet1.Cells[i, 17] = i - 1;
                        worksheet1.Cells[i, 16] = (hosts[i - 2].Company == null) ? "" : hosts[i - 2].Company.Name;
                        worksheet1.Cells[i, 15] = hosts[i - 2].Client.FirstName;
                        worksheet1.Cells[i, 14] = hosts[i - 2].Client.LastName;
                        worksheet1.Cells[i, 13] = hosts[i - 2].Client.PassportNumber;
                        worksheet1.Cells[i, 12] = hosts[i - 2].Client.BirthDate.ToString("yyyy/MM/dd");
                        worksheet1.Cells[i, 11] = hosts[i - 2].Client.Gender ? "أنثى" : "ذكر";
                        worksheet1.Cells[i, 10] = hosts[i - 2].Client.Phones.Count > 0 ? hosts[i - 2].Client.Phones[0].Number : "";
                        worksheet1.Cells[i, 9] = hosts[i - 2].HotelRoom.FlightHotel.Hotel.Name;
                        worksheet1.Cells[i, 8] = hosts[i - 2].HotelRoom.Room.Type;
                        worksheet1.Cells[i, 7] = hosts[i - 2].Client.Feed ? hosts[i - 2].HotelRoom.FlightHotel.Feed : 0;
                        worksheet1.Cells[i, 6] = hosts[i - 2].HotelRoom.Price;
                        worksheet1.Cells[i, 5] = hosts[i - 2].PaidPrice;
                        worksheet1.Cells[i, 4] = hosts[i - 2].Discount;
                        worksheet1.Cells[i, 3].Formula = "=F" + i +"+G" + i + "-E" + i+"-D"+i;
                        worksheet1.Cells[i, 2] = hosts[i - 2].Client.HealthStatus;
                        worksheet1.Cells[i, 1] = hosts[i - 2].Client.Description;
                        _Excel.Range range = worksheet1.get_Range("A" + i, "Q" + i);
                        range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(hosts[i - 2].Client.Color));

                        range.Cells.Style.font.size = 15;
                        range.Borders.Color = System.Drawing.Color.Black.ToArgb();
                    }

                    // Hosts mini table data
                    string[] miniTitles = { "مجموع المبالغ المتبقية", "مجموع المبالغ المطلوبة", "مجموع المبالغ المدفوعة", "مجموع التخفيضات", "مجموع المعتمرين", "مجموع المعتمرين ADT", "مجموع المعتمرين INF", "مجموع المعتمرين CHD" };

                    for (int i = 1; i < miniTitles.Length + 1; i++)
                    {
                        worksheet1.Cells[hosts.Count + 3, i + 8] = miniTitles[i - 1];
                    }
                    _Excel.Range miniTitleRange = worksheet1.Range[worksheet1.Cells[hosts.Count + 3, 9], worksheet1.Cells[hosts.Count + 3, 16]];
                    miniTitleRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                    _Excel.Font miniTitleFont = miniTitleRange.Font;
                    miniTitleFont.Bold = true;
                    miniTitleFont.Size = 15;
                    worksheet1.Range[worksheet1.Cells[hosts.Count + 4, 9], worksheet1.Cells[hosts.Count + 4, 16]].Borders.Color = System.Drawing.Color.Black.ToArgb();

                    worksheet1.Cells[hosts.Count + 4, 9].Formula = "=SUM(C" + 2 + ":C" + (hosts.Count+1) + ")";
                    worksheet1.Cells[hosts.Count + 4, 10].Formula = "=SUM(F" + 2 + ":F" + (hosts.Count + 1) + ")";
                    worksheet1.Cells[hosts.Count + 4, 11].Formula = "=SUM(E" + 2 + ":E" + (hosts.Count + 1) + ")";
                    worksheet1.Cells[hosts.Count + 4, 12].Formula = "=SUM(D" + 2 + ":D" + (hosts.Count + 1) + ")";

                    worksheet1.Cells[hosts.Count + 4, 13].NumberFormat = "@";
                    worksheet1.Cells[hosts.Count + 4, 13].Style.HorizontalAlignment = _Excel.XlHAlign.xlHAlignRight;
                    worksheet1.Cells[hosts.Count + 4, 13] = hostsTotal.ToString() + "/" + flight.Capacity.ToString();
                    worksheet1.Cells[hosts.Count + 4, 14].NumberFormat = "@";
                    worksheet1.Cells[hosts.Count + 4, 14] = adultHostsTotal.ToString() + "/" + flight.Capacity.ToString();
                    worksheet1.Cells[hosts.Count + 4, 15].NumberFormat = "@";
                    worksheet1.Cells[hosts.Count + 4, 15] = iNFHostsTotal.ToString() + "/" + flight.Capacity.ToString();
                    worksheet1.Cells[hosts.Count + 4, 16].NumberFormat = "@";
                    worksheet1.Cells[hosts.Count + 4, 16] = cHDHostsTotal.ToString() + "/" + flight.Capacity.ToString();
                    worksheet1.Columns.AutoFit();

                    // Hotels data
                    // Prepare flighthotels Data
                    List<FlightHotelModel> flightHotels = new();
                    flightHotels = hosts.Select(h => h.HotelRoom.FlightHotel).Distinct().ToList();
                    // Hotels table titles
                    var worksheet2 = (_Excel.Worksheet)workbook.Sheets.Add(After: workbook.Sheets[1]);
                    worksheet2.Name = "قائمة الفنادق";
                    string[] hotelTableTitles = { "الإعاشة", "التقييم", "مسافة البعد عن الحرم", "العنوان", "إسم الفندق", "الرقم" };
                    for (int i = 1; i < hotelTableTitles.Length + 1; i++)
                    {
                        worksheet2.Cells[1, i] = hotelTableTitles[i - 1];
                    }
                    _Excel.Range hotelTableTitlesRange = worksheet2.Range[worksheet2.Cells[1, 1], worksheet2.Cells[1, hotelTableTitles.Length]];
                    hotelTableTitlesRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                    _Excel.Font hotelTableTitlesFont = hotelTableTitlesRange.Font;
                    hotelTableTitlesFont.Bold = true;
                    hotelTableTitlesFont.Size = 15;
                    // Hotels table data
                    for (int i = 2; i < flightHotels.Count + 2; i++)
                    {
                        worksheet2.Cells[i, 6] = i - 1;
                        worksheet2.Cells[i, 5] = flightHotels[i - 2].Hotel.Name;
                        worksheet2.Cells[i, 4] = flightHotels[i - 2].Hotel.Address;
                        worksheet2.Cells[i, 3] = flightHotels[i - 2].Hotel.Distance;
                        worksheet2.Cells[i, 2] = flightHotels[i - 2].Hotel.Rate;
                        worksheet2.Cells[i, 1] = flightHotels[i - 2].Feed;
                        _Excel.Range range = worksheet2.get_Range("A" + i, "F" + i);
                       
                        range.Cells.Style.font.size = 15;
                        range.Borders.Color = System.Drawing.Color.Black.ToArgb();
                    }
                    worksheet2.Columns.AutoFit();

                    // Rooms data
                    // Prepare Rooms Data
                    List<HotelRoomModel> hotelRooms = new();
                    hotelRooms = hosts.Select(h => h.HotelRoom).Distinct().ToList();
                    // Hotels table titles
                    var worksheet3 = (_Excel.Worksheet)workbook.Sheets.Add(After: workbook.Sheets[2]);
                    worksheet3.Name = "قائمة الغرف";
                    string[] roomTableTitles = { "اللون", "الفندق", "السعر", "السعة", "نوع الغرفة", "الرقم" };
                    for (int i = 1; i < roomTableTitles.Length + 1; i++)
                    {
                        worksheet3.Cells[1, i] = roomTableTitles[i - 1];
                    }
                    _Excel.Range roomTableTitlesRange = worksheet3.Range[worksheet3.Cells[1, 1], worksheet3.Cells[1, roomTableTitles.Length]];
                    roomTableTitlesRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                    _Excel.Font roomTableTitlesFont = roomTableTitlesRange.Font;
                    roomTableTitlesFont.Bold = true;
                    roomTableTitlesFont.Size = 15;
                    // Hotels table data
                    for (int i = 2; i < hotelRooms.Count + 2; i++)
                    {
                        worksheet3.Cells[i, 6] = i - 1;
                        worksheet3.Cells[i, 5] = hotelRooms[i - 2].Room.Type;
                        worksheet3.Cells[i, 4] = hotelRooms[i - 2].Room.Capacity;
                        worksheet3.Cells[i, 3] = hotelRooms[i - 2].Price;
                        worksheet3.Cells[i, 2] = hotelRooms[i - 2].FlightHotel.Hotel.Name;
                        worksheet3.Cells[i, 1] = hotelRooms[i - 2].Room.Color;
                        _Excel.Range range = worksheet3.get_Range("A" + i, "F" + i);
                        range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(hotelRooms[i - 2].Room.Color));
                        range.Cells.Style.font.size = 15;
                        range.Borders.Color = System.Drawing.Color.Black.ToArgb();
                    }
                    worksheet3.Columns.AutoFit();

                    // feeds data
                    // Prepare feeds Data
                    List<HostModel> hostsfeed = hosts.Where(h => h.Client.Feed).ToList();
                    // feeds table titles
                    var worksheet4 = (_Excel.Worksheet)workbook.Sheets.Add(After: workbook.Sheets[3]);
                    worksheet4.Name = "قائمة الإعاشة";
                    string[] feedTableTitles = { "العشاء", "الفطور", "سعر الإعاشة", "المعتمر", "الرقم" };
                    for (int i = 1; i < feedTableTitles.Length + 1; i++)
                    {
                        worksheet4.Cells[1, i] = feedTableTitles[i - 1];
                    }
                    _Excel.Range feedTableTitlesRange = worksheet4.Range[worksheet4.Cells[1, 1], worksheet4.Cells[1, feedTableTitles.Length]];
                    feedTableTitlesRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                    _Excel.Font feedTableTitlesFont = feedTableTitlesRange.Font;
                    feedTableTitlesFont.Bold = true;
                    feedTableTitlesFont.Size = 15;
                    // feeds table data
                    _Excel.Range headerRange = worksheet4.get_Range("A1", "E1");
                    headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#FFD700"));

                    for (int i = 2; i < hostsfeed.Count + 2; i++)
                    {
                        worksheet4.Cells[i, 5] = i - 1;
                        worksheet4.Cells[i, 4] = hostsfeed[i - 2].Client.FirstName + " " + hostsfeed[i - 2].Client.LastName;
                        worksheet4.Cells[i, 3] = hostsfeed[i - 2].HotelRoom.FlightHotel.Feed;
                        _Excel.Range range = worksheet4.get_Range("A" + i, "E" + i);
                        range.Cells.Style.font.size = 15;
                        range.Borders.Color = System.Drawing.Color.Black.ToArgb();
                    }
                    worksheet4.Columns.AutoFit();
                    worksheet1.Activate();

                }
                catch (Exception e)
                {
                    Growl.Error(e.Message);
                }
            });
            return excel;
        }

        public async Task ExportFlightExcelData(List<HostModel> hosts,FlightModel flight,int hostsTotal,int adultHostsTotal,int iNFHostsTotal,int cHDHostsTotal)
        {
            try
            {
                Error = false;
                var excel = await _createFlightExelFile(hosts,flight,hostsTotal,adultHostsTotal,iNFHostsTotal,cHDHostsTotal);
                
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl files (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = DateTime.Now.ToString("dd_MM_yyyy")+"_"+flight.DepartName+"_"+flight.ReturnName + "_بيانات";
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.Title = "Export Excel File To";
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.CreatePrompt = false;
                System.Windows.Window win = System.Windows.Application.Current.MainWindow;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    excel.ActiveWorkbook.SaveCopyAs(saveFileDialog.FileName);
                    excel.ActiveWorkbook.Saved = true;
                    excel.ActiveWorkbook.Close();
                    excel.Quit();
                    Growl.Success("تم طباعة بيانات الرحلة بنجاح");
                }
                else
                {
                    excel.ActiveWorkbook.Close(false, saveFileDialog.FileName);
                    excel.Quit();
                    Growl.Info("تم إلغاء طباعة بيانات الرحلة");
                }
                
              
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
            }
        }

        private string DataConverter(HostModel host, string Content)
        {
            string result;
            result = Regex.Replace(Content, @"<FRN>", host.HotelRoom.FlightHotel.Flight.ReturnName, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"<FDN>", host.HotelRoom.FlightHotel.Flight.DepartName, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"<FRD>", host.HotelRoom.FlightHotel.Flight.ReturntDate.ToString("dd/MM/yyyy"), RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"<FDD>", host.HotelRoom.FlightHotel.Flight.DepartDate.ToString("dd/MM/yyyy"), RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"<FRT>", host.HotelRoom.FlightHotel.Flight.ReturnTime.ToString(@"\hh\:mm"), RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"<FDT>", host.HotelRoom.FlightHotel.Flight.DepartTime.ToString(@"\hh\:mm"), RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"<FRI>", host.HotelRoom.FlightHotel.Flight.ReturnItinerary??"", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"<FDI>", host.HotelRoom.FlightHotel.Flight.DepartItinerary??"", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"<FC>", host.HotelRoom.FlightHotel.Flight.Capacity != null ? host.HotelRoom.FlightHotel.Flight.Capacity.ToString() : "", RegexOptions.IgnoreCase); ;
            result = Regex.Replace(result, @"<CFN>", host.Client.FirstName, RegexOptions.IgnoreCase); ;
            result = Regex.Replace(result, @"<CLN>", host.Client.LastName, RegexOptions.IgnoreCase); ;
            result = Regex.Replace(result, @"<CBD>", host.Client.BirthDate.ToString("dd/MM/yyyy"), RegexOptions.IgnoreCase); ;
            result = Regex.Replace(result, @"<CG>", host.Client.Gender ? "أنثى" : "ذكر", RegexOptions.IgnoreCase); ;
            result = Regex.Replace(result, @"<CPN>", host.Client.PassportNumber??"", RegexOptions.IgnoreCase); ;
            result = Regex.Replace(result, @"<CHS>", host.Client.HealthStatus??"", RegexOptions.IgnoreCase); ;
            result = Regex.Replace(result, @"<HN>", host.HotelRoom.FlightHotel.Hotel.Name, RegexOptions.IgnoreCase); ;
            result = Regex.Replace(result, @"<HA>", host.HotelRoom.FlightHotel.Hotel.Address, RegexOptions.IgnoreCase); ;
            result = Regex.Replace(result, @"<HD>", host.HotelRoom.FlightHotel.Hotel.Distance.ToString(), RegexOptions.IgnoreCase); ;
            result = Regex.Replace(result, @"<RT>", host.HotelRoom.Room.Type, RegexOptions.IgnoreCase); ;
            result = Regex.Replace(result, @"\r", "", RegexOptions.IgnoreCase); ;
            return result;
        }

        private async Task<_Word.Application> _getHostWordFile(HostModel host, FlightModel flight)
        {
            var word = new _Word.Application();
            await Task.Run(async () =>
            {
                int errorindex = 0;
                try
                {
                    EnvironmentServices environmentServices = new();
                    var Environment = await environmentServices.GetEnvironment();

                    word.WindowState = _Word.WdWindowState.wdWindowStateNormal;
                    _Word.Document document = word.Documents.Add();

                    foreach (_Word.Section section in document.Sections)
                    {
                        if (!string.IsNullOrEmpty(Environment.HeaderSource))
                        {
                            _Word.HeaderFooter header = section.Headers[_Word.WdHeaderFooterIndex.wdHeaderFooterPrimary];

                            section.PageSetup.HeaderDistance = 1.2f;
                            _Word.Shape hshape = header.Shapes.AddPicture(Environment.HeaderSource);
                        }
                        if (!string.IsNullOrEmpty(Environment.FooterSource))
                        {
                            _Word.HeaderFooter footer = section.Footers[_Word.WdHeaderFooterIndex.wdHeaderFooterPrimary];
                            section.PageSetup.FooterDistance = 1.2f;
                            _Word.Shape fshape = footer.Shapes.AddPicture(Environment.FooterSource, Type.Missing, Type.Missing, 0, -20);
                        }
                    }
                    errorindex++;
                    _Word.Paragraph title;
                    title = document.Content.Paragraphs.Add();
                    title.Range.Font.Size = 20f;
                    var titleTxt = "\nعقد معتمر";
                    title.Range.Text = titleTxt;
                    var titleRange = document.Range(title.Range.Start, title.Range.Start + titleTxt.Length-1);
                    titleRange.Font.BoldBi = 1;
                    titleRange.Font.Underline = _Word.WdUnderline.wdUnderlineSingle;
                    titleRange.Font.SizeBi = 20;
                    titleRange.Font.NameBi = "K Elham";
                    titleRange.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    titleRange.InsertParagraphAfter();

                    float position = (float)(title.Range.get_Information(_Word.WdInformation.wdVerticalPositionRelativeToPage) - 42.5);
                    errorindex++;
                    _Word.Shape shape = document.Shapes.AddShape(5, 237, position, 140, 45);
                    shape.WrapFormat.Type = _Word.WdWrapType.wdWrapBehind;
                    shape.Fill.ForeColor.RGB = ColorTranslator.ToOle(Color.FromArgb(255, 255, 255));
                    shape.Line.Weight = 1.75f;
                    shape.Line.ForeColor.RGB = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                  
                    errorindex++;
                   
                    var content = "\n" + DataConverter(host, Environment.ClientContractContent);
                    var lines = content.Split(new char[] {'\n'}, StringSplitOptions.None).ToList();
                    foreach (var line in lines)
                    {
                        _Word.Paragraph para1;
                        para1 = document.Content.Paragraphs.Add();
                        para1.Range.Text = line;
                        para1.ReadingOrder = _Word.WdReadingOrder.wdReadingOrderRtl;
                        para1.Range.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        _Word.Range contentrange = document.Range(para1.Range.Start, para1.Range.Start + line.Length);
                        contentrange.Underline = _Word.WdUnderline.wdUnderlineNone;
                        contentrange.Font.BoldBi = 1;
                        contentrange.Font.SizeBi = 22;
                        contentrange.InsertParagraphAfter();
                    }
                    errorindex++;


                    _Word.Paragraph para6;
                    para6 = document.Content.Paragraphs.Add();
                    var para6text = "الختم و الإمضاء";
                    para6.Range.Text = para6text;
                    _Word.Range para6range = document.Range(para6.Range.Start, para6.Range.Start + para6text.Length);
                    para6range.Font.NameBi = "Adobe Arabic";
                    para6range.Font.BoldBi = 1;
                    para6range.Font.SizeBi = 22;
                    para6range.Font.Underline = _Word.WdUnderline.wdUnderlineNone;
                    para6range.ParagraphFormat.Alignment = _Word.WdParagraphAlignment.wdAlignParagraphRight;
                    para6range.InsertParagraphAfter();
                    errorindex++;

                }
                catch (Exception e)
                {
                    Growl.Error(e.Message+errorindex.ToString()); 
                }
            });
            return word;
        }

        public async Task ExportHostWordData(HostModel host,FlightModel flight)
        {   
            try
            {
                Error = false;
                var word = await _getHostWordFile(host, flight);

                SaveFileDialog saveFileDialog = new();
                saveFileDialog.Filter = "Word files (*.docx)|*.docx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = DateTime.Now.ToString("dd_MM_yyyy") + "_"+host.Client.LastName+"_"+ host.Client.FirstName + "عقد المعتمر_";
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.Title = "Export Word File To";
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.CreatePrompt = false;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    word.ActiveDocument.SaveAs(saveFileDialog.FileName);
                    word.ActiveDocument.Saved = true;
                    word.ActiveDocument.Close();
                    word.Quit();
                    Growl.Success("تم طباعة عقد المعتمر بنجاح");
                }
                else
                {
                    word.ActiveDocument.Close(_Word.WdSaveOptions.wdDoNotSaveChanges, saveFileDialog.FileName);
                    word.Quit();
                    Growl.Info("تم إلغاء طباعة عقد معتمر");
                }

                word = null;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
            }
        }
    }
}
