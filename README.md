# MonopolyWithCSharp

(RIP) I am recently focusing on German language and the knowledge of C#.
Although I can work on this project parallely with the language,
I can't write good codes without enough background of C#.
I recently study here, <a href="https://dotnettutorials.net/course/csharp-dot-net-tutorials/">https://dotnettutorials.net/course/csharp-dot-net-tutorials/</a>.
I will be back when I can write codes that I, in the future, would want to reuse.

0. There are UML diagrams below

1. For fun, please check the "prototpye.gif" file

2. If you want to try the demo version,
  - open this project with VSCode
  - execute "dotnet build" in your terminal command line (of course without the double quotation marks)
  - make your terminal as big as possible
  - press f5
  - It is checked that some lines are not working in Visual Studio, therefore Visual Studio Code is only checked if it is executable for now.

3. Current features
  - AI game simulation
  - repeating turns
  - purchasing property
  - paying rent
  - auction event
  - trade event
  - house build event
  - sell items event
  - unmortgage event

4. Planned features
  - Monopoly with WPF
  - Machine Learning applied AIs

5. Although the game works to the end, there are many designs to refactor and tests to add.
  The tasks can be pushed back to develop ML first.


The class diagram for the current Monopoly Demo version
```mermaid
classDiagram

class UI{
+Visualizer
+Game
}

class Visualizer{
+Visualize()
}

class Game{
+Run()
}

class DataCenter{
+IBoardHandlerData
+IBankHandlerData
+IMoreHandlersData
}

class Delegator{
-nextEvent << delegate or generally function pointer >>
+AddNextEvent()
+RunEvent()
}

class Event{
+RollDice()
+MoveByRollDiceResultTotal()
+PayJailFine()
+BuyProperty()
+MoreEventMethods()
+CallNextEvent()
}

class BoardHandler{
+List~int~ Positions 
+List~bool~ PassedGos 
+MoveAroundBoard(playerNumber int, amount int)
+Teleport(playerNumber int, point int)
}

class IBoardHandlerData
<<interface>> IBoardHandlerData
IBoardHandlerData : List~int~ Positions
IBoardHandlerData : List~bool~ PassedGos

UI --> Game
UI --> Visualizer
Visualizer ..> DataCenter
Event --> Delegator
Game *--> Delegator
Event --> "*" BoardHandler
BoardHandler ..|> IBoardHandlerData
DataCenter --> "*" IBoardHandlerData
```

The sequence diagram for the current Monopoly Demo version
  - Other handlers like BankHandler in the diagram play similar rules at the same position.
```mermaid
sequenceDiagram
UI->>Game : Run
activate Game
Game->>Delegator : RunEvent
activate Delegator
Delegator->>Event : PayRent
activate Event
Event->>Property : GetRent
activate Property
Property-->>Event : Rent
deactivate Property
Event->>BankHandler : DecreaseBalance
activate BankHandler
deactivate BankHandler
Event->>Event : SetRecommendedString
Event->>Delegator : AddNextEvent
deactivate Delegator
deactivate Event
deactivate Game
UI->>DataCenter : GetRecommendedString
activate DataCenter
DataCenter->> Event : GetRecommendedString
activate Event
Event-->>DataCenter : RecommendedString
deactivate Event
DataCenter-->>UI : RecommendedString
deactivate DataCenter
opt RecommendedString was changed
  UI->>Visualizer : Visualize
  activate Visualizer
  Visualizer->>DataCenter : GetData
  activate DataCenter
  DataCenter->>BankHandler : GetData
  activate BankHandler
  BankHandler-->>DataCenter : Data
  deactivate BankHandler
  DataCenter-->>Visualizer : Data
  deactivate DataCenter
  Visualizer->>Console : WriteLines
  deactivate Visualizer
end
UI->>Game : Run
```


DONTREADME :
Ha... I still think it is not difficult to make Monopoly if my goal is just to make it work.
It is totally different if I try to make it perfect as much as possible.
I began to use interfaces quite recently, and unfortunately I already wrote lots of lines without them.
It is literally imposible to make unit tests for high-level classes without them.
My Event class makes it worse because it is too large to test properly.
It is why I have to refactor them before studying the machine learning for AI.
As hard as I recently studied, I learned a lot, and I like to apply new knowledge,
however things to learn are infinite and time to learn is finite. :(
