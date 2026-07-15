using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Net.Mail;
using System.Net;

namespace BAL
{
  public   class MasterUpdate:BusinessBase
    {
      public UserMaster SaveUserMaster(int id, int roleId, string email, string userPassword,string username,string phone)
      {
          UserMaster objUserMaster = new UserMaster();
          if (id == 0)
          {
              objUserMaster.RoleId = roleId;
              objUserMaster.Email = email;
              objUserMaster.UserName = username;
              objUserMaster.Phone = phone;
              objUserMaster.UserPassword = userPassword;
              objUserMaster.CreatedDate = DateTime.Now;
              DB.UserMasters.InsertOnSubmit(objUserMaster);
          }
          else
          {
              objUserMaster = (from userObj in DB.UserMasters where userObj.Id == id select userObj).SingleOrDefault();
              objUserMaster.RoleId = roleId;
              objUserMaster.Email = email;
              objUserMaster.UserName = username;
              objUserMaster.Phone = phone;
              if (userPassword != "")
              {
                  objUserMaster.UserPassword = userPassword;
              }
              objUserMaster.UpdatedDate = DateTime.Now;
          }
          DB.SubmitChanges();
          return objUserMaster;
      }

      public UserMaster LoginUser(string email, string password)
      {
        return (from objUser in DB.UserMasters where (objUser.Email == email || objUser.UserName==email) && objUser.UserPassword == password select objUser).FirstOrDefault();
      }

      public UserMaster GetUserMasterById(int id)
      {
          return (from getUserId in DB.UserMasters where getUserId.Id == id select getUserId).SingleOrDefault();
      }

      public void DeleteUserById(int id)
      {
          var deleteUser = (from deleteId in DB.UserMasters where deleteId.Id == id select deleteId).SingleOrDefault();
          DB.UserMasters.DeleteOnSubmit(deleteUser);
          DB.SubmitChanges();
      }

      public bool IsAlreadyRegister(string email)
      {
          var already = (from register in DB.UserMasters where register.Email == email select register).ToList();
          if (already != null && already.Count > 0)
          {
              return true;
          }
          else
          {
              return false;
          }
      }
      public bool IsAlreadyUserName(string username)
      {
          var already = (from register in DB.UserMasters where register.UserName == username select register).ToList();
          if (already != null && already.Count > 0)
          {
              return true;
          }
          else
          {
              return false;
          }
      }
     

      public List<UserListClass> GetAllUserMasterList()
      {
          var getUserList = DB.sp_GetUserList().ToList();
          List<UserListClass> objUserListClassList = new List<UserListClass>();
          UserListClass objUserListClass;
          foreach (var userList in getUserList)
          {
              objUserListClass = new UserListClass();
              objUserListClass.Id = userList.Id;
              objUserListClass.RoleId =userList.RoleId;
              objUserListClass.RoleName = userList.RoleName;
              objUserListClass.Email = userList.Email;
              objUserListClass.UserPassword = userList.UserPassword;
              objUserListClass.CreatedDate =userList.CreatedDate;
              objUserListClass.UpdateDate = userList.UpdatedDate;
              objUserListClassList.Add(objUserListClass);
          }
          return objUserListClassList.OrderByDescending(x => x.CreatedDate).ToList(); //OrderBy(x => x.Email).ThenBy(x => x.UserPassword).ThenBy(x => x.RoleName).ThenBy(x => x.CreatedDate).ToList();
      }

