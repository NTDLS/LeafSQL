[Setup]
;-- Main Setup Information
 AppName                          = LeafSQL
 AppVerName                       = LeafSQL 1.0.0.0
 AppCopyright                     = Copyright © 1995-2019 NetworkDLS.
 DefaultDirName                   = {pf}\NetworkDLS\LeafSQL
 DefaultGroupName                 = NetworkDLS\LeafSQL
 UninstallDisplayIcon             = {app}\IDE\LeafSQL.UI.exe
 PrivilegesRequired               = admin
 Uninstallable                    = Yes
 MinVersion                       = 0.0,5.0
 Compression                      = bZIP/9
 ChangesAssociations              = Yes
 OutputBaseFilename               = LeafSQL_Client
 ArchitecturesInstallIn64BitMode  = x64

;-- Windows 2000 & XP (Support Dialog)
 AppPublisher    = NetworkDLS
 AppPublisherURL = http://www.NetworkDLS.com/
 AppUpdatesURL   = http://www.NetworkDLS.com/
 AppVersion      = 1.0.0.0

[Files]
 Source: "..\LeafSQL.UI\bin\Release\LeafSQL.UI.exe"; DestDir: "{app}\IDE";                  Flags: IgnoreVersion;
 Source: "..\LeafSQL.UI\bin\Release\*.dll";          DestDir: "{app}\IDE";                  Flags: IgnoreVersion;
 Source: ".\Highlighters\*.*";                       DestDir: "{app}\IDE\Highlighters";     Flags: IgnoreVersion;

[Icons]
 Name: "{group}\LeafSQL Manager";          Filename: "{app}\IDE\LeafSQL.UI.exe";
 Name: "{commondesktop}\LeafSQL Manager";  Filename: "{app}\IDE\LeafSQL.UI.exe";
 Name: "{userdesktop}\LeafSQL Manager";    Filename: "{app}\IDE\LeafSQL.UI.exe";

[Registry]
;-- Create SS "Leaf SQL Script" Association
 Root: HKCR; Subkey: ".lsql"; Flags: uninsdeletekey;
 Root: HKCR; Subkey: ".lsql"; ValueType: string; ValueData: "LeafSQLScript";
 Root: HKCR; Subkey: "LeafSQLScript"; Flags: uninsdeletekey;
 Root: HKCR; Subkey: "LeafSQLScript"; ValueType: string; ValueData:"LeafSQL Script";
 Root: HKCR; Subkey: "LeafSQLScript\Shell"; ValueType: string; ValueData:"Open";
 Root: HKCR; Subkey: "LeafSQLScript\Shell\Open\Command"; Flags: uninsdeletekey; 
 Root: HKCR; Subkey: "LeafSQLScript\Shell\Open\Command"; ValueType: string; ValueData:"""{app}\IDE\LeafSQL.UI.exe"" ""%1""";
 Root: HKCR; Subkey: "LeafSQLScript\DefaultIcon"; Flags: uninsdeletekey;
 Root: HKCR; Subkey: "LeafSQLScript\DefaultIcon"; ValueType: string; ValueData:"{app}\IDE\LeafSQL.UI.exe,1";

 Root: HKLM; Subkey: "Software\NetworkDLS\LeafSQL";                                                             Flags: uninsdeletekey noerror;
 Root: HKLM; Subkey: "Software\NetworkDLS\LeafSQL\"; ValueType: string; ValueName: "Path";   ValueData:"{app}"; Flags: CreateValueIfDoesntExist;

;[Tasks]
; Name: DesktopIcon;        Description: "Create a &desktop icon"; GroupDescription: "Additional icons:"; Components: SSIDE
; Name: DesktopIcon\Common; Description: "For all users"; GroupDescription: "Additional icons:"; Components: SSIDE; Flags: exclusive
; Name: DesktopIcon\User;   Description: "For the current user only"; GroupDescription: "Additional icons:"; Components: SSIDE; Flags: exclusive unchecked

;[Run]
; Filename: "{app}\IDE\SSIDE.exe"; Description: "Run IDE now?"; Flags: postinstall nowait skipifsilent shellexec; Components: SSIDE;
