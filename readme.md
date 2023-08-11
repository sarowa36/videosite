

# Introduction
I started to produce this project arbitraril and currently not finish. You can [check](http://sarowa36.com.tr) live example. Based from [Turkish Movie Website](https://www.turkanime.co/). Please if you have any recommandation share with me.

# Features Im Working On
 - LazyLoad ( Message :white_check_mark:, Content :x:)
 - Message ui/ux improvments
 - Content Filter (I did only css design) 
 - Comment reply and Comment like/dislike
 - WatchCount and Content Anchor
 - General ui/ux improvments
 - Seo 

# Dependencies
 - [MSSQL](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) & [MSSQl Managament Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
 - [npm & node.js](https://nodejs.org/en/download/current)
 - [Net 7](https://dotnet.microsoft.com/en-us/download/dotnet)

# Installation

 1. Clone Project 
 2. Install "database.bak" file in your mssql
 2. Open Cmd and open "client" folder from cmd (like cd C:\Users\[our username]\Desktop\videosite\client)
 3. Write "npm install" to cmd
 4. Return main directory and open "VideoSite" folder (like C:\Users\[our username]\Desktop\videosite\VideoSite")
 5. Create appsettings.json (example: `{
  "ConnectionStrings": {
    "Default": "[Your Connection string]"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}`)
 6. (Attention if you get a error about DbConnection try to add this config to your connection string `Trusted_Connection=True; TrustServerCertificate=True; Encrypt=False`)
 7. Now everything its done
 
# How To Run

 1. Open cmd and open "client" folder from cmd
 2. Type the command: `npm run dev` in cmd
 3. Open new cmd and open "VideoSite" (attention main project and solution have same name. You should open ".\videosite\VideoSite")
 4. Type the command: `dotnet watch run` in cmd
 5. Open https://localhost:7292 url in your browser

# How To Publish
1. Open cmd and open "client" folder from cmd
 2. Write this command: `npm run build`
 3. Open new cmd and open "VideoSite" (attention main project and solution have same name. You should open ".\videosite\VideoSite")
 4. Write this command: `dotnet publish`
 5. Upload "\videosite\VideoSite\bin\Debug\net7.0\publish" to your server

