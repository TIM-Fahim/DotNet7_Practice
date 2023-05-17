using B2BCompany.Web.Data;
using B2BCompany.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace B2BCompany.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IdentityUser user = await _userManager.GetUserAsync(User);



            if (user != null)
            {
                string userId = user.Id;
                List<Company> companies = _dbContext.Companys.Where(i => i.UserID == user.Id).ToList();
                return View(companies);
            }

            // Handle the case where the user is not found
            return NotFound();
        }

        public IActionResult Configure()
        {

            List<CompanyConfig> companyConfigs = _dbContext.CompanyConfigs.ToList();

            return View(companyConfigs);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddFieldAsync(CompanyConfig companyConfig)
        //{
        //    IdentityUser user = await _userManager.GetUserAsync(User);


        //    CompanyConfig companyConfigNew = new CompanyConfig()
        //    {
        //        UserID = user.Id,
        //        Name = companyConfig.Name,
        //        Type = companyConfig.Type
        //    };

        //    _dbContext.CompanyConfigs.Add(companyConfig);
        //    _dbContext.SaveChanges();

        //    return RedirectToAction("Index");
        //}


        [HttpPost]
        public ActionResult AddField(CompanyConfig model)
        {
            IdentityUser identityUser = _userManager.GetUserAsync(User).Result;

            CompanyConfig companyConfig = new CompanyConfig()
            {
                UserID = identityUser.Id,
                Name = model.Name,
                Type = model.Type
            };

            _dbContext.CompanyConfigs.Add(companyConfig);
            _dbContext.SaveChanges();
            // Here, you can handle the logic to add the new field to the database or perform any other necessary operations

            return RedirectToAction("Index");
        }


        public IActionResult Create()
        {
            IdentityUser identityUser = _userManager.GetUserAsync(User).Result;

            List<CompanyConfig> Others = _dbContext.CompanyConfigs.Where(i => i.UserID == identityUser.Id).ToList();

            Dictionary<string, string> otherFields = new Dictionary<string, string>();

            foreach (var field in Others)
            {
                otherFields.Add(field.Name, "");
            }
            ViewBag.Others = otherFields;
            return View();
        }


        public ActionResult CreateNew(Company model, Dictionary<string, string> otherFields)
        {
            //model.Id = _dbContext.Companys.Count() + 1;
            model.UserID = _userManager.GetUserAsync(User).Result.Id;

            _dbContext.Add(model);


            _dbContext.SaveChanges();

            _dbContext.Entry(model).GetDatabaseValues();

            List<CompanyConfig> all = _dbContext.CompanyConfigs.Where(i => i.UserID == model.UserID).ToList();

            foreach (var field in otherFields)
            {

                if ((field.Key != "Name") && (field.Key != "Address") && field.Key != "__RequestVerificationToken")
                {
                    int ConfigID = all.Where(i => i.Name == field.Key).FirstOrDefault().CompanyConfigID;

                    ConfigValue configValue = new ConfigValue()
                    {
                        ComapanyConfigID = ConfigID,
                        Name = field.Key,
                        Values = field.Value,
                        ComapnyId = model.Id
                    };
                    _dbContext.ConfigValues.Add(configValue);
                    _dbContext.SaveChanges();

                }
            }

            // Rest of your logic for creating a company

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Company company = _dbContext.Companys.Where(c => c.Id == id).FirstOrDefault();

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Company company = await _dbContext.Companys.FindAsync(id);

            CompanyConfig companyConfig = _dbContext.CompanyConfigs.Where(c => c.UserID == company.UserID).FirstOrDefault();

            if (companyConfig != null)
            {

                List<ConfigValue> configValues = _dbContext.ConfigValues.Where(c => c.ComapnyId == company.Id).ToList();

                foreach (var configValue in configValues)
                {
                    _dbContext.ConfigValues.Remove(configValue);
                }
            }
            _dbContext.Companys.Remove(company);

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<CompanyConfig> Others = _dbContext.CompanyConfigs.Where(i => i.UserID == _userManager.GetUserAsync(User).Result.Id).ToList();

            List<ConfigValue> configValues = _dbContext.ConfigValues.Where(i => i.ComapnyId == id).ToList();

            Company company = _dbContext.Companys.FirstOrDefault(c => c.Id == id);
            if (company != null)
            {
                Dictionary<string, string> otherFields = new Dictionary<string, string>();

                foreach (var field in Others)
                {

                    var temp = configValues.Where(e => e.ComapnyId == id && e.Name == field.Name).Select(e => e.Values).FirstOrDefault();
                    otherFields.Add(field.Name, temp);

                }
                ViewBag.Others = otherFields;
                return View(company);

            }


            return View(company);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<CompanyConfig> Others = _dbContext.CompanyConfigs.Where(i => i.UserID == _userManager.GetUserAsync(User).Result.Id).ToList();

            List<ConfigValue> configValues = _dbContext.ConfigValues.Where(i => i.ComapnyId == id).ToList();

            Company company = _dbContext.Companys.FirstOrDefault(c => c.Id == id);
            if (company != null)
            {
                Dictionary<string, string> otherFields = new Dictionary<string, string>();

                foreach (var field in Others)
                {

                    var temp = configValues.Where(e => e.ComapnyId == id && e.Name == field.Name).Select(e => e.Values).FirstOrDefault();
                    otherFields.Add(field.Name, temp);

                }
                ViewBag.Others = otherFields;
                return View(company);

            }


            return View(company);
        }

        [HttpPost]
        public IActionResult EditSave(Company model, Dictionary<string, string> otherFields)
        {
            // Retrieve the existing company from the database
            Company existingCompany = _dbContext.Companys.FirstOrDefault(c => c.Id == model.Id);
            if (existingCompany == null)
            {
                // Company not found, handle the error accordingly
                return NotFound();
            }

            // Update the properties of the existing company
            existingCompany.Name = model.Name;
            existingCompany.Address = model.Address;

            _dbContext.Update(existingCompany);
            _dbContext.SaveChanges();
            // Update the other fields based on the submitted values



            var existingField = _dbContext.ConfigValues.Where(e => e.ComapnyId == existingCompany.Id).ToList();
            foreach (var kvp in otherFields)
            {
                // Assuming the field names in the dictionary match the property names in the CompanyConfig class


                //if (kvp.Key != "Name"){}
                //else if (kvp.Key != "Id"){}
                //else if (kvp.Key != "UserID"){}
                //else if (kvp.Key != "Address"){ }
                //else if (kvp.Key != "__RequestVerificationToken") { }
                //else
                //{
                //    var Configvalue = existingField.Where(i => i.Name == kvp.Key).FirstOrDefault();

                //    Configvalue.Values = kvp.Value;

                //    _dbContext.Update(Configvalue);
                //    _dbContext.SaveChanges();
                //}

                if (kvp.Key != "Name" && kvp.Key != "Id" && kvp.Key != "UserId" && kvp.Key != "Address" && kvp.Key != "__RequestVerificationToken")
                {
                    var configValue = existingField.FirstOrDefault(i => i.Name == kvp.Key);
                    if (configValue != null)
                    {
                        configValue.Values = kvp.Value;
                        _dbContext.Update(configValue);
                        _dbContext.SaveChanges();
                    }


                }
            }
            return RedirectToAction("Details", new { id = existingCompany.Id });

        }



    }
}
