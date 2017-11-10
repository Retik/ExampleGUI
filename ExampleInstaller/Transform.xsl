<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:wix="http://schemas.microsoft.com/wix/2006/wi">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>

  <xsl:template match="wix:Wix">
    <xsl:copy>
      <xsl:processing-instruction name="include">Include.wxi</xsl:processing-instruction>
      <xsl:apply-templates/>
    </xsl:copy>
  </xsl:template>

  <xsl:key name="xml-search" match="wix:Component[contains(wix:File/@Source, '.xml')]" use="@Id"/>
  <xsl:template match="wix:Component[key('xml-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('xml-search', @Id)]"/>

  <xsl:key name="pdb-search" match="wix:Component[contains(wix:File/@Source, '.pdb')]" use="@Id"/>
  <xsl:template match="wix:Component[key('pdb-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('pdb-search', @Id)]"/>

  <xsl:key name="log-search" match="wix:Component[contains(wix:File/@Source, '.log')]" use="@Id"/>
  <xsl:template match="wix:Component[key('log-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('log-search', @Id)]"/>

  <xsl:strip-space elements="*" />
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()" />
    </xsl:copy>
  </xsl:template>
</xsl:stylesheet>