using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Configuration;
using System.Data.SqlClient; 
namespace RealEstate
{
    /// <summary>
    /// Summary description for MyService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MyService : System.Web.Services.WebService
    {
        SqlConnection con;
        databasehelper.dbHelper mydb = new databasehelper.dbHelper();
        [WebMethod]
        public string HelloWorld()
        {
         
          
                return "Hello World";
          
        }

        [WebMethod]
        public string userReg(string uname, string uemail, string ucell, string upass, string uaddress, string type, string regid)
        {
            string check = "";
            check = mydb.userreg(uname, uemail, ucell, upass, uaddress,type,regid);
            return check;
        }
        [WebMethod]
        public string loginUser(string username, string pass)
        {
            string check = "";
            check = mydb.loginuser(username, pass);
            return check;
        }
        [WebMethod]
        public string profiledistributer(string name, string address, string phone, string cat, string nameceo, string phoneceo, string cdes, string uid,string pic,string matone,string val)
        {
            string check = "";
            check = mydb.uDistributerProfile(name,address,phone,cat,nameceo,phoneceo,cdes,uid,pic,matone,val);
            return check;

        }
        [WebMethod]
        public string checkCOntractorExistsProfile(string uname)
        {
            string check = "";
            check= mydb.checkprofileexistecont(uname);
            return check;

        }
        [WebMethod]
        public string checkDistributerExistsProfile(string uname)
        {
            string check = "";
            check = mydb.checkprofileexistedist(uname);
            return check;

        }
        [WebMethod]
        public string profilecontractor(string name, string address, string phone, string cat, string nameceo, string phoneceo, string cdes, string uid, string pic,string matone,string val)
        {
            string check = "";
            check = mydb.uContracterProfile(name, address, phone, cat, nameceo, phoneceo, cdes, uid, pic,matone,val);
            return check;

        }
        [WebMethod]
        public string getAllAreas()
        {
            string check = "";
            check = mydb.getAllAreas();
            return check;

        }
        [WebMethod]
        public string getAllMaterials()
        {
            string check = "";
            check = mydb.getmaterials();
            return check;
        }
        [WebMethod]
        public string GetAllContTypes()
        {
            string check = "";
            check = mydb.getconttypee();
            return check;
        }
        [WebMethod]
        public string getinitialcontractorData(string username)
        {
            string check = "";
            check = mydb.getcontractorinitialInfo(username);
            return check;
        }
        [WebMethod]
        public string getinitialdistributerData(string username)
        {
            string check = "";
            check = mydb.getdistributerinitialInfo(username);
            return check;
        }
        [WebMethod]
        public string getcontractorprofile(string id)
        {
            string check = "";
            check = mydb.getcontractorprofile(id);
            return check;

        }
        [WebMethod]
        public string getdistributerprofile(string id)
        {
            string check = "";
            check = mydb.getdistributerprofile(id);
            return check;

        }
        [WebMethod]
        public string savePrevImages(string val, string username, string path)
        {
            string check = "";
            check = mydb.saveprevpics(val, username, path);
            return check;

        }
        [WebMethod]
        public string getPrevWorkImages(string val, string uid)
        {
            string check = "";
            check = mydb.getPrevWorkImages(val, uid);
            return check;

        }
        [WebMethod]
        public string searchDistributer(string keyword)
        {
            string check = "";
            check = mydb.searchdistributer(keyword);
            return check;

        }
        [WebMethod]
        public string searchContractor(string keyword)
        {
            string check = "";
            check = mydb.searchcontractor(keyword);
            return check;

        }
        [WebMethod]
        public string saveContractorRating(string rating_rec, string conid)
        {
            string check = "";
            check = mydb.setratingcontractor(rating_rec, conid);
            return check;

        }
        [WebMethod]
        public string saveDistributerRating(string rating_rec, string conid)
        {
            string check = "";
            check = mydb.setratingdistributer(rating_rec, conid);
            return check;

        }
        [WebMethod]
        public string getcontractortotalrating(string uid)
        {
            string check = "";
            check = mydb.getratings(uid);
            return check;

        }
        [WebMethod]
        public string getdistributertotalrating(string uid)
        {
            string check = "";
            check = mydb.getratingsdis(uid);
            return check;

        }
        [WebMethod]
        public string getallmaps()
        {
            string check = "";
            check = mydb.getallmarlas();
            return check;

        }
        [WebMethod]
        public string getmapsoftype(string type)
        {
            string check = "";
            check = mydb.getmapsoftype(type);
            return check;

        }
        [WebMethod]
        public string getImage(string id)
        {
            string check = "";
            check = mydb.getimage(id);
            return check;
        }
        [WebMethod]
        public string getXyzData(string type, string typetwo)
        {
            string check = "";
            check = mydb.getXYZData(type, typetwo);
            return check;

        }
        [WebMethod]
        public string sendNotification(string fromname, string toid)
        {
            string check = "";
            check = mydb.sendnotificationforcontact(fromname, toid);
            return check;
        }
        [WebMethod]
        public string senNotificationDis(string fromname, string toid)
        {
            string check = "";
            check = mydb.sendnotificationfordis(fromname, toid);
            return check;
        }
        [WebMethod]
        public string getPlots()
        {
            string check = "";
            check = mydb.initialviewplots();
            return check;
            
        }
        [WebMethod]
        public string searchPlots(string keyword)
        {
            string check = "";
            check = mydb.searchplot(keyword);
            return check;

        }
        [WebMethod]
        public string getplotdetails(string id)
        {
            string check = "";
            check = mydb.getplotdetails(id);
            return check;

        }
        [WebMethod]
        public string insertmaps(string mapof, string pic)
        {
            string check = "";
            check = mydb.insertmaps(mapof, pic);
            return check;

        }
        [WebMethod]
        public string insertplots(string pname, string psize, string parea, string pic1, string pic2, string pic3, string pic4, string pprice, string pdes, string pby, string cno)
        {
            string check = "";
            check = mydb.insertplots(pname, psize, parea, pic1, pic2, pic3, pic4, pprice, pdes, pby, cno);
            return check;

        }
        [WebMethod]
        public string getUserdataforUpdate(string val,string uname)
        {
            string check = "";
            if (val.Equals("dis"))
            {

                check = mydb.getuprofiledata(uname);
            }
            else
            {
                check = mydb.getuprofiledatatwo(uname);
            }
            return check;

        }
    }
}
