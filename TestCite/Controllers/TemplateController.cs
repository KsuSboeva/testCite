using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestCite.Dal;
using TestCite.Models;
using TestCite.ViewModels;

namespace TestCite.Controllers
{
    public class TemplateController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Template
        public ActionResult Index(int? id, int? settingID)
        {
            
            var viewModel = new InstructorIndexData();

            viewModel.Templates = db.Templates
                .Include(i => i.Settings);
                

            if (id != null)
            {
                ViewBag.TemplateID = id.Value;
                viewModel.Settings = viewModel.Templates.Where(
                    i => i.Id == id.Value).Single().Settings;
            }

            if (settingID != null)
            {
                ViewBag.Id = settingID.Value;
                // Lazy loading
                //viewModel.Enrollments = viewModel.Settings.Where(
                //    x => x.SettingID == SettingID).Single().Enrollments;
                // Explicit loading
                var selectedSetting = viewModel.Settings.Where(x => x.Id == settingID).Single();
               
                
            }

            return View(viewModel);
        }

        // GET: Template/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        //// GET: Templatesdd/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Templatesdd/Create
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,TemplateId,Name,Description")] Template template)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Templates.Add(template);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(template);
        //}
        // GET: Template/Create
        public ActionResult Create()
        {
            var template = new Template();
            template.Settings = new List<Setting>();
            PopulateAssignedSettingData(template);
            return View();
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TemplateId,Name,Description")]Template template, string[] selectedSettings)
        {
            if (selectedSettings != null)
            {
                template.Settings = new List<Setting>();
                foreach (var setting in selectedSettings)
                {
                    var settingToAdd = db.Settings.Find(int.Parse(setting));
                    template.Settings.Add(settingToAdd);
                    db.SaveChanges();
                }
            }
            if (ModelState.IsValid)
            {
                db.Templates.Add(template);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedSettingData(template);
            return View(template);
        }


        // GET: Template/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template Template = db.Templates
                .Include(i => i.Settings)
                .Where(i => i.Id == id)
                .Single();
            PopulateAssignedSettingData(Template);
            if (Template == null)
            {
                return HttpNotFound();
            }
            return View(Template);
        }

        private void PopulateAssignedSettingData(Template Template)
        {
            System.Diagnostics.Debug.WriteLine("This will be displayed in output window");
            var allSettings = db.Settings;
            var TemplateSettings = new HashSet<int>(Template.Settings.Select(c => c.Id));
            var viewModel = new List<AssignedSettingData>();
            foreach (var setting in allSettings)
            {
                viewModel.Add(new AssignedSettingData
                {
                    SettingID = setting.Id,
                    
                    Title = setting.Name,
                    Assigned = TemplateSettings.Contains(setting.Id)
                });
            }
            
            ViewBag.Example = viewModel;
        }
        // POST: Template/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedSettings)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var TemplateToUpdate = db.Templates
               .Include(i => i.Settings)
               .Where(i => i.Id == id)
               .Single();

            if (TryUpdateModel(TemplateToUpdate, "",
               new string[] { "TemplateId", "Name", "Description"}))
            {
                try
                {
                    

                    UpdateTemplateSettings(selectedSettings, TemplateToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedSettingData(TemplateToUpdate);
            return View(TemplateToUpdate);
        }
        private void UpdateTemplateSettings(string[] selectedSettings, Template TemplateToUpdate)
        {
            if (selectedSettings == null)
            {
                TemplateToUpdate.Settings = new List<Setting>();
                return;
            }

            var selectedSettingsHS = new HashSet<string>(selectedSettings);
            var TemplateSettings = new HashSet<int>
                (TemplateToUpdate.Settings.Select(c => c.Id));
            foreach (var Setting in db.Settings)
            {
                if (selectedSettingsHS.Contains(Setting.Id.ToString()))
                {
                    if (!TemplateSettings.Contains(Setting.Id))
                    {
                        TemplateToUpdate.Settings.Add(Setting);
                    }
                }
                else
                {
                    if (TemplateSettings.Contains(Setting.Id))
                    {
                        TemplateToUpdate.Settings.Remove(Setting);
                    }
                }
            }
        }



        // GET: Template/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template Template = db.Templates.Find(id);
            if (Template == null)
            {
                return HttpNotFound();
            }
            return View(Template);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Template template = db.Templates
              .Where(i => i.Id == id)
              .Single();

            db.Templates.Remove(template);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