      public IntermediatePoint SaveIntermediatePoint(int id, string Address, string longi, string lati, string newsLink, string newsText, string newsImage, string newsVideo, string newsAudio,string pointTitle,int sortingNo,string highway,string southboundText,string introductorymusic,string angle1 ,string angle2,string promotext,string region,
           string Address2, string longi2, string lati2, string newsLink2, string newsImage2, string newsVideo2, string newsAudio2, string pointTitle2, string introductorymusic2, string promotext2, string region2, string highwayname, string middle, string conAutio, string conaudio2, int regionId, int WeekGroupId, string DayFullName, string Frequency,int onepoint=0)
      {
          IntermediatePoint objIntermediatePoint = new IntermediatePoint();
          if (id == 0)
          {
              objIntermediatePoint.UpdatedFromOnePoint = onepoint;
              objIntermediatePoint.PointAddress = Address;
              objIntermediatePoint.PointLongitude = longi;
              objIntermediatePoint.PointLatitude = lati;
              objIntermediatePoint.NewsLink = newsLink;
              objIntermediatePoint.NewsText = newsText;
              objIntermediatePoint.NewsImage = newsImage;
              objIntermediatePoint.NewsVideo = newsVideo;
              objIntermediatePoint.NewsAudio = newsAudio;
              objIntermediatePoint.PointTitle = pointTitle;

              objIntermediatePoint.PointAddress2 = Address2;
              objIntermediatePoint.PointLongitude2 = longi2;
              objIntermediatePoint.PointLatitude2 = lati2;
              objIntermediatePoint.NewsLink2 = newsLink2;
              
              objIntermediatePoint.NewsImage2 = newsImage2;
              objIntermediatePoint.NewsVideo2 = newsVideo2;
              objIntermediatePoint.NewsAudio2 = newsAudio2;
              objIntermediatePoint.PointTitle2 = pointTitle2;
             
              objIntermediatePoint.HighwayRuns = highway;
              objIntermediatePoint.SouthBoundText = southboundText;
              objIntermediatePoint.Angle = angle1;
              objIntermediatePoint.Angle2 = angle1;
              objIntermediatePoint.CreatedDate = DateTime.Now;
              objIntermediatePoint.PromoText = promotext;
              objIntermediatePoint.PromoText2 = promotext2;
              objIntermediatePoint.Region = region;
              objIntermediatePoint.IntroductoryMusicFile = introductorymusic;
              objIntermediatePoint.IntroductoryMusicFile2 = introductorymusic2;
              objIntermediatePoint.ConclusionAudio = conAutio;
              objIntermediatePoint.ConclusionAudio2 = conaudio2;
              objIntermediatePoint.RegionId = regionId;


              objIntermediatePoint.DayFullName = DayFullName;
              objIntermediatePoint.Frequency = Frequency;
              objIntermediatePoint.WeekGroupId = WeekGroupId;

              objIntermediatePoint.Highway =Convert.ToInt32( highwayname);
              var dd = (from t in DB.IntermediatePoints where t.Sorting == sortingNo && t.Highway == Convert.ToInt32(highwayname) select t).SingleOrDefault();
              if (dd == null)
              {
                  //objIntermediatePoint.Sorting = GetAllIntermediatePoint().Count() + 1;//sortingNo;
                  objIntermediatePoint.Sorting = GetAllIntermediatePointByHighway(Convert.ToInt32(highwayname)).Count() + 1; 
              }
              else
              {
                  //dd.Sorting = GetAllIntermediatePoint().Count() + 1;
                  if (middle == "1")
                  {
                      var ddList = (from t in DB.IntermediatePoints where t.Sorting >= sortingNo && t.Highway == Convert.ToInt32(highwayname) select t).ToList().OrderBy(x=>x.Sorting).ToList();
                      foreach (var dl in ddList)
                      {
                          if (IsOdd(sortingNo))
                          {
                              if (IsOdd((int)dl.Sorting))
                                  dl.Sorting = dl.Sorting + 2;
                          }
                          else
                          {
                              if (!IsOdd((int)dl.Sorting))
                                  dl.Sorting = dl.Sorting + 2;
                          }
                          
                      }

                      objIntermediatePoint.Sorting = sortingNo;
                      
                  }
                  else
                  {
                      dd.Sorting = GetAllIntermediatePointByHighway(Convert.ToInt32(highwayname)).Count() + 1;
                      objIntermediatePoint.Sorting = sortingNo;
                  }
              }
              DB.IntermediatePoints.InsertOnSubmit(objIntermediatePoint);
          }
          else
          {
              objIntermediatePoint = (from objPoint in DB.IntermediatePoints where objPoint.Id == id select objPoint).SingleOrDefault();
              objIntermediatePoint.UpdatedFromOnePoint = onepoint;
              objIntermediatePoint.PointAddress = Address;
              objIntermediatePoint.PointLongitude = longi;
              objIntermediatePoint.PointLatitude = lati;
              objIntermediatePoint.NewsLink = newsLink;
              objIntermediatePoint.NewsText = newsText;
              objIntermediatePoint.HighwayRuns = highway;
              objIntermediatePoint.RegionId = regionId;
              objIntermediatePoint.SouthBoundText = southboundText;
              if (introductorymusic != "" || introductorymusic == "no")
              {
                  objIntermediatePoint.IntroductoryMusicFile = introductorymusic == "no" ? "" : introductorymusic;
              }
              objIntermediatePoint.Angle = angle1;
              objIntermediatePoint.Angle2 = angle1;
              objIntermediatePoint.PointTitle = pointTitle;
              objIntermediatePoint.PromoText = promotext;


              objIntermediatePoint.PointAddress2 = Address2;
              objIntermediatePoint.PointLongitude2 = longi2;
              objIntermediatePoint.PointLatitude2 = lati2;
              objIntermediatePoint.NewsLink2 = newsLink2;
              if (newsImage2 != "" || newsImage2 == "no")
              {
                  objIntermediatePoint.NewsImage2 = newsImage2 == "no" ? "" : newsImage2;
              }
              if (newsVideo2 != "" || newsVideo2 == "no")
              {
                  objIntermediatePoint.NewsVideo2 = newsVideo2 == "no" ? "" : newsVideo2;
              }
              if (newsAudio2 != "" || newsAudio2 == "no")
              {
                  objIntermediatePoint.NewsAudio2 = newsAudio2 == "no" ? "" : newsAudio2;
              }
              objIntermediatePoint.PointTitle2 = pointTitle2;
              if (introductorymusic2 != "" || introductorymusic2 == "no")
              {
                  objIntermediatePoint.IntroductoryMusicFile2 = introductorymusic2 == "no" ? "" : introductorymusic2;
              }
              objIntermediatePoint.PromoText2 = promotext2;

              objIntermediatePoint.Region = region;
              if (newsImage != "" || newsImage == "no")
              {
                  objIntermediatePoint.NewsImage = newsImage == "no" ? "" : newsImage;
              }
              if (newsVideo != "" || newsVideo == "no")
              {
                  objIntermediatePoint.NewsVideo = newsVideo == "no" ? "" : newsVideo;
              }
              if (newsAudio != "" || newsAudio == "no")
              {
                  objIntermediatePoint.NewsAudio = newsAudio == "no" ? "" : newsAudio;
              }
              if (conAutio != "" || conAutio == "no")
              {
                  objIntermediatePoint.ConclusionAudio = conAutio == "no" ? "" : conAutio;
              }
              if (conaudio2 != "" || conaudio2 == "no")
              {
                  objIntermediatePoint.ConclusionAudio2 = conaudio2 == "no" ? "" : conaudio2;
              }
              
              objIntermediatePoint.ModifiedDate = DateTime.Now;


             // objIntermediatePoint.DayFullName = DayFullName;
              objIntermediatePoint.Frequency = Frequency;
             // objIntermediatePoint.WeekGroupId = WeekGroupId;

              var dd = (from t in DB.IntermediatePoints where t.Sorting == sortingNo && t.Highway == Convert.ToInt32(highwayname) select t).SingleOrDefault();
              if (dd == null)
              {
                 // objIntermediatePoint.Sorting = sortingNo;
                  //objIntermediatePoint.Sorting = GetAllIntermediatePoint().Count() + 1;//sortingNo;
                  objIntermediatePoint.Sorting = GetAllIntermediatePointByHighway(Convert.ToInt32(highwayname)).Count() + 1;//sortingNo;
              }
              else
              {
                  dd.Sorting = objIntermediatePoint.Sorting;
                  objIntermediatePoint.Sorting = sortingNo;
              }

              objIntermediatePoint.Highway = Convert.ToInt32(highwayname);
          }


          LastChange l = new LastChange();
          DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
          long ms = (long)(DateTime.UtcNow - epoch).TotalMilliseconds;
          var lold = (from t in DB.LastChanges select t).SingleOrDefault();
          if (lold != null)
          {
              lold.UpdateDate = ms.ToString();
          }
          else
          {
              l.UpdateDate = ms.ToString();
              DB.LastChanges.InsertOnSubmit(l);
          }

          DB.SubmitChanges();
         System.Web.HttpContext.Current.Session["tempid"] = objIntermediatePoint.Id;

        

          return objIntermediatePoint;
      }


      public IntermediatePointsChild SaveIntermediatePointChild(int id,int pointid,int pointid2, string Address, string longi, string lati, string newsLink, string newsText, string newsImage, string newsVideo, string newsAudio, string pointTitle, int sortingNo, string highway, string southboundText, string introductorymusic, string angle1, string angle2, string promotext, string region,
          string Address2, string longi2, string lati2, string newsLink2, string newsImage2, string newsVideo2, string newsAudio2, string pointTitle2, string introductorymusic2, string promotext2, string region2, string highwayname, string middle, string conAutio, string conaudio2, int regionId, int WeekGroupId, string DayFullName, string Frequency)
      {
          IntermediatePointsChild objIntermediatePoint = new IntermediatePointsChild();
          objIntermediatePoint = (from objPoint in DB.IntermediatePointsChilds where objPoint.DayFullName == DayFullName && objPoint.PointId==pointid && objPoint.PointId2==pointid2 select objPoint).SingleOrDefault();
          if (objIntermediatePoint==null)
          {
              objIntermediatePoint = new IntermediatePointsChild();
              objIntermediatePoint.PointId = pointid;
              objIntermediatePoint.PointId2 = pointid2;
              objIntermediatePoint.PointAddress = Address;
              objIntermediatePoint.PointLongitude = longi;
              objIntermediatePoint.PointLatitude = lati;
              objIntermediatePoint.NewsLink = newsLink;
              objIntermediatePoint.NewsText = newsText;
              objIntermediatePoint.NewsImage = newsImage;
              objIntermediatePoint.NewsVideo = newsVideo;
              objIntermediatePoint.NewsAudio = newsAudio;
              objIntermediatePoint.PointTitle = pointTitle;

              objIntermediatePoint.PointAddress2 = Address2;
              objIntermediatePoint.PointLongitude2 = longi2;
              objIntermediatePoint.PointLatitude2 = lati2;
              objIntermediatePoint.NewsLink2 = newsLink2;

              objIntermediatePoint.NewsImage2 = newsImage2;
              objIntermediatePoint.NewsVideo2 = newsVideo2;
              objIntermediatePoint.NewsAudio2 = newsAudio2;
              objIntermediatePoint.PointTitle2 = pointTitle2;

              objIntermediatePoint.HighwayRuns = highway;
              objIntermediatePoint.SouthBoundText = southboundText;
              objIntermediatePoint.Angle = angle1;
              objIntermediatePoint.Angle2 = angle1;
              objIntermediatePoint.CreatedDate = DateTime.Now;
              objIntermediatePoint.PromoText = promotext;
              objIntermediatePoint.PromoText2 = promotext2;
              objIntermediatePoint.Region = region;
              objIntermediatePoint.IntroductoryMusicFile = introductorymusic;
              objIntermediatePoint.IntroductoryMusicFile2 = introductorymusic2;
              objIntermediatePoint.ConclusionAudio = conAutio;
              objIntermediatePoint.ConclusionAudio2 = conaudio2;
              objIntermediatePoint.RegionId = regionId;


              objIntermediatePoint.DayFullName = DayFullName;
              objIntermediatePoint.Frequency = Frequency;
              objIntermediatePoint.WeekGroupId = WeekGroupId;

              objIntermediatePoint.Highway = Convert.ToInt32(highwayname);
                var dd = (from t in DB.IntermediatePointsChilds where t.Sorting == sortingNo && t.Highway == Convert.ToInt32(highwayname) select t).FirstOrDefault();
              if (dd == null)
              {
                  //objIntermediatePoint.Sorting = GetAllIntermediatePoint().Count() + 1;//sortingNo;
                 // objIntermediatePoint.Sorting = GetAllIntermediatePointByHighway(Convert.ToInt32(highwayname)).Count() + 1; 
              }
              else
              {
                  //dd.Sorting = GetAllIntermediatePoint().Count() + 1;
                  if (middle == "1")
                  {
                     
                  }
              }
              objIntermediatePoint.Sorting = sortingNo;
              DB.IntermediatePointsChilds.InsertOnSubmit(objIntermediatePoint);
          }
          else
          {
              
              objIntermediatePoint.PointAddress = Address;
              objIntermediatePoint.PointLongitude = longi;
              objIntermediatePoint.PointLatitude = lati;
              objIntermediatePoint.NewsLink = newsLink;
              objIntermediatePoint.NewsText = newsText;
              objIntermediatePoint.HighwayRuns = highway;
              objIntermediatePoint.RegionId = regionId;
              objIntermediatePoint.SouthBoundText = southboundText;
              if (introductorymusic != "" || introductorymusic == "no")
              {
                  objIntermediatePoint.IntroductoryMusicFile = introductorymusic == "no" ? "" : introductorymusic;
              }
              objIntermediatePoint.Angle = angle1;
              objIntermediatePoint.Angle2 = angle1;
              objIntermediatePoint.PointTitle = pointTitle;
              objIntermediatePoint.PromoText = promotext;


              objIntermediatePoint.PointAddress2 = Address2;
              objIntermediatePoint.PointLongitude2 = longi2;
              objIntermediatePoint.PointLatitude2 = lati2;
              objIntermediatePoint.NewsLink2 = newsLink2;
              if (newsImage2 != "" || newsImage2 == "no")
              {
                  objIntermediatePoint.NewsImage2 = newsImage2 == "no" ? "" : newsImage2;
              }
              if (newsVideo2 != "" || newsVideo2 == "no")
              {
                  objIntermediatePoint.NewsVideo2 = newsVideo2 == "no" ? "" : newsVideo2;
              }
              if (newsAudio2 != "" || newsAudio2 == "no")
              {
                  objIntermediatePoint.NewsAudio2 = newsAudio2 == "no" ? "" : newsAudio2;
              }
              objIntermediatePoint.PointTitle2 = pointTitle2;
              if (introductorymusic2 != "" || introductorymusic2 == "no")
              {
                  objIntermediatePoint.IntroductoryMusicFile2 = introductorymusic2 == "no" ? "" : introductorymusic2;
              }
              objIntermediatePoint.PromoText2 = promotext2;

              objIntermediatePoint.Region = region;
              if (newsImage != "" || newsImage == "no")
              {
                  objIntermediatePoint.NewsImage = newsImage == "no" ? "" : newsImage;
              }
              if (newsVideo != "" || newsVideo == "no")
              {
                  objIntermediatePoint.NewsVideo = newsVideo == "no" ? "" : newsVideo;
              }
              if (newsAudio != "" || newsAudio == "no")
              {
                  objIntermediatePoint.NewsAudio = newsAudio == "no" ? "" : newsAudio;
              }
              if (conAutio != "" || conAutio == "no")
              {
                  objIntermediatePoint.ConclusionAudio = conAutio == "no" ? "" : conAutio;
              }
              if (conaudio2 != "" || conaudio2 == "no")
              {
                  objIntermediatePoint.ConclusionAudio2 = conaudio2 == "no" ? "" : conaudio2;
              }

              objIntermediatePoint.ModifiedDate = DateTime.Now;


               objIntermediatePoint.DayFullName = DayFullName;
              objIntermediatePoint.Frequency = Frequency;
              // objIntermediatePoint.WeekGroupId = WeekGroupId;

             
                  objIntermediatePoint.Sorting = sortingNo;
             

              objIntermediatePoint.Highway = Convert.ToInt32(highwayname);
          }


          LastChange l = new LastChange();
          DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
          long ms = (long)(DateTime.UtcNow - epoch).TotalMilliseconds;
          var lold = (from t in DB.LastChanges select t).SingleOrDefault();
          if (lold != null)
          {
              lold.UpdateDate = ms.ToString();
          }
          else
          {
              l.UpdateDate = ms.ToString();
              DB.LastChanges.InsertOnSubmit(l);
          }

          DB.SubmitChanges();
         // System.Web.HttpContext.Current.Session["tempid"] = objIntermediatePoint.Id;



          return objIntermediatePoint;
      }

