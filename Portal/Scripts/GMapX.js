/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is WCPierce Web Controls.
 *
 * The Initial Developer of the Original Code is William C. Pierce.
 * Portions created by the Initial Developer are Copyright (C) 2005
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *   René Strauss
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 *
 * ***** END LICENSE BLOCK ***** */
 
/*
2005-09-08
+ Changed getMarkerById to getOverlayById
2005-09-10
+ Changed GMarker_ServerClick to call new server side event MarkerClick
+ Added ZoomLevel to GMap_SaveState
2005-10-05
+ Added optimizations to GMap_SaveState
+ Moved _ef and _defaultMarker to "global" variables
2005-10-12
+ Added the client counterparts to two new events, ClientLoad and MapTypeChanged
+ Updated GMap_SaveState to save the current Map Type to a new hidden field
*/

var _ef = function(){};
var _defaultMarker = new GMarker();
var _gMapRegX = new RegExp(":", "gi");
_gMapRegX.compile(":", "gi");
GMap.prototype.getOverlayById=function(a){for(var b=0;b<this.overlays.length;b++){if(this.overlays[b].id==a)return this.overlays[b];}return null;};
function addListener(a,b,c,d){if(a.addEventListener){a.addEventListener(b,c,d);return true;}else if(a.attachEvent){var e=a.attachEvent("on"+b,c);return e;}else{alert("Handler could not be attached");}}
function bind(a,b,c,d){return window.addListener(a,b,function(){d.apply(c,arguments)});}

function cbo_Complete(responseText, responseXML)
{
  eval(responseText);
}

function cbo_Error(status, statusText, responseText)
{
  alert('Error: ' + status + '\n' + statusText + '\n' + responseText);
}

function __DoCallBack(eventTarget, eventArgument)
{
  var cbo = new CallBackObject();
  cbo.OnComplete = function(){cbo_Complete.apply(eventTarget, arguments)};
  cbo.OnError = cbo_Error;
  window.GMap_SaveState(eventTarget);
  cbo.DoCallBack(eventTarget.id, eventArgument);
}

function GMap_ServerClick(overlay, point)
{
  var arg = 'GMap_Click|'+point.x+','+point.y;
  __DoCallBack(this, arg);
}

function GMap_ServerMoveStart()
{
  var center = this.getCenterLatLng();
  var arg = 'GMap_MoveStart|'+center.x+','+center.y;
  __DoCallBack(this, arg);
}
      
function GMap_ServerMoveEnd()
{
  var center = this.getCenterLatLng();
  var arg = 'GMap_MoveEnd|'+center.x+','+center.y;
  __DoCallBack(this, arg);
}

function GMarker_ServerClick()
{
  var arg = 'GMarker_Click|'+this.point.x+','+this.point.y+','+this.id;
  __DoCallBack(this.map, arg);
}

function GMap_ServerZoom(oldZoomLevel, newZoomLevel)
{
  var arg = 'GMap_Zoom|'+oldZoomLevel+','+newZoomLevel;
  __DoCallBack(this, arg);
}

function GMap_ServerClientLoad(map)
{
  var arg = 'GMap_ClientLoad|';
  __DoCallBack(map, arg);
}

function GMap_ServerMapTypeChanged()
{
  var arg = 'GMap_MapTypeChanged|';
  __DoCallBack(this, arg);
}  

function GMap_SaveState(eventTarget)
{
  var evt = eventTarget.pan?eventTarget:this;
  var evtId = evt.id.replace(_gMapRegX,'_');
  document.getElementById(evtId + '_CenterLatLng').value = evt.getCenterLatLng();
  document.getElementById(evtId + '_SpanLatLng').value   = evt.getSpanLatLng();
  document.getElementById(evtId + '_BoundsLatLng').value = evt.getBoundsLatLng();
  document.getElementById(evtId + '_ZoomLevel').value = evt.getZoomLevel();
  
  var mapType = 'G_MAP_TYPE';
  switch( evt.getCurrentMapType() )
  {
    case G_HYBRID_TYPE:
      mapType = 'G_HYBRID_TYPE';
      break;
    case G_SATELLITE_TYPE:
      mapType = 'G_SATELLITE_TYPE';
      break;
    default:
      mapType = 'G_MAP_TYPE';
      break;
  }
  document.getElementById(evtId + '_MapType').value = mapType;
}  