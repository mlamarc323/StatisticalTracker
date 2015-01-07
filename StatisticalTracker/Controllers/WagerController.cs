﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
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
            return View();
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload(List<HttpPostedFileBase> uploadFiles)
        {
            try
            {
                var contestMaster = new List<ContestModel>();
                
                foreach (var file in uploadFiles)
                {
                    if (file == null) break;
                    if (file.ContentLength <= 0) continue;
                    string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads"),
                        Path.GetFileName(file.FileName));
                    file.SaveAs(filePath);

                    var contests = ConvertExcelToDataSet(filePath);
                    
                    // Foreach loop all contests in file to add to master collection
                    contestMaster.AddRange(contests);

                    ViewBag.Contests = contestMaster.Count();
                    
                }

                return View("Upload", contestMaster);
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