      public IntermediatePointsChild SaveIntermediatePointChildShort(int id, int pointid, int pointid2, string Address, string longi, string lati, 
         string Address2, string longi2, string lati2,  string DayFullName)
      {
          IntermediatePointsChild objIntermediatePoint = new IntermediatePointsChild();
          objIntermediatePoint = (from objPoint in DB.IntermediatePointsChilds where objPoint.DayFullName == DayFullName && objPoint.PointId == pointid && objPoint.PointId2 == pointid2 select objPoint).SingleOrDefault();
          if (objIntermediatePoint == null)
          {
             
          }
          else
          {

              objIntermediatePoint.PointAddress = Address;
              objIntermediatePoint.PointLongitude = longi;
              objIntermediatePoint.PointLatitude = lati;
            


              objIntermediatePoint.PointAddress2 = Address2;
              objIntermediatePoint.PointLongitude2 = longi2;
              objIntermediatePoint.PointLatitude2 = lati2;              


              objIntermediatePoint.DayFullName = DayFullName;
             
          }


        

          DB.SubmitChanges();
          

          return objIntermediatePoint;
      }
      public void UpdateOnepointMile(int id1,int id2)
      {
          var d = (from t in DB.IntermediatePoints where t.Id == id1 select t).FirstOrDefault();
          if (d != null)
          {
              d.UpdatedFromOnePoint = 0;
              DB.SubmitChanges();
          }
          var d2 = (from t in DB.IntermediatePoints where t.Id == id2 select t).FirstOrDefault();
          if (d2 != null)
          {
              d2.UpdatedFromOnePoint = 0;
              DB.SubmitChanges();
          }
      }

      public List<IntermediatePoint> GetAllIntermediatePoint()
      {
          return (from objInterPoint in DB.IntermediatePoints select objInterPoint).ToList();
      }
      public List<IntermediatePointsChild> GetAllIntermediatePointChiled(int pointid,int point2)
      {
          return (from objInterPoint in DB.IntermediatePointsChilds where objInterPoint.PointId == pointid && objInterPoint.PointId2 == point2 select objInterPoint).ToList();
      }

      public IntermediatePointsChild GetIntermediatePointChildById(int pointid,int point2, string day)
      {
          return (from point in DB.IntermediatePointsChilds where point.PointId == pointid && point.PointId2 == point2 && point.DayFullName == day select point).SingleOrDefault();
      }

      public List<IntermediatePoint> GetAllIntermediatePointByHighway(int highway)
      {
          return (from objInterPoint in DB.IntermediatePoints where  objInterPoint.Highway==highway select objInterPoint).ToList();
      }

      public IntermediatePoint GetIntermediatePointById(int id)
      {
          return (from point in DB.IntermediatePoints where point.Id == id select point).SingleOrDefault();
      }

