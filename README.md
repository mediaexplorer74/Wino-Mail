# Wino-Mail v1.9.1-alpha - modern branch 
![](Images/logo.png)

My little RnD of Burak Kaan Köse's [AllInOneApp](https://github.com/bkaankose/Wino-Mail) uwp app (v1.9.1). Goal is to build the handsome universal mail app for my Windows 11 Lite "desktop OS" :)


## Progress / Status
- Successfully assembled build 1.9.1 (Min. Wi. SDK = 17763)


## Screenshots
![](Images/sshot01.png)
![](Images/sshot02.png)


## Problems
- Adding Outlook account failed. Diagnosys:
[... ERR] Failed to connect to the server.
System.UnauthorizedAccessException: Access is denied. (Exception from HRESULT: 0x80070005 (E_ACCESSDENIED))
   at Wino.Core.UWP.Services.WinoServerConnectionManager.ConnectAsync()
[... INF] Server connection status changed to "Failed".
Exception thrown: 'System.NullReferenceException' in Wino.Mail.ViewModels.dll
Object reference not set to an instance of an object. 

## TODO
- Fix adding account bug (Server access?)

## References
 - Repo URL (original project): https://github.com/bkaankose/Wino-Mail
-  Developer (original project): [Burak Kaan Köse](https://github.com/bkaankose/)


## ..
AS IS. No support. RnD only. DIY.

## .
- [M][E] July, 1 2025


