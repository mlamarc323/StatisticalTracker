using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.ClientServices.Providers;
using System.Web.Helpers;
using System.Web.Mvc;
using Excel;
using StatisticalTracker.Models;

namespace StatisticalTracker.Controllers
{
    [Authorize(Roles = "Over21, Admin")]
    public class WagerController : Controller
    {
        //
        // GET: /Wager/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload()
        {
            Session["Contests"] = string.Empty;
            return View();
        }

        public ActionResult ContestResults(string sort, string sortdir)
        {
            //Session["Contests"] = string.Empty;
            var contestMaster = new List<ContestModel>();
            contestMaster = (List<ContestModel>)Session["Contests"];

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "EntryId":
                        if (sortdir == "ASC")
                        {
                            contestMaster = contestMaster.OrderBy(x => x.EntryId).ToList();
                        }
                        else if (sortdir == "DESC")
                        {
                            contestMaster = contestMaster.OrderByDescending(x => x.EntryId).ToList();
                        }
                        break;

                    case "ContestTitle":
                        if (sortdir == "ASC")
                        {
                            contestMaster = contestMaster.OrderBy(x => x.ContestTitle).ToList();
                        }
                        else if (sortdir == "DESC")
                        {
                            contestMaster = contestMaster.OrderByDescending(x => x.ContestTitle).ToList();
                        }
                        break;

                    case "Date":
                        if (sortdir == "ASC")
                        {
                            contestMaster = contestMaster.OrderBy(x => x.Date).ToList();
                        }
                        else if (sortdir == "DESC")
                        {
                            contestMaster = contestMaster.OrderByDescending(x => x.Date).ToList();
                        }
                        break;

                    case "Score":
                        if (sortdir == "ASC")
                        {
                            contestMaster = contestMaster.OrderBy(x => x.Score).ToList();
                        }
                        else if (sortdir == "DESC")
                        {
                            contestMaster = contestMaster.OrderByDescending(x => x.Score).ToList();
                        }
                        break;

                    case "Winnings":
                        if (sortdir == "ASC")
                        {
                            contestMaster = contestMaster.OrderBy(x => x.Winnings).ToList();
                        }
                        else if (sortdir == "DESC")
                        {
                            contestMaster = contestMaster.OrderByDescending(x => x.Winnings).ToList();
                        }
                        break;

                    case "EntryFee":
                        if (sortdir == "ASC")
                        {
                            contestMaster = contestMaster.OrderBy(x => x.EntryFee).ToList();
                        }
                        else if (sortdir == "DESC")
                        {
                            contestMaster = contestMaster.OrderByDescending(x => x.EntryFee).ToList();
                        }
                        break;
                }

            }

            return View(contestMaster);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ContestResults()
        {
            try
            {
                //Clears "Contests" session
                Session["Contests"] = string.Empty;
                
                var files = Request.Files;
                var contestMaster = new List<ContestModel>();

                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i] == null) break;
                    if (files[i].ContentLength <= 0) continue;
                    string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads"),
                        Path.GetFileName(files[i].FileName));
                    
                    files[i].SaveAs(filePath);

                    var contests = ConvertExcelToDataSet(filePath);

                    // Foreach loop all contests in file to add to master collection
                    contestMaster.AddRange(contests);

                    // Sets session to "Contest" collection
                    Session["Contests"] = contestMaster;

                    ViewBag.Contests = contestMaster.Count();

                }

                

                return View("ContestResults", contestMaster);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Files are unable to be uploaded.  Please try again.";
                return View("Upload");
            }
        }
        
        private List<ContestModel> ConvertExcelToDataSet(string fileName)
        {
            DataSet data;
            var contestCollection = new List<ContestModel>();
            try
            {
                using (var stream = new FileStream(fileName, FileMode.Open))
                {
                    if (fileName.EndsWith(".xls") || fileName.EndsWith(".xslx"))
                    {
                        using (var excelReader = ExcelReaderFactory.CreateBinaryReader(stream))
                        {
                            excelReader.IsFirstRowAsColumnNames = true;
                            data = excelReader.AsDataSet();
                            contestCollection = ConvertDatasetToCollection(data);
                        }
                    }
                    else
                    {
                        using (var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                        {
                            excelReader.IsFirstRowAsColumnNames = true;
                            data = excelReader.AsDataSet();
                            contestCollection = ConvertDatasetToCollection(data);
                        }
                    }
                }
            }
            catch
            {
                ViewBag.ErrorMessage = "You have some invalid data in your dataset!";
                return new List<ContestModel>();
            }
            return contestCollection;
        }

        private static List<ContestModel> ConvertDatasetToCollection(DataSet data)
        {
            var contestCollection = new List<ContestModel>();

            for (var j = 0; j < data.Tables.Count; j++)
            {
                var numRows = data.Tables[j].Rows.Count;
                for (var i = 0; i < numRows; i++)
                {
                    // Reads and sets values of rows within selected table
                    if (data.Tables[j].Rows[i]["Score"].ToString() == string.Empty ||
                        data.Tables[j].Rows[i][13].ToString() == "voided" ||
                        data.Tables[j].Rows[i][13].ToString() == "endedunmatched") continue;
                    var entryId = data.Tables[j].Rows[i].Field<string>("Entry Id");
                    var sport = data.Tables[j].Rows[i].Field<string>("Sport");
                    var date = Convert.ToDateTime(data.Tables[j].Rows[i].Field<DateTime>("Date").Date);
                    var title = data.Tables[j].Rows[i].Field<string>("Title");
                    var salary = data.Tables[j].Rows[i].Field<string>("SalaryCap");
                    var score = data.Tables[j].Rows[i]["Score"] == null
                        ? 0.00
                        : data.Tables[j].Rows[i].Field<double>("Score");
                    var position = data.Tables[j].Rows[i]["Position"] == null
                        ? 0.00
                        : data.Tables[j].Rows[i].Field<double>("Position");
                    var entries = data.Tables[j].Rows[i]["Entries"] == null
                        ? 0.00
                        : data.Tables[j].Rows[i].Field<double>("Entries");
                    var opponent = data.Tables[j].Rows[i].Field<string>("Opponent");
                    var entryFee = Convert.ToDouble(data.Tables[j].Rows[i].Field<double>("Entry ($)"));
                    var winnings = Convert.ToDouble(data.Tables[j].Rows[i].Field<double>("Winnings ($)"));
                    var link = data.Tables[j].Rows[i].Field<string>("Link");

                    // Adds values of selected table to master collection
                    contestCollection.Add(new ContestModel()
                    {
                        EntryId = entryId,
                        Sport = sport,
                        Date = date,
                        ContestTitle = title,
                        SalaryCap = salary,
                        Score = score,
                        Opponent = opponent,
                        Position = position,
                        Entries = entries,
                        EntryFee = entryFee,
                        Winnings = winnings,
                        Link = link
                    });
                }
            }

            // Returns master collection
            return contestCollection;
        }
    }
}
