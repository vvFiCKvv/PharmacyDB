using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace PhamacyDB
{
    public partial class randomeDataBase : System.Web.UI.Page
    {
        public System.Random rad = new Random(DateTime.Now.Millisecond);
        public char[] nameCharTable = "asdfghjklzxcvbnmqwertyuiopASDFGHJKLZXCVBNMQWERTYUIOP1234567890".ToCharArray();
        public char[] greekNameCharTable = "ασδφγηξκλζχψωβνμ;ςερτυθιοπΑΣΔΦΓΗΞΚΛΖΧΨΩΒΝΜςΕΡΤΥΘΙΟΠ1234567890".ToCharArray();

        public char[] textCharTable = "asdfghjklzxcvbnmqwertyuiop ".ToCharArray();
        public string randomName(int size)
        {
            if (size < 5)
                return randomName(5);
            String res = "";
            for (int i = 0; i < size; i++)
            {
                int irand = rad.Next() % nameCharTable.Count();
                res+= nameCharTable[irand];
            }
            return res;
        }
        public string randomGreekName(int size)
        {
            if (size < 5)
                return randomName(5);
            String res = "";
            for (int i = 0; i < size; i++)
            {
                int irand = rad.Next() % greekNameCharTable.Count();
                res += greekNameCharTable[irand];
            }
            return res;
        }
        public string randomText(int size)
        {
            int wordLength = 0;
            if (size < 15)
                return randomName(15);
            String res = "";
            for (int i = 0; i < size; i++)
            {
                char ch;
                int irand = rad.Next() % textCharTable.Count();
                 ch = textCharTable[irand];
                wordLength++;
                if (wordLength > 7 || ch==' ')
                {
                    wordLength = 0;
                    if (rad.Next() % 6 == 0)
                    {
                        res += ".";
                    }
                    ch = ' ';
                }
                res += ch;
            }
            return res;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rad = new Random(DateTime.Now.Millisecond);
            int categoryCnt = 20;
            int subCategoryCnt = 3;
            int PharamacyChemicalCnt = 2000;
            int PharamacyCommercialCnt = 10000;
            DatabaseDataContext db = new DatabaseDataContext();
         //   db.Log = Response.Output;
           

          //  fillCategory(categoryCnt, db);
            Response.Write("<br>Telos fillCategory");
      //      fillSubCategory(subCategoryCnt, db);
            Response.Write("<br>Telos fillSubCategory");
         
       //     fillPharmacyChemical(PharamacyChemicalCnt, db);
            Response.Write("<br>Telos fillPharmacyChemical");
      //      fillInteractionsPharmacy(db);
            Response.Write("<br>Telos fillInteractionsPharmacy");
      //      fillInteractionsSubCategory(db);
            Response.Write("<br>Telos fillInteractionsSubCategory");
            fillPharamcyCommercial(PharamacyCommercialCnt, db);
            Response.Write("<br>Telos fillPharamcyCommercial");
           
        }

        private void fillPharamcyCommercial(int PharamacyCommercialCnt, DatabaseDataContext db)
        {
            foreach (PharmacyChemical ph in db.PharmacyChemicals)
            {
                for (int i=0; i < 50; i++)
                {
                    if (rad.Next() % 20 == 0)
                        break;
                    try
                    {
                        PharmacyCommercial tmpitem = new PharmacyCommercial();
                        tmpitem.name = randomName(rad.Next() % 20);
                        tmpitem.morph = randomText(rad.Next() % 10);
                        tmpitem.company = randomName(rad.Next() % 20);

                        tmpitem.PharmacyChemical = ph;
                        
                        db.PharmacyCommercials.InsertOnSubmit(tmpitem);
                        db.SubmitChanges();
                        if (rad.Next() % 50 == 0)
                        {
                            PharmacyCommercial tmpitem1 = new PharmacyCommercial();
                            tmpitem1.name = tmpitem.name;
                            tmpitem1.morph = tmpitem.morph;
                            tmpitem1.company = tmpitem.company;

                            tmpitem1.chemicalName = db.PharmacyChemicals.ToList()[rad.Next() % db.PharmacyChemicals.Count()].chemicalName;
                            if ((from phar in db.PharmacyCommercials
                                 where (phar.name == tmpitem1.name &&
                                 phar.morph == tmpitem1.morph &&
                                 phar.company == tmpitem1.company &&
                                 phar.chemicalName == tmpitem1.chemicalName)
                                 select
                                     phar.chemicalName).Count() < 1)
                            {
                                db.PharmacyCommercials.InsertOnSubmit(tmpitem1);
                                db.SubmitChanges();
                            }
                        }

                    }
                    catch (Exception exp)
                    {
                        Response.Write(exp.Message);
                        Response.Write("<br>");
                        Response.Flush();
                    }
                    
                }

            }
        }

        private void fillInteractionsSubCategory(DatabaseDataContext db)
        {

                PharmacyChemical[] pharms = db.PharmacyChemicals.ToArray();
                for (int i = 0; i < pharms.Count(); i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (rad.Next() % 3 != 0)
                            break;
                        try
                        {

                            
                                InteractionPharmacySubCategory tmpitem = new InteractionPharmacySubCategory();
                                tmpitem.chemicalName = pharms[i].chemicalName;
                                tmpitem.subCategoryName = pharms[rad.Next() % pharms.Count()].subCategoryName;
                                tmpitem.comment = randomText(rad.Next() % 100);
                                if ((from inter in db.InteractionPharmacySubCategories
                                     where (inter.chemicalName == tmpitem.chemicalName && inter.subCategoryName == tmpitem.subCategoryName)
                                     select
                                         inter.chemicalName).Count() < 1)
                                {

                                    db.InteractionPharmacySubCategories.InsertOnSubmit(tmpitem);
                                    db.SubmitChanges();

                                }
  
                        }
                        catch (Exception exp)
                        {
                            Response.Write(exp.Message);
                            Response.Write("<br>");
                            Response.Flush();
                        }
                    }


                }
            
        }
        private void fillInteractionsPharmacy(DatabaseDataContext db)
        {

            PharmacyChemical[] pharms = db.PharmacyChemicals.ToArray();
            for (int i = 0; i < pharms.Count(); i++)
            {
                for (int j = 0; j < 30; j++)
                {
                   if (rad.Next() % 20 == 0)
                        break;
                    InteractionPharmacyPharmacy tmpitem = new InteractionPharmacyPharmacy();
                    try
                    {
                            
                            tmpitem.pharmacyname1 = pharms[i].chemicalName;
                            tmpitem.pharmacyname2 = pharms[rad.Next() % pharms.Count()].chemicalName;
                            tmpitem.comment = randomText(rad.Next() % 100);
                            if ((from inter in db.InteractionPharmacyPharmacies
                                 where ((inter.pharmacyname1 == tmpitem.pharmacyname1 && inter.pharmacyname2 == tmpitem.pharmacyname2) || (inter.pharmacyname2 == tmpitem.pharmacyname1 && inter.pharmacyname1 == tmpitem.pharmacyname2))
                                 select
                                     inter.pharmacyname1).Count() < 1)
                            {

                                db.InteractionPharmacyPharmacies.InsertOnSubmit(tmpitem);
                                db.SubmitChanges();
                   
                            }

                    }
                    catch (Exception exp)
                    {
                        Response.Write(exp.Message);
                        Response.Write("<br>");
                        Response.Flush();
                    }

                }

            }

        }

        private void fillPharmacyChemical(int PharamacyChemicalCnt, DatabaseDataContext db)
        {
            for (int i = 0; i < PharamacyChemicalCnt; i++)
            {
                try
                {
                    PharmacyChemical tmpitem = new PharmacyChemical();
                    tmpitem.chemicalName = randomName(rad.Next() % 20);
                    tmpitem.greekName = randomGreekName(rad.Next() % 20);
                    tmpitem.dose = randomText(rad.Next() % 30);
                    tmpitem.indication = randomText(rad.Next() % 400);
                    tmpitem.contraIndication = randomText(rad.Next() % 400);
                    tmpitem.undesirableReactions = randomText(rad.Next() % 400);
                    tmpitem.interactionGeneral = randomText(rad.Next() % 400);
                    tmpitem.subCategoryName = db.SubCategories.ToList()[rad.Next() % db.SubCategories.Count()].subCategoryName;
                    db.PharmacyChemicals.InsertOnSubmit(tmpitem);
                    db.SubmitChanges();
                }
                catch (Exception exp)
                {
                    Response.Write(exp.Message);
                    Response.Write("<br>");
                    Response.Flush();
                }
            }
        }

        private void fillCategory(int categoryCnt, DatabaseDataContext db)
        {
            for (int i = 0; i < categoryCnt; i++)
            {

                Category tmpitem = new Category();
                tmpitem.categoryName = randomName(rad.Next() % 20);
                db.Categories.InsertOnSubmit(tmpitem);

            }
            db.SubmitChanges();
        }
        private void fillSubCategory(int subCategoryCnt, DatabaseDataContext db)
        {
            
            foreach (Category tmpCat in db.Categories)
            {
                for (int i = 0; i < subCategoryCnt;i++ )
                {
                    SubCategory tmpitem = new SubCategory();
                    tmpitem.categoryName = tmpCat.categoryName;
                    tmpitem.subCategoryName = randomName(rad.Next() % 20);
                    db.SubCategories.InsertOnSubmit(tmpitem);
                }
            }
            db.SubmitChanges();
        }
    }
}
