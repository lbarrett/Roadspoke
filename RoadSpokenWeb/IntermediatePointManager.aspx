<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="IntermediatePointManager.aspx.cs" Inherits="IntermediatePointManager" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript">
//    $(document).ready(function () {
//        var div = document.getElementById("dvWayPoint");
//        div.style.display = "none";
//    });

    function Show_Hide_By_Display() {
        var div1 = document.getElementById("dvWayPoint");

        if (div1.style.display == "" || div1.style.display == "block") {
            div1.style.display = "none";
        }
        else {

            div1.style.display = "block";
        }
        return false;
    }
    

</script>
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
     function geocodePosition2(pos) {
        geocoder.geocode({
            latLng: pos
        }, function (responses) {
            if (responses && responses.length > 0) {
                updateMarkerAddress2(responses[0].formatted_address);
                 var longg= document.getElementById('<%=txtLongitude2.ClientID %>').value ;
       var lat=document.getElementById('<%=txtLatitude2.ClientID %>').value ;
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
     function updateMarkerPosition2(latLng) {

        document.getElementById('<%=txtLongitude2.ClientID %>').value = latLng.lng();
       document.getElementById('<%=txtLatitude2.ClientID %>').value = latLng.lat();
   
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
       function blankMarker2() {
        document.getElementById('<%=txtLongitude2.ClientID %>').value=""; 
       document.getElementById('<%=txtLatitude2.ClientID %>').value = "";
       document.getElementById('<%=txtAddress2.ClientID %>').value="";
    }
    function updateMarkerAddress(str) {
    
      document.getElementById('<%=txtAddress.ClientID %>').value = str;
    }
     function updateMarkerAddress2(str) {
    
      document.getElementById('<%=txtAddress2.ClientID %>').value = str;
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
    CreateMap2();
    }
    window.onload = function () {

        LoadMaps();
    }

    function CreateMap2()
    {
     var latLng = new google.maps.LatLng(<%=lat1 %>, <%=lng1 %>);
        var mapOptions = {
            center: new google.maps.LatLng(<%=lat1 %>, <%=lng1 %>),
            zoom: 5,
            streetViewControl:false,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("dvMap2"), mapOptions);
         map.setZoom(14);
       <%=javascrpt2 %>
       for (var i = 0; i < markersLat2.length; i++) {
            //Attach click event handler to the marker.            
              var markerinuse = new google.maps.Marker({
                position: new google.maps.LatLng(markersLat2[i], markersLong2[i]),
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
                blankMarker2();
                 markers2.pop(markerinuse);
            });
        
            // Add dragging event listeners.
            google.maps.event.addListener(markerinuse, 'dragstart', function () {
                updateMarkerAddress2('Dragging...');
            });

            google.maps.event.addListener(markerinuse, 'drag', function () {
//                updateMarkerStatus('Dragging...');
                updateMarkerPosition2(markerinuse.getPosition());
//                updateMarkerLatPosition(markerinuse.getPosition());
            });

            google.maps.event.addListener(markerinuse, 'dragend', function () {
//                updateMarkerStatus('Drag ended');
                geocodePosition2(markerinuse.getPosition());
            });
        }
    
        //Attach click event handler to the map.
         
        google.maps.event.addListener(map, 'click', function (e) {
         if(markers2.length==0)
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
                blankMarker2();
                 markers2.pop(marker);
            });
            updateMarkerPosition2(marker.getPosition());
            geocodePosition2(marker.getPosition());

            // Add dragging event listeners.
            google.maps.event.addListener(marker, 'dragstart', function () {
                updateMarkerAddress2('Dragging...');
            });

            google.maps.event.addListener(marker, 'drag', function () {
                updateMarkerPosition2(marker.getPosition());
            });

            google.maps.event.addListener(marker, 'dragend', function () {
                geocodePosition2(marker.getPosition());
            });

            markers2.push(marker);
           }
        });
    }

     function CreateMap3()
    {
     var latLng = new google.maps.LatLng(<%=lat1 %>, <%=lng1 %>);
        var mapOptions = {
            center: new google.maps.LatLng(<%=lat1 %>, <%=lng1 %>),
            zoom: 5,
            streetViewControl:false,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("dvMap3"), mapOptions);
         map.setZoom(12);
    
       <%=javascrpt3 %>
       for (var i = 0; i < markersLat3.length; i++) {
            //Attach click event handler to the marker.            
              var markerinuse = new google.maps.Marker({
                position: new google.maps.LatLng(markersLat3[i], markersLong3[i]),
                map: map
            });
             map.setZoom(12);
            map.panTo(markerinuse.position);
            google.maps.event.addListener(markerinuse, "click", function (e) {
                var infoWindow = new google.maps.InfoWindow({
                    content: 'Latitude: ' + markerinuse.getPosition().lat() + '<br />Longitude: ' + markerinuse.getPosition().lng()
                });
                infoWindow.open(map, markerinuse);
            });
            
            
        }
    
        //Attach click event handler to the map.
       //Show_Hide_By_Display();
    }


    function ChangeHighwayRun()
    {
    if( $("#<%=ddlHighway.ClientID %>").val()=="Northbound/Southbound")
    {    
    $("#<%=lblDirection1.ClientID %>").text('Northbound Point ▲');
    $("#<%=lbldirection2.ClientID %>").text('Southbound Point ▼');
     $("#<%=lblText1.ClientID %>").text('Text for TTS NorthBound (▲)');
    $("#<%=lblText2.ClientID %>").text('Text for TTS SouthBound (▼)');
    }
    else
    {
    $("#<%=lblDirection1.ClientID %>").text('Eastbound Point ►');
    $("#<%=lbldirection2.ClientID %>").text('Westbound Point ◄');
     $("#<%=lblText1.ClientID %>").text('Text for TTS EastBound (►)');
    $("#<%=lblText2.ClientID %>").text('Text for TTS WestBound (◄)');
    }
    }
    </script>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }


       
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sc" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdnregion" runat="server" Value="" />
    <section class="content-header">
        <div>
            <div>
                <h1>
                    Route Points    
                </h1>
            </div>
        </div>
        <div runat="server" id="dvError" visible="false">
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
  

<asp:HiddenField ID="hdnRouteId" runat="server" Value="0"></asp:HiddenField>
    <asp:HiddenField ID="hdn2" runat="server" Value="" />
        <div class="box box-primary" id="dvAddEdit" runat="server" visible="false">
            <div>
                <asp:LinkButton ID="leftLinkBtn" runat="server" OnClick="LeftBtnClick" ValidationGroup="Save">
                    <img id="leftImg" runat="server" src="Images/left%20btn.png" alt="" style="width: 50px;" /></asp:LinkButton>
                 
                <asp:LinkButton ID="rightLinkBtn" runat="server" OnClick="RightBtnClick" ValidationGroup="Save">
                    <img id="rightImg" runat="server" src="Images/right%20btn.png" alt="" style="float: right;
                        width: 50px;" /></asp:LinkButton>
            </div>
            <div class="box-header with-border">
                <h4 class="box-title">
                    Add/Edit Route Points </h4><asp:Panel id="dvMiddle" runat="server" visible="false" style="text-align:center;float:right">  <asp:Button ID="btnAddMiddleRoute" runat="server" OnClick="btnAddMiddleRoute_Click" class="btn btn-primary" Text="Insert New route point in the middle." /></asp:Panel>
            </div>

             <%-- /*=====================================================================*/--%>
    <div id="dvWayPoint" style="position:absolute;z-index:1000;border:1px solid black;background-color:Aqua">
  <div><div style="float:left"><b>NearBy WayPoints</b></div>  <div><img src="images/delete.png" onclick="Show_Hide_By_Display()"  style="float:right;cursor:pointer"/></div></div>
                            <div id="dvMap3" style="width: 500px; height: 480px;float:left">
                            </div>
                        </div>
            <!-- /.box-header -->
            <!-- form start -->
            <form role="form">
            <table>
            <tr>
                       <td colspan="4" style="padding-top:10px">
                        <div class="form-group">
                            <asp:Label ID="Label7" Width="120px" runat="server" Text="Chambers :" class="col-sm-2 control-label"></asp:Label>
                            <div class="col-sm-10">
                                <asp:RadioButtonList ID="ddlDay" runat="server"  Style="width: 500px;" RepeatColumns="7" Enabled="false" AutoPostBack="true"  OnSelectedIndexChanged="ddlDay_SelectedIndexChanged">
                                 
                                    <asp:ListItem Text="Chamber 1" Value="Monday" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Chamber 2" Value="Tuesday" ></asp:ListItem>
                                    <asp:ListItem Text="Chamber 3" Value="Wednesday"></asp:ListItem>
                                    <asp:ListItem Text="Chamber 4" Value="Thursday" ></asp:ListItem>
                                    <asp:ListItem Text="Chamber 5" Value="Friday"></asp:ListItem>
                                    <asp:ListItem Text="Chamber 6" Value="Saturday" ></asp:ListItem>
                                    <asp:ListItem Text="Chamber 7" Value="Sunday" ></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:HiddenField ID="hdnDay" runat="server" Value="Monday" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Select Day"
                                    InitialValue="-1" ControlToValidate="ddlDay" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </td>
                </tr>
                 <tr>
                       <td colspan="4" style="padding-top:10px">
                        <div class="form-group">
                            <asp:Label ID="Label8" Width="120px" runat="server" Text=" Frequency :" class="col-sm-2 control-label"></asp:Label>
                            <div class="col-sm-10">
                            <asp:CheckBox ID="chkFrequency" runat="server" Text="Occasional" />
                               <%-- <asp:DropDownList ID="ddlFrequency" runat="server"  Style="width: 500px;" >
                                 
                                 <asp:ListItem Text="Always" Value="Always" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Occasional" Value="Occasional" ></asp:ListItem>
                                    <asp:ListItem Text="Intermittent" Value="Intermittent" ></asp:ListItem>
                                    <asp:ListItem Text="Frequent" Value="Frequent" ></asp:ListItem>                                   
                                  
                                </asp:DropDownList>--%>
                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please Select Day"
                                    InitialValue="-1" ControlToValidate="ddlDay" ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="padding-top:10px">
                        <div class="form-group">
                            <asp:Label ID="Label3" Width="120px" runat="server" Text=" Highway Runs :" class="col-sm-2 control-label"></asp:Label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlHighway" runat="server" onchange="ChangeHighwayRun()" Style="width: 500px;">
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
                                <asp:DropDownList ID="ddlHighwayName" runat="server"  Style="width: 500px;" Enabled="false">
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
                                <asp:DropDownList ID="ddlRegion" runat="server"  Style="width: 500px;">                                
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Select Interstate "
                                    InitialValue="-1" ControlToValidate="ddlRegion" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </td>
                </tr>
                   <tr>
                    <td colspan="4" style="padding-top:10px">
                            <div class="col-sm-10">
                       <asp:LinkButton ID="lnkMap" runat="server" Font-Underline="True"  OnClientClick="return Show_Hide_By_Display()">NearBy WayPoint On Map</asp:LinkButton>      
                     <%-- <asp:LinkButton ID="lnkMap" runat="server" Font-Underline="True" OnClick="Map_Click" OnClientClick="return Show_Hide_By_Display()">NearBy WayPoint On Map</asp:LinkButton> --%>    
                            </div>
                    </td>
                </tr>
                <tr id="trLoc1">
                <td colspan="4"><table>
                <tr><td colspan="4" style="height:5px">&nbsp;</td></tr>
                <tr style="margin-top:10px"><td colspan="4" style="height:20px;color:Black;background-color:Aqua;text-align:center;font-size:larger;font-weight:bold"><asp:Label ID="lblDirection1" runat="server" Text="Northbound Point ▲"></asp:Label>  </td></tr>
                <tr>
                 <td style="vertical-align: top">
                        <div style="float: left; padding-top: 15px;" id="dvMapOuter1" runat="server" >
                            <div id="dvMap" style="width: 360px; height: 480px" >
                            </div>
                        </div>
                    </td>
                    <td>
                        <div>
                            <div class="box-body">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="hdnVideoValue" runat="server" Value="0"></asp:HiddenField>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    Sorting :</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtSorting" runat="server" class="form-control" Style="width: 500px;"
                                                        placeholder="Enter Sorting" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RangeValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSorting"
                                                        MinimumValue="1" MaximumValue="99999" Type="Integer" ErrorMessage="Sorting is Required and must be numeric"
                                                        ValidationGroup="Save">* </asp:RangeValidator>
                                                          <asp:RequiredFieldValidator ID="RangeValidator2" runat="server" ControlToValidate="txtSorting"
                                                         ErrorMessage="Sorting is Required "
                                                        ValidationGroup="Save">* </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    Address :</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtAddress" runat="server" class="form-control" Style="width: 500px;"
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
                                                    <asp:TextBox ID="txtLongitude" runat="server" Style="width: 500px;" class="form-control"
                                                        placeholder="Enter Longitude" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvLongitude" runat="server" ErrorMessage="Longitude is Required"
                                                        ControlToValidate="txtLongitude" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                    <asp:HiddenField ID="hdnuserid" runat="server" Value="0" />
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
                                                    <asp:TextBox ID="txtLatitude" runat="server" ValidationGroup="Save" Style="width: 500px;"
                                                        class="form-control" placeholder="Enter Latitude" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvLati" runat="server" ErrorMessage="Latitude is Required"
                                                        ControlToValidate="txtLatitude" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                                <label class="col-sm-2 control-label" style="width: 110px">
                                                    Introductory Music :</label>
                                                <div class="col-sm-10" style="padding-bottom: 10px">
                                                    <asp:FileUpload ID="flIntroductory" runat="server" class="form-control" Style="width: 500px;">
                                                    </asp:FileUpload>

                                                 <asp:UpdatePanel ID="up1" runat="server"><ContentTemplate>  <asp:LinkButton ID="lnkflIntroductory" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkflIntroductory_Click"></asp:LinkButton>
                                                        <asp:ImageButton ID="imgBtn1" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" OnClick="imgBtn1_Click" Visible="false"/>
                                                        </ContentTemplate></asp:UpdatePanel> 
                                                    
                                                </div>
                                            </div>
                                        </td>
                                    </tr><tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                                <label class="col-sm-2 control-label" style="width: 110px">
                                                    Conclusion Music :</label>
                                                <div class="col-sm-10" style="padding-bottom: 10px">
                                                    <asp:FileUpload ID="flConclusion" runat="server" class="form-control" Style="width: 500px;">
                                                    </asp:FileUpload>

                                                 <asp:UpdatePanel ID="UpdatePanel8" runat="server"><ContentTemplate>  <asp:LinkButton ID="lnkflConclusion" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkflConclusion_Click"></asp:LinkButton>
                                                        <asp:ImageButton ID="imgflConclusion" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" OnClick="imgflConclusion_Click" Visible="false"/>
                                                        </ContentTemplate></asp:UpdatePanel> 
                                                    
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                                <label class="col-sm-2 control-label" style="width: 110px">
                                                    Audio :</label>
                                                <div class="col-sm-10" style="padding-bottom: 10px">
                                                    <asp:FileUpload ID="audioUpload" runat="server" class="form-control" Style="width: 500px;">
                                                    </asp:FileUpload>
                                                   <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>       <asp:LinkButton ID="lnkaudioUpload" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkaudioUpload_Click"></asp:LinkButton>
                                                           <asp:ImageButton ID="imgBtn2" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" OnClick="imgBtn2_Click" Visible="false"/>
                                                        </ContentTemplate></asp:UpdatePanel> 
                                                    <%-- <asp:RequiredFieldValidator ID="rfvAudioUpload" runat="server" ErrorMessage="News Audio must be uploaded" ControlToValidate="audioUpload" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    Promo Image :</label>
                                                <div class="col-sm-10">
                                                    <asp:FileUpload ID="imageUpload" runat="server" class="form-control" Style="width: 500px;">
                                                    </asp:FileUpload>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate>   
                                                      <asp:LinkButton ID="lnkimageUpload" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkimageUpload_Click"></asp:LinkButton>
                                                           <asp:ImageButton ID="imgBtn4" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" OnClick="imgBtn4_Click" />
                                                        </ContentTemplate>
                                                       
                                                        </asp:UpdatePanel> 
                                                    <%--                 <asp:RequiredFieldValidator ID="rfvImageUpload" runat="server" ErrorMessage="News Image must be uploaded" ControlToValidate="imageUpload" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                                <label class="col-sm-2 control-label" style="width: 110px">
                                                    Promo url :</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtNewsLink" runat="server" Style="width: 500px;" placeholder="Enter Url"
                                                        class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfvNewsLink" runat="server" ErrorMessage="News Link is Required" ControlToValidate="txtNewsLink" ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
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
                                                    <%--<asp:RequiredFieldValidator ID="rfvNewsLink" runat="server" ErrorMessage="News Link is Required" ControlToValidate="txtNewsLink" ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="display:none">
                                        <td>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    Promo
                                                    <br />
                                                    Video :</label>
                                                <div class="col-sm-10">
                                                    <asp:FileUpload ID="videoUpload" runat="server" class="form-control" Style="width: 500px;">
                                                    </asp:FileUpload>
                                              <asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate>        <asp:LinkButton ID="lnkvideoUpload" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkvideoUpload_Click"></asp:LinkButton>
                                                            <asp:ImageButton ID="imgBtn3" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" OnClick="imgBtn3_Click" Visible="false"/>
                                                        </ContentTemplate></asp:UpdatePanel> 
                                                    <%-- <asp:RequiredFieldValidator ID="rfvVideoUpload" runat="server" ErrorMessage="News Video must be uploaded" ControlToValidate="videoUpload" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                                <label class="col-sm-2 control-label" style="width: 110px">
                                                    Title :</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtPointTitle" runat="server" class="form-control" Style="width: 500px;"
                                                        placeholder="Enter Title"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtPointTitle"
                                                        ErrorMessage="Point Title is Required" ValidationGroup="Save">* </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                  
                                </table>
                                <asp:HiddenField ID="hdnSortingValue" runat="server"></asp:HiddenField>
                                 <asp:HiddenField ID="hdnSortingValue2" runat="server" Value=""></asp:HiddenField>
                            </div>
                            <!-- /.box-body -->
                        </div>
                    </td>
                </tr>
                    <tr>
                                       <td colspan="4">
                                            <div class="form-group">
                                                <%-- <label >
                       </label>--%>
                                                <asp:Label ID="lblText1" Width="120px" runat="server" Text=" Text for TTS NorthBound (▲):" class="col-sm-2 control-label"></asp:Label>
                                                
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtNorthBoundText" runat="server" Style="width: 900px; height: 400px;"
                                                        TextMode="MultiLine" placeholder="Enter Text" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNewsText" runat="server" ErrorMessage="Text is Required"
                                                        ControlToValidate="txtNorthBoundText" ValidationGroup="Save" Enabled="false">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                </table></td>
                   
                </tr>
              

                 <tr id="trLoc2">
                 <td colspan="4">
                 <table>
                 <tr><td colspan="4" style="height:5px">&nbsp;</td></tr>
                <tr style="margin-top:10px"><td colspan="4" style="height:20px;color:Black;background-color:Aqua;text-align:center;font-size:larger;font-weight:bold"><asp:Label ID="lbldirection2" runat="server" Text="Southbound Point ▼"></asp:Label>  </td></tr>
                 <tr>
                  <td style="vertical-align: top">
                        <div style="float: left; padding-top: 15px;">
                            <div id="dvMap2" style="width: 360px; height: 480px">
                            </div>
                        </div>
                    </td>
                    <td>
                        <div>
                            <div class="box-body">
                                <table>
                                    <tr>
                                        <td>
                                          
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    Sorting :</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtSorting2" runat="server" class="form-control" Style="width: 500px;"
                                                        placeholder="Enter Sorting" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtSorting2"
                                                        MinimumValue="1" MaximumValue="99999" Type="Integer" ErrorMessage="Sorting is Required"
                                                        ValidationGroup="Save">* </asp:RangeValidator>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSorting2"
                                                         ErrorMessage="Sorting is Required "
                                                        ValidationGroup="Save">* </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                         <asp:HiddenField ID="hdnVideoValue2" runat="server" Value="0"></asp:HiddenField>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    Address :</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtAddress2" runat="server" class="form-control" Style="width: 500px;"
                                                        placeholder="Enter Address" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress2"
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
                                                    <asp:TextBox ID="txtLongitude2" runat="server" Style="width: 500px;" class="form-control"
                                                        placeholder="Enter Longitude" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Longitude is Required"
                                                        ControlToValidate="txtLongitude2" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                    <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
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
                                                    <asp:TextBox ID="txtLatitude2" runat="server" ValidationGroup="Save" Style="width: 500px;"
                                                        class="form-control" placeholder="Enter Latitude" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Latitude is Required"
                                                        ControlToValidate="txtLatitude" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                                <label class="col-sm-2 control-label" style="width: 110px">
                                                    Introductory Music :</label>
                                                <div class="col-sm-10" style="padding-bottom: 10px">
                                                    <asp:FileUpload ID="flIntroductory2" runat="server" class="form-control" Style="width: 500px;">
                                                    </asp:FileUpload>
                                                  <asp:UpdatePanel ID="UpdatePanel7" runat="server"><ContentTemplate>     <asp:LinkButton ID="lnkflIntroductory2" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkflIntroductory2_Click"></asp:LinkButton>
                                                          <asp:ImageButton ID="imgBtn5" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" OnClick="imgBtn5_Click" Visible="false"/>
                                                        </ContentTemplate></asp:UpdatePanel> 
                                                    <%-- <asp:RequiredFieldValidator ID="rfvAudioUpload" runat="server" ErrorMessage="News Audio must be uploaded" ControlToValidate="audioUpload" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </td>
                                    </tr><tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                                <label class="col-sm-2 control-label" style="width: 110px">
                                                    Conclusion Music :</label>
                                                <div class="col-sm-10" style="padding-bottom: 10px">
                                                    <asp:FileUpload ID="flConclusion2" runat="server" class="form-control" Style="width: 500px;">
                                                    </asp:FileUpload>

                                                 <asp:UpdatePanel ID="UpdatePanel9" runat="server"><ContentTemplate>  <asp:LinkButton ID="lnkflConclusion2" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkflConclusion2_Click"></asp:LinkButton>
                                                        <asp:ImageButton ID="imgflConclusion2" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" OnClick="imgflConclusion2_Click" Visible="false"/>
                                                        </ContentTemplate></asp:UpdatePanel> 
                                                    
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                                <label class="col-sm-2 control-label" style="width: 110px">
                                                    Audio :</label>
                                                <div class="col-sm-10" style="padding-bottom: 10px">
                                                    <asp:FileUpload ID="audioUpload2" runat="server" class="form-control" Style="width: 500px;">
                                                    </asp:FileUpload>
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server"><ContentTemplate>    <asp:LinkButton ID="lnkaudioUpload2" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkaudioUpload2_Click"></asp:LinkButton>
                                                          <asp:ImageButton ID="imgBtn6" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" OnClick="imgBtn6_Click" Visible="false"/>
                                                        </ContentTemplate></asp:UpdatePanel> 
                                                    <%-- <asp:RequiredFieldValidator ID="rfvAudioUpload" runat="server" ErrorMessage="News Audio must be uploaded" ControlToValidate="audioUpload" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    Promo Image :</label>
                                                <div class="col-sm-10">
                                                    <asp:FileUpload ID="imageUpload2" runat="server" class="form-control" Style="width: 500px;">
                                                    </asp:FileUpload>
                                         	<asp:UpdatePanel ID="UpdatePanel5" runat="server"><ContentTemplate> 
                                                        <asp:LinkButton ID="lnkimageUpload2" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkimageUpload2_Click"></asp:LinkButton>
                                                          <asp:ImageButton ID="imgBtn8" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" OnClick="imgBtn8_Click" Visible="false"/>
                                                        </ContentTemplate>
                                                       
                                                        </asp:UpdatePanel> 
                                                    <%--                 <asp:RequiredFieldValidator ID="rfvImageUpload" runat="server" ErrorMessage="News Image must be uploaded" ControlToValidate="imageUpload" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                                <label class="col-sm-2 control-label" style="width: 110px">
                                                    Promo url :</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtNewsLink2" runat="server" Style="width: 500px;" placeholder="Enter Url"
                                                        class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfvNewsLink" runat="server" ErrorMessage="News Link is Required" ControlToValidate="txtNewsLink" ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
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
                                                    <asp:TextBox ID="txtPromotext2" runat="server" Style="width: 500px;" placeholder="Enter Promo Text"
                                                        class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfvNewsLink" runat="server" ErrorMessage="News Link is Required" ControlToValidate="txtNewsLink" ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                  <tr style="display:none">
                                        <td>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">
                                                    Promo
                                                    <br />
                                                    Video :</label>
                                                <div class="col-sm-10">
                                                    <asp:FileUpload ID="videoUpload2" runat="server" class="form-control" Style="width: 500px;">
                                                    </asp:FileUpload>
                                                    <asp:LinkButton ID="lnkvideoUpload2" runat="server" Text="Listen Audio" Visible="false"
                                                        OnClick="lnkvideoUpload_Click"></asp:LinkButton>
                                                  	 <asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate>          <asp:ImageButton ID="imgBtn7" runat="server" ImageUrl="images/DeleteRed.png" Width="20px" Height="20px" Visible="false" OnClick="imgBtn7_Click" />
                                                        </ContentTemplate></asp:UpdatePanel> 
                                                    <%-- <asp:RequiredFieldValidator ID="rfvVideoUpload" runat="server" ErrorMessage="News Video must be uploaded" ControlToValidate="videoUpload" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group" style="float: left;">
                                                <label class="col-sm-2 control-label" style="width: 110px">
                                                    Title :</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtPointTitle2" runat="server" class="form-control" Style="width: 500px;"
                                                        placeholder="Enter Title"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPointTitle2"
                                                        ErrorMessage="Point Title is Required" ValidationGroup="Save">* </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                   
                                </table>
                            
                            </div>
                            <!-- /.box-body -->
                        </div>
                    </td>
                 </tr>
                 
                           <tr>
                                    <td colspan="4">
                                        <div class="form-group" style="float: left;">
                                          <asp:Label ID="lblText2" runat="server" Width="120px" Text="Text for TTS SouthBound (▼) :" class="col-sm-2 control-label"></asp:Label>
                                         
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtSouthBoundText" runat="server" Style="width: 900px; height: 400px;"
                                                    TextMode="MultiLine" placeholder="Enter Text" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSouthBound" runat="server" ErrorMessage="Text is Required"
                                                    ControlToValidate="txtSouthBoundText" ValidationGroup="Save" Enabled="false">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                 </table>
                 </td>
                   
          
                </tr>



                <tr>
                    <td colspan="2">
                        <div class="box-body">
                            <table>
                                <tr style="display: none">
                                    <td>
                                        <div class="form-group">
                                            <asp:Label ID="Label1" Width="120px" runat="server" Text=" Angle(Degree) :" class="col-sm-2 control-label"></asp:Label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlAngle" runat="server" Style="width: 500px;" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlAngle_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvAngle" runat="server" ErrorMessage="Please Select Angle"
                                                    Enabled="false" InitialValue="-1" ControlToValidate="ddlAngle" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                <asp:Image ID="imgAngle1" runat="server" ImageUrl="~/Images/up.png" Height="50px"
                                                    Width="50px" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>
                                        <div class="form-group">
                                            <asp:Label ID="Label2" Width="120px" runat="server" Text=" Angle(Degree) :" class="col-sm-2 control-label"></asp:Label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlAngle2" runat="server" Style="width: 500px;" OnSelectedIndexChanged="ddlAngle2_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Angle"
                                                    Enabled="false" InitialValue="-1" ControlToValidate="ddlAngle2" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                <asp:Image ID="imgAngle2" runat="server" ImageUrl="~/Images/up.png" Height="50px"
                                                    Width="50px" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                               
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="box-footer" style="padding: 15px 0px 10px 430px;">
                <asp:Button ID="btnSave" Text="Save" ValidationGroup="Save" runat="server" OnClick="btnSave_Click"
                    class="btn btn-primary" TabIndex="11" />
                <asp:Button ID="btnSaveAndCopy" Text="Save and Copy" ValidationGroup="Save" runat="server"
                    OnClick="btnSaveCopy_Click" class="btn btn-primary" TabIndex="11" Visible="false" />
                <asp:Button ID="btnCancel" Text="Cancel" ValidationGroup="Save" runat="server" OnClick="btnCancel_Click"
                    class="btn btn-primary" TabIndex="11" CausesValidation="False" />
                <asp:ValidationSummary ID="val" runat="server" ShowMessageBox="true" ValidationGroup="Save"
                    ShowSummary="false" />
            </div>
            </form>
        </div>
        <div id="dvIntermediateList" runat="server">
            <div id="tab_2">
         <%--   <asp:UpdatePanel ID="uphighway" runat="server"><ContentTemplate>
              </ContentTemplate></asp:UpdatePanel>--%>
             <div class="form-group">
                            <asp:Label ID="Label5" Width="120px" runat="server" Text="Highway Name:" class="col-sm-2 control-label"></asp:Label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlMainhighway" runat="server"  Style="width: 500px;" AutoPostBack="true" OnSelectedIndexChanged="ddlMainhighway_SelectedIndex">
                                   <%-- <asp:ListItem Text="Select" Value="-1"></asp:ListItem>--%>
                                
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Select Highway "
                                    InitialValue="-1" ControlToValidate="ddlHighwayName" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                          <br /><br /><br />
                          <div class="form-group">
                           <div class="col-sm-10">
                           <asp:RadioButtonList ID="rdbRoutePointType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbRoutePointType_SelectedIndexChanged">
                           <asp:ListItem Text="All Waypoint" Value="All" Selected="True"></asp:ListItem>
                           <asp:ListItem Text="WithTTS Waypoint" Value="WithTTS"></asp:ListItem>
                           <asp:ListItem Text="WithoutTTS Waypoint" Value="WithoutTTS"></asp:ListItem>
                           </asp:RadioButtonList>
                           </div>
                          </div>
                        <br /><br />
                        
                <div>
                    <asp:LinkButton ID="lnkAddIntermediatePoint" runat="server" OnClick="lnkAdd_Click">Add Route Points</asp:LinkButton>
                    <br />
                    <br />
                    <div>
                        <div>
                            <h2>
                                <i></i><span>Route Points List</span>&nbsp;
                            </h2>
                        </div>
                        <div>
                            <!--/row-->
                            <br>
                            <div>
                                <asp:GridView ID="gvIntermediatePoint" runat="server" AutoGenerateColumns="false"
                                    Width="100%" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvIntermediatePoint_OnPageindexchanging"
                                    OnRowCommand="gvIntermediatePoint_RowCommand" OnRowDataBound="gvIntermediatePoint_RowDataBound">
                                    <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Point Title">
                                            <ItemTemplate>
                                                <%#  Convert.ToInt32(Eval("sorting")) % 2 == 0 ? Eval("PointTitle2") : Eval("PointTitle")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Point Address">
                                            <ItemTemplate>
                                             <%#  Convert.ToInt32(Eval("sorting")) % 2 == 0 ? Eval("PointAddress2") : Eval("PointAddress")%>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Point Longitiude">
                                            <ItemTemplate>
                                             <%#  Convert.ToInt32(Eval("sorting")) % 2 == 0 ? Eval("PointLongitude2") : Eval("PointLongitude")%>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Point Latitude">
                                            <ItemTemplate>
                                             <%#  Convert.ToInt32(Eval("sorting")) % 2 == 0 ? Eval("PointLatitude2") : Eval("PointLatitude")%>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="News Url">
                                            <ItemTemplate>
                                            
                                                <a href="<%# Convert.ToInt32(Eval("sorting")) % 2 == 0?(Convert.ToString( Eval("NewsLink2")).Contains("http")? Eval("NewsLink2"): "http://"+ Eval("NewsLink2")):(Convert.ToString( Eval("NewsLink")).Contains("http")? Eval("NewsLink"): "http://"+ Eval("NewsLink")) %>" target="_blank">
                                                    <%#Convert.ToInt32(Eval("sorting")) % 2 == 0 ? Eval("NewsLink2") : Eval("NewsLink")%></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="News Text">
                          <ItemTemplate>
                         <p style="text-align: justify; width: 20px"> <%#Eval("NewsText")%></p>
                          </ItemTemplate>
                          </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Promo Image">
                                            <ItemTemplate>
                                                <a href="<%#"FileUpload/"+ (Convert.ToInt32(Eval("sorting")) % 2 == 0?Eval("NewsImage2"):Eval("NewsImage")) %>" target="_blank">
                                                    <asp:Image ID="newsImage" runat="server" Height="100" Width="100" ImageUrl='<%#"FileUpload/"+ (Convert.ToInt32(Eval("sorting")) % 2 == 0?Eval("NewsImage2"):Eval("NewsImage")) %>' /></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Promo Video" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkVideo" runat="server" Text="Watch Video" CommandName="WatchVideo"
                                                    CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                                <%-- <a href="<%#"FileUpload/"+Eval("NewsVideo")%>" id="disableVideo"  runat="server" target="_blank">Watch Video</a>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Audio">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAudio" runat="server" Text="Listen Audio" CommandName="ListenAudio"
                                                    CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                                <%-- <a href="<%#"FileUpload/"+Eval("NewsAudio") %>" target="_blank">Listen Audio</a>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sorting Number">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSorting" runat="server" OnTextChanged="Sorting_Changed" AutoPostBack="true"
                                                    onkeypress="return IsNumeric(event);" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Id") %>'
                                                    Style="width: 60px;"></asp:TextBox>
                                                <br />
                                                <span id="error" style="color: Red; display: none">Enter number</span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Created Date">
                                            <ItemTemplate>
                                                <%#string.Format("{0:MM/dd/yy}", Eval("CreatedDate"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="EditItem"
                                                    Text="Edit"></asp:LinkButton>/
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id") %>'
                                                    CommandName="DeleteItem" OnClientClick="return confirm('Are you sure you want to delete this Intermediate Point?');"
                                                    Text="Delete"></asp:LinkButton>
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
