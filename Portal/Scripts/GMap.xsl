<?xml version="1.0" encoding="UTF-8"?>
<!--
2005-09-10
+ Added defaultMarker and changed GIcon constructor
+ Tweaked enableDragging and enableInfoWindow to work properly
2005-10-05
+ Changed the rendering of the GMap so that the map is now placed in a function
  which is called after the window loads.
+ Moved _ef and _defaultMarker to GMapX.js
+ Moved Icons template outside of the rendering function to support markers added
  as the result of a server-side call back
+ Added the friendlyControlId parameter for naming the Render method
+ Changed the way GMap_SaveState is called.  Is now called anytime the map is moved
  or zoomed, rather than trying to tie into the form submit
2005-10-12
+ Corrected a bug introduced with previous changes where MoveEnd and Zoom events weren't 
  fired initially
+ Added a call to GMap_ServerClientLoad to support the new ClientLoad event when Callbacks
  are enabled.
+ Changed output method to html with indent=no
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="no" />
    
  <xsl:param name="jsId" />
  <xsl:param name="divId" />
  <xsl:param name="controlId" />
  <xsl:param name="friendlyControlId" />
  <xsl:param name="enableClientCallBacks" />
  <xsl:param name="initJs" />
  <xsl:param name="enableDragging" />
  <xsl:param name="enableInfoWindow" />
  <xsl:param name="zoomLevel" />
  <xsl:param name="mapType" />

  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  
  <xsl:template match="GXPage">
    <script type="text/javascript">
      var <xsl:value-of select="$jsId" /> = null;
      function <xsl:value-of select="$friendlyControlId" />_Render() {
        if( GBrowserIsCompatible()) {
        
        <xsl:value-of select="$jsId" /> = new GMap( document.getElementById( "<xsl:value-of select="$divId" />"));
        <xsl:value-of select="$jsId" />.id = '<xsl:value-of select="$controlId" />';
        <xsl:if test="$enableDragging=false()">
          <xsl:value-of select="$jsId" />.disableDragging();
        </xsl:if>
        <xsl:if test="$enableInfoWindow=false()">
          <xsl:value-of select="$jsId" />.disableInfoWindow();
        </xsl:if>
        <xsl:value-of select="$jsId" />.zoomTo(<xsl:value-of select="$zoomLevel" />);
        <xsl:value-of select="$jsId" />.setMapType(<xsl:value-of select="$mapType" />); 
        
        <xsl:if test="$enableClientCallBacks=true()">
          GEvent.addListener(<xsl:value-of select="$jsId" />, 'click', window.GMap_ServerClick);
          GEvent.addListener(<xsl:value-of select="$jsId" />, 'movestart', window.GMap_ServerMoveStart);
          GEvent.addListener(<xsl:value-of select="$jsId" />, 'moveend', window.GMap_ServerMoveEnd);
          GEvent.addListener(<xsl:value-of select="$jsId" />, 'zoom', window.GMap_ServerZoom);
          GEvent.addListener(<xsl:value-of select="$jsId" />, 'maptypechanged', window.GMap_ServerMapTypeChanged);
        </xsl:if>
        GEvent.addListener(<xsl:value-of select="$jsId" />, 'click', window.GMap_Click||_ef);
        GEvent.addListener(<xsl:value-of select="$jsId" />, 'move', window.GMap_Move||_ef);
        GEvent.addListener(<xsl:value-of select="$jsId" />, 'movestart', window.GMap_MoveStart||_ef);
        GEvent.addListener(<xsl:value-of select="$jsId" />, 'moveend', window.GMap_MoveEnd||_ef);
        GEvent.addListener(<xsl:value-of select="$jsId" />, 'zoom', window.GMap_Zoom||_ef);        
        GEvent.addListener(<xsl:value-of select="$jsId" />, 'maptypechanged', window.GMap_MapTypeChanged||_ef);
        GEvent.addListener(<xsl:value-of select="$jsId" />, 'windowopen', window.GMap_WindowOpen||_ef);
        GEvent.addListener(<xsl:value-of select="$jsId" />, 'windowclose', window.GMap_WindowClose||_ef);
        GEvent.addListener(<xsl:value-of select="$jsId" />, 'addoverlay', window.GMap_AddOverlay||_ef);
        GEvent.addListener(<xsl:value-of select="$jsId" />, 'removeoverlay', window.GMap_RemoveOverlay||_ef);
        GEvent.addListener(<xsl:value-of select="$jsId" />, 'clearoverlays', window.GMap_ClearOverlays||_ef);

        GEvent.addListener(<xsl:value-of select="$jsId" />, 'maptypechanged', window.GMap_SaveState|_ef);
        GEvent.addListener(<xsl:value-of select="$jsId" />, 'moveend', window.GMap_SaveState|_ef);
        GEvent.addListener(<xsl:value-of select="$jsId" />, 'zoom', window.GMap_SaveState||_ef);
        <xsl:value-of disable-output-escaping="yes" select="$initJs" />
        <xsl:apply-templates select="Overlays" />
        <xsl:apply-templates select="Controls" />
        
        <xsl:if test="$enableClientCallBacks=true()">
          GMap_ServerClientLoad(<xsl:value-of select="$jsId" />);
        </xsl:if>
        }
      }
      window.addListener(window, 'load', <xsl:value-of select="$friendlyControlId" />_Render);
      <xsl:apply-templates select="Icons" />
    </script>
  </xsl:template> 
  
  <xsl:template match="Icons">
    <xsl:apply-templates select="GIcon" />
  </xsl:template>
  
  <xsl:template match="GIcon">
    <xsl:variable name="iconId" select="@Id" />
    var <xsl:value-of select="$iconId" /> = new GIcon(_defaultMarker.icon);
    <xsl:if test="@Image!=''">
      <xsl:value-of select="$iconId" />.image = "<xsl:value-of select="@Image" />";
    </xsl:if>
    <xsl:if test="@Shadow!=''">
      <xsl:value-of select="$iconId" />.shadow = "<xsl:value-of select="@Shadow" />";
    </xsl:if>
    <xsl:if test="@PrintImage!=''">
      <xsl:value-of select="$iconId" />.printImage = "<xsl:value-of select="@PrintImage" />";
    </xsl:if>
    <xsl:if test="@MozPrintImage!=''">
      <xsl:value-of select="$iconId" />.mozPrintImage = "<xsl:value-of select="@MozPrintImage" />";
    </xsl:if>
    <xsl:if test="@Transparent!=''">
      <xsl:value-of select="$iconId" />.transparent = "<xsl:value-of select="@Transparent" />";
    </xsl:if>
    <xsl:if test="IconSize/@Width!=''">
      <xsl:value-of select="$iconId" />.iconSize = <xsl:call-template name="GSize">
                                                    <xsl:with-param name="w">
                                                      <xsl:value-of select="IconSize/@Width" />
                                                    </xsl:with-param>
                                                    <xsl:with-param name="h">
                                                      <xsl:value-of select="IconSize/@Height" />
                                                    </xsl:with-param>
                                                  </xsl:call-template>;
    </xsl:if>                                   
    <xsl:if test="ShadowSize/@Width!=''">
      <xsl:value-of select="$iconId" />.shadowSize = <xsl:call-template name="GSize">
                                                      <xsl:with-param name="w">
                                                        <xsl:value-of select="ShadowSize/@Width" />
                                                      </xsl:with-param>
                                                      <xsl:with-param name="h">
                                                        <xsl:value-of select="ShadowSize/@Height" />
                                                      </xsl:with-param>
                                                    </xsl:call-template>;
    </xsl:if>
    <xsl:if test="IconAnchor/@X!=''">
      <xsl:value-of select="$iconId" />.iconAnchor = <xsl:call-template name="GPoint">
                                                      <xsl:with-param name="x">
                                                        <xsl:value-of select="IconAnchor/@X" />
                                                      </xsl:with-param>
                                                      <xsl:with-param name="y">
                                                        <xsl:value-of select="IconAnchor/@Y" />
                                                      </xsl:with-param>
                                                    </xsl:call-template>;
    </xsl:if>                                     
    <xsl:if test="InfoWindowAnchor/@X!=''">
      <xsl:value-of select="$iconId" />.infoWindowAnchor = <xsl:call-template name="GPoint">
                                                            <xsl:with-param name="x">
                                                              <xsl:value-of select="InfoWindowAnchor/@X" />
                                                            </xsl:with-param>
                                                            <xsl:with-param name="y">
                                                              <xsl:value-of select="InfoWindowAnchor/@Y" />
                                                            </xsl:with-param>
                                                          </xsl:call-template>;
    </xsl:if>
    <xsl:if test="count(ImageMap/*)>0">
      <xsl:value-of select="$iconId" />.imageMap = [<xsl:apply-templates select="ImageMap" />];
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="ImageMap">
    <xsl:apply-templates select="Point" />
  </xsl:template>
  
  <xsl:template match="Point">
    <xsl:value-of select="X" />,<xsl:value-of select="Y" /><xsl:if test="not(position()=last())">,</xsl:if>
  </xsl:template>
  
  <xsl:template match="GPoint">
    <xsl:text>new GPoint(</xsl:text><xsl:value-of select="@X" />,<xsl:value-of select="@Y" />)<xsl:if test="not(position()=last())">,</xsl:if>
  </xsl:template>
  
  <xsl:template match="Overlays">
    <xsl:apply-templates select="GMarker" />
    <xsl:apply-templates select="GPolyline" />
  </xsl:template>
  
  <xsl:template match="GMarker">
    <xsl:variable name="gmId" select="concat('gMarker',position())" />
    var <xsl:value-of select="$gmId" /> <xsl:text> = new GMarker(</xsl:text>
                                                                <xsl:call-template name="GPoint">
                                                                  <xsl:with-param name="x">
                                                                    <xsl:value-of select="Point/@X" />
                                                                  </xsl:with-param>
                                                                  <xsl:with-param name="y">
                                                                    <xsl:value-of select="Point/@Y" />
                                                                  </xsl:with-param>
                                                                </xsl:call-template><xsl:if test="not(@IconId='')">, <xsl:value-of select="@IconId" /></xsl:if>);                                                             

    <xsl:value-of select="$gmId" />.id='<xsl:value-of select="@Id" />';
    <xsl:if test="$enableClientCallBacks=true()">
      GEvent.addListener(<xsl:value-of select="$gmId" />, 'click', window.GMarker_ServerClick);
    </xsl:if>
    GEvent.addListener(<xsl:value-of select="$gmId" />, 'click', window.GMarker_Click||_ef);
    GEvent.addListener(<xsl:value-of select="$gmId" />, 'infowindowopen', window.GMarker_InfoWindowOpen||_ef);
    GEvent.addListener(<xsl:value-of select="$gmId" />, 'infowindowclose', window.GMarker_InfoWindowClose||_ef);
    <xsl:value-of select="$jsId" />.addOverlay(<xsl:value-of select="$gmId" />);
  </xsl:template>
  
  <xsl:template match="GPolyline">
    <xsl:variable name="gpId" select="concat('gPolyline',position())" />
    var <xsl:value-of select="$gpId" /><xsl:text> = new GPolyline([</xsl:text><xsl:apply-templates select="GPoints" />], '<xsl:value-of select="@Color" />', <xsl:value-of select="@Weight" />, <xsl:value-of select="@Opacity" />);
    <xsl:value-of select="$gpId" />.id='<xsl:value-of select="@Id" />';
    <xsl:value-of select="$jsId" />.addOverlay(<xsl:value-of select="$gpId" />);
  </xsl:template>
  
  <xsl:template match="GPoints">
    <xsl:apply-templates select="GPoint" />
  </xsl:template>
  
  <xsl:template match="Controls">
    <xsl:for-each select="./*">
      <xsl:value-of select="$jsId" />.addControl(new <xsl:value-of select="name()" />());
    </xsl:for-each>
  </xsl:template>
  
  <xsl:template name="GSize" >
    <xsl:param name="w" />
    <xsl:param name="h" />
    <xsl:text>new GSize(</xsl:text><xsl:value-of select="$w" />,<xsl:value-of select="$h" /><xsl:text>)</xsl:text>
  </xsl:template>
  
  <xsl:template name="GPoint" >
    <xsl:param name="x" />
    <xsl:param name="y" />
    <xsl:text>new GPoint(</xsl:text><xsl:value-of select="$x" />,<xsl:value-of select="$y" /><xsl:text>)</xsl:text>
  </xsl:template>
</xsl:stylesheet>