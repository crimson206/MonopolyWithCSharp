# MonopolyWithCSharp

Learning how to manage the github page is also so difficult...
I decided to open the Monopoly Demo version on this master branch instead of only those well refactored codes.


1. For fun, please check the "prototpye.gif" file

2. If you want to try the demo version,
  - open this project with Visual Studio Code
  - write "dotnet build" on your command line and execute (without double quotation marks)
  - make your terminal as big as possible
  - press f5
  - I couldn't try my project with other IDEs yet...

3. Currently two different sized maps are serviced. The bigger map won't work with normal sized monitors.
   The default size is the smaller map. To change the size,
  - go to src/UI/Program.cs
  - change the value "isBoardSmall" to false
  
4. About future developing...
  - I refactored some lowest level classes to make my master clean, but it seems that I need to change them again,
    The game design is still changing frequently. Therefore, I want to focus on compeleting the design first for a while with necessary unittests.
  - For now I am not interested in adding the real human interface to play the game by myself.
    There is no reason for people to play the game in this platform (at least right now).
    I want to finish the design with machine learning AIs.
    If I am satisfied enough with the inteligence of them, it would be the time to add the interface to play against them.
    
 5. I will try to keep updating UML diagrams for current and planned designs in different aspects
