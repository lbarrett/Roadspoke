<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AddMultiplePonts.aspx.cs" Inherits="AddMultiplePonts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
  <%--<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyATOL3Yl2vqQubW44Vkrn4J24d5-6mIlpA"></script>--%>  <%--Comment By SIDDHARTH--%>

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyATOL3Yl2vqQubW44Vkrn4J24d5-6mIlpA"></script> <%--Added By SIDDHARTH--%>

   
     
  <script>
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
      window.onload = function () {

          LoadMaps();
      }
       function geocodePosition(pos) {
        geocoder.geocode({
            latLng: pos
        }, function (responses) {
            if (responses && responses.length > 0) {
                updateMarkerAddress(responses[0].formatted_address);
                 var longg= document.getElementById('<%=hdnLong1.ClientID %>').value ;
       var lat=document.getElementById('<%=hdnLat1.ClientID %>').value ;
                //PageMethods.GetRegion(lat,longg, onSucess, onError);
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
                 var longg= document.getElementById('<%=hdnLong2.ClientID %>').value ;
       var lat=document.getElementById('<%=hdnLat2.ClientID %>').value ;
               // PageMethods.GetRegion(lat,longg, onSucess, onError);
            } else {
                updateMarkerAddress('Cannot determine address at this location.');
            }
        });
    }


     function geocodePosition12(pos) {
        geocoder.geocode({
            latLng: pos
        }, function (responses) {
            if (responses && responses.length > 0) {
                updateMarkerAddress2(responses[0].formatted_address);
                 var longg= document.getElementById('<%=hdnLong11.ClientID %>').value ;
       var lat=document.getElementById('<%=hdnLat11.ClientID %>').value ;
               // PageMethods.GetRegion(lat,longg, onSucess, onError);
            } else {
                updateMarkerAddress('Cannot determine address at this location.');
            }
        });
    }
     function geocodePosition22(pos) {
        geocoder.geocode({
            latLng: pos
        }, function (responses) {
            if (responses && responses.length > 0) {
                updateMarkerAddress22(responses[0].formatted_address);
                 var longg= document.getElementById('<%=hdnLong12.ClientID %>').value ;
       var lat=document.getElementById('<%=hdnLat12.ClientID %>').value ;
               // PageMethods.GetRegion(lat,longg, onSucess, onError);
            } else {
                updateMarkerAddress('Cannot determine address at this location.');
            }
        });
    }
       function updateMarkerPosition(latLng) {

        document.getElementById('<%=hdnLat1.ClientID %>').value = latLng.lat();
       document.getElementById('<%=hdnLong1.ClientID %>').value = latLng.lng();
   
    }
     function updateMarkerPosition2(latLng) {

        document.getElementById('<%=hdnLat2.ClientID %>').value = latLng.lat();
       document.getElementById('<%=hdnLong2.ClientID %>').value = latLng.lng();
   
    }
      function blankMarker() {
        document.getElementById('<%=hdnLat1.ClientID %>').value=""; 
       document.getElementById('<%=hdnLong1.ClientID %>').value = "";
       document.getElementById('<%=hdnAddress1.ClientID %>').value="";
    }
       function blankMarker2() {
        document.getElementById('<%=hdnLat2.ClientID %>').value=""; 
       document.getElementById('<%=hdnLong2.ClientID %>').value = "";
       document.getElementById('<%=hdnAddress2.ClientID %>').value="";
    }


     function updateMarkerPosition12(latLng) {

        document.getElementById('<%=hdnLat11.ClientID %>').value = latLng.lat();
       document.getElementById('<%=hdnLong11.ClientID %>').value = latLng.lng();
   
    }
     function updateMarkerPosition22(latLng) {

        document.getElementById('<%=hdnLat12.ClientID %>').value = latLng.lat();
       document.getElementById('<%=hdnLong12.ClientID %>').value = latLng.lng();
   
    }
      function blankMarker12() {
        document.getElementById('<%=hdnLat11.ClientID %>').value=""; 
       document.getElementById('<%=hdnLong11.ClientID %>').value = "";
       document.getElementById('<%=hdnAddress11.ClientID %>').value="";
    }
       function blankMarker22() {
        document.getElementById('<%=hdnLat12.ClientID %>').value=""; 
       document.getElementById('<%=hdnLong12.ClientID %>').value = "";
       document.getElementById('<%=hdnAddress12.ClientID %>').value="";
    }
    function updateMarkerAddress(str) {
    
      document.getElementById('<%=hdnAddress1.ClientID %>').value = str;
    }
     function updateMarkerAddress2(str) {
    
      document.getElementById('<%=hdnAddress2.ClientID %>').value = str;
    }

    function updateMarkerAddress12(str) {
    
      document.getElementById('<%=hdnAddress11.ClientID %>').value = str;
    }
     function updateMarkerAddress22(str) {
    
      document.getElementById('<%=hdnAddress12.ClientID %>').value = str;
    }
      function LoadMaps()
    {
     var latLng2 = new google.maps.LatLng(<%=lat12 %>, <%=lng12 %>);
       var latLng = new google.maps.LatLng(<%=lat22 %>, <%=lng22 %>);
        var mapOptions = {
            center: new google.maps.LatLng(<%=lat12 %>, <%=lng12 %>),
            zoom: 5,
            streetViewControl:false,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
     
        var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
         map.setZoom(14);
      
    
        //Attach click event handler to the map.
         
       // google.maps.event.addListener(map, 'click', function (e) {
         //if(markers.length==0)
         // {
            //Determine the location where the user has clicked.
            var location = latLng;//e.latLng;

            //Create a marker and placed it on the map.
            var marker = new google.maps.Marker({
                position: location,
                map: map,
                draggable: true,
                 title: "S",
                 label: "S"
            });

 
            //Attach click event handler to the marker.
            google.maps.event.addListener(marker, "click", function (e) {
                var infoWindow = new google.maps.InfoWindow({
                    content: 'Latitude: ' + marker.getPosition().lat() + '<br />Longitude: ' + marker.getPosition().lng()
                });
                infoWindow.open(map, marker);
            });
//            google.maps.event.addListener(marker, "dblclick", function (e) {
//                marker.setMap(null)
//                blankMarker();
//                 markers.pop(marker);
//            });
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

            var location = latLng;//e.latLng;

            //Create a marker and placed it on the map.
            var marker2 = new google.maps.Marker({
                position: latLng2,
                map: map,
                draggable: true,
                title: "D",
                label: "D"
            });

 
            //Attach click event handler to the marker.
            google.maps.event.addListener(marker2, "click", function (e) {
                var infoWindow = new google.maps.InfoWindow({
                    content: 'Latitude: ' + marker2.getPosition().lat() + '<br />Longitude: ' + marker2.getPosition().lng()
                });
                infoWindow.open(map, marker2);
            });
//            google.maps.event.addListener(marker, "dblclick", function (e) {
//                marker.setMap(null)
//                blankMarker();
//                 markers.pop(marker);
//            });
            updateMarkerPosition2(marker2.getPosition());
//            updateMarkerLatPosition(latLng);
            geocodePosition2(marker2.getPosition());

            // Add dragging event listeners.
            google.maps.event.addListener(marker2, 'dragstart', function () {
                updateMarkerAddress2('Dragging...');
            });

            google.maps.event.addListener(marker2, 'drag', function () {
//                updateMarkerStatus('Dragging...');
                updateMarkerPosition2(marker2.getPosition());
//               updateMarkerLatPosition(marker.getPosition());
            });

            google.maps.event.addListener(marker2, 'dragend', function () {
//                updateMarkerStatus('Drag ended');
                geocodePosition2(marker2.getPosition());
            });

            markers.push(marker2);
         //  }
        //});
    CreateMap2();
    }
      function CreateMap2()
    {
     var latLng = new google.maps.LatLng(<%=lat1 %>, <%=lng1 %>);
     var latLng2 = new google.maps.LatLng(<%=lat2 %>, <%=lng2 %>);
        var mapOptions = {
            center: new google.maps.LatLng(<%=lat1 %>, <%=lng1 %>),
            zoom: 5,
            streetViewControl:false,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("dvMap2"), mapOptions);
         map.setZoom(14);
     
        //Attach click event handler to the map.
         
       // google.maps.event.addListener(map, 'click', function (e) {
        // if(markers2.length==0)
         // {
            //Determine the location where the user has clicked.
            var location = latLng;//e.latLng;

            //Create a marker and placed it on the map.
            var marker = new google.maps.Marker({
                position: location,
                map: map,
                draggable: true,
                label: "S"
            });

 
            //Attach click event handler to the marker.
            google.maps.event.addListener(marker, "click", function (e) {
                var infoWindow = new google.maps.InfoWindow({
                    content: 'Latitude: ' + marker.getPosition().lat() + '<br />Longitude: ' + marker.getPosition().lng()
                });
                infoWindow.open(map, marker);
            });
//            google.maps.event.addListener(marker, "dblclick", function (e) {
//                marker.setMap(null)
//                blankMarker2();
//                 markers2.pop(marker);
//            });
            updateMarkerPosition12(marker.getPosition());
            geocodePosition12(marker.getPosition());

            // Add dragging event listeners.
            google.maps.event.addListener(marker, 'dragstart', function () {
                updateMarkerAddress12('Dragging...');
            });

            google.maps.event.addListener(marker, 'drag', function () {
                updateMarkerPosition12(marker.getPosition());
            });

            google.maps.event.addListener(marker, 'dragend', function () {
                geocodePosition12(marker.getPosition());
            });

            markers2.push(marker);



             var marker2 = new google.maps.Marker({
                position: latLng2,
                map: map,
                draggable: true,
                label: "D"
            });

 
            //Attach click event handler to the marker.
            google.maps.event.addListener(marker2, "click", function (e) {
                var infoWindow = new google.maps.InfoWindow({
                    content: 'Latitude: ' + marker2.getPosition().lat() + '<br />Longitude: ' + marker2.getPosition().lng()
                });
                infoWindow.open(map, marker2);
            });
//            google.maps.event.addListener(marker, "dblclick", function (e) {
//                marker.setMap(null)
//                blankMarker2();
//                 markers2.pop(marker);
//            });
            updateMarkerPosition22(marker2.getPosition());
            geocodePosition22(marker2.getPosition());

            // Add dragging event listeners.
            google.maps.event.addListener(marker2, 'dragstart', function () {
                updateMarkerAddress22('Dragging...');
            });

            google.maps.event.addListener(marker2, 'drag', function () {
                updateMarkerPosition22(marker2.getPosition());
            });

            google.maps.event.addListener(marker2, 'dragend', function () {
                geocodePosition22(marker2.getPosition());
            });

            markers2.push(marker2);
          // }
        //});
    }
     function ChangeHighwayRun()
    {
    if( $("#<%=ddlHighway.ClientID %>").val()=="Northbound/Southbound")
    {    
    $("#<%=lblDirection1.ClientID %>").text('Northbound Point ▲');
    $("#<%=lbldirection2.ClientID %>").text('Southbound Point ▼');
    }
    else
    {
    $("#<%=lblDirection1.ClientID %>").text('Eastbound Point ►');
    $("#<%=lbldirection2.ClientID %>").text('Westbound Point ◄');
    }
    }

   
  </script>
<asp:ScriptManager ID="sc" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdnregion" runat="server" Value="" />
    <section class="content-header">
        <div>
           <%-- <div>
                <h1>
                    Route Points    
                </h1>
            </div>--%>
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
        <div class="box box-primary" id="dvAddEdit" runat="server" visible="true">
            
          <div class="box-header with-border">
                <h4 class="box-title">
                    Add 1-Mile Waypoints </h4>
            </div>

             <%-- /*=====================================================================*/--%>
    
            <!-- /.box-header -->
            <!-- form start -->
            <form role="form">
            <table>
           
                 <tr>
                       <td colspan="4" style="padding-top:10px">
                        <div class="form-group">
                            <asp:Label ID="Label8" Width="120px" runat="server" Text=" Frequency :" class="col-sm-2 control-label"></asp:Label>
                            <div class="col-sm-10">
                            <asp:CheckBox ID="chkFrequency" runat="server" Text="Occasional" />
                              
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
                                <asp:DropDownList ID="ddlHighwayName" runat="server"  Style="width: 500px;" Enabled="true">
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
                   
                <tr id="trLoc1">
                <td colspan="4"><table>
                <tr><td colspan="4" style="height:5px">&nbsp;</td></tr>
                <tr style="margin-top:10px"><td  style="height:20px;color:Black;background-color:Aqua;text-align:center;font-size:larger;font-weight:bold"><asp:Label ID="lblDirection1" runat="server" Text="Northbound Point ▲"></asp:Label>  </td>
                <td  style="height:20px;color:Black;background-color:Aqua;text-align:center;font-size:larger;font-weight:bold"><asp:Label ID="lbldirection2" runat="server" Text="Southbound Point ▼"></asp:Label>  </td>
                </tr>
                <tr>
                 <td style="vertical-align: top">
                        <div style="float: left; padding-top: 15px;" id="dvMapOuter1" runat="server" >
                            <div id="dvMap" style="width: 360px; height: 480px" >
                            </div>
                            <asp:HiddenField ID="hdnLat1" runat="server" />
                            <asp:HiddenField ID="hdnLong1" runat="server" />
                            <asp:HiddenField ID="hdnAddress1" runat="server" />

                             
                               <asp:HiddenField ID="hdnLat2" runat="server" />
                            <asp:HiddenField ID="hdnLong2" runat="server" />
                            <asp:HiddenField ID="hdnAddress2" runat="server" />
                        </div>
                    </td>
                    <td>
                         <div style="float: left; padding-top: 15px;padding-left:200px">
                            <div id="dvMap2" style="width: 360px; height: 480px">
                            </div>
                             <asp:HiddenField ID="hdnLat11" runat="server" />
                            <asp:HiddenField ID="hdnLong11" runat="server" />
                            <asp:HiddenField ID="hdnAddress11" runat="server" />

                            <asp:HiddenField ID="hdnLat12" runat="server" />
                            <asp:HiddenField ID="hdnLong12" runat="server" />
                            <asp:HiddenField ID="hdnAddress12" runat="server" />
                          
                        </div>
                    </td>
                </tr>
                    
                </table></td>
                   
                </tr>
                <tr style="display:none" id="trmap">
                <td colspan="2" style="padding:40px;text-align:center" align="center">
                <div id="map-canvas" style="width: 814px; height: 580px" >
                            </div>
                            <ul id="vertex" runat="server"></ul>
                            <asp:HiddenField ID="lblpoints" runat="server"></asp:HiddenField>
                              <asp:HiddenField ID="lblPointsReverse" runat="server"></asp:HiddenField>
                </td>
                </tr>
             
            </table>
            <div class="box-footer" style="padding: 15px 0px 10px 430px;">
            <table cellpadding="5" cellspacing="5" width="50%">
            <tr>
            <td> <asp:Button ID="btnSave" Text="Save" ValidationGroup="Save" runat="server" OnClick="btnSave_Click"  style="display:none"
                    class="btn btn-primary" TabIndex="11" /></td>
            <td>  <input type="button" value="Preview" id="brnSave"  class="btn btn-primary" /></td>
            <td>  <asp:Button ID="btnCancel" Text="Cancel" ValidationGroup="Save" runat="server" OnClick="btnCancel_Click"
                    class="btn btn-primary" TabIndex="11" CausesValidation="False" /></td>
            </tr>
            </table>
               
                   
             
              
                <asp:ValidationSummary ID="val" runat="server" ShowMessageBox="true" ValidationGroup="Save"
                    ShowSummary="false" />
            </div>
            </form>
        </div>
       
    </section>
    <script>
        function initMap() {
            debugger;
            var directionsService = new google.maps.DirectionsService;
            var directionsDisplay = new google.maps.DirectionsRenderer;
            var map = new google.maps.Map(document.getElementById('map-canvas'), {
                zoom: 7,
                center: { lat: 41.85, lng: -87.65 }
            });
            directionsDisplay.setMap(map);

            var onChangeHandler = function () {
            document.getElementById('trmap').style.display="block";
            document.getElementById('<%=btnSave.ClientID %>').style.display="block";
                calculateAndDisplayRoute(directionsService, directionsDisplay);
               // calculateAndDisplayRoute2(directionsService, directionsDisplay);
                return false;
            };
            document.getElementById('brnSave').addEventListener('click', onChangeHandler);
            // document.getElementById('end').addEventListener('change', onChangeHandler);
        }

        //         

        function calculateAndDisplayRoute(directionsService, directionsDisplay) {
            debugger;
            directionsService.route({
                origin: document.getElementById('<%=hdnLat1.ClientID %>').value + ' , ' + document.getElementById('<%=hdnLong1.ClientID %>').value,
                destination: document.getElementById('<%=hdnLat2.ClientID %>').value + " , " + document.getElementById('<%=hdnLong2.ClientID %>').value,
                travelMode: 'DRIVING'
            }, function (response, status) {
                if (status === 'OK') {

                    var map = new google.maps.Map(document.getElementById('map-canvas'), {
                        zoom: 7,
                        center: { lat: 41.85, lng: -87.65 }
                    });

                    for (var j = 0; j < response.routes.length; j++) {
                        var directionsDisplay2 = new google.maps.DirectionsRenderer({ map: map, directions: response, routeIndex: j });
                        // directionsDisplays.push(directionsDisplay2);
                        var points = response.routes[j].overview_path;
                        var ul = document.getElementById("<%=lblpoints.ClientID %>");
                        var ulreverse = document.getElementById("<%=lblPointsReverse.ClientID %>");
                        //for (var i = 0; i < points.length; i++) {
                        //    // var li = document.createElement('li');
                        //    //  li.innerHTML = getLiText(points[i]);
                        //    // ul.appendChild(li);
                        //    ul.value = ul.value + points[i].lat() + ',' + points[i].lng() + ':';
                        //    var markerinuse = new google.maps.Marker({
                        //        position: new google.maps.LatLng(points[i].lat(), points[i].lng()),
                        //        map: map
                        //        //draggable: true

                        //    });
                        //} // commnet By SIDDHARTH


                        let distance = 0; // Added By SIDDHARTH
                        let previousPoint = null;// Added By SIDDHARTH

                        for (let i = 0; i < points.length; i++) {
                            const currentPoint = points[i];

                            if (previousPoint) {
                                const segmentDistance = google.maps.geometry.spherical.computeDistanceBetween(previousPoint, currentPoint); // in meters
                                distance += segmentDistance;

                                if (distance >= 1609.34) { // ~1 mile in meters
                                    // Add marker
                                    new google.maps.Marker({
                                        position: currentPoint,
                                        map: map
                                    });

                                    distance = 0; // reset after adding marker
                                }
                            }

                            previousPoint = currentPoint;
                        }


                    }  // Added By SIDDHARTH


                    calculateAndDisplayRoute2(directionsService, directionsDisplay, map);
                    // directionsDisplay.setDirections(response);
                } else {
                    //window.alert('Directions request failed due to ' + status);
                }
               
            });
            
        }
        function calculateAndDisplayRoute2(directionsService, directionsDisplay, map) {
            debugger;
            directionsService.route({
                origin: document.getElementById('<%=hdnLat11.ClientID %>').value + " , " + document.getElementById('<%=hdnLong11.ClientID %>').value,
                destination: document.getElementById('<%=hdnLat12.ClientID %>').value + ' , ' + document.getElementById('<%=hdnLong12.ClientID %>').value,
                travelMode: 'DRIVING'
            }, function (response, status) {
                if (status === 'OK') {

//                    var map = new google.maps.Map(document.getElementById('map-canvas'), {
//                        zoom: 7,
//                        center: { lat: 41.85, lng: -87.65 }
//                    });

                    for (var j = 0; j < response.routes.length; j++) {
                        var directionsDisplay2 = new google.maps.DirectionsRenderer({ map: map, directions: response, routeIndex: j });
                        // directionsDisplays.push(directionsDisplay2);
                        var points = response.routes[j].overview_path;                       
                        var ulreverse = document.getElementById("<%=lblPointsReverse.ClientID %>");
                        //for (var i = 0; i < points.length; i++) {
                        //    ulreverse.value = ulreverse.value + points[i].lat() + ',' + points[i].lng() + ':';
                        //    var markerinuse = new google.maps.Marker({
                        //        position: new google.maps.LatLng(points[i].lat(), points[i].lng()),
                        //        map: map,
                        //        icon: 'http://maps.google.com/mapfiles/ms/icons/green-dot.png'//google.maps.SymbolPath.FORWARD_CLOSED_ARROW,

                        //        //draggable: true

                        //    });
                        //} // comment by SIDDHARTH

                        let distance = 0; //Added by SIDDHARTH
                        let previousPoint = null; //Added by SIDDHARTH

                        for (let i = 0; i < points.length; i++) {
                            const currentPoint = points[i];

                            if (previousPoint) {
                                const segmentDistance = google.maps.geometry.spherical.computeDistanceBetween(previousPoint, currentPoint); // in meters
                                distance += segmentDistance;

                                if (distance >= 1609.34) { // ~1 mile in meters
                                    // Add marker
                                    new google.maps.Marker({
                                        position: currentPoint,
                                        map: map
                                    });

                                    distance = 0; // reset after adding marker
                                }
                            }

                            previousPoint = currentPoint;
                        } //Added by SIDDHARTH


                    }

                    // directionsDisplay.setDirections(response);
                } else {
                    //window.alert('Directions request failed due to ' + status);
                }
            });
        }
        function getLiText(point) {
            var lat = point.lat(),
                lng = point.lng();
            return "lat: " + lat + " lng: " + lng;
        }
        initMap();
    </script>
</asp:Content>