      public void DeleteIntermediatePoint(int id,int highwaymain)
      {
          var deletePoint = (from pointDelete in DB.IntermediatePoints where pointDelete.Id == id select pointDelete).SingleOrDefault();
          if (IsOdd((int)deletePoint.Sorting))
          {
              var deletePoint2 = (from pointDelete in DB.IntermediatePoints where pointDelete.Sorting == (int)deletePoint.Sorting + 1 && pointDelete.Highway == highwaymain select pointDelete).SingleOrDefault();
              if(deletePoint2!=null)
              {
                  DB.IntermediatePoints.DeleteOnSubmit(deletePoint2);

                 
               }
              if (deletePoint2 != null)
              {
                  var child = (from t in DB.IntermediatePointsChilds where t.PointId2 == deletePoint2.Id && t.PointId == 0 select t).ToList();
                  DB.IntermediatePointsChilds.DeleteAllOnSubmit(child);
                

                  var child1 = (from t in DB.IntermediatePointsChilds where t.PointId2 == 0 && t.PointId == deletePoint.Id select t).ToList();
                  DB.IntermediatePointsChilds.DeleteAllOnSubmit(child1);
                
              }
          }
          else
          {
              var deletePoint2 = (from pointDelete in DB.IntermediatePoints where pointDelete.Sorting == (int)deletePoint.Sorting - 1 && pointDelete.Highway == highwaymain select pointDelete).SingleOrDefault();
              if (deletePoint2 != null)
              {
                  DB.IntermediatePoints.DeleteOnSubmit(deletePoint2);
                 
              }
              if (deletePoint2 != null)
              {
                  var child = (from t in DB.IntermediatePointsChilds where t.PointId2 == 0 && t.PointId == deletePoint2.Id select t).ToList();
                  DB.IntermediatePointsChilds.DeleteAllOnSubmit(child);
                 

                  var child1 = (from t in DB.IntermediatePointsChilds where t.PointId2 == deletePoint.Id && t.PointId == 0 select t).ToList();
                  DB.IntermediatePointsChilds.DeleteAllOnSubmit(child1);
                 
              }
          }
          DB.IntermediatePoints.DeleteOnSubmit(deletePoint);
          
          
          DB.SubmitChanges();
          var getIntermediatePoint = (from getList in DB.IntermediatePoints where getList.Highway== highwaymain select getList).ToList().OrderBy(x=>x.Sorting).ToList();
          int i = 1;
          int highway = 0;
          foreach (var getRoutePoint in getIntermediatePoint)
          {
              var objIntermediatePoint = (from point in DB.IntermediatePoints where point.Id == getRoutePoint.Id select point).SingleOrDefault();
              if (IsOdd(i))
              {
                  if (string.IsNullOrEmpty(objIntermediatePoint.PointAddress))
                  objIntermediatePoint.PointAddress = objIntermediatePoint.PointAddress2;
                  if (string.IsNullOrEmpty(objIntermediatePoint.PointLongitude))
                  objIntermediatePoint.PointLongitude = objIntermediatePoint.PointLongitude2;
                  if (string.IsNullOrEmpty(objIntermediatePoint.PointLatitude))
                  objIntermediatePoint.PointLatitude = objIntermediatePoint.PointLatitude2;
                  if (string.IsNullOrEmpty(objIntermediatePoint.NewsLink))
                      objIntermediatePoint.NewsLink = objIntermediatePoint.NewsLink;
                  if (string.IsNullOrEmpty(objIntermediatePoint.NewsText))
                      objIntermediatePoint.NewsText = objIntermediatePoint.SouthBoundText;
                  if (string.IsNullOrEmpty(objIntermediatePoint.NewsImage))
                  objIntermediatePoint.NewsImage = objIntermediatePoint.NewsImage2;
                  if (string.IsNullOrEmpty(objIntermediatePoint.NewsVideo))
                  objIntermediatePoint.NewsVideo = objIntermediatePoint.NewsVideo2;
                  if (string.IsNullOrEmpty(objIntermediatePoint.NewsAudio))
                  objIntermediatePoint.NewsAudio = objIntermediatePoint.NewsAudio2;
                  if (string.IsNullOrEmpty(objIntermediatePoint.PointTitle))
                  objIntermediatePoint.PointTitle = objIntermediatePoint.PointTitle2;

                  if (string.IsNullOrEmpty(objIntermediatePoint.PromoText))
                  objIntermediatePoint.PromoText = objIntermediatePoint.PromoText2;

                  if (string.IsNullOrEmpty(objIntermediatePoint.IntroductoryMusicFile))
                  objIntermediatePoint.IntroductoryMusicFile = objIntermediatePoint.IntroductoryMusicFile2;
                  highway = (int)objIntermediatePoint.Highway;
              }
              else
              {
                  if (string.IsNullOrEmpty(objIntermediatePoint.PointAddress2))
                      objIntermediatePoint.PointAddress2 = objIntermediatePoint.PointAddress;
                  if (string.IsNullOrEmpty(objIntermediatePoint.PointLongitude2))
                      objIntermediatePoint.PointLongitude2 = objIntermediatePoint.PointLongitude;
                  if (string.IsNullOrEmpty(objIntermediatePoint.PointLatitude2))
                      objIntermediatePoint.PointLatitude2 = objIntermediatePoint.PointLatitude;
                  if (string.IsNullOrEmpty(objIntermediatePoint.NewsLink2))
                      objIntermediatePoint.NewsLink2 = objIntermediatePoint.NewsLink;
                  if (string.IsNullOrEmpty(objIntermediatePoint.SouthBoundText))
                      objIntermediatePoint.SouthBoundText = objIntermediatePoint.NewsText;
                  if (string.IsNullOrEmpty(objIntermediatePoint.NewsImage2))
                      objIntermediatePoint.NewsImage2 = objIntermediatePoint.NewsImage;
                  if (string.IsNullOrEmpty(objIntermediatePoint.NewsVideo2))
                      objIntermediatePoint.NewsVideo2 = objIntermediatePoint.NewsVideo;
                  if (string.IsNullOrEmpty(objIntermediatePoint.NewsAudio2))
                      objIntermediatePoint.NewsAudio2 = objIntermediatePoint.NewsAudio;
                  if (string.IsNullOrEmpty(objIntermediatePoint.PointTitle2))
                      objIntermediatePoint.PointTitle2 = objIntermediatePoint.PointTitle;

                  if (string.IsNullOrEmpty(objIntermediatePoint.PromoText2))
                      objIntermediatePoint.PromoText2 = objIntermediatePoint.PromoText;

                  if (string.IsNullOrEmpty(objIntermediatePoint.IntroductoryMusicFile2))
                      objIntermediatePoint.IntroductoryMusicFile2 = objIntermediatePoint.IntroductoryMusicFile;
                  objIntermediatePoint.Highway = highway;
                  highway = 0;

              }
              objIntermediatePoint.Sorting = i;
              i++;
          }

          LastChange l = new LastChange();
          DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
          long ms = (long)(DateTime.UtcNow - epoch).TotalMilliseconds;
          var lold = (from t in DB.LastChanges select t).SingleOrDefault();
          if (lold != null)
          {
              lold.UpdateDate = ms.ToString();
          }
          else
          {
              l.UpdateDate = ms.ToString();
              DB.LastChanges.InsertOnSubmit(l);
          }
          DB.SubmitChanges();
      }

