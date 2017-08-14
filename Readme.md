vaConnect application was written with C#, using Visual Studio 2017.
On that moment vaConnect  is able to set profiles for TTLS and PEAP connections. For TLS connection it can install certificates with profile. 
Important – it is highly recommended to start application in admin mode and register it as handler for vaconnect: protocol.

The application is implemented as a single instance application, meaning that there will only be one instance of vaConnect.exe running at any time.

To manage and control profiles application use managedwifi package.