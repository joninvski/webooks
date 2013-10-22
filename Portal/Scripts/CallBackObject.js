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
 *   Jörg Lang
 *   amitchell
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
2005-09-21
+ Corrected problem with query string being added twice
2005-09-22
+ Corrected problem with buttons being on the form
*/
function CallBackObject()
{
  this.XmlHttp = this.GetHttpObject();
}
 
CallBackObject.prototype.GetHttpObject = function()
{ 
  var xmlhttp;
  /*@cc_on
  @if (@_jscript_version >= 5)
    try {
      xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
    } catch (e) {
      try {
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
      } catch (E) {
        xmlhttp = false;
      }
    }
  @else
  xmlhttp = false;
  @end @*/
  if (!xmlhttp && typeof XMLHttpRequest != 'undefined') {
    try {
      xmlhttp = new XMLHttpRequest();
    } catch (e) {
      xmlhttp = false;
    }
  }
  return xmlhttp;
}
 
CallBackObject.prototype.DoCallBack = function(eventTarget, eventArgument)
{
  var theData = '';
  var theform = document.forms[0];
  var thePage = window.location.pathname;
  var eName = '';
  
  // Default page handling (Jörg Lang)
  if (thePage.indexOf('.aspx', 0) > 0)
		thePage = thePage + window.location.search;
  else
		thePage = thePage + 'default.aspx' + window.location.search;
 
  theData  = '__EVENTTARGET='  + escape(eventTarget.split("$").join(":")) + '&';
  theData += '__EVENTARGUMENT=' + eventArgument + '&';
  theData += '__VIEWSTATE='    + escape(theform.__VIEWSTATE.value).replace(new RegExp('\\+', 'g'), '%2b') + '&';
  theData += 'IsCallBack=true&';
  for( var i=0; i<theform.elements.length; i++ )
  {
    eName = theform.elements[i].name;
    if( eName && eName != '')
    {
      if( eName == '__EVENTTARGET' || eName == '__EVENTARGUMENT' || eName == '__VIEWSTATE' )
      {
        // Do Nothing
      }
      else
      {
        // Checkbox handling (amitchell)
        if( (theform.elements[i].type!="checkbox" && theform.elements[i].type!="submit") || theform.elements[i].checked )
          theData = theData + escape(eName.split("$").join(":")) + '=' + theform.elements[i].value;
        if( i != theform.elements.length - 1 )
          theData = theData + '&';
      }
    }
  }
 
  if( this.XmlHttp )
  {
    if( this.XmlHttp.readyState == 4 || this.XmlHttp.readyState == 0 )
    {
      var oThis = this;
      this.XmlHttp.open('POST', thePage, true);
      this.XmlHttp.onreadystatechange = function(){ oThis.ReadyStateChange(); };
      this.XmlHttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
      this.XmlHttp.send(theData);
    }
  }
}
 
CallBackObject.prototype.AbortCallBack = function()
{
  if( this.XmlHttp )
    this.XmlHttp.abort();
}
 
CallBackObject.prototype.OnLoading = function()
{
  // Loading
}
 
CallBackObject.prototype.OnLoaded = function()
{
  // Loaded
}
 
CallBackObject.prototype.OnInteractive = function()
{
  // Interactive
}
 
CallBackObject.prototype.OnComplete = function(responseText, responseXml)
{
  // Complete
}
 
CallBackObject.prototype.OnAbort = function()
{
  // Abort
}
 
CallBackObject.prototype.OnError = function(status, statusText, responseText)
{
  // Error
}
 
CallBackObject.prototype.ReadyStateChange = function()
{
  if( this.XmlHttp.readyState == 1 )
  {
    this.OnLoading();
  }
  else if( this.XmlHttp.readyState == 2 )
  {
    this.OnLoaded();
  }
  else if( this.XmlHttp.readyState == 3 )
  {
    this.OnInteractive();
  }
  else if( this.XmlHttp.readyState == 4 )
  {
    if( this.XmlHttp.status == 0 )
      this.OnAbort();
    else if( this.XmlHttp.status == 200 && this.XmlHttp.statusText == "OK" )
      this.OnComplete(this.XmlHttp.responseText, this.XmlHttp.responseXML);
    else
      this.OnError(this.XmlHttp.status, this.XmlHttp.statusText, this.XmlHttp.responseText);   
  }
}
