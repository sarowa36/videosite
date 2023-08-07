

# Introduction
I started to produce this project arbitraril and currently not finish. You can [check](http://sarowa36.com.tr) live example. Based from [Turkish Movie Website](https://www.turkanime.co/). Please if you have any recommandation share with me.

# Features Im Working On
 - Message (90% done)
 - Content Filter (I did only css design) 
 - Comment (as soon as possible i will add comment reply and like/dislike)
 - WatchCount and Content Anchor
 

# Installation

 1. Clone Project 
 2. Open Cmd and open "client" folder from cmd (like cd C:\Users\[our username]\Desktop\videosite\client)
 3. Write "npm install" to cmd
 4. Return main directory and open "VideoSite" folder (like C:\Users\[our username]\Desktop\videosite\VideoSite")
 5. Create appsettings.json and write `{
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
}`
 6. (Attention if you get a error about DbConnection try to add this config to your connection string `Trusted_Connection=True; TrustServerCertificate=True; Encrypt=False`)
 7. Now everything its done

# How To Run

 1. Open cmd and open "client" folder from cmd
 2. Write this command: `npm run dev`
 3. Open new cmd and open "VideoSite" (attention main project and solution have same name. You should open ".\videosite\VideoSite")
 4. Write this command: `dotnet watch run`
 5. Open https://localhost:7292 url in your browser

# How To Publish
1. Open cmd and open "client" folder from cmd
 2. Write this command: `npm run build`
 3. Open new cmd and open "VideoSite" (attention main project and solution have same name. You should open ".\videosite\VideoSite")
 4. Write this command: `dotnet publish`
 5. Copy ".\videosite\client\dist" to ".\videosite\VideoSite\bin\Debug\net7.0\publish\" folder 
 6. Upload "\videosite\VideoSite\bin\Debug\net7.0\publish" to your server

