using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhamacyDB
{
    public partial class deleteEntry : System.Web.UI.Page
    {
        String chemicalName;
        String subCategoryName;
        String categoryName;
        String name;
        String morph;
        String company;
        String prevPage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            parseArguments();
            if (!IsPostBack)
            {
                try
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                catch
                {
                    prevPage="";
                }
                loadMenus();
            }
        }
        private void loadMenus()
        {
            menu.InnerHtml += Util.Menu.createAdminMenu(Request.Url.LocalPath);
            footerMenu.InnerHtml += Util.Menu.createFooterMenu();
        }
        private void parseArguments()
        {
            chemicalName = Request["chemicalName"];
            if (chemicalName == null)
            {
                chemicalName = "";
            }else
            {
                h1Title.InnerText = "Delete " + chemicalName;
            }
            subCategoryName = Request["subCategoryName"];
            if (subCategoryName == null)
            {
                subCategoryName = "";
            }
            else
            {
                h1Title.InnerText = "Delete " + subCategoryName;
            }
            categoryName = Request["categoryName"];
            if (categoryName == null)
            {
                categoryName = "";
            }else
            {
                h1Title.InnerText = "Delete " + categoryName;
            }


            name = Request["name"];
            if (name == null)
            {
                name = "";

            }
            morph = Request["morph"];
            if (morph == null)
            {
                morph = "";

            }
            company = Request["company"];
            if (company == null)
            {
                company = "";

            }
            if (name != "" && company != "" && morph != "")
            {
                h1Title.InnerText = "Delete " + name + " " + company + " " + morph;
            }
        }
        protected void deletePharmacyChemical(String name)
        {
            //deletePharmacyCommercial
            deletePharmacyCommercial(name);
            //deleteInteractionPharmacySubCategory
            deleteInteractionPharmacySubCategory(name);
            //deleteInteractionPharmacyPharmacy
            deleteInteractionPharmacyPharmacy(name);
            //deletePharmacy
            DatabaseDataContext db = new DatabaseDataContext();
            var query = (from phar in db.PharmacyChemicals
                         where (phar.chemicalName == name)
                         select phar);
            if (query.Count() > 0)
            {
                PharmacyChemical tmpItem = query.First();
                db.PharmacyChemicals.DeleteOnSubmit(tmpItem);
                db.SubmitChanges();
            }

        }
        protected void deleteSubCategory(String name)
        {
            //Delete Pharmacies
            DatabaseDataContext db = new DatabaseDataContext();
            var query = (from phar in db.PharmacyChemicals
                         where (phar.subCategoryName == name)
                         select phar);
            foreach (PharmacyChemical tmpItem in query)
            {
                deletePharmacyChemical(tmpItem.chemicalName);
                db.SubmitChanges();
            }

            //Delete SubCategoryInteractions

            var query2 = (from inter in db.InteractionPharmacySubCategories
                         where (inter.subCategoryName == name)
                         select inter);
            foreach(InteractionPharmacySubCategory tmpItem in  query2)
            {
                db.InteractionPharmacySubCategories.DeleteOnSubmit(tmpItem);
                db.SubmitChanges();
            }
            //Delete SubCategory
            var query1 = (from cat in db.SubCategories 
                     where cat.subCategoryName == name
                         select cat);
            if (query1.Count() > 0)
            {
                SubCategory tmpItem1 = query1.First();
                db.SubCategories.DeleteOnSubmit(tmpItem1);
                db.SubmitChanges();
            }
        }
        protected void deleteCategory(String name)
        {
            //Delete SubCategories
            DatabaseDataContext db = new DatabaseDataContext();
            var query = (from cat in db.SubCategories
                         where (cat.categoryName== name)
                         select cat);
            foreach (SubCategory tmpItem in query)
            {
                deleteSubCategory(tmpItem.subCategoryName);
                db.SubmitChanges();
            }
            //Delete SubCategory
            var query1 = (from cat in db.Categories
                          where cat.categoryName == name
                          select cat);
            if (query1.Count() > 0)
            {
                Category tmpItem1 = query1.First();
                db.Categories.DeleteOnSubmit(tmpItem1);
                db.SubmitChanges();
            }
        }
        protected void deleteInteractionPharmacySubCategory(String namePharmacy, String nameSubCategory)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            var query = (from inter in db.InteractionPharmacySubCategories
                         where (inter.chemicalName == namePharmacy && inter.subCategoryName == nameSubCategory)
                         select inter);
            if (query.Count() > 0)
            {
                InteractionPharmacySubCategory tmpItem = query.First();
                db.InteractionPharmacySubCategories.DeleteOnSubmit(tmpItem);
                db.SubmitChanges();
            }
        }
        protected void deleteInteractionPharmacySubCategory(String namePharmacy)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            var query = (from inter in db.InteractionPharmacySubCategories
                         where (inter.chemicalName == namePharmacy)
                         select inter);
            if (query.Count() > 0)
            {
                InteractionPharmacySubCategory tmpItem = query.First();
                db.InteractionPharmacySubCategories.DeleteOnSubmit(tmpItem);
                db.SubmitChanges();
            }
        }
        protected void deleteInteractionPharmacyPharmacy(String name1, String name2)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            var query = (from inter in db.InteractionPharmacyPharmacies
                         where (inter.pharmacyname1 == name1 && inter.pharmacyname2 == name2)
                         select inter);
            if (query.Count() < 1)
            {
                query = (from inter in db.InteractionPharmacyPharmacies
                         where (inter.pharmacyname1 == name2 && inter.pharmacyname2 == name1)
                         select inter);
            }
            if (query.Count() > 0)
            {
                InteractionPharmacyPharmacy tmpItem = query.First();
                db.InteractionPharmacyPharmacies.DeleteOnSubmit(tmpItem);
                db.SubmitChanges();
            }
        }
        protected void deleteInteractionPharmacyPharmacy(String name)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            var query = (from inter in db.InteractionPharmacyPharmacies
                         where (inter.pharmacyname1 == name || inter.pharmacyname2 == name)
                         select inter);
            foreach (InteractionPharmacyPharmacy tmpItem in query)
            {
                db.InteractionPharmacyPharmacies.DeleteOnSubmit(tmpItem);
                db.SubmitChanges();
            }
        }

        protected void deletePharmacyCommercial(String name, String company, String morph)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            var query = (from phar in db.PharmacyCommercials
                         where (phar.name == name && phar.company == company && phar.morph == morph)
                         select phar);
            foreach (PharmacyCommercial tmpItem in query)
            {
                db.PharmacyCommercials.DeleteOnSubmit(tmpItem);
                db.SubmitChanges();
            }
        }
        protected void deletePharmacyCommercial(String cname)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            var query = (from phar in db.PharmacyCommercials
                         where (phar.chemicalName == cname)
                         select phar);
            foreach (PharmacyCommercial tmpItem in query)
            {
                db.PharmacyCommercials.DeleteOnSubmit(tmpItem);
                db.SubmitChanges();
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (chemicalName != "")
            {
                deletePharmacyChemical(chemicalName);
            }
            if (subCategoryName != "")
            {
                deleteSubCategory(subCategoryName);
            }
            if (categoryName != "")
            {
                deleteCategory(categoryName);
            }
            if (name != "" && company != "" && morph != "")
            {
                deletePharmacyCommercial(name, company, morph);
            }
            Response.Write("<script>history.go(-2);</script>");//.Redirect(prevPage);

        }

    }
}
