using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace RoadSpokenService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRoadSpokenWebService" in both code and config file together.
    [ServiceContract]
    public interface IRoadSpokenWebService
    {
        [OperationContract(Name = "Login")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Login")]
        System.ServiceModel.Channels.Message Login(LoginParam loginp);

        [OperationContract(Name = "RegisterUser")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegisterUser")]
        System.ServiceModel.Channels.Message RegisterUser(RegistrationParam loginp);

        [OperationContract(Name = "GetRoutePointList")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRoutePointList")]
        System.ServiceModel.Channels.Message GetRoutePointList(RouteParam objRoute);

        [OperationContract(Name = "GetAllRegion")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllRegion")]
        System.ServiceModel.Channels.Message GetAllRegion();

      
        [OperationContract(Name = "GetNearByRoutePointList")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetNearByRoutePointList")]
        System.ServiceModel.Channels.Message GetNearByRoutePointList(NearByRouteParam objRoute);

        [OperationContract(Name = "GetRegionByRoutePointList")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRegionByRoutePointList")]
        System.ServiceModel.Channels.Message GetRegionByRoutePointList(RegionRouteParam objRoute);

        [OperationContract(Name = "GetAllRoutePointsByHighway")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllRoutePointsByHighway")]
        System.ServiceModel.Channels.Message GetAllRoutePointsByHighway(HighwayParam objRoute);

        [OperationContract(Name = "GetAllPartners")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllPartners")]
        System.ServiceModel.Channels.Message GetAllPartners();

        [OperationContract(Name = "GetAllHighways")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllHighways")]
        System.ServiceModel.Channels.Message GetAllHighways();

        [OperationContract(Name = "ForgotPassword")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ForgotPassword")]
        System.ServiceModel.Channels.Message ForgotPassword(ForgotPassword loginp);

        [OperationContract(Name = "GetLastUpdatetimeStamp")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLastUpdatetimeStamp")]
        System.ServiceModel.Channels.Message GetLastUpdatetimeStamp();


        [OperationContract(Name = "GetAdList")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAdList")]
        System.ServiceModel.Channels.Message GetAdList();

        [OperationContract(Name = "GetNearByAdList")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetNearByAdList")]
        System.ServiceModel.Channels.Message GetNearByAdList(NearByRouteParam objRoute);

        [OperationContract(Name = "GetRegionByAdList")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRegionByAdList")]
        System.ServiceModel.Channels.Message GetRegionByAdList(RegionRouteParam objRoute);

        [OperationContract(Name = "GetAllAdByHighway")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllAdByHighway")]
        System.ServiceModel.Channels.Message GetAllAdByHighway(HighwayParam objRoute);
    }

    [DataContract]
    public class LoginParam
    {
        [DataMember]
        public string Email;
        [DataMember]
        public string Password;
    }

    [DataContract]
    public class RegistrationParam
    {
        [DataMember]
        public string email;
        [DataMember]
        public string password;
        [DataMember]
        public string username;
        [DataMember]
        public string phone;
    }

    [DataContract]
    public class ForgotPassword
    {
        [DataMember]
        public string Email;
      
    }
    [DataContract]
    public class RouteParam
    {
        [DataMember]
        public string Longitude;
        [DataMember]
        public string Latitude;
       
    }
    [DataContract]
    public class NearByRouteParam
    {
        [DataMember]
        public string Longitude;
        [DataMember]
        public string Latitude;
        [DataMember]
        public int Radius;
        [DataMember]
        public string Frequency;
        [DataMember]
        public int UserId;
    }
    [DataContract]
    public class RegionRouteParam
    {
        [DataMember]
        public string Region;
        [DataMember]
        public string Frequency;
        [DataMember]
        public int UserId;
       
    }
    [DataContract]
    public class HighwayParam
    {
        [DataMember]
        public int Highway;
        [DataMember]
        public string Frequency;
        [DataMember]
        public int UserId;

    }
}
