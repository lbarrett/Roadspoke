using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Web.Script.Serialization;
using BAL;
using System.ServiceModel.Web;
using Newtonsoft.Json;

namespace RoadSpokenService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RoadSpokenWebService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]

    public class RoadSpokenWebService : IRoadSpokenWebService
    {
        string ResponseOk = "{\"status\": \"1\", \"message\": \"success\", \"data\":[message]}";
        string ResponseErr = "{\"status\": \"0\", \"message\": \"error occurred\"}";
        string path = System.Configuration.ConfigurationManager.AppSettings["Path"];



        #region Public Var
        Encoding encoding = Encoding.UTF8;
        string ResponseType = "application/json; charset=utf-8";

        #endregion
        private string GetSerialized(object obj)
        {

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = int.MaxValue;
            return javaScriptSerializer.Serialize(obj);
        }
        private string GetSerializedJsonSoft(object obj)
        { 

            return JsonConvert.SerializeObject(obj);
        }

        public System.ServiceModel.Channels.Message Login(LoginParam loginp)
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {
                var objLogin = objMasterUpdate.LoginUser(loginp.Email, loginp.Password);
                if (objLogin != null)
                {
                    retVal = GetSerialized(objLogin);
                    retVal = ResponseOk.Replace("[message]", retVal).Replace("\\", "");
                }
                else
                {
                    retVal = ResponseOk.Replace("success", "Invalid Login Name or Password!").Replace("[message]", "[]").Replace("\\", "");
                }
            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }

        public System.ServiceModel.Channels.Message RegisterUser(RegistrationParam loginp)
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {
                if (!objMasterUpdate.IsAlreadyRegister(loginp.email))
                {
                       if (!objMasterUpdate.IsAlreadyUserName(loginp.username))
                {

                    var objRegisterDetail = objMasterUpdate.SaveUserMaster(0, 3, loginp.email, loginp.password,loginp.username,loginp.phone);
                    if (objRegisterDetail != null)
                    {
                        retVal = GetSerialized(objRegisterDetail);
                        retVal = ResponseOk.Replace("[message]", retVal).Replace("\\", "");
                    }
                    else
                    {
                        retVal = ResponseErr.Replace("\\", "");
                    }
                }
                       else
                       {
                           retVal = ResponseErr.Replace("error occurred", "User name Already Registered!").Replace("\\", "");
                       }
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "Email Already Registered!").Replace("\\", "");
                }
            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "");
            }
            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//
        }

        public System.ServiceModel.Channels.Message GetRoutePointList(RouteParam objRoute)
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {

                var objRouteList = objMasterUpdate.GetRoutePointList(objRoute.Longitude,objRoute.Latitude);
                string region = "";
                string regionold = "";
                foreach (var t in objRouteList)
                {
                    if (!string.IsNullOrEmpty(t.NewsImage1))
                    {
                        t.NewsImage1 = path + t.NewsImage1;
                    }

                    if (!string.IsNullOrEmpty(t.NewsVideo1))
                    {
                        t.NewsVideo1 = path + t.NewsVideo1;
                    }

                    if (!string.IsNullOrEmpty(t.NewsAudio1))
                    {
                        t.NewsAudio1 = path + t.NewsAudio1;
                    }
                    if (!string.IsNullOrEmpty(t.IntroductoryMusicFile1))
                    {
                        t.IntroductoryMusicFile1 = path + t.IntroductoryMusicFile1;
                    }

                    if (!string.IsNullOrEmpty(t.ConclusionAudio1))
                    {
                        t.ConclusionAudio1 = path + t.ConclusionAudio1;
                    }
                    if (!string.IsNullOrEmpty(t.ConclusionAudio2))
                    {
                        t.ConclusionAudio2 = path + t.ConclusionAudio2;
                    }
                  

                    if (!string.IsNullOrEmpty(t.NewsImage2))
                    {
                        t.NewsImage2 = path + t.NewsImage2;
                    }

                    if (!string.IsNullOrEmpty(t.NewsVideo2))
                    {
                        t.NewsVideo2 = path + t.NewsVideo2;
                    }

                    if (!string.IsNullOrEmpty(t.NewsAudio2))
                    {
                        t.NewsAudio2 = path + t.NewsAudio2;
                    }
                    if (!string.IsNullOrEmpty(t.IntroductoryMusicFile2))
                    {
                        t.IntroductoryMusicFile2 = path + t.IntroductoryMusicFile2;
                    }
                    t.NewsText1 = t.NewsText1.Replace("[", " ").Replace("]", " ");
                    t.NewsText2 = t.NewsText2.Replace("[", " ").Replace("]", " ");
                    //t.NorthTTS = t.NorthTTS.Replace("[", " ").Replace("]", " ");
                    //t.SouthTTS = t.SouthTTS.Replace("[", " ").Replace("]", " ");
                    if (!region.Contains(t.Region))
                    {
                        region += t.Region + ",";                        
                    }
                }


                if (objRouteList != null)
                {
                    retVal = GetSerialized(objRouteList);
                    retVal = ResponseOk.Replace("[message]", retVal);
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "No Data found!").Replace("\\", "");

                }

            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }
        public System.ServiceModel.Channels.Message GetNearByRoutePointList(NearByRouteParam objRoute)
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {

                var objRouteList = objMasterUpdate.GetNearByRoutePointList(objRoute.Longitude, objRoute.Latitude,objRoute.Radius,objRoute.Frequency,objRoute.UserId);
                foreach (var t in objRouteList)
                {
                    if (!string.IsNullOrEmpty(t.NewsImage1))
                    {
                        t.NewsImage1 = path + t.NewsImage1;
                    }

                    if (!string.IsNullOrEmpty(t.NewsVideo1))
                    {
                        t.NewsVideo1 = path + t.NewsVideo1;
                    }

                    if (!string.IsNullOrEmpty(t.NewsAudio1))
                    {
                        t.NewsAudio1 = path + t.NewsAudio1;
                    }
                    if (!string.IsNullOrEmpty(t.IntroductoryMusicFile1))
                    {
                        t.IntroductoryMusicFile1 = path + t.IntroductoryMusicFile1;
                    }

                    if (!string.IsNullOrEmpty(t.ConclusionAudio1))
                    {
                        t.ConclusionAudio1 = path + t.ConclusionAudio1;
                    }
                    if (!string.IsNullOrEmpty(t.ConclusionAudio2))
                    {
                        t.ConclusionAudio2 = path + t.ConclusionAudio2;
                    }
                    if (!string.IsNullOrEmpty(t.NewsImage2))
                    {
                        t.NewsImage2 = path + t.NewsImage2;
                    }

                    if (!string.IsNullOrEmpty(t.NewsVideo2))
                    {
                        t.NewsVideo2 = path + t.NewsVideo2;
                    }

                    if (!string.IsNullOrEmpty(t.NewsAudio2))
                    {
                        t.NewsAudio2 = path + t.NewsAudio2;
                    }
                    if (!string.IsNullOrEmpty(t.IntroductoryMusicFile2))
                    {
                        t.IntroductoryMusicFile2 = path + t.IntroductoryMusicFile2;
                    }

                    t.NewsText1 = t.NewsText1.Replace("[", " ").Replace("]", " ");
                    t.NewsText2 = t.NewsText2.Replace("[", " ").Replace("]", " ");
                    //t.NorthTTS = t.NorthTTS.Replace("[", " ").Replace("]", " ");
                    //t.SouthTTS = t.SouthTTS.Replace("[", " ").Replace("]", " ");
                }


                if (objRouteList != null)
                {
                    retVal = GetSerialized(objRouteList);
                    retVal = ResponseOk.Replace("[message]", retVal);
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "No Data found!").Replace("\\", "");

                }

            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }

        public System.ServiceModel.Channels.Message GetRegionByRoutePointList(RegionRouteParam objRoute)
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {

                var objRouteList = objMasterUpdate.GetRegionByRoutePointList(objRoute.Region, objRoute.Frequency, objRoute.UserId);
                foreach (var t in objRouteList)
                {
                    if (!string.IsNullOrEmpty(t.NewsImage1))
                    {
                        t.NewsImage1 = path + t.NewsImage1;
                    }

                    if (!string.IsNullOrEmpty(t.NewsVideo1))
                    {
                        t.NewsVideo1 = path + t.NewsVideo1;
                    }

                    if (!string.IsNullOrEmpty(t.NewsAudio1))
                    {
                        t.NewsAudio1 = path + t.NewsAudio1;
                    }
                    if (!string.IsNullOrEmpty(t.IntroductoryMusicFile1))
                    {
                        t.IntroductoryMusicFile1 = path + t.IntroductoryMusicFile1;
                    }
                    if (!string.IsNullOrEmpty(t.ConclusionAudio1))
                    {
                        t.ConclusionAudio1 = path + t.ConclusionAudio1;
                    }
                    if (!string.IsNullOrEmpty(t.ConclusionAudio2))
                    {
                        t.ConclusionAudio2 = path + t.ConclusionAudio2;
                    }
                    if (!string.IsNullOrEmpty(t.NewsImage2))
                    {
                        t.NewsImage2 = path + t.NewsImage2;
                    }

                    if (!string.IsNullOrEmpty(t.NewsVideo2))
                    {
                        t.NewsVideo2 = path + t.NewsVideo2;
                    }

                    if (!string.IsNullOrEmpty(t.NewsAudio2))
                    {
                        t.NewsAudio2 = path + t.NewsAudio2;
                    }
                    if (!string.IsNullOrEmpty(t.IntroductoryMusicFile2))
                    {
                        t.IntroductoryMusicFile2 = path + t.IntroductoryMusicFile2;
                    }
                    t.NewsText1 = t.NewsText1.Replace("[", " ").Replace("]", " ");
                    t.NewsText2 = t.NewsText2.Replace("[", " ").Replace("]", " ");
                    //t.NorthTTS = t.NorthTTS.Replace("[", " ").Replace("]", " ");
                    //t.SouthTTS = t.SouthTTS.Replace("[", " ").Replace("]", " ");
                }


                if (objRouteList != null)
                {
                    retVal = GetSerialized(objRouteList);
                    retVal = ResponseOk.Replace("[message]", retVal);
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "No Data found!").Replace("\\", "");

                }

            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }

        public System.ServiceModel.Channels.Message GetAllRoutePointsByHighway(HighwayParam objRoute)
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {

                var objRouteList = objMasterUpdate.GetAllRoutePointsByHighway(objRoute.Highway, objRoute.Frequency, objRoute.UserId);
                foreach (var t in objRouteList)
                {
                    if (!string.IsNullOrEmpty(t.NewsImage1))
                    {
                        t.NewsImage1 = path + t.NewsImage1;
                    }

                    if (!string.IsNullOrEmpty(t.NewsVideo1))
                    {
                        t.NewsVideo1 = path + t.NewsVideo1;
                    }

                    if (!string.IsNullOrEmpty(t.NewsAudio1))
                    {
                        t.NewsAudio1 = path + t.NewsAudio1;
                    }
                    if (!string.IsNullOrEmpty(t.IntroductoryMusicFile1))
                    {
                        t.IntroductoryMusicFile1 = path + t.IntroductoryMusicFile1;
                    }
                    if (!string.IsNullOrEmpty(t.ConclusionAudio1))
                    {
                        t.ConclusionAudio1 = path + t.ConclusionAudio1;
                    }
                    if (!string.IsNullOrEmpty(t.ConclusionAudio2))
                    {
                        t.ConclusionAudio2 = path + t.ConclusionAudio2;
                    }
                    if (!string.IsNullOrEmpty(t.NewsImage2))
                    {
                        t.NewsImage2 = path + t.NewsImage2;
                    }

                    if (!string.IsNullOrEmpty(t.NewsVideo2))
                    {
                        t.NewsVideo2 = path + t.NewsVideo2;
                    }

                    if (!string.IsNullOrEmpty(t.NewsAudio2))
                    {
                        t.NewsAudio2 = path + t.NewsAudio2;
                    }
                    if (!string.IsNullOrEmpty(t.IntroductoryMusicFile2))
                    {
                        t.IntroductoryMusicFile2 = path + t.IntroductoryMusicFile2;
                    }
                    t.NewsText1 = t.NewsText1.Replace("[", " ").Replace("]", " ");
                    t.NewsText2 = t.NewsText2.Replace("[", " ").Replace("]", " ");
                    //t.NorthTTS = t.NorthTTS.Replace("[", " ").Replace("]", " ");
                    //t.SouthTTS = t.SouthTTS.Replace("[", " ").Replace("]", " ");
                }


                if (objRouteList != null)
                {
                    retVal = GetSerialized(objRouteList);
                    retVal = ResponseOk.Replace("[message]", retVal);
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "No Data found!").Replace("\\", "");

                }

            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }

        public System.ServiceModel.Channels.Message GetAllRegion()
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {

                var objRouteList = objMasterUpdate.GetRegionList();               
                if (objRouteList != null && objRouteList.Count>0)
                {
                    retVal = GetSerialized(objRouteList);
                    retVal = ResponseOk.Replace("[message]", retVal);
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "No Data found!").Replace("\\", "");

                }

            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }


        public System.ServiceModel.Channels.Message GetAllPartners()
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {

                var objRouteList = objMasterUpdate.GetAllPartners();
                if (objRouteList != null)
                {
                    foreach (var t in objRouteList)
                    {
                        if (!string.IsNullOrEmpty(t.PartnerImage))
                        {
                            t.PartnerImage = path + t.PartnerImage;
                        }
                    }
                    retVal = GetSerialized(objRouteList);
                    retVal = ResponseOk.Replace("[message]", retVal);
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "No Data found!").Replace("\\", "");

                }

            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }

        public System.ServiceModel.Channels.Message GetAllHighways()
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {

                var objRouteList = objMasterUpdate.GetAllHighways();
                if (objRouteList != null)
                {
                    retVal = GetSerialized(objRouteList);
                    retVal = ResponseOk.Replace("[message]", retVal);
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "No Data found!").Replace("\\", "");

                }

            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }


        public System.ServiceModel.Channels.Message ForgotPassword(ForgotPassword loginp)
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {

                if (objMasterUpdate.IsAlreadyRegister(loginp.Email))
                {
                    var objForgotPassword = objMasterUpdate.ForgotPassword(loginp.Email);
                    if (objForgotPassword)
                    {
                        List<bool> lst = new List<bool>();
                        lst.Add(objForgotPassword);
                        retVal = GetSerialized(lst);
                        retVal = ResponseOk.Replace("[message]", retVal);
                    }
                    else
                    {
                        retVal = ResponseErr.Replace("error occurred", "No Data found!");

                    }
                }
                else
                {
                    retVal = ResponseOk.Replace("[message]", GetSerialized(objMasterUpdate.IsAlreadyRegister(loginp.Email)));
                }

            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }

        public System.ServiceModel.Channels.Message GetLastUpdatetimeStamp()
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {

             
                    var objForgotPassword = objMasterUpdate.GetLastUpdatetimeStamp();
                   
                        List<string> lst = new List<string>();
                        lst.Add(objForgotPassword);
                        retVal = GetSerialized(lst);
                        retVal = ResponseOk.Replace("[message]", retVal);
                   

            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }




        public System.ServiceModel.Channels.Message GetAdList()
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {

                var objRouteList = objMasterUpdate.GetAdList("","");
                string region = "";
                string regionold = "";
                foreach (var t in objRouteList)
                {

                    
                    if (!string.IsNullOrEmpty(t.AdAudio))
                    {
                        t.AdAudio = path + t.AdAudio;
                    }

                    if (!string.IsNullOrEmpty(t.AdImage))
                    {
                        t.AdImage = path + t.AdImage;
                    }
                    if (!string.IsNullOrEmpty(t.IntroMusic))
                    {
                        t.IntroMusic = path + t.IntroMusic;
                    }

                    if (!string.IsNullOrEmpty(t.ConclusionMusic))
                    {
                        t.ConclusionMusic = path + t.ConclusionMusic;
                    }
                }


                if (objRouteList != null)
                {
                    retVal = GetSerializedJsonSoft(objRouteList);
                    retVal = ResponseOk.Replace("[message]", retVal);
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "No Data found!").Replace("\\", "");

                }

            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }
        public System.ServiceModel.Channels.Message GetNearByAdList(NearByRouteParam objRoute)
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {

                var objRouteList = objMasterUpdate.GetNearByAdList(objRoute.Longitude, objRoute.Latitude, objRoute.Radius);
                foreach (var t in objRouteList)
                {

              
                    if (!string.IsNullOrEmpty(t.AdAudio))
                    {
                        t.AdAudio = path + t.AdAudio;
                    }

                    if (!string.IsNullOrEmpty(t.AdImage))
                    {
                        t.AdImage = path + t.AdImage;
                    }
                    if (!string.IsNullOrEmpty(t.IntroMusic))
                    {
                        t.IntroMusic = path + t.IntroMusic;
                    }

                    if (!string.IsNullOrEmpty(t.ConclusionMusic))
                    {
                        t.ConclusionMusic = path + t.ConclusionMusic;
                    }
                }


                if (objRouteList != null)
                {
                    retVal = GetSerializedJsonSoft(objRouteList);
                    retVal = ResponseOk.Replace("[message]", retVal);
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "No Data found!").Replace("\\", "");

                }

            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }

        public System.ServiceModel.Channels.Message GetRegionByAdList(RegionRouteParam objRoute)
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {

                var objRouteList = objMasterUpdate.GetRegionByAdList(objRoute.Region);
                foreach (var t in objRouteList)
                {
                   

                    if (!string.IsNullOrEmpty(t.AdAudio))
                    {
                        t.AdAudio = path + t.AdAudio;
                    }

                    if (!string.IsNullOrEmpty(t.AdImage))
                    {
                        t.AdImage = path + t.AdImage;
                    }
                    if (!string.IsNullOrEmpty(t.IntroMusic))
                    {
                        t.IntroMusic = path + t.IntroMusic;
                    }

                    if (!string.IsNullOrEmpty(t.ConclusionMusic))
                    {
                        t.ConclusionMusic = path + t.ConclusionMusic;
                    }
                }


                if (objRouteList != null)
                {
                    retVal = GetSerializedJsonSoft(objRouteList);
                    retVal = ResponseOk.Replace("[message]", retVal);
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "No Data found!").Replace("\\", "");

                }

            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }

        public System.ServiceModel.Channels.Message GetAllAdByHighway(HighwayParam objRoute)
        {
            string retVal = string.Empty;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            try
            {

                var objRouteList = objMasterUpdate.GetAllAdByHighway(objRoute.Highway);
                foreach (var t in objRouteList)
                {
                   

                    if (!string.IsNullOrEmpty(t.AdAudio))
                    {
                        t.AdAudio = path + t.AdAudio;
                    }

                    if (!string.IsNullOrEmpty(t.AdImage))
                    {
                        t.AdImage = path + t.AdImage;
                    }
                    if (!string.IsNullOrEmpty(t.IntroMusic))
                    {
                        t.IntroMusic = path + t.IntroMusic;
                    }

                    if (!string.IsNullOrEmpty(t.ConclusionMusic))
                    {
                        t.ConclusionMusic = path + t.ConclusionMusic;
                    }
                }


                if (objRouteList != null)
                {
                    retVal = GetSerializedJsonSoft(objRouteList);
                    retVal = ResponseOk.Replace("[message]", retVal);
                }
                else
                {
                    retVal = ResponseErr.Replace("error occurred", "No Data found!").Replace("\\", "");

                }

            }
            catch (Exception ex)
            {
                retVal = ResponseErr.Replace("\\", "") + ex.Message;
            }


            return WebOperationContext.Current.CreateTextResponse(retVal, ResponseType, encoding);//textmessgae;//

        }
    }
}
