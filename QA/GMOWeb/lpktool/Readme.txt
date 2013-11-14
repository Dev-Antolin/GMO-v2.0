===============================================
License Package Authoring Tool - LPK_Tool
================================================
Last Updated: July 20, 2001

DESCRIPTION
============
A licensed ActiveX control does not load properly in an Internet Explorer HTML page if the computer is not licensed to use the control. For example, if you use Microsoft® Visual C++® to build the control, the HTML page loads properly with the control on the computer where it was built, but it will not load correctly on a different computer.

BROWSER/PLATFORM COMPATIBILITY
===============================
The LPK_Tool is supported in Microsoft® Internet Explorer 3
or later on Microsoft® Windows 95® or later; or Microsoft® Windows NT® 4.0 or later.
Requires 300K Disk Space with a File Size of 99.5 KB

USAGE
========
LPK_TOOL displays two list boxes with the following control buttons. 

Available Controls List Box
The first list box, Available Controls, lists all controls registered 
in your system. 

Controls in License Package List Box
The second list box, Controls in License Package, lists all 
controls, in which licensing information, if available, is stored in 
the LPK file. The licensing information is save when you click 
the Save & Exit button.

Add Button
In the Available Control List Box, select controls and 
click the Add button to add them to the LPK file. 
LPK_TOOL responds by moving the selected controls to the Controls 
in License Package List Box.

Remove Button
In the Controls in License Package List Box, select controls 
and click the Remove button so they will be removed from the 
LPK file. The LPK_TOOL responds by moving the selected controls 
to the Available Control List Box.

Save and Exit Button
The Save & Exit button saves the licensing 
information to the LPK file. LPK_TOOL prompts for a file name and 
save the Licensing information with the file name provided. LPK_TOOL 
terminates after saving the file.

Cancel Button
The Cancel button terminates the application without creating the  
LPK file.

About Button
The About button displays the version information. 

Help button
The Help button displays the Help file for the tool. 

Show only Controls that support Licensing Check Box
When checked, LPK_TOOL displays only those controls that support 
licenses (IClassFactory2) interface.

Note:
This tool makes use of the License Manager components. Please make 
sure the License Manager is installed on your system and registered 
before you use this tool.


SOURCE FILES
=============
LPK_Tool.exe

OTHER FILES
============
LPK_Tool.gid
LPK_Tool.hlp
ReadMe.txt

FURTHER REFERENCE
===================
Microsoft Internal refer to: http://support.microsoft.com/support/kb/articles/Q159/9/23.ASP

=========================
© Microsoft Corporation


