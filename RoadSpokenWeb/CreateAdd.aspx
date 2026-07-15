<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="CreateAdd.aspx.cs" Inherits="CreateAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/bootstrap-timepicker/compiled/timepicker.css" rel="stylesheet"
        type="text/css" />
    <script src="assets/bootstrap-timepicker/js/bootstrap-timepicker.js" type="text/javascript"></script>
     <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyATOL3Yl2vqQubW44Vkrn4J24d5-6mIlpA"></script>
    <%--    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&key=AIzaSyATOL3Yl2vqQubW44Vkrn4J24d5-6mIlpA&signed_in=true&libraries=places"></script>--%>
    <script type="text/javascript">
    var geocoder = new google.maps.Geocoder();
    var markers=[];
    var markersLat = [];
    var markersLong = [];  
    var markers2=[];
    var markersLat2 = [];
    var markersLong2 = [];
     var markers3=[];
    var markersLat3 = [];
    var markersLong3 = [];
    function geocodePosition(pos) {
        geocoder.geocode({
            latLng: pos
        }, function (responses) {
            if (responses && responses.length > 0) {
                updateMarkerAddress(responses[0].formatted_address);
                 var longg= document.getElementById('<%=txtLongitude.ClientID %>').value ;
       var lat=document.getElementById('<%=txtLatitude.ClientID %>').value ;
                PageMethods.GetRegion(lat,longg, onSucess, onError);
            } else {
                updateMarkerAddress('Cannot determine address at this location.');
            }
        });
    }
   

    function updateMarkerPosition(latLng) {

        document.getElementById('<%=txtLongitude.ClientID %>').value = latLng.lng();
       document.getElementById('<%=txtLatitude.ClientID %>').value = latLng.lat();
   
    }
     
     function callUpdate(lat,lang) {
            $.ajax({
                type: "POST",
                url: "IntermediatePointManager.aspx/GetRegion",
                data: { lat : lat, longi :  lang },
                contentType:
                "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                  alert(msg);
                }
            });
           
        }

     function onSucess(result) {               
               document.getElementById('<%=hdnregion.ClientID %>').value  =result;
            }
            function onError(result) {
                //alert('Something wrong.');
            }
     function blankMarker() {
        document.getElementById('<%=txtLongitude.ClientID %>').value=""; 
       document.getElementById('<%=txtLatitude.ClientID %>').value = "";
       document.getElementById('<%=txtAddress.ClientID %>').value="";
    }
       
    function updateMarkerAddress(str) {
    
      document.getElementById('<%=txtAddress.ClientID %>').value = str;
    }
   
    function LoadMaps()
    {
     var latLng = new google.maps.LatLng(<%=lat1 %>, <%=lng1 %>);
        var mapOptions = {
            center: new google.maps.LatLng(<%=lat1 %>, <%=lng1 %>),
            zoom: 5,
            streetViewControl:false,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
         map.setZoom(14);
       <%=javascrpt %>
       for (var i = 0; i < markersLat.length; i++) {
            //Attach click event handler to the marker.            
              var markerinuse = new google.maps.Marker({
                position: new google.maps.LatLng(markersLat[i], markersLong[i]),
                map: map,
                draggable: true

            });
            map.setZoom(14);
            map.panTo(markerinuse.position);
            google.maps.event.addListener(markerinuse, "click", function (e) {
                var infoWindow = new google.maps.InfoWindow({
                    content: 'Latitude: ' + markerinuse.getPosition().lat() + '<br />Longitude: ' + markerinuse.getPosition().lng()
                });
                infoWindow.open(map, markerinuse);
            });
            google.maps.event.addListener(markerinuse, "dblclick", function (e) {
                markerinuse.setMap(null)
                blankMarker();
                 markers.pop(markerinuse);
            });
        
            // Add dragging event listeners.
            google.maps.event.addListener(markerinuse, 'dragstart', function () {
                updateMarkerAddress('Dragging...');
            });

            google.maps.event.addListener(markerinuse, 'drag', function () {
//                updateMarkerStatus('Dragging...');
                updateMarkerPosition(markerinuse.getPosition());
//                updateMarkerLatPosition(markerinuse.getPosition());
            });

            google.maps.event.addListener(markerinuse, 'dragend', function () {
//                updateMarkerStatus('Drag ended');
                geocodePosition(markerinuse.getPosition());
            });
        }
    
        //Attach click event handler to the map.
         
        google.maps.event.addListener(map, 'click', function (e) {
         if(markers.length==0)
          {
            //Determine the location where the user has clicked.
            var location = e.latLng;

            //Create a marker and placed it on the map.
            var marker = new google.maps.Marker({
                position: location,
                map: map,
                draggable: true
            });

 
            //Attach click event handler to the marker.
            google.maps.event.addListener(marker, "click", function (e) {
                var infoWindow = new google.maps.InfoWindow({
                    content: 'Latitude: ' + marker.getPosition().lat() + '<br />Longitude: ' + marker.getPosition().lng()
                });
                infoWindow.open(map, marker);
            });
            google.maps.event.addListener(marker, "dblclick", function (e) {
                marker.setMap(null)
                blankMarker();
                 markers.pop(marker);
            });
            updateMarkerPosition(marker.getPosition());
//            updateMarkerLatPosition(latLng);
            geocodePosition(marker.getPosition());

            // Add dragging event listeners.
            google.maps.event.addListener(marker, 'dragstart', function () {
                updateMarkerAddress('Dragging...');
            });

            google.maps.event.addListener(marker, 'drag', function () {
//                updateMarkerStatus('Dragging...');
                updateMarkerPosition(marker.getPosition());
//               updateMarkerLatPosition(marker.getPosition());
            });

            google.maps.event.addListener(marker, 'dragend', function () {
//                updateMarkerStatus('Drag ended');
                geocodePosition(marker.getPosition());
            });

            markers.push(marker);
           }
        });
  
    }
    window.onload = function () {

        LoadMaps();
    }

    
    


    
    </script>
     
      <script language="javascript" type="text/javascript">
    
        $(document).ready(function () {

            $("#<%=txtStartDate.ClientID%>").datepicker({
                showOn: "button",
                format: "mm/dd/yyyy",
                buttonImage: "Images/calendar.gif",
                buttonImageOnly: true

            });
            $("#<%=txtEndDate.ClientID%>").datepicker({
                showOn: "button",
                format: "mm/dd/yyyy",
                buttonImage: "Images/calendar.gif",
                buttonImageOnly: true

            });
           
        });
             

      
        function ValidateDate() {
            if (Page_ClientValidate("Save")) {
                var startDate = document.getElementById('<%=txtStartDate.ClientID %>').value;
                var endDate = document.getElementById('<%=txtEndDate.ClientID %>').value;
                var startHour = document.getElementById('<%=ddlHour.ClientID %>').value;
                var endHour = document.getElementById('<%=ddlEndHour.ClientID %>').value;
                var startMinute = document.getElementById('<%=ddlMinute.ClientID %>').value;
                var endMinute = document.getElementById('<%=ddlEndMinute.ClientID %>').value;
                if (startDate != '' && endDate != '') {
                    var startDate1 = startDate.split("/");
                    var StartDate = startDate1[0] + "/" + startDate1[1] + "/" + startDate1[2];                   
                    var endDate1 = endDate.split("/");
                    var EndDate = endDate1[0] + "/" + endDate1[1] + "/" + endDate1[2];
                    var start = new Date(StartDate);
                    var end = new Date(EndDate);
                    if (startHour != 'Select' && startMinute != 'Select' && endHour != 'Select' && endMinute != 'Select') {
                        var StartTime = parseInt((startHour * 60)) + parseInt(startMinute);
                        var EndTime = parseInt((endHour * 60)) + parseInt(endMinute);

                        if (end < start) {
                            alert('please enter valid End Date');
                            return false;
                        }

                        else if (StartTime >= EndTime && start >= end) {
                            alert('please enter Valid End Time');
                            return false;
                        }

                        else {
                            //return true;                           
                            var stDate = toTimestamp(startDate1[2], startDate1[0], startDate1[1], startHour, startMinute, 0);                          
                            var enDate = toTimestamp(endDate1[2], endDate1[0], endDate1[1], endHour, endMinute, 0);

                            document.getElementById('<%=hdnStartDate2.ClientID %>').value = stDate;
                            document.getElementById('<%=hdnEndDate2.ClientID %>').value = enDate;

                        }
                    }
                    else {
                        alert('please enter Valid Start and End time');
                        return false;
                    }
                }
                else {
                    alert('please enter Valid Start and End date');
                    return false;
                }
            }
        }
        function convertUTCDateToLocalDate(date) {
            var newDate = new Date(date.getTime() + date.getTimezoneOffset() * 60 * 1000);

            var offset = date.getTimezoneOffset() / 60;
            var hours = date.getHours();

            newDate.setHours(hours - offset);

            return newDate;
        }
        function toTimestamp(year, month, day, hour, minute, second) {
            var datum = new Date(Date.UTC(year, month - 1, day, hour, minute, second));
            var newDate = new Date(datum.getTime() + datum.getTimezoneOffset() * 60 * 1000);
            return newDate.getTime(); /// 1000;
        }
        function FillDateTime() {
            var ValidStartDate = $get('<%=hdnStartDate.ClientID %>').value;
            var ValidEndDate = $get('<%=hdnEndDate.ClientID %>').value;

            if (ValidStartDate != '' && ValidEndDate != '') {
                var validStartDate = ValidStartDate.split("/");
               
                var d = new Date(validStartDate[2] + '/' + validStartDate[1] + '/' + validStartDate[0] + ' ' + validStartDate[3] + ':' + validStartDate[4] + ':' + validStartDate[5] + ' UTC');
              
                var startdate = d.toLocaleDateString();
                var starttime = d.toLocaleTimeString();
              
                var validEndDate = ValidEndDate.split("/");
               
                var d1 = new Date(validEndDate[2] + '/' + validEndDate[1] + '/' + validEndDate[0] + ' ' + validEndDate[3] + ':' + validEndDate[4] + ':' + validEndDate[5] + ' UTC');

                var enddate = d1.toLocaleDateString();
                var endtime = d1.toLocaleTimeString();
                var stardatesplit = startdate.split('/');
                var startimesplit = starttime.split(':');
                var enddatesplit = enddate.split('/');
                var endimesplit = endtime.split(':');
                document.getElementById('<%=txtStartDate.ClientID %>').value = stardatesplit[0] + "/" + stardatesplit[1] + "/" + stardatesplit[2];
                var starthour = startimesplit[0];
                var startmnt = startimesplit[1];

                var startAMPM = startimesplit[2].toString().split(' ')[1];
                var endAMPM = endimesplit[2].toString().split(' ')[1];
                var endhour = endimesplit[0];
                var endmnt = endimesplit[1];
                if (startAMPM == 'PM') {
                    starthour = parseInt(starthour) + 12;
                }
                if (endAMPM == 'PM') {
                    endhour = parseInt(endhour) + 12;
                }
                if (parseInt(starthour) < 10) {
                    starthour = "0" + parseInt(starthour).toString();
                }

                if (parseInt(startmnt) < 10) {
                    startmnt = "0" + parseInt(startmnt).toString();
                }

                if (parseInt(endhour) < 10) {
                    endhour = "0" + parseInt(endhour).toString();
                }
                if (parseInt(endmnt) < 10) {
                    endmnt = "0" + parseInt(endmnt).toString();
                }
                if (starthour == "24") {
                    starthour = "00";
                }
                if (endhour == "24") {
                    endhour = "00";
                }
                if (startmnt == "60") {
                    startmnt = "00";
                }
                if (endmnt == "60") {
                    endmnt = "00";
                }
                document.getElementById('<%=ddlHour.ClientID %>').value = starthour;
                document.getElementById('<%=ddlMinute.ClientID %>').value = startmnt;

                document.getElementById('<%=txtEndDate.ClientID %>').value = enddatesplit[0] + "/" + enddatesplit[1] + "/" + enddatesplit[2];
                document.getElementById('<%=ddlEndHour.ClientID %>').value = endhour;
                document.getElementById('<%=ddlEndMinute.ClientID %>').value = endmnt;
            }
        }
 

</script>
 <script type="text/javascript">
     function isNumberKey(evt) {
         var charCode = (evt.which) ? evt.which : evt.keyCode;
         if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
             return false;

         return true;
     }

       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="sd" runat="server"></asp:ScriptManager>
<asp:HiddenField ID="hdnStartDate" runat="server" />
<asp:HiddenField ID="hdnEndDate" runat="server" />

<asp:HiddenField ID="hdnStartDate2" runat="server" />
<asp:HiddenField ID="hdnEndDate2" runat="server" />
  <asp:HiddenField ID="hdnregion" runat="server" Value="" />
<section class="content-header">
   <div>
         <div>
                <h1>
           <%--       $("#<%=txtStartTime.ClientID%>").timepicker();
           $("#<%=txtEndTime.ClientID%>").timepicker();--%>
                    Ad
               </h1>
            </div>
        </div>
         <div  runat="server" id="dvError" visible="false">
            <div style="width: 100%; text-align: center;">
                <div>
                    <p>
                        <asp:Literal Text="" ID="ltrError" runat="server" />
                    </p>
                </div>
            </div>
            </div>
  </section>
 
 <section class="content">  
<div class="box box-primary" id="dvAddEdit" runat="server" visible="false">
                <div class="box-header with-border">
                  <h4 class="box-title">Add/Edit Ad</h4>
                </div><!-- /.box-header -->
                <!-- form start -->
                <form role="form">
                  <div class="box-body">
                  <asp:HiddenField ID="hdnId" runat="server" Value="0" />
                  <table>
                  <tr><td colspan="4">
                  <asp:UpdatePanel ID="up" runat="server"><ContentTemplate>
                 <table>
                     <tr>
                    <td colspan="4">
                        <div class="form-group">
                            <asp:Label ID="Label3" Width="120px" runat="server" Text=" Highway Runs :" class="col-sm-2 control-label"></asp:Label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlHighway" runat="server"  Style="width: 500px;">
                                   <%-- <asp:ListItem Text="Select" Value="-1"></asp:ListItem>--%>
                                    <asp:ListItem Text="Northbound/Southbound" Value="Northbound/Southbound"></asp:ListItem>
                                    <asp:ListItem Text="Eastbound/Westbound" Value="Eastbound/Westbound"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHighway" runat="server" ErrorMessage="Please Select Highway Runs"
                                    InitialValue="-1" ControlToValidate="ddlHighway" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </td>
                </tr> 
                  <tr>
                    <td colspan="4" style="padding-top:10px">
                        <div class="form-group">
                            <asp:Label ID="Label4" Width="120px" runat="server" Text="Highway Name:" class="col-sm-2 control-label"></asp:Label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlHighwayName" runat="server"  Style="width: 500px;"  AutoPostBack="true" OnSelectedIndexChanged="ddlHighwayName_SelectedIndex">
                                   <%-- <asp:ListItem Text="Select" Value="-1"></asp:ListItem>--%>
                                
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Select Highway "
                                    InitialValue="-1" ControlToValidate="ddlHighwayName" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </td>
                </tr>
                    <tr>
                    <td colspan="4" style="padding-top:10px">
                        <div class="form-group">
                            <asp:Label ID="Label6" Width="120px" runat="server" Text="Interstate:" class="col-sm-2 control-label"></asp:Label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlRegion" runat="server"  Style="width: 500px;" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndex">                                
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Select Interstate "
                                    InitialValue="-1" ControlToValidate="ddlRegion" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </td>
                </tr>
               <%--  <tr>
                    <td colspan="4" style="padding-top:10px">
                        <div class="form-group">
                            <asp:Label ID="Label1" Width="120px" runat="server" Text="Way Point:" class="col-sm-2 control-label"></asp:Label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlWaypoint" runat="server"  Style="width: 500px;">                                
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select way point "
                                    InitialValue="-1" ControlToValidate="ddlWaypoint" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </td>
                </tr>--%>
             </table>
                </ContentTemplate>
                <Triggers >
                <asp:AsyncPostBackTrigger ControlID="ddlHighwayName" EventName="SelectedIndexChanged" />
                 <asp:AsyncPostBackTrigger ControlID="ddlRegion" EventName="SelectedIndexChanged" />
                  
                </Triggers>
                 </asp:UpdatePanel>
                    </td></tr>
                    <tr><td colspan="4">
                    <table>
                     <tr>
                     <td rowspan="3">
                      <div id="dvMap" style="width: 360px; height: 480px">
                            </div>
                     </td>

                                        <td>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    Address :</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtAddress" runat="server" class="form-control" Style="width: 400px;"
                                                        placeholder="Enter Address" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                                                        ErrorMessage="Point Address is Required" ValidationGroup="Save">* </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    Longitude:</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtLongitude" runat="server" Style="width: 400px;" class="form-control"
                                                        placeholder="Enter Longitude" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvLongitude" runat="server" ErrorMessage="Longitude is Required"
                                                        ControlToValidate="txtLongitude" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                 
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    Latitude :</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtLatitude" runat="server" ValidationGroup="Save" Style="width: 400px;"
                                                        class="form-control" placeholder="Enter Latitude" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvLati" runat="server" ErrorMessage="Latitude is Required"
                                                        ControlToValidate="txtLatitude" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                    </table>
                    
                    </td></tr>
                    

                 <tr>
                                        <td>
                                            <div >
                                                <label class="col-sm-2 control-label">
                                                    Promo Image :</label>
                                                <div >
                                                    <asp:FileUpload ID="imageUpload" runat="server"  Style="width: 500px;">
                                                    </asp:FileUpload>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate>   
                                                      <asp:LinkButton ID="lnkimageUpload" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkimageUpload_Click"></asp:LinkButton>
                                                           <asp:ImageButton ID="imgBtn4" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" OnClick="imgBtn4_Click" Visible="false" />
                                                        </ContentTemplate>
                                                       
                                                        </asp:UpdatePanel> 
                                                    
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            <div  >
                                                <label class="col-sm-2 control-label" >
                                                    Audio :</label>
                                                <div  style="padding-bottom: 10px">
                                                    <asp:FileUpload ID="audioUpload" runat="server"  Style="width: 500px;">
                                                    </asp:FileUpload>
                                                   <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>       <asp:LinkButton ID="lnkaudioUpload" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkaudioUpload_Click"></asp:LinkButton>
                                                           <asp:ImageButton ID="imgBtn2" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" OnClick="imgBtn2_Click" Visible="false"/>
                                                        </ContentTemplate></asp:UpdatePanel> 
                                                    
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <div  >
                                                <label class="col-sm-2 control-label" >
                                                  Intro Audio :</label>
                                                <div  style="padding-bottom: 10px">
                                                    <asp:FileUpload ID="flIntro" runat="server"  Style="width: 500px;">
                                                    </asp:FileUpload>
                                                   <asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate>       <asp:LinkButton ID="lnkIntroUoload" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkIntroUoload_Click"></asp:LinkButton>
                                                           <asp:ImageButton ID="imgbtnIntro" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" OnClick="imgbtnIntro_Click" Visible="false"/>
                                                        </ContentTemplate></asp:UpdatePanel> 
                                                    
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <div  >
                                                <label class="col-sm-2 control-label" >
                                                   Conclusion Audio :</label>
                                                <div  style="padding-bottom: 10px">
                                                    <asp:FileUpload ID="flConclu" runat="server"  Style="width: 500px;">
                                                    </asp:FileUpload>
                                                   <asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate>       <asp:LinkButton ID="lnkConUpload" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkConUpload_Click"></asp:LinkButton>
                                                           <asp:ImageButton ID="imgbtnCon" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" OnClick="imgbtnCon_Click" Visible="false"/>
                                                        </ContentTemplate></asp:UpdatePanel> 
                                                    
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                                <label class="col-sm-2 control-label" style="width: 110px">
                                                    Promo Text :</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtPromotext" runat="server" Style="width: 500px;" placeholder="Enter Promo Text"
                                                        class="form-control"></asp:TextBox>
                                                  
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                                <label class="col-sm-2 control-label" style="width: 110px">
                                                  Radius :</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtRadius" runat="server" MaxLength="7"  placeholder="Enter Radius" Style="width: 500px;"  onkeypress="return isNumberKey(event)" 
                                                        class="form-control"></asp:TextBox>
                                                  
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                     <tr>
                                       <td colspan="4">
                                            <div class="form-group">
                                             
                                                <asp:Label ID="lblText1" Font-Bold="true" Width="110px" runat="server" Text=" Text for TTS " class="col-sm-2 control-label"></asp:Label>
                                                
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtTTS" runat="server" Style="width: 700px; height: 400px;"
                                                        TextMode="MultiLine" placeholder="Enter Text" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNewsText" runat="server" ErrorMessage="Text is Required"
                                                        ControlToValidate="txtTTS" ValidationGroup="Save" Enabled="false">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                            <table><tr> <label class="col-sm-2 control-label" style="width: 110px">
                                                  Start Date :</label><td></td>
                                            <td>   <div class="col-sm-10">
                                                    <asp:TextBox ID="txtStartDate" runat="server"  placeholder="Enter Start Date" Style="width: 200px;"
                                                        class="form-control"></asp:TextBox>
                                           <%--     <asp:TextBox ID="txtStartTime" runat="server"  placeholder="Enter Start Date" Style="width: 100px;"
                                                        class="form-control"></asp:TextBox>--%>
                                                  
                                                </div></td>
                                            <td>   <label class="control-label">
                                                  Start Time :</label></td>
                                            <td>         <div class="controls">
                                                    <asp:DropDownList ID="ddlHour" runat="server" ValidationGroup="Save" 
                                                        Width="120px">
                                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                     <asp:ListItem Text="00 hour" Value="00"></asp:ListItem>
                                                    <asp:ListItem Text="01 hour" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="02 hour" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="03 hour" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="04 hour" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="05 hour" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="06 hour" Value="06"></asp:ListItem>  
                                                    <asp:ListItem Text="07 hour" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="08 hour" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="09 hour" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="10 hour" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11 hour" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12 hour" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="13 hour" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="14 hour" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="15 hour" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="16 hour" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="17 hour" Value="17"></asp:ListItem>
                                                    <asp:ListItem Text="18 hour" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="19 hour" Value="19"></asp:ListItem>
                                                    <asp:ListItem Text="20 hour" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="21 hour" Value="21"></asp:ListItem>
                                                    <asp:ListItem Text="22 hour" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="23 hour" Value="23"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlMinute" runat="server" ValidationGroup="Save"  Width="120px">
                                                   <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                    <asp:ListItem Text="00 Minute" Value="00"></asp:ListItem>
                                                    <asp:ListItem Text="01 Minute" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="02 Minute" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="03 Minute" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="04 Minute" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="05 Minute" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="06 Minute" Value="06"></asp:ListItem>  
                                                    <asp:ListItem Text="07 Minute" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="08 Minute" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="09 Minute" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="10 Minute" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11 Minute" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12 Minute" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="13 Minute" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="14 Minute" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="15 Minute" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="16 Minute" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="17 Minute" Value="17"></asp:ListItem>
                                                    <asp:ListItem Text="18 Minute" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="19 Minute" Value="19"></asp:ListItem>
                                                    <asp:ListItem Text="20 Minute" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="21 Minute" Value="21"></asp:ListItem>
                                                    <asp:ListItem Text="22 Minute" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="23 Minute" Value="23"></asp:ListItem>
                                                    <asp:ListItem Text="24 Minute" Value="24"></asp:ListItem>
                                                    <asp:ListItem Text="25 Minute" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="26 Minute" Value="26"></asp:ListItem>
                                                    <asp:ListItem Text="27 Minute" Value="27"></asp:ListItem>
                                                    <asp:ListItem Text="28 Minute" Value="28"></asp:ListItem>
                                                    <asp:ListItem Text="29 Minute" Value="29"></asp:ListItem>
                                                    <asp:ListItem Text="30 Minute" Value="30"></asp:ListItem>  
                                                    <asp:ListItem Text="31 Minute" Value="31"></asp:ListItem>
                                                    <asp:ListItem Text="32 Minute" Value="32"></asp:ListItem>
                                                    <asp:ListItem Text="33 Minute" Value="33"></asp:ListItem>
                                                    <asp:ListItem Text="34 Minute" Value="34"></asp:ListItem>
                                                    <asp:ListItem Text="35 Minute" Value="35"></asp:ListItem>
                                                    <asp:ListItem Text="36 Minute" Value="36"></asp:ListItem>
                                                    <asp:ListItem Text="37 Minute" Value="37"></asp:ListItem>
                                                    <asp:ListItem Text="38 Minute" Value="38"></asp:ListItem>
                                                    <asp:ListItem Text="39 Minute" Value="39"></asp:ListItem>
                                                    <asp:ListItem Text="40 Minute" Value="40"></asp:ListItem>
                                                    <asp:ListItem Text="41 Minute" Value="41"></asp:ListItem>
                                                    <asp:ListItem Text="42 Minute" Value="42"></asp:ListItem>
                                                    <asp:ListItem Text="43 Minute" Value="43"></asp:ListItem>
                                                    <asp:ListItem Text="44 Minute" Value="44"></asp:ListItem>
                                                    <asp:ListItem Text="45 Minute" Value="45"></asp:ListItem>
                                                    <asp:ListItem Text="46 Minute" Value="46"></asp:ListItem>
                                                    <asp:ListItem Text="47 Minute" Value="47"></asp:ListItem>
                                                    <asp:ListItem Text="48 Minute" Value="48"></asp:ListItem>
                                                    <asp:ListItem Text="49 Minute" Value="49"></asp:ListItem>
                                                    <asp:ListItem Text="50 Minute" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="51 Minute" Value="51"></asp:ListItem>
                                                    <asp:ListItem Text="52 Minute" Value="52"></asp:ListItem>
                                                    <asp:ListItem Text="53 Minute" Value="53"></asp:ListItem>
                                                    <asp:ListItem Text="54 Minute" Value="54"></asp:ListItem>
                                                    <asp:ListItem Text="55 Minute" Value="55"></asp:ListItem>
                                                    <asp:ListItem Text="56 Minute" Value="56"></asp:ListItem>
                                                    <asp:ListItem Text="57 Minute" Value="57"></asp:ListItem>
                                                    <asp:ListItem Text="58 Minute" Value="58"></asp:ListItem>
                                                    <asp:ListItem Text="59 Minute" Value="59"></asp:ListItem>
                                                    </asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="rfvHour" runat="server" ControlToValidate="ddlHour" InitialValue="-1"  ErrorMessage="Please Select Start Time" ValidationGroup="Save"> </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfvMinute" runat="server" ControlToValidate="ddlMinute" InitialValue="-1"  ErrorMessage="Please Select Start Time" ValidationGroup="Save"> </asp:RequiredFieldValidator>
                                                </div></td>
                                            </tr></table>
                                               
                                             
                                                  
                                            </div>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                            <table><tr>
                                            
                                                <label class="col-sm-2 control-label" style="width: 110px">
                                                   End Date:</label><td></td>
                                            <td>  <div class="col-sm-10">
                                                    <asp:TextBox ID="txtEndDate" runat="server"  placeholder="Enter end Date" Style="width: 200px;"
                                                        class="form-control"></asp:TextBox>
                                                      <%--  <asp:TextBox ID="txtEndTime" runat="server"  placeholder="Enter Start Date" Style="width: 100px;"
                                                        class="form-control"></asp:TextBox>
                                                  --%>
                                                </div></td>
                                            <td>  <label class="control-label">
                                                 End Time :</label></td>
                                            <td>
                                              <div class="controls">
                                <asp:DropDownList ID="ddlEndHour" runat="server"  Width="120px">
                                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                     <asp:ListItem Text="00 hour" Value="00"></asp:ListItem>
                                                    <asp:ListItem Text="01 hour" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="02 hour" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="03 hour" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="04 hour" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="05 hour" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="06 hour" Value="06"></asp:ListItem>  
                                                    <asp:ListItem Text="07 hour" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="08 hour" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="09 hour" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="10 hour" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11 hour" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12 hour" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="13 hour" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="14 hour" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="15 hour" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="16 hour" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="17 hour" Value="17"></asp:ListItem>
                                                    <asp:ListItem Text="18 hour" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="19 hour" Value="19"></asp:ListItem>
                                                    <asp:ListItem Text="20 hour" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="21 hour" Value="21"></asp:ListItem>
                                                    <asp:ListItem Text="22 hour" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="23 hour" Value="23"></asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:DropDownList ID="ddlEndMinute" runat="server"  Width="120px">
                                                   <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                    <asp:ListItem Text="00 Minute" Value="00"></asp:ListItem>
                                                    <asp:ListItem Text="01 Minute" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="02 Minute" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="03 Minute" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="04 Minute" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="05 Minute" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="06 Minute" Value="06"></asp:ListItem>  
                                                    <asp:ListItem Text="07 Minute" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="08 Minute" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="09 Minute" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="10 Minute" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11 Minute" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12 Minute" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="13 Minute" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="14 Minute" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="15 Minute" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="16 Minute" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="17 Minute" Value="17"></asp:ListItem>
                                                    <asp:ListItem Text="18 Minute" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="19 Minute" Value="19"></asp:ListItem>
                                                    <asp:ListItem Text="20 Minute" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="21 Minute" Value="21"></asp:ListItem>
                                                    <asp:ListItem Text="22 Minute" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="23 Minute" Value="23"></asp:ListItem>
                                                    <asp:ListItem Text="24 Minute" Value="24"></asp:ListItem>
                                                    <asp:ListItem Text="25 Minute" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="26 Minute" Value="26"></asp:ListItem>
                                                    <asp:ListItem Text="27 Minute" Value="27"></asp:ListItem>
                                                    <asp:ListItem Text="28 Minute" Value="28"></asp:ListItem>
                                                    <asp:ListItem Text="29 Minute" Value="29"></asp:ListItem>
                                                    <asp:ListItem Text="30 Minute" Value="30"></asp:ListItem>  
                                                    <asp:ListItem Text="31 Minute" Value="31"></asp:ListItem>
                                                    <asp:ListItem Text="32 Minute" Value="32"></asp:ListItem>
                                                    <asp:ListItem Text="33 Minute" Value="33"></asp:ListItem>
                                                    <asp:ListItem Text="34 Minute" Value="34"></asp:ListItem>
                                                    <asp:ListItem Text="35 Minute" Value="35"></asp:ListItem>
                                                    <asp:ListItem Text="36 Minute" Value="36"></asp:ListItem>
                                                    <asp:ListItem Text="37 Minute" Value="37"></asp:ListItem>
                                                    <asp:ListItem Text="38 Minute" Value="38"></asp:ListItem>
                                                    <asp:ListItem Text="39 Minute" Value="39"></asp:ListItem>
                                                    <asp:ListItem Text="40 Minute" Value="40"></asp:ListItem>
                                                    <asp:ListItem Text="41 Minute" Value="41"></asp:ListItem>
                                                    <asp:ListItem Text="42 Minute" Value="42"></asp:ListItem>
                                                    <asp:ListItem Text="43 Minute" Value="43"></asp:ListItem>
                                                    <asp:ListItem Text="44 Minute" Value="44"></asp:ListItem>
                                                    <asp:ListItem Text="45 Minute" Value="45"></asp:ListItem>
                                                    <asp:ListItem Text="46 Minute" Value="46"></asp:ListItem>
                                                    <asp:ListItem Text="47 Minute" Value="47"></asp:ListItem>
                                                    <asp:ListItem Text="48 Minute" Value="48"></asp:ListItem>
                                                    <asp:ListItem Text="49 Minute" Value="49"></asp:ListItem>
                                                    <asp:ListItem Text="50 Minute" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="51 Minute" Value="51"></asp:ListItem>
                                                    <asp:ListItem Text="52 Minute" Value="52"></asp:ListItem>
                                                    <asp:ListItem Text="53 Minute" Value="53"></asp:ListItem>
                                                    <asp:ListItem Text="54 Minute" Value="54"></asp:ListItem>
                                                    <asp:ListItem Text="55 Minute" Value="55"></asp:ListItem>
                                                    <asp:ListItem Text="56 Minute" Value="56"></asp:ListItem>
                                                    <asp:ListItem Text="57 Minute" Value="57"></asp:ListItem>
                                                    <asp:ListItem Text="58 Minute" Value="58"></asp:ListItem>
                                                    <asp:ListItem Text="59 Minute" Value="59"></asp:ListItem>
                                                    </asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="rfvEndHour" runat="server" ControlToValidate="ddlEndHour" InitialValue="-1"  ErrorMessage="Please Select End Time" ValidationGroup="Save"> </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfvEndMinute" runat="server" ControlToValidate="ddlEndMinute" InitialValue="-1"  ErrorMessage="Please Select End Time" ValidationGroup="Save"> </asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            
                                            </tr></table>

                                              
                                            </div>
                                        </td>
                                    </tr>
                    </table>
                    
                  </div><!-- /.box-body -->

                  <div class="box-footer">
                  <asp:Button ID="btnSave" Text="Save"  ValidationGroup="Save" runat="server" OnClick="btnSave_Click" class="btn btn-primary" TabIndex="11"  OnClientClick=" return ValidateDate()" /> 
                  <asp:Button ID="btnCancel" Text="Cancel"  ValidationGroup="Save" runat="server" OnClick="btnCancel_Click" class="btn btn-primary" TabIndex="11" CausesValidation="False" /> 
                   <asp:ValidationSummary ID="val" runat="server" ShowMessageBox="true"  ValidationGroup="Save" ShowSummary="false"/>
                  </div>
                </form>
              </div>

                    <div  id="dvUserList" runat="server">
                     <div id="tab_2">
                        <div ><asp:LinkButton ID="lnkAddUser" runat="server" OnClick="lnkAdd_Click">Create Ad</asp:LinkButton>
                        <br /><br />
                          <div >
                          <div>
                                    <h2>
                                        <i ></i><span >Ad List</span>&nbsp;
                                    </h2>
                                </div>
                             <div>
                                    <!--/row-->
                           <br>
                           <div >
                          <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="false"  Width="96%"  PageSize="10" AllowPaging="true" 
                          OnPageIndexChanging="gvUser_OnPageindexchanging"
                                  onrowcommand="gvUser_RowCommand">
                                  <RowStyle HorizontalAlign="Center" />
                          <Columns>
                         
                      
                           <asp:TemplateField HeaderText="Ad Text">
                          <ItemTemplate>    
                          <%#Eval("adtext")%>
                          </ItemTemplate>
                          </asp:TemplateField>
                          
                           <asp:TemplateField HeaderText="Created Date">
                          <ItemTemplate>
                            <%#string.Format("{0:MM/dd/yy}", Eval("CreatedDate"))%>
                          </ItemTemplate>
                          </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                          <ItemTemplate>
                             <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("Id") %>' 
                            CommandName="EditItem"  Text="Edit"></asp:LinkButton>/
                          <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id") %>' 
                            CommandName="DeleteItem" OnClientClick="return confirm('Are you sure you want to delete this Ad?');" Text="Delete"></asp:LinkButton>
                          </ItemTemplate>
                          </asp:TemplateField>
                          </Columns>
                          </asp:GridView>
                          </div>
                          </div>
                          </div>
                        </div>
                        </div>
                </div>
</section>

</asp:Content>

