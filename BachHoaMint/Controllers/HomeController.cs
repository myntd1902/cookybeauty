using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BachHoaMint.Models;
using PagedList;

namespace BachHoaMint.Controllers
{
    public class HomeController : Controller
    {
        QLBHEntities db = new QLBHEntities();
        public ActionResult Index(string currentFilter, int?page ,int MaLoaiSP = 0, string SearchString="")
        {
            if (SearchString != "")
            {
                page = 1;
                var sanPhams = db.SanPhams.Include(s => s.LoaiSP)
                    .Where(x => x.TenSP.ToUpper().Contains(SearchString.ToUpper())).OrderBy(m => m.TenSP);
                int pageSize = sanPhams.Count();
                int pageNumber = (page ?? 1);
                return View(sanPhams.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrenFilter = SearchString;
            if (MaLoaiSP == 0) //lấy hết
            {
                int pageSize = 12;
                int pageNumber = (page ?? 1);
                var sanPhams = db.SanPhams.Include(s => s.LoaiSP).OrderBy(x=>x.TenSP);
                return View(sanPhams.ToPagedList(pageNumber, pageSize)); 
            }
            else //tìm theo loại sản phẩm
            {
                var sanPhams = db.SanPhams.Include(s => s.LoaiSP).Where(x=>x.MaLoaiSP == MaLoaiSP).OrderBy(m => m.TenSP);
                int pageSize = sanPhams.Count();
                int pageNumber = (page ?? 1);
                return View(sanPhams.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}