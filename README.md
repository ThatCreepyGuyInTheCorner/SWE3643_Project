# Introduction

This is a calculator web application made for SWE3643 2024 fall semester.
## Table of Contents

## Team Members
All the work on this project was done by Trevor Green.

## Architecture
The project is split into 3 (working) modules:

 1. The Calculation Module
 2. The Unit Test Module
 3. The Web Api Module

Both the Web Api and Unit Test Modules reference the Calculation module.

![A diagram displaying the project archetecture.](https://camo.githubusercontent.com/f57304556cc21eddcfd7c21474cb0596085f0199564adfa349c9eba9a4cd2822/68747470733a2f2f7777772e706c616e74756d6c2e636f6d2f706c616e74756d6c2f7376672f664c4448517a696d3437787468705a51627571695673324371517839513455775853526a4f306f43416a7a53694f6a615442417043466856667943385177634a625f65586f643756564e567477544335312d4c336a774c3234526454726c6d6e6a5963414c6b6456676134756b72416145724231554c59766e635975544e4c3755305264503967647934575435647942593979636e476d425045724436716f4663773232754d30716d753247526d4f4659616d544379762d78676f4d6179566971454a5f6e37454879512d4348554e6c694637383732366c6664752d6a556f7a4447354176433347366d4c56393775374e75535a656a5278343944395a337341583239373662503831326d4b494f4e506a34524c6654524f31657578543759426f6376326c53322d6b6946475553695f446a6676564644736537575847784e536a63365f596249664f45637a797538567a33694f504a785f453374364446496d744c37506c6b435a524433747745366f52477a546164493937614259434e756d6b436c324173553354714a654d4b52464f7661636d4c796334632d5545527268336670557a43595949726c4649705546482d51464451647a70695544336f6d31656f4c3659625851456c64396c3036703337315f4b7963356b627a593255476750503335376441724d4f4b4139314d5a3378776269654e584469774c33333750767952737964484572774d3755366d3561492d4e79707253622d4c5175324a675f42486d5a51656350385046746b714f6c4f59686f4e4b72643654324650615147486b6c32475650695739385f2d3333594e51487a6d396a744d7466376d3030)
## Environment
This is a cross-platform application and should work in Windows 10+, Mac OS, and most Linux environments. The application has only been tested in Windows 11.

Before running this application make sure you have the latest Java runtime installed for your system. 
https://www.java.com/en/download/manual.jsp

## Executing the Web Application
To run this application:

1. Download the project source code off GitHub.
2. Run the Start.bat file and allow it to finish running.
3. Open your web browser of choice and enter 'localhost:7015' into your search bar.
If you run into any issues, open the command interface in the project's downloaded file location and run the following command:
```
dotnet run --project ./src/SWE3643_Project/WebApi/WebApi.csproj
```

## Executing Unit Tests
To run Unit tests on this project, navigate from the project's root folder > src >SWE3643_Project > PlaywrightTests

Once you've found that folder, open the command interface and run the following command:

    dotnet test

## Video Presentation
The video presentation of this project can be found at the following link:
https://youtu.be/lRK5qkAvrHY

