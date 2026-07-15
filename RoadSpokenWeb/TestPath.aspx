<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPath.aspx.cs" Inherits="TestPath" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
     <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyATOL3Yl2vqQubW44Vkrn4J24d5-6mIlpA"></script>
     <%--<script src="https://maps.googleapis.com/maps/api/js?sensor=false"></script>--%>
    <style>
      /* Always set the map height explicitly to define the size of the div
       * element that contains the map. */
      #map {
        height: 100%;
      }
      /* Optional: Makes the sample page fill the window. */
      html, body {
        height: 100%;
        margin: 0;
        padding: 0;
      }
      #floating-panel {
        position: absolute;
        top: 10px;
        left: 25%;
        z-index: 5;
        background-color: #fff;
        padding: 5px;
        border: 1px solid #999;
        text-align: center;
        font-family: 'Roboto','sans-serif';
        line-height: 30px;
        padding-left: 10px;
      }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <div>
    <div style="padding:30px">
<strong>Start: </strong>
<select id="start">
 <option value="Select">Select</option>
  <option value="36.8775138, -77.4131415">I 95 Start</option>
 <option value="37.119696, -77.524421">85 Start</option>
</select>
<strong>End: </strong>
<select id="end" >
<option value="Select">Select</option>
  <option value="36.132212, -77.792213">I 95 End</option>
  <option value="36.387071, -78.344998">85 End</option>
  
</select>
</div>
      <div id="map-canvas" style="width: 660px; height: 580px" >
                            </div>
                            <ul id="vertex"></ul>
    </div>

   </center>
      <script>
          function initMap() {

              var directionsService = new google.maps.DirectionsService;
              var directionsDisplay = new google.maps.DirectionsRenderer;
              var map = new google.maps.Map(document.getElementById('map-canvas'), {
                  zoom: 7,
                  center: { lat: 41.85, lng: -87.65 }
              });
              directionsDisplay.setMap(map);

              var onChangeHandler = function () {
                  calculateAndDisplayRoute(directionsService, directionsDisplay);
              };
              document.getElementById('start').addEventListener('change', onChangeHandler);
              document.getElementById('end').addEventListener('change', onChangeHandler);

          }



//          function calculateAndDisplayRoute(directionsService, directionsDisplay, pointA, pointB) {
//              directionsService.route({
//                  origin: pointA,
//                  destination: pointB,
//                  travelMode: google.maps.TravelMode.DRIVING
//              }, function (response, status) {
//                  if (status == google.maps.DirectionsStatus.OK) {
//                      directionsDisplay.setDirections(response);
//                  } else {
//                      window.alert('Directions request failed due to ' + status);
//                  }
//              });
//          }

          function calculateAndDisplayRoute(directionsService, directionsDisplay) {
              directionsService.route({
                  origin: document.getElementById('start').value,
                  destination: document.getElementById('end').value,
                  travelMode: 'DRIVING'
              }, function (response, status) {
                  if (status === 'OK') {

                      var map = new google.maps.Map(document.getElementById('map-canvas'), {
                          zoom: 7,
                          center: { lat: 41.85, lng: -87.65 }
                      });
                      if (document.getElementById('end').value != 'Select') {
                          for (var j = 0; j < response.routes.length; j++) {
                              var directionsDisplay2 = new google.maps.DirectionsRenderer({ map: map, directions: response, routeIndex: j });
                              // directionsDisplays.push(directionsDisplay2);
                              var points = response.routes[j].overview_path;
                              var ul = document.getElementById("vertex");
                              for (var i = 0; i < points.length; i++) {
                                  // var li = document.createElement('li');
                                  //  li.innerHTML = getLiText(points[i]);
                                  // ul.appendChild(li);
                                  var markerinuse = new google.maps.Marker({
                                      position: new google.maps.LatLng(points[i].lat(), points[i].lng()),
                                      map: map
                                      //draggable: true

                                  });
                              }
                          }
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
    </form>
</body>
</html>
