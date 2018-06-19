using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;
using System.Net;
using System.Drawing;
using System.Web.Script.Serialization;
namespace RealEstate.databasehelper
{

    public class dbHelper
    {
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlConnection con2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        string pathh = "http://sidalitechnologies.somee.com/ImageStorage/";
        string pathh2 = "http://sidalitechnologies.somee.com/ImageStorage/";
        string pathh3 = "http://sidalitechnologies.somee.com/ImageStorage/";
        string pathh4 = "http://sidalitechnologies.somee.com/ImageStorage/";

        public long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }
        public string userreg(string uname, string uemail, string ucell, string upass, string uaddress, string type, string regid)
        {
            string finalstring = "";
            string querry2 = "select * from uReg where uName='" + uname + "'";
            SqlCommand cmd2 = new SqlCommand(querry2, con2);
            con2.Open();
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                finalstring = "UserExists";

            }
            else
            {
                con2.Close();
                dr.Close();
                string querry = "insert into uReg(uName,uEmail,uCell,uPass,uAddress,uType,regid)values('" + uname + "','" + uemail + "','" + ucell + "','" + upass + "','" + uaddress + "','" + type + "','" + regid + "')";
                SqlCommand cmd = new SqlCommand(querry, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a != 0)
                {
                    finalstring = "Inserted";

                }
                else
                {
                    finalstring = "NotInserted";
                }
            }

            return finalstring;
        }
        public string loginuser(string username, string pass)
        {
            string finalstring = "";
            string querry = "select * from uReg where uName='" + username + "' AND uPass='" + pass + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                finalstring = dr["uType"].ToString();
            }
            else
            {
                finalstring = "NotLogin";
            }
            return finalstring;
        }
        public string checkprofileexistecont(string uname)
        {
            string finalstring = "";
            con.Close();
            string id = "";
            string querry = "select * from uContractor where cUser=(select Id from uReg where uName='" + uname + "')";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                id = dr["Id"].ToString();
                finalstring = "Exists" + "," + id;

            }
            else
            {
                finalstring = "NotExists";
            }
            dr.Close();
            con.Close();
            return finalstring;
        }
        public string checkprofileexistedist(string uname)
        {
            string finalstring = "";
            con.Close();
            string id = "";
            string querry = "select * from uDistributer where cUser=(select Id from uReg where uName='" + uname + "')";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                id = dr["Id"].ToString();
                finalstring = "Exists" + "," + id;

            }
            else
            {
                finalstring = "NotExists";
            }
            dr.Close();
            con.Close();
            return finalstring;
        }
        public string uDistributerProfile(string name, string address, string phone, string cat, string nameceo, string phoneceo, string cdes, string uid, string pic, string matone, string val)
        {
            string finalstring = "";
            if (val.Equals("yes"))
            {
                string imagePath = "";
                imagePath = SaveImage(pic, "xyz" + UnixTimeNow());
                pathh += "" + "xyz" + UnixTimeNow() + ".jpeg";
                string querry2 = "update uDistributer set cName='" + name + "',cAddress='" + address + "',cPhone='" + phone + "',cArea='" + cat + "',cNameCEO='" + nameceo + "',cPhoneCEO='" + phoneceo + "',cDescription='" + cdes + "',cPic='" + pathh + "',cMatOne='" + matone + "' where cUser=(select Id from uReg where uName='" + uid + "')";
                SqlCommand cmd2 = new SqlCommand(querry2, con2);
                con2.Open();
                int a = cmd2.ExecuteNonQuery();
                if (a != 0)
                {
                    finalstring = "Updated";
                }
                else
                {
                    finalstring = "NotUpdated";
                }
            }
            else
            {
                con.Close();
                string result = checkolderProfile("dis",uid);
                if (result.Equals("ExistProfile"))
                {
                    finalstring = result;
                }
                else
                {
                    string imagePath = "";
                    imagePath = SaveImage(pic, "xyz" + UnixTimeNow());
                    pathh += "" + "xyz" + UnixTimeNow() + ".jpeg";
                    string querry = "insert into uDistributer(cName,cAddress,cPhone,cArea,cNameCEO,cPhoneCEO,cDescription,cUser,cPic,cMatOne)values('" + name + "','" + address + "','" + phone + "',(select Id from mAreas where areaName='" + cat + "'),'" + nameceo + "','" + phoneceo + "','" + cdes + "',(select Id from uReg where uName='" + uid + "'),'" + pathh + "','" + matone + "')";
                    SqlCommand cmd = new SqlCommand(querry, con);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    if (a != 0)
                    {
                        finalstring = "Inserted";

                    }
                    else
                    {
                        finalstring = "NotInserted";
                    }
                }
            }
            return finalstring;
        }
        public string uContracterProfile(string name, string address, string phone, string cat, string nameceo, string phoneceo, string cdes, string uid, string pic, string matone, string val)
        {
            string finalstring = "";
            if (val.Equals("yes"))
            {
                string imagePath = "";
                imagePath = SaveImage(pic, "xyz" + UnixTimeNow());
                pathh += "" + "xyz" + UnixTimeNow() + ".jpeg";
                string querry2 = "update uContractor set cName='" + name + "',cAddress='" + address + "',cPhone='" + phone + "',cArea='" + cat + "',cNameCEO='" + nameceo + "',cPhoneCEO='" + phoneceo + "',cDescription='" + cdes + "',cPic='" + pathh + "',cMatOne='" + matone + "' where cUser=(select Id from uReg where uName='" + uid + "')";
                SqlCommand cmd2 = new SqlCommand(querry2, con2);
                con2.Open();
                int a = cmd2.ExecuteNonQuery();
                if (a != 0)
                {
                    finalstring = "Updated";
                }
                else
                {
                    finalstring = "NotUpdated";
                }
            }
            else
            {
                con.Close();
                string result = checkolderProfile("con",uid);
                if (result.Equals("ExistProfile"))
                {
                    finalstring = result;
                }
                else
                {
                    string imagePath = "";
                    imagePath = SaveImage(pic, "xyz" + UnixTimeNow());
                    pathh += "" + "xyz" + UnixTimeNow() + ".jpeg";

                    string querry = "insert into uContractor(cName,cAddress,cPhone,cArea,cNameCEO,cPhoneCEO,cDescription,cUser,cPic,cMatOne)values('" + name + "','" + address + "','" + phone + "',(select Id from mAreas where areaName='" + cat + "'),'" + nameceo + "','" + phoneceo + "','" + cdes + "',(select Id from uReg where uName='" + uid + "'),'" + pathh + "','" + matone + "')";
                    SqlCommand cmd = new SqlCommand(querry, con);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    if (a != 0)
                    {
                        finalstring = "Inserted";

                    }
                    else
                    {
                        finalstring = "NotInserted";
                    }
                }
            }
            return finalstring;
        }
        private string checkolderProfile(string val,string uname)
        {
            string finalstring = "";
            con2.Close();

            if (val.Equals("con"))
            {
                string querry = "select COUNT(Id) from uContractor where cUser=(select Id from uReg where uName='" + uname + "')";
                SqlCommand cmd = new SqlCommand(querry, con2);
                con2.Open();
                int tres = (Int32)cmd.ExecuteScalar();
                if (tres > 0)
                {
                    finalstring = "ExistProfile";
                }
                else
                {
                    finalstring = "NotExists";

                }
            }
            else
            {
                string querry = "select COUNT(Id) from uDistributer where cUser=(select Id from uReg where uName='" + uname + "')";
                SqlCommand cmd = new SqlCommand(querry, con2);
                con2.Open();
                int tres = (Int32)cmd.ExecuteScalar();
                if (tres > 0)
                {
                    finalstring = "ExistProfile";
                }
                else
                {
                    finalstring = "NotExists";

                }

            }
            return finalstring;
        }
        public string getAllAreas()
        {
            string finalstring = "";
            string areaname = "";
            con.Close();
            string querry = "select * from mAreas";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int a = 0;
            while (dr.Read())
            {
                areaname = dr["areaName"].ToString();
                if (a == 0)
                {
                    finalstring = areaname;
                }
                else
                {
                    finalstring += "," + areaname;
                }
                a++;
            }

            return finalstring;

        }

        public static Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }
        public string SaveImage(string ImgStr, string ImgName)
        {
            Image image = Base64ToImage(ImgStr);
            String path = HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path

            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = ImgName + ".jpeg";

            //set the image path
            string imgPath = Path.Combine(path, imageName);

            image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);

            return imgPath;
        }
        public string getmaterials()
        {
            string finalstring = "";
            string matname = "";
            con.Close();
            string querry = "select * from Materials";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int c = 0;
            while (dr.Read())
            {
                matname = dr["mat_name"].ToString();
                if (c == 0)
                {
                    finalstring = matname;
                }
                else
                {
                    finalstring += "," + matname;
                }
                c++;


            }
            return finalstring;
        }
        public string getconttypee()
        {
            string finalstring = "";
            string matname = "";
            con.Close();
            string querry = "select * from ContractorType";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int c = 0;
            while (dr.Read())
            {
                matname = dr["c_type"].ToString();
                if (c == 0)
                {
                    finalstring = matname;
                }
                else
                {
                    finalstring += "," + matname;
                }
                c++;


            }
            return finalstring;
        }
        public string getcontractorinitialInfo(string username)
        {
            string finalstring = "NotRead";
            con.Close();
            string name = "";
            string address = "";
            string description = "";
            string picpath = "";
            string id = "";
            string querry = "select * from uContractor where cUser NOT IN (SELECT Id FROM uReg where uName='" + username + "')";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int c = 0;
            while (dr.Read())
            {
                name = dr["cName"].ToString();
                address = dr["cAddress"].ToString();
                description = dr["cDescription"].ToString();
                picpath = dr["cPic"].ToString();
                id = dr["Id"].ToString();
                if (c == 0)
                {
                    finalstring = name + "," + address + "," + description + "," + picpath + "," + id;
                }
                else
                {
                    finalstring += "|" + name + "," + address + "," + description + "," + picpath + "," + id;
                }
                c++;
            }

            dr.Close();
            con.Close();
            return finalstring;
        }
        public string getdistributerinitialInfo(string username)
        {
            string finalstring = "";
            con.Close();
            string name = "";
            string address = "";
            string description = "";
            string picpath = "";
            string id = "";
            string querry = "select * from uDistributer where cUser NOT IN (SELECT Id FROM uReg where uName='" + username + "')";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int c = 0;
            while (dr.Read())
            {
                name = dr["cName"].ToString();
                address = dr["cAddress"].ToString();
                description = dr["cDescription"].ToString();
                picpath = dr["cPic"].ToString();
                id = dr["Id"].ToString();
                if (c == 0)
                {
                    finalstring = name + "," + address + "," + description + "," + picpath + "," + id;
                }
                else
                {
                    finalstring += "|" + name + "," + address + "," + description + "," + picpath + "," + id;
                }
                c++;
            }

            dr.Close();
            con.Close();
            return finalstring;
        }
        public string getcontractorprofile(string id)
        {
            con.Close();
            string finalstring = "";
            string cname = "";
            string caddress = "";
            string cphone = "";
            string carea = "";
            string nameceo = "";
            string phoneceo = "";
            string cuser = "";
            string cpic = "";
            string cdesc = "";
            string cmatone = "";

            string querry = "select * from uContractor where Id='" + id + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cname = dr["cName"].ToString();
                caddress = dr["cAddress"].ToString();
                cphone = dr["cPhone"].ToString();
                carea = dr["cArea"].ToString();
                nameceo = dr["cNameCEO"].ToString();
                phoneceo = dr["cPhoneCEO"].ToString();
                cdesc = dr["cDescription"].ToString();
                cuser = dr["cUser"].ToString();
                cpic = dr["cPic"].ToString();
                cmatone = dr["cMatOne"].ToString();
                finalstring = cname + "," + caddress + "," + cphone + "," + carea + "," + nameceo + "," + phoneceo + "," + cdesc + "," + cuser + "," + cpic + "," + cmatone;

            }
            else
            {
                finalstring = "NotRead";
            }
            return finalstring;

        }
        public string getdistributerprofile(string id)
        {
            con.Close();
            string finalstring = "";
            string cname = "";
            string caddress = "";
            string cphone = "";
            string carea = "";
            string nameceo = "";
            string phoneceo = "";
            string cuser = "";
            string cpic = "";
            string cdesc = "";
            string cmatone = "";

            string querry = "select * from uDistributer where Id='" + id + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cname = dr["cName"].ToString();
                caddress = dr["cAddress"].ToString();
                cphone = dr["cPhone"].ToString();
                carea = dr["cArea"].ToString();
                nameceo = dr["cNameCEO"].ToString();
                phoneceo = dr["cPhoneCEO"].ToString();
                cdesc = dr["cDescription"].ToString();
                cuser = dr["cUser"].ToString();
                cpic = dr["cPic"].ToString();
                cmatone = dr["cMatOne"].ToString();
                finalstring = cname + "," + caddress + "," + cphone + "," + carea + "," + nameceo + "," + phoneceo + "," + cdesc + "," + cuser + "," + cpic + "," + cmatone;

            }
            else
            {
                finalstring = "NotRead";
            }
            return finalstring;

        }
        public string saveprevpics(string val, string username, string path)
        {
            string finalstring = "";
            con.Close();
            string imagePath = "";
            if (val.Equals("dis"))
            {
                imagePath = SaveImage(path, "xyz" + UnixTimeNow());
                pathh += "" + "xyz" + UnixTimeNow() + ".jpeg";
                string querry = "insert into PrevWork(userid,pic)values((select Id from uReg where uName='" + username + "'),'" + pathh + "')";
                SqlCommand cmd = new SqlCommand(querry, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a != 0)
                {
                    finalstring = "Inserted";
                }
                else
                {
                    finalstring = "NotInserted";
                }
                con.Close();
            }
            else
            {
                imagePath = SaveImage(path, "xyz" + UnixTimeNow());
                pathh += "" + "xyz" + UnixTimeNow() + ".jpeg";
                string querry = "insert into PrevWorkTwo(userid,pic)values((select Id from uReg where uName='" + username + "'),'" + pathh + "')";
                SqlCommand cmd = new SqlCommand(querry, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a != 0)
                {
                    finalstring = "Inserted";
                }
                else
                {
                    finalstring = "NotInserted";
                }

                con.Close();
            }


            return finalstring;
        }
        public string getPrevWorkImages(string val, string uid)
        {
            con.Close();
            string finalstring = "";
            string picpaths = "";
            string userid = "";
            if (val.Equals("con"))
            {
                string querry = "select * from PrevWorkTwo where userid='" + uid + "'";
                SqlCommand cmd = new SqlCommand(querry, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int count = 0;
                while (dr.Read())
                {
                    picpaths = dr["pic"].ToString();
                    userid = dr["userid"].ToString();
                    if (count == 0)
                    {
                        finalstring = picpaths + "," + userid;

                    }
                    else
                    {
                        finalstring += "|" + picpaths + "," + userid;
                    }
                    count++;
                }
                dr.Close();
                con.Close();

            }
            else
            {
                string querry = "select * from PrevWork where userid='" + uid + "'";
                SqlCommand cmd = new SqlCommand(querry, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int count = 0;
                while (dr.Read())
                {
                    picpaths = dr["pic"].ToString();
                    userid = dr["userid"].ToString();


                    if (count == 0)
                    {
                        finalstring = picpaths + "," + userid;

                    }
                    else
                    {
                        finalstring += "|" + picpaths + "," + userid;
                    }
                    count++;
                }
                dr.Close();
                con.Close();

            }
            return finalstring;

        }
        public string searchdistributer(string keyword)
        {
            con.Close();
            string finalstring = "";
            string name = "";
            string address = "";
            string description = "";
            string picpath = "";
            string id = "";
            string querry = "select * from uDistributer where cMatOne LIKE '%" + keyword + "%'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int c = 0;
            while (dr.Read())
            {
                name = dr["cName"].ToString();
                address = dr["cAddress"].ToString();
                description = dr["cDescription"].ToString();
                picpath = dr["cPic"].ToString();
                id = dr["Id"].ToString();
                if (c == 0)
                {
                    finalstring = name + "," + address + "," + description + "," + picpath + "," + id;
                }
                else
                {
                    finalstring += "|" + name + "," + address + "," + description + "," + picpath + "," + id;
                }
                c++;
            }
            return finalstring;

        }
        public string searchcontractor(string keyword)
        {
            con.Close();
            string finalstring = "";
            string name = "";
            string address = "";
            string description = "";
            string picpath = "";
            string id = "";
            string querry = "select * from uContractor where cArea LIKE '%" + keyword + "%'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int c = 0;
            while (dr.Read())
            {
                name = dr["cName"].ToString();
                address = dr["cAddress"].ToString();
                description = dr["cDescription"].ToString();
                picpath = dr["cPic"].ToString();
                id = dr["Id"].ToString();
                if (c == 0)
                {
                    finalstring = name + "," + address + "," + description + "," + picpath + "," + id;
                }
                else
                {
                    finalstring += "|" + name + "," + address + "," + description + "," + picpath + "," + id;
                }
                c++;
            }
            return finalstring;

        }
        public string setratingcontractor(string rating_rec, string conid)
        {
            string finalstring = "";
            int trating;
            con.Close();
            con2.Close();
            string querry = "select TOP 1 * from RatingContractor ORDER BY Id Desc";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                trating = Convert.ToInt32(dr["rating_total"].ToString());
                trating = trating + 1;
            }
            else
            {
                trating = 1;
            }
            dr.Close();
            con.Close();
            string qerry2 = "insert into RatingContractor(rating_reciv,rating_from,rating_total)values('" + rating_rec + "','" + conid + "','" + trating + "')";
            SqlCommand cmd2 = new SqlCommand(qerry2, con2);
            con2.Open();
            int a = cmd2.ExecuteNonQuery();
            if (a != 0)
            {
                finalstring = "Inserted";

            }
            else
            {

                finalstring = "NotInserted";
            }
            con2.Close();
            return finalstring;
        }
        public string setratingdistributer(string rating_rec, string conid)
        {
            string finalstring = "";
            int trating;
            con.Close();
            con2.Close();
            string querry = "select TOP 1 * from RatingDistributer ORDER BY Id Desc";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                trating = Convert.ToInt32(dr["rating_total"].ToString());
                trating = trating + 1;
            }
            else
            {
                trating = 1;
            }
            dr.Close();
            con.Close();
            string qerry2 = "insert into RatingDistributer(rating_reciv,rating_from,rating_total)values('" + rating_rec + "','" + conid + "','" + trating + "')";
            SqlCommand cmd2 = new SqlCommand(qerry2, con2);
            con2.Open();
            int a = cmd2.ExecuteNonQuery();
            if (a != 0)
            {
                finalstring = "Inserted";

            }
            else
            {

                finalstring = "NotInserted";
            }
            con2.Close();
            return finalstring;
        }
        public string getratings(string uid)
        {
            string finalstring = "";
            con.Close();
            string querry = "select count(*) from RatingContractor where rating_from='" + uid + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            int totlrating = (Int32)cmd.ExecuteScalar();
            if (totlrating > 0)
            {
                finalstring = "" + totlrating;
            }
            else
            {
                totlrating = 1;
                finalstring = "" + totlrating;
            }

            return finalstring;

        }
        public string getratingsdis(string uid)
        {
            string finalstring = "";
            con.Close();
            string querry = "select count(*) from RatingDistributer where rating_from='" + uid + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            int totlrating = (Int32)cmd.ExecuteScalar();
            if (totlrating > 0)
            {
                finalstring = "" + totlrating;
            }
            else
            {
                totlrating = 1;
                finalstring = "" + totlrating;
            }

            return finalstring;

        }
        public string getallmarlas()
        {
            con.Close();
            string finalstring = "";
            string id = "1";
            string mapof = "";

            string querry = "select DISTINCT map_of from Maps ORDER BY map_of DESC";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                // id = dr["Id"].ToString();
                mapof = dr["map_of"].ToString();
                if (count == 0)
                {
                    finalstring = mapof + "," + id;

                }
                else
                {
                    finalstring += "|" + mapof + "," + id;
                }
                count++;
            }
            dr.Close();
            con.Close();
            return finalstring;
        }
        public string getmapsoftype(string type)
        {
            con.Close();
            string finalstring = "";
            string id = "";
            string path = "";

            string querry = "select * from Maps where map_of='" + type + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int c = 0;
            while (dr.Read())
            {
                id = dr["Id"].ToString();
                path = dr["map_path"].ToString();

                if (c == 0)
                {
                    finalstring = id + "," + path;

                }
                else
                {
                    finalstring += "|" + id + "," + path;
                }
                c++;

            }
            dr.Close();
            con.Close();
            return finalstring;

        }
        public string getimage(string id)
        {
            con.Close();
            string finalstring = "";
            string querry = "select map_path from Maps where Id='" + id + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                finalstring = dr["map_path"].ToString();

            }
            else
            {
                finalstring = "NotRead";
            }
            return finalstring;
        }
        public string getXYZData(string value, string type)
        {
            string finalstring = "NotRead";
            con.Close();
            if (type.Equals("con"))
            {
                con.Close();
                string name = "";
                string address = "";
                string description = "";
                string picpath = "";
                string id = "";
                string querry = "select * from uContractor where cMatOne='" + value + "'";
                SqlCommand cmd = new SqlCommand(querry, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int c = 0;
                while (dr.Read())
                {
                    name = dr["cName"].ToString();
                    address = dr["cAddress"].ToString();
                    description = dr["cDescription"].ToString();
                    picpath = dr["cPic"].ToString();
                    id = dr["Id"].ToString();
                    if (c == 0)
                    {
                        finalstring = name + "," + address + "," + description + "," + picpath + "," + id;
                    }
                    else
                    {
                        finalstring += "|" + name + "," + address + "," + description + "," + picpath + "," + id;
                    }
                    c++;
                }

                dr.Close();
                con.Close();

            }
            else
            {
                con.Close();
                string name = "";
                string address = "";
                string description = "";
                string picpath = "";
                string id = "";
                string querry = "select * from uDistributer where cMatOne='" + value + "'";
                SqlCommand cmd = new SqlCommand(querry, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int c = 0;
                while (dr.Read())
                {
                    name = dr["cName"].ToString();
                    address = dr["cAddress"].ToString();
                    description = dr["cDescription"].ToString();
                    picpath = dr["cPic"].ToString();
                    id = dr["Id"].ToString();
                    if (c == 0)
                    {
                        finalstring = name + "," + address + "," + description + "," + picpath + "," + id;
                    }
                    else
                    {
                        finalstring += "|" + name + "," + address + "," + description + "," + picpath + "," + id;
                    }
                    c++;
                }

                dr.Close();
                con.Close();
            }
            return finalstring;
        }

        public string sendnotificationforcontact(string fromName, string toid)
        {
            con.Close();
            string finalstring = "";
            string fromsendid = "";
            string uName = "";
            string uEmail = "";
            string uCell = "";
            string uAddress = "";
            string recivingid="";
            string message = "";
            string querry = " select * from uReg where uName='" + fromName + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                uName = fromName;
                uEmail = dr["uEmail"].ToString();
                uCell = dr["uCell"].ToString();
                uAddress = dr["uAddress"].ToString();
                fromsendid = dr["regid"].ToString();
               recivingid=tosenddetails(toid);
               message = "Hellow EENT user..! MR/MRs " + uName + " wants to connect you for service of his/her work/home contact details are given " + "Email : " + uEmail + " Cell: " + uCell + " and Address is " + uAddress; 
               finalstring = SendPushNotification(recivingid, message);
            }
            else
            {
                finalstring = "NotRead";
            }
            dr.Close();
            con.Close();
            return finalstring;
        }
        private string tosenddetails(string tosendid)
        {
            string regid = "";
            string finalstring = "";
            con2.Close();
            string querry2 = "select regid from uContractor c JOIN uReg on uReg.Id=c.cUser  where c.Id='" + tosendid + "'";
            SqlCommand cmd = new SqlCommand(querry2, con2);
            con2.Open();
            SqlDataReader dr2 = cmd.ExecuteReader();
            if (dr2.Read())
            {
                finalstring = dr2["regid"].ToString();
                
            }
            else
            {
                finalstring = "NotRead";
            }
            dr2.Close();
            con2.Close();
            return finalstring;
        }

        public string sendnotificationfordis(string fromName, string toid)
        {
            con.Close();
            string finalstring = "";
            string fromsendid = "";
            string uName = "";
            string uEmail = "";
            string uCell = "";
            string uAddress = "";
            string recivingid = "";
            string message = "";
            string querry = " select * from uReg where uName='" + fromName + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                uName = fromName;
                uEmail = dr["uEmail"].ToString();
                uCell = dr["uCell"].ToString();
                uAddress = dr["uAddress"].ToString();
                fromsendid = dr["regid"].ToString();
                recivingid = tosenddetailsdis(toid);
                message = "Hellow EENT user..! MR/MRs " + uName + " wants to connect you for service of his/her work/home contact details are given " + "Email : " + uEmail + " Cell: " + uCell + " and Address is " + uAddress;
                finalstring = SendPushNotification(recivingid, message);
            }
            else
            {
                finalstring = "NotRead";
            }
            dr.Close();
            con.Close();
            return finalstring;
        }
        private string tosenddetailsdis(string tosendid)
        {
            string regid = "";
            string finalstring = "";
            con2.Close();
            string querry2 = "select regid from uDistributer c JOIN uReg on uReg.Id=c.cUser  where c.Id='" + tosendid + "'";
            SqlCommand cmd = new SqlCommand(querry2, con2);
            con2.Open();
            SqlDataReader dr2 = cmd.ExecuteReader();
            if (dr2.Read())
            {
                finalstring = dr2["regid"].ToString();

            }
            else
            {
                finalstring = "NotRead";
            }
            dr2.Close();
            con2.Close();
            return finalstring;
        }
        private static string SendPushNotification(string regid,string message)
        {
            string response;

            try
            {

                // From https://console.firebase.google.com/project/x.y.z/settings/
                // cloudmessaging/android:x,y,z
                // Server-Key: AAAA0...    ...._4

                string serverKey = "AAAAH0XE-AA:APA91bFX8ZhB6TV7OwQvJIhihZdndd5ntqmv8ayb0Xqxo79wSRM85JxKRD1nVjl7MgrPCGRETha8fzhKpm_1bwM5kmW9KlCvoC6jnx_4NJQXvwa5ekeNfCP2QkTayOyP54LpR4CLJciT";
                string senderId = "134314522624";
                string deviceId = regid; // Also something very long, 
                // got from android
                //string deviceId = "//topics/all";             // Use this to notify all devices, 
                // but App must be subscribed to 
                // topic notification
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");

                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = message,
                        title = "EENT",
                        sound = "Enabled"
                    },
                    data = new
                    {
                        body = message,
                        title = "EENT",
                        
                    }
                };

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                response = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }

       
        public string initialviewplots()
        {
            con.Close();
            string name = "";
            string address = "";
            string description = "";
            string picpath = "";
            string id = "";
            string finalstring = "";
            string querry = "select * from Plots";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int c = 0;
            while (dr.Read())
            {
                name = dr["plot_name"].ToString();
                address = dr["plot_area"].ToString();
                description = dr["plot_description"].ToString();
                picpath = dr["plot_pic1"].ToString();
                id = dr["Id"].ToString();
                if (c == 0)
                {
                    finalstring = name + "," + address + "," + description + "," + picpath + "," + id;
                }
                else
                {
                    finalstring += "|" + name + "," + address + "," + description + "," + picpath + "," + id;
                }
                c++;
            }
            dr.Close();
            con.Close();
            return finalstring;

        }
        public string searchplot(string keyword)
        {
            con.Close();
            string finalstring = "";
            string name = "";
            string address = "";
            string description = "";
            string picpath = "";
            string id = "";
            string querry = "select * from Plots where plot_area LIKE '%" + keyword + "%'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int c = 0;
            while (dr.Read())
            {
                name = dr["plot_name"].ToString();
                address = dr["plot_area"].ToString();
                description = dr["plot_description"].ToString();
                picpath = dr["plot_pic1"].ToString();
                id = dr["Id"].ToString();
                if (c == 0)
                {
                    finalstring = name + "," + address + "," + description + "," + picpath + "," + id;
                }
                else
                {
                    finalstring += "|" + name + "," + address + "," + description + "," + picpath + "," + id;
                }
                c++;
            }
            return finalstring;



        }
        public string getplotdetails(string idd)
        {
            con.Close();
            string finalstring = "";
            string name = "";
            string address = "";
            string description = "";
            string picpath = "";
            string id = "";
            string size = "";
            string picpath2 = "";
            string picpath3 = "";
            string picpath4 = "";
            string plotby = "";
            string contactno = "";
            string price = "";
            string querry = "select * from Plots where Id='" + idd + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            
            if (dr.Read())
            {
                id = dr["Id"].ToString();
                name = dr["plot_name"].ToString();
                size = dr["plot_size"].ToString();
                address = dr["plot_area"].ToString();
                picpath = dr["plot_pic1"].ToString();
                picpath2 = dr["plot_pic2"].ToString();
                picpath3 = dr["plot_pic3"].ToString();
                picpath4 = dr["plot_pic4"].ToString();
                price = dr["plot_price"].ToString();
                description = dr["plot_description"].ToString();
                plotby = dr["plot_by"].ToString();
                contactno = dr["contact_no"].ToString();

                finalstring = id + "," + name + "," + size + "," + address + "," + picpath + "," + picpath2 + "," + picpath3 + "," + picpath4 + "," + price + "," + description + "," + plotby + "," + contactno;
             
            }
            return finalstring;
            
        }

        public string insertmaps(string mapof,string pic)
        {
            string finalstring = "";
            con.Close();
            string imagePath = "";
            imagePath = SaveImage(pic, "xyz" + UnixTimeNow());
            pathh += "" + "xyz" + UnixTimeNow() + ".jpeg";
            string querry = "insert into Maps(map_of,map_path)values('"+mapof+"','"+pathh+"')";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a != 0)
            {
                finalstring = "Inserted";
            }
            else
            {
                finalstring = "NotInserted";

            }

            return finalstring;
        }
        public string insertplots(string pname,string psize,string parea,string pic1,string pic2,string pic3,string pic4,string pprice,string pdes,string pby,string cno)
        {
            string finalstring = "";
            con.Close();
            string imagePath = "";
            imagePath = SaveImage(pic1, "xyz" + UnixTimeNow());
            pathh += "" + "xyz" + UnixTimeNow() + ".jpeg";

            string imagePath2 = "";
            imagePath2 = SaveImage(pic2, "xyz2" + UnixTimeNow());
            pathh2 += "" + "xyz2" + UnixTimeNow() + ".jpeg";

            string imagePath3 = "";
            imagePath3 = SaveImage(pic3, "xyz3" + UnixTimeNow());
            pathh3 += "" + "xyz3" + UnixTimeNow() + ".jpeg";

            string imagePath4 = "";
            imagePath4 = SaveImage(pic4, "xyz4" + UnixTimeNow());
            pathh4 += "" + "xyz4" + UnixTimeNow() + ".jpeg";


            string querry = "insert into Plots(plot_name,plot_size,plot_area,plot_pic1,plot_pic2,plot_pic3,plot_pic4,plot_price,plot_description,plot_by,contact_no)values('"+pname+"','"+psize+"','"+parea+"','"+pathh+"','"+pathh2+"','"+pathh3+"','"+pathh4+"','"+pprice+"','"+pdes+"','"+pby+"','"+cno+"')";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a != 0)
            {
                finalstring = "Inserted";
            }
            else
            {
                finalstring = "NotInserted";
            }


            return finalstring;

        }
        public string getuprofiledata(string uname)
        {
            con.Close();
            string finalstring = "";
            string cname = "";
            string caddress = "";
            string cphone = "";
            string carea = "";
            string nameceo = "";
            string phoneceo = "";
            string cuser = "";
            string cpic = "";
            string cdesc = "";
            string cmatone = "";

            string querry = "select * from uDistributer where cUser=(select Id from uReg where uName='" + uname + "')";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cname = dr["cName"].ToString();
                caddress = dr["cAddress"].ToString();
                cphone = dr["cPhone"].ToString();
                carea = dr["cArea"].ToString();
                nameceo = dr["cNameCEO"].ToString();
                phoneceo = dr["cPhoneCEO"].ToString();
                cdesc = dr["cDescription"].ToString();
                cuser = dr["cUser"].ToString();
                cpic = dr["cPic"].ToString();
                cmatone = dr["cMatOne"].ToString();
                finalstring = cname + "," + caddress + "," + cphone + "," + carea + "," + nameceo + "," + phoneceo + "," + cdesc + "," + cuser + "," + cpic + "," + cmatone;

            }
            else
            {
                finalstring = "NotRead";

            }
            dr.Close();
            return finalstring;
        }
        public string getuprofiledatatwo(string uname)
        {
            con.Close();
            string finalstring = "";
            string cname = "";
            string caddress = "";
            string cphone = "";
            string carea = "";
            string nameceo = "";
            string phoneceo = "";
            string cuser = "";
            string cpic = "";
            string cdesc = "";
            string cmatone = "";

            string querry = "select * from uContractor where cUser=(select Id from uReg where uName='" + uname + "')";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cname = dr["cName"].ToString();
                caddress = dr["cAddress"].ToString();
                cphone = dr["cPhone"].ToString();
                carea = dr["cArea"].ToString();
                nameceo = dr["cNameCEO"].ToString();
                phoneceo = dr["cPhoneCEO"].ToString();
                cdesc = dr["cDescription"].ToString();
                cuser = dr["cUser"].ToString();
                cpic = dr["cPic"].ToString();
                cmatone = dr["cMatOne"].ToString();
                finalstring = cname + "," + caddress + "," + cphone + "," + carea + "," + nameceo + "," + phoneceo + "," + cdesc + "," + cuser + "," + cpic + "," + cmatone;

            }
            else
            {
                finalstring = "NotRead";

            }
            dr.Close();
            return finalstring;
        }
    }
    
}