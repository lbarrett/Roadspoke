<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="EditRouteManager.aspx.cs" Inherits="EditRouteManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
<script type="text/javascript">
    var geocoder = new google.maps.Geocoder();
    var markers=[];
    var markersLat = [];
    var markersLong = [];
    function geocodePosition(pos) {
        geocoder.geocode({
            latLng: pos
        }, function (responses) {
            if (responses && responses.length > 0) {
                updateMarkerAddress(responses[0].formatted_address);
            } else {
                updateMarkerAddress('Cannot determine address at this location.');
            }
        });
    }

//    function updateMarkerStatus(str) {
//        document.getElementById('markerStatus').innerHTML = str;
//    }
//   
    function updateMarkerPosition(latLng) {

        document.getElementById('<%=txtLongitude.ClientID %>').value = latLng.lng();
       document.getElementById('<%=txtLatitude.ClientID %>').value = latLng.lat();
    }

     function blankMarker() {
        document.getElementById('<%=txtLongitude.ClientID %>').value=""; 
       document.getElementById('<%=txtLatitude.ClientID %>').value = "";
       document.getElementById('<%=txtAddress.ClientID %>').value="";
    }
       

    function updateMarkerAddress(str) {
    
      document.getElementById('<%=txtAddress.ClientID %>').value = str;
    }
    window.onload = function () {

        var latLng = new google.maps.LatLng(40.7141667, -74.0063889);
        var mapOptions = {
            center: new google.maps.LatLng(40.7141667, -74.0063889),
            zoom: 5,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);

       <%=javascrpt %>
       for (var i = 0; i < markersLat.length; i++) {
            //Attach click event handler to the marker.            
              var markerinuse = new google.maps.Marker({
                position: new google.maps.LatLng(markersLat[i], markersLong[i]),
                map: map,
                draggable: true

            });

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
    
    };
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<section class="content-header">
   <div>
         <div>
                <h1>
               Edit Route Points
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
<div class="box box-primary" id="dvAddEdit" runat="server">
             <%--   <div class="box-header with-border">
                  <h4 class="box-title">Add/Edit Route Points</h4>
                </div><!-- /.box-header -->--%>
                <!-- form start -->
                <form role="form">
                <div style="float: left; padding-top: 15px;">
                <div id="dvMap" style="width: 360px; height: 420px"></div>
                </div>
                <div style="padding-left: 380px;">
             
                  <div class="box-body">

                      <div class="form-group">
                      <label class="col-sm-2 control-label">Title :</label>
                      <div  class="col-sm-10">
                  <asp:TextBox ID="txtPointTitle" runat="server" class="form-control" style="width: 500px;"  placeholder="Enter Title" ></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfvTitle" runat="server"  ControlToValidate="txtPointTitle"  ErrorMessage="Point Title is Required" ValidationGroup="Save">* </asp:RequiredFieldValidator>            
                    </div>
                    </div>
                    <asp:HiddenField ID="hdnVideoValue" runat="server" Value="0"></asp:HiddenField>
                    <div class="form-group">
                      <label class="col-sm-2 control-label">Address :</label>
                      <div  class="col-sm-10">
                  <asp:TextBox ID="txtAddress" runat="server" class="form-control" style="width: 500px;"  placeholder="Enter Address" ClientIDMode="Static"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfvAddress" runat="server"  ControlToValidate="txtAddress"  ErrorMessage="Point Address is Required" ValidationGroup="Save">* </asp:RequiredFieldValidator>            
                    </div>
                    </div>

                    <div class="form-group">
                     <label class="col-sm-2 control-label">Longitude:</label>
                     <div class="col-sm-10">
                          <asp:TextBox ID="txtLongitude" runat="server"  style="width: 500px;" class="form-control"  placeholder="Enter Longitude" ClientIDMode="Static"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rfvLongitude" runat="server" ErrorMessage="Longitude is Required" ControlToValidate="txtLongitude" ValidationGroup="Save">*</asp:RequiredFieldValidator> 
                           <asp:HiddenField ID="hdnuserid" runat="server" Value="0" />
                    </div>
                    </div>
                    <div class="form-group">
                      <label class="col-sm-2 control-label"> Latitude :</label>
                    <div class="col-sm-10">
                   <asp:TextBox ID="txtLatitude" runat="server" ValidationGroup="Save" style="width: 500px;"  class="form-control" placeholder="Enter Latitude" ClientIDMode="Static"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rfvLati" runat="server" ErrorMessage="Latitude is Required" ControlToValidate="txtLatitude" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                    </div>
                    </div>
                    
                     <div class="form-group">
                      <label class="col-sm-2 control-label" style="width:25%" >Text for TTS :</label>
                      <div class="col-sm-10">
                        <asp:TextBox ID="txtNewsText" runat="server" style="width: 500px;height: 200px;" TextMode="MultiLine"   placeholder="Enter Text" class="form-control"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfvNewsText" runat="server" ErrorMessage="Text is Required" ControlToValidate="txtNewsText" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                    </div>
                    </div>

                     <div class="form-group">
                      <label class="col-sm-2 control-label" > Audio :</label>
                      <div class="col-sm-10">
                      <asp:FileUpload ID="audioUpload" runat="server" class="form-control"></asp:FileUpload>
                 <%-- <asp:RequiredFieldValidator ID="rfvAudioUpload" runat="server" ErrorMessage="News Audio must be uploaded" ControlToValidate="audioUpload" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </div>
                    </div>
              
                     
                     <div class="form-group">
                      <label class="col-sm-2 control-label" style="width:25%" > Promo Image :</label>
                      <div class="col-sm-10">
                      <asp:FileUpload ID="imageUpload" runat="server" class="form-control" ></asp:FileUpload>
 <%--                 <asp:RequiredFieldValidator ID="rfvImageUpload" runat="server" ErrorMessage="News Image must be uploaded" ControlToValidate="imageUpload" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </div>
                    </div>

                     <div class="form-group" style="float: left;">
                      <label class="col-sm-2 control-label" > News url :</label>
                      <div class="col-sm-10">
                        <asp:TextBox ID="txtNewsLink" runat="server" style="width: 500px;"  placeholder="Enter Url" class="form-control"></asp:TextBox>
                  <%--<asp:RequiredFieldValidator ID="rfvNewsLink" runat="server" ErrorMessage="News Link is Required" ControlToValidate="txtNewsLink" ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
                    </div>
                    </div>

                     <div class="form-group">
                      <label class="col-sm-2 control-label" style="width:25%" > Promo Video :</label>
                      <div class="col-sm-10">
                      <asp:FileUpload ID="videoUpload" runat="server" class="form-control"></asp:FileUpload>
                 <%-- <asp:RequiredFieldValidator ID="rfvVideoUpload" runat="server" ErrorMessage="News Video must be uploaded" ControlToValidate="videoUpload" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </div>
                    </div>
                    


                  </div><!-- /.box-body -->

                  <div class="box-footer">
                  <asp:Button ID="btnSave" Text="Save"  ValidationGroup="Save" runat="server" OnClick="btnSave_Click" class="btn btn-primary" TabIndex="11" /> 
                  <asp:Button ID="btnCancel" Text="Cancel"  ValidationGroup="Save" runat="server" OnClick="btnCancel_Click" class="btn btn-primary" TabIndex="11" CausesValidation="False" /> 
                   <asp:ValidationSummary ID="val" runat="server" ShowMessageBox="true"  ValidationGroup="Save" ShowSummary="false"/>
                  </div>
                  </div>
                </form>
              </div>
  </section>
</asp:Content>

