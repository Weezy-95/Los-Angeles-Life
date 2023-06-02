# Los-Angeles-Life
Es handelt sich hierbei um ein alt:V Hardcore Roleplay Script.


## Anforderungen
### Backend
- [Net 6.0 SDK - latest](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.408-windows-x64-installer)
- [Net 6.0 APS .NET Core Runtime - latest](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-6.0.16-windows-x64-installer)

### Frontend
- [Node JS 18.16.0 - latest](https://nodejs.org/dist/v18.16.0/node-v18.16.0-x64.msi)

### Datenbank
- [MySQL Community Server & Workbench](https://dev.mysql.com/downloads/file/?id=518834)

### Git
- [Git Fork](https://git-fork.com) oder was du bevorzugst


## Installation
1. Downloade die o.g. Anforderungen.
2. Klone das `git` Projekt in dein gewünschtes Verzeichnis.
3. Richte die MySQL Datenbank über den Installer ein.
4. Öffne das C# Projekt und ändere die MySQL Daten im Ordner Handlers die Klasse `DatabaseHandler`.
5. Öffne nun den Client Ordner und führe im Terminal den `npm i`Befehl aus und anschließen `npm run build`.
6. Nun kannst du im C# Projekt das Projekt builden und dich in alt:V unter `127.0.0.1`verbinden.