      public void DeleteChild(int highway)
      {
          var d = (from t in DB.IntermediatePointsChilds where t.Highway == highway select t).ToList();
          DB.IntermediatePointsChilds.DeleteAllOnSubmit(d);
          DB.SubmitChanges();
      }

      public List<sp_GetAllRoutePointsResult> GetRoutePointList(string longitude, string latitude)
      {
          return DB.sp_GetAllRoutePoints(longitude, latitude).ToList();
      }
      public List<sp_GetNearByRoutePointsResult> GetNearByRoutePointList(string longitude, string latitude,int radius,string frequncy,int userid)
      {
          return DB.sp_GetNearByRoutePoints(longitude, latitude, radius, frequncy, userid).ToList();
      }
      public List<sp_GetRegionByRoutePointsResult> GetRegionByRoutePointList(string region, string frequncy, int userid)
      {
          return DB.sp_GetRegionByRoutePoints(region, frequncy, userid).ToList();
      }

      public List<sp_GetAllRoutePointsByHighwayResult> GetAllRoutePointsByHighway(int highway, string frequncy, int userid)
      {
          return DB.sp_GetAllRoutePointsByHighway(highway, frequncy, userid).ToList();
      }
      public List<sp_GetAllRoutePointsByHighway2Result> GetAllRoutePointsByHighway2(int highway)
      {
          return DB.sp_GetAllRoutePointsByHighway2(highway).ToList();
      }




      public List<sp_GetAllRouteAdResult> GetAdList(string longitude, string latitude)
      {
          return DB.sp_GetAllRouteAd(longitude, latitude).ToList();
      }
      public List<sp_GetNearByAdResult> GetNearByAdList(string longitude, string latitude, int radius)
      {
          return DB.sp_GetNearByAd(longitude, latitude, radius).ToList();
      }
      public List<sp_GetRegionByAdResult> GetRegionByAdList(string region)
      {
          return DB.sp_GetRegionByAd(region).ToList();
      }

      public List<sp_GetAllAdByHighwayResult> GetAllAdByHighway(int highway)
      {
          return DB.sp_GetAllAdByHighway(highway).ToList();
      }




      public IntermediatePoint SaveSortingNumber(int id, int sortingNumber)
      {
          IntermediatePoint objIntermediatePoint = new IntermediatePoint();
          objIntermediatePoint = (from point in DB.IntermediatePoints where point.Id == id select point).SingleOrDefault();
          objIntermediatePoint.Sorting = sortingNumber;
          DB.SubmitChanges();
          return objIntermediatePoint;
      }

      public IntermediatePoint GetSortingValue(int id)
      {
          IntermediatePoint objIntermediatePoint = new IntermediatePoint();
          objIntermediatePoint=(from sortValue in DB.IntermediatePoints where sortValue.Id == id select sortValue).SingleOrDefault();
          return objIntermediatePoint;
      }

      public IntermediatePoint GetRouteIdBySortingValue(int sortingValue,int highway)
      {
          IntermediatePoint objIntermediatePoint = new IntermediatePoint();
          objIntermediatePoint = (from getRouteId in DB.IntermediatePoints where getRouteId.Sorting == sortingValue && getRouteId.Highway==highway select getRouteId).SingleOrDefault();
          return objIntermediatePoint;
      }
      public void UpdateFrequency(int sortingValue, int highway,string frequnecy)
      {
          IntermediatePoint objIntermediatePoint = new IntermediatePoint();
          objIntermediatePoint = (from getRouteId in DB.IntermediatePoints where getRouteId.Sorting == sortingValue && getRouteId.Highway == highway select getRouteId).SingleOrDefault();
          if (objIntermediatePoint != null)
          {
              objIntermediatePoint.Frequency = frequnecy;
          }
          DB.SubmitChanges();

          var d=(from getRouteId in DB.IntermediatePointsChilds where getRouteId.Sorting == sortingValue && getRouteId.Highway == highway select getRouteId).ToList();
          foreach (var t in d)
          {
              t.Frequency = frequnecy;
          }
          DB.SubmitChanges();
      }
      public void UpdateLocation(int sortingValue, int highway, string lat,string lng,string address,int id)
      {
          IntermediatePoint objIntermediatePoint = new IntermediatePoint();
          objIntermediatePoint = (from getRouteId in DB.IntermediatePoints where getRouteId.Sorting == sortingValue && getRouteId.Highway == highway select getRouteId).SingleOrDefault();
          if (objIntermediatePoint != null)
          {
              if (id == 1)
              {
                  objIntermediatePoint.PointAddress = address;
                  objIntermediatePoint.PointLongitude = lng;
                  objIntermediatePoint.PointLatitude = lat;
              }
              else
              {
                  objIntermediatePoint.PointAddress2 = address;
                  objIntermediatePoint.PointLongitude2 = lng;
                  objIntermediatePoint.PointLatitude2 = lat;
              }
          }
          DB.SubmitChanges();

          var d = (from getRouteId in DB.IntermediatePointsChilds where getRouteId.Sorting == sortingValue && getRouteId.Highway == highway select getRouteId).ToList();
          foreach (var t in d)
          {
              if (id == 1)
              {
                  t.PointAddress = address;
                  t.PointLongitude = lng;
                  t.PointLatitude = lat;
              }
              else
              {
                  t.PointAddress2 = address;
                  t.PointLongitude2 = lng;
                  t.PointLatitude2 = lat;
              }
          }
          DB.SubmitChanges();
      }

      public List<string> GetAllRegion()
      {
          return (from t in DB.IntermediatePoints select t.Region).ToList().Distinct().ToList();
      }
      public bool IsOdd(int value)
      {
          return value % 2 != 0;
      }

      public List<RoadspokenPartner> GetAllPartners()
      {
          return (from t in DB.RoadspokenPartners select t).ToList();
      }
      public List<Highway> GetAllHighways()
      {
          return (from t in DB.Highways select t).ToList();
      }

      public void SavePartner(int id, string name, string image, string url)
      {
          RoadspokenPartner p = new RoadspokenPartner();
          if (id > 0)
          {
              var d = (from t in DB.RoadspokenPartners where t.Id==id select t).SingleOrDefault();
              if(image!="")
              d.PartnerImage = image;
              d.PartnerName = name;
              d.PartnerURL = url;              
          }
          else
          {
              if (image != "")
              p.PartnerImage = image;
              p.PartnerName = name;
              p.PartnerURL = url;
              p.CreateDate = DateTime.Now;
              DB.RoadspokenPartners.InsertOnSubmit(p);
          }
          DB.SubmitChanges();
      }
      public RoadspokenPartner GetPartnerById(int id)
      {
          return (from t in DB.RoadspokenPartners where t.Id==id select t).SingleOrDefault();
      }
      public void DeletePartnerById(int id)
      {
          var d= (from t in DB.RoadspokenPartners where t.Id == id select t).SingleOrDefault();
          DB.RoadspokenPartners.DeleteOnSubmit(d);
          DB.SubmitChanges();
      }

      public void SaveHighway(int id, string name)
      {
          Highway p = new Highway();
          if (id > 0)
          {
              var d = (from t in DB.Highways where t.Id == id select t).SingleOrDefault();
             
              d.HighwayName = name;
          }
          else
          {
             
              p.HighwayName = name;
             
              p.CreateDate = DateTime.Now;
              DB.Highways.InsertOnSubmit(p);
          }
          DB.SubmitChanges();
      }
      public Highway GetHighwayById(int id)
      {
          return (from t in DB.Highways where t.Id == id select t).SingleOrDefault();
      }
      public void DeleteHighwayById(int id)
      {
          var d = (from t in DB.Highways where t.Id == id select t).SingleOrDefault();
          DB.Highways.DeleteOnSubmit(d);
          DB.SubmitChanges();
      }

      public string GetLastUpdatetimeStamp()
      {
          var d= (from t in DB.LastChanges select t).SingleOrDefault();
          if (d != null)
          {
              return d.UpdateDate;
          }
          else
          {
              return "";
          }
      }

      public bool ForgotPassword(string email)
      {
          var d = (from t in DB.UserMasters where t.Email == email select t).SingleOrDefault();
          if (d != null)
          {
              SendMail(email, "Forgot Password", "Dear " + d.Email + "<br /><br />Your password is " + d.UserPassword + "<br /> <br /> Thanks");
              return true;
          }
          else
          {
              return false;
          }
      }

      public static void SendMail(string toMail, string subject, string body = "")
      {
          SmtpClient ss = new SmtpClient();

          try
          {
              //ss.Host = "mail.unipoi.com";
              ss.Host = "smtp.domain.com";

              ss.Port = 25;
              ss.Timeout = 10000;
              ss.DeliveryMethod = SmtpDeliveryMethod.Network;
              ss.UseDefaultCredentials = false;
              ss.Credentials = new NetworkCredential("info@roadspoken.com", "Ex1t#123");
              if (body == "")
              {
                  //body = "Your password has been changed. Your new password is "+;
              }
              //"The Smart Plan Dating Network"
              MailMessage mailMsg = new MailMessage();
              mailMsg.To.Add(toMail);
              mailMsg.From = new MailAddress("info@roadspoken.com");
              mailMsg.Subject = subject;
              mailMsg.Body = body;
              mailMsg.IsBodyHtml = true;
              mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
              ss.Send(mailMsg);

          }
          catch (Exception ex)
          {
              //Console.WriteLine(ex.Message);
              //Console.ReadKey();
          }
      }


      public void SelectHighway(int highwayid,int userid)
      {
          SeletedHighway s = new SeletedHighway();
          var d = (from t in DB.SeletedHighways where t.HighwayId==highwayid && t.UserId==userid select t).SingleOrDefault();
          if (d != null)
          {
              d.UpdatedDate = DateTime.Now;
          }
          else
          {
               var dd = (from t in DB.SeletedHighways where t.UserId==userid select t).SingleOrDefault();
               if (dd != null)
               {
                   dd.HighwayId = highwayid;
                   dd.UpdatedDate = DateTime.Now;
               }
               else
               {
                   s.HighwayId = highwayid;
                   s.UserId = userid;
                   s.CreatedDate = DateTime.Now;
                   DB.SeletedHighways.InsertOnSubmit(s);
               }

          }
          DB.SubmitChanges();
      }
      public SeletedHighway GetSelectedHighway(int userid)
      {
          return (from t in DB.SeletedHighways where t.UserId == userid select t).SingleOrDefault();
      }

