using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class Default1Controller : Controller
    {
        private helloworldEntities db = new helloworldEntities();

        public ActionResult Index()
        {
            return View(db.helloworlds.ToList());
        }


        #region 人員新增
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(helloworlds helloworlds)
        {
            if (ModelState.IsValid)
            {
                db.helloworlds.Add(helloworlds);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(helloworlds);
        }
        #endregion

        #region 人員編輯
        public ActionResult Editindex(string searchString)
        {
            var helloworlds = from m in db.helloworlds
                              select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                helloworlds = helloworlds.Where(s => s.man.Contains(searchString));
            }

            return View(helloworlds);
        }

        public ActionResult Edit(int id = 0)
        {
            helloworlds helloworlds = db.helloworlds.Find(id);
            if (helloworlds == null)
            {
                return HttpNotFound();
            }
            return View(helloworlds);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(helloworlds helloworlds)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(helloworlds).State = EntityState.Modified;
                    db.SaveChanges();
                    var returnData = new { IsSuccess = true };
                    return Content(Newtonsoft.Json.JsonConvert.SerializeObject(returnData), "application/json");
                    //return Json(true, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    var returnData = new { IsSuccess = false };
                    return Content(Newtonsoft.Json.JsonConvert.SerializeObject(returnData), "application/json");
                    //return Json(false, JsonRequestBehavior.AllowGet);
                }   
            }
            return View(helloworlds);
        }
        #endregion

        #region 人員刪除
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if(!id.Equals("")) 
            {
                try
                {
                    var returnData = new { IsSuccess = true };
                    helloworlds helloworlds = db.helloworlds.Find(id);
                    db.helloworlds.Remove(helloworlds);
                    db.SaveChanges();
                    return Content(Newtonsoft.Json.JsonConvert.SerializeObject(returnData), "application/json");
                }
                catch
                {
                    var returnData = new { IsSuccess = false };
                    return Content(Newtonsoft.Json.JsonConvert.SerializeObject(returnData), "application/json");
                }   
            }
            else
            {
                var returnData = new { IsSuccess = false };
                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(returnData), "application/json");
            }
            
            //try
            //{
            //    helloworlds helloworlds = db.helloworlds.Find(id);
            //    db.helloworlds.Remove(helloworlds);
            //    db.SaveChanges();
            //    return Json(true, JsonRequestBehavior.AllowGet);
            //}
            //catch
            //{
            //    return Json(false, JsonRequestBehavior.AllowGet);
            //}
        }
        #endregion
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /*public ActionResult SearchIndex(string id)
        {
            string searchString = id; 
            var helloworlds = from m in db.helloworlds
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                helloworlds = helloworlds.Where(s => s.man.Contains(searchString));
            }

            return View(helloworlds);
        }*/

        #region 人員查詢
        
        public ActionResult SearchIndex(string searchString)
         {
             var helloworlds = from m in db.helloworlds
                              select m;

             if (!String.IsNullOrEmpty(searchString))
             {
                 helloworlds = helloworlds.Where(s => s.man.Contains(searchString));
             }

             return View(helloworlds);
         }
        #endregion

         //[HttpPost]
         //public string SearchIndex(FormCollection fc, string searchString)
         //{
         //    return "<h3> From [HttpPost]SearchIndex: " + searchString + "</h3>";
         //}

         #region 人員介紹/投票
         public ActionResult Introduce(int id = 0)
         {
             helloworlds helloworlds = db.helloworlds.Find(id);
             if (helloworlds == null)
             {
                 return HttpNotFound();
             }
             return View(helloworlds);
         }

         [HttpPost]
         public ActionResult Introduce(helloworlds helloworlds)
         {
             if (helloworlds.skill == null)
             {
                 helloworlds.skill = "1";
             }
             else
             {
                 helloworlds.skill = (int.Parse(helloworlds.skill) + 1).ToString();
             }
             if (ModelState.IsValid)
             {
                 
                 db.Entry(helloworlds).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("rank");
             }
             return View(helloworlds);
         }
         #endregion
         #region 排行榜
         public ActionResult rank(string searchString)
         {
             var helloworlds = from m in db.helloworlds
                               select m;

             if (!String.IsNullOrEmpty(searchString))
             {
                 helloworlds = helloworlds.Where(s => s.man.Contains(searchString));
             }

             return View(helloworlds);
         }
         #endregion

         public ActionResult get_data()
         {
             var helloworlds = from m in db.helloworlds.AsEnumerable()
                               orderby int.Parse(m.skill) descending
                               select m;
             return Json(helloworlds, JsonRequestBehavior.AllowGet);
         }


            /*//Genre
            if (string.IsNullOrEmpty(helloworldGenre))
            {
                
            }
            else
            {
                return View(helloworlds.Where(x => x.Genre == helloworldGenre));
            }*/



        //public ActionResult SearchIndex(string helloworldGenre, string helloworldRating, string searchString)
        //{
        //    //GENRE
        //    var GenreLst = new List<string>();

        //    var GenreQry = from d in db.helloworlds
        //                   orderby d.Genre
        //                   select d.Genre;
        //    GenreLst.AddRange(GenreQry.Distinct());
        //    ViewBag.helloworldGenre = new SelectList(GenreLst);

        //    //Rating
        //    var RatingLst = new List<string>();

        //    var RatingQry = from d in db.helloworlds
        //                    orderby d.Rating
        //                    select d.Rating;
        //    RatingLst.AddRange(RatingQry.Distinct());
        //    ViewBag.helloworldRating = new SelectList(RatingLst);

        //    var helloworlds = from m in db.helloworlds
        //                     select m;

        //    //TITLE
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        helloworlds = helloworlds.Where(s => s.Title.Contains(searchString));
        //    }

        //    if (!string.IsNullOrEmpty(helloworldGenre))
        //    {
        //        helloworlds = helloworlds.Where(x => x.Genre == helloworldGenre);
        //    }

        //    //Rating
        //    if (string.IsNullOrEmpty(helloworldRating))
        //    {
        //        return View(helloworlds);
        //    }
        //    else
        //    {
        //        return View(helloworlds.Where(x => x.Rating == helloworldRating));
        //    }

             public ActionResult Uploadtest()
             {
                 return View();
             }
        }
    }