      public RegionMaster SaveRegionMaster(int id, string regionName)
      {
          RegionMaster objRegionMaster = new RegionMaster();
          if (id == 0)
          {
              objRegionMaster.RegionName = regionName;
              objRegionMaster.CreatedDate = DateTime.Now;
              DB.RegionMasters.InsertOnSubmit(objRegionMaster);
          }

          else
          {
              objRegionMaster = (from getRegion in DB.RegionMasters where getRegion.Id == id select getRegion).SingleOrDefault();
              objRegionMaster.RegionName = regionName;
          }

          DB.SubmitChanges();
          return objRegionMaster;
      }

      public RegionMaster GetRegionById(int id)
      {
          return (from getRegion in DB.RegionMasters where getRegion.Id == id select getRegion).SingleOrDefault();
      }

      public void DeleteRegionById(int id)
      {
          var deleteRegion = (from region in DB.RegionMasters where region.Id == id select region).SingleOrDefault();
          DB.RegionMasters.DeleteOnSubmit(deleteRegion);
          DB.SubmitChanges();
      }

      public List<RegionMaster> GetRegionList()
      {
          return (from getRegionList in DB.RegionMasters select getRegionList).ToList();
      }



      public void SaveAd(int id, string highwayrun, int highway, int region, int waypointid, string text, string image, string audio, string video, int radius, DateTime startdate, DateTime enddate,string lat  ,string lng,string address,string tts,string conaudio,string introaudio)
      {
          PublicAd p = new PublicAd();
          if (id > 0)
          {
              p = (from t in DB.PublicAds where t.Id == id select t).SingleOrDefault();
              p.Highway = highway;
              p.HighwayRun = highwayrun;
              p.RegionId = region;
              p.WayPointId = waypointid;
              p.AdText = text;
              if(image!="")
              p.Imagefile = image;
              if (audio != "")
                  p.AudioFile = audio;
              if (conaudio != "")
                  p.ConclusionMusic = conaudio;
              if (introaudio != "")
                  p.IntroMusic = introaudio;
              if (video != "")
                  p.VideoFile = video;
              p.Radius = radius;

              p.PointAddress = address;
              p.PointLatitude = lat;
              p.PointLongitude = lng;
              p.TTS = tts;
              p.StartTime = TimeZoneInfo.ConvertTimeToUtc((DateTime)startdate);
              p.EndTime = TimeZoneInfo.ConvertTimeToUtc((DateTime)enddate);
          }
          else
          {
              p.Highway = highway;
              p.HighwayRun = highwayrun;
              p.RegionId = region;
              p.WayPointId = waypointid;
              p.AdText = text;
              if (image != "")
                  p.Imagefile = image;
              if (audio != "")
                  p.AudioFile = audio;
              if (video != "")
                  p.VideoFile = video;
              if (conaudio != "")
                  p.ConclusionMusic = conaudio;
              if (introaudio != "")
                  p.IntroMusic = introaudio;
              p.Radius = radius;

              p.PointAddress = address;
              p.PointLatitude = lat;
              p.PointLongitude = lng;
              p.TTS = tts;
              p.StartTime = TimeZoneInfo.ConvertTimeToUtc((DateTime)startdate);
              p.EndTime = TimeZoneInfo.ConvertTimeToUtc((DateTime)enddate);
              p.CreatedDate = DateTime.Now;
              DB.PublicAds.InsertOnSubmit(p);
          }
          DB.SubmitChanges();
      }


      public PublicAd GetAdById(int id)
      {
          return (from t in DB.PublicAds where t.Id==id select t).SingleOrDefault();
      }
      public List< PublicAd> GetAllAdBy()
      {
          return (from t in DB.PublicAds  select t).ToList();
      }

      public void DeleteAdById(int id)
      {
          var d= (from t in DB.PublicAds where t.Id == id select t).SingleOrDefault();
          DB.PublicAds.DeleteOnSubmit(d);
          DB.SubmitChanges();
      }

      public void UpdateChild(int sortingNo, int highwayname)
      {
          var ddList = (from t in DB.IntermediatePointsChilds where t.Sorting >= sortingNo && t.Highway == Convert.ToInt32(highwayname) select t).ToList().OrderBy(x => x.Sorting).ToList();
          int i = 6;
          int sortingvalue = 0;
          foreach (var dl in ddList)
          {
             
              if (i == 6)
              {
                  dl.Sorting = dl.Sorting + 2;
                  sortingvalue = dl.Sorting ?? 0;
              }
              else
              {
                  dl.Sorting = sortingvalue;
                  if (i == 1)
                  {
                      i = 7;
                  }
              }
              var ddsingle = (from t in DB.IntermediatePoints where t.Sorting == dl.Sorting && t.Highway == Convert.ToInt32(highwayname) select t).ToList().OrderBy(x => x.Sorting).FirstOrDefault();
              if (ddsingle != null)
              {
                  if (IsOdd(sortingNo))
                  {
                      dl.PointId = ddsingle.Id;
                      dl.PointId2 = 0;
                  }
                  else
                  {
                      dl.PointId = 0;
                      dl.PointId2 = ddsingle.Id;
                  }
              }
              //if (IsOdd(sortingNo))
              //{
              //    if (IsOdd((int)dl.Sorting))
              //        dl.Sorting = dl.Sorting + 2;
              //}
              //else
              //{
              //    if (!IsOdd((int)dl.Sorting))
              //        dl.Sorting = dl.Sorting + 2;
              //}
              i = i - 1;
          }
          DB.SubmitChanges();
      }
    }

  public class UserListClass
  {
      public int Id { get; set; }
      public int RoleId { get; set; }
      public string RoleName { get; set; }
      public string Email { get; set; }
      public string UserPassword { get; set; }
      public DateTime CreatedDate { get; set; }
      public DateTime? UpdateDate { get; set; }
  }
}
