```mermaid
classDiagram
class Property{
    +ChangeOwnerPlayerNumber(int? playerNumber)
}
class Tile{
    +string Name
}
class DataCenter{
    +List< ITileData > TileDatas
    +List< IPropertyData > PropertyDatas
}
class IProperty{
    <<interface>>
    +ChangeOwnerPlayerNumber(int? playerNumber)
}
class IPropertyData{
    <<interface>>
    +int Price
    +int Rent
}
class ITile{
    <<interface>>
}
class ITileData{
    <<interface>>
    +string Name
}

Tile ..|> ITile
ITile --|> ITileData
Property --|> Tile
IProperty --|> IPropertyData
IProperty --|> ITile
IPropertyData --|> ITileData
Property ..|> IProperty

ITileData <-- DataCenter
IPropertyData <-- DataCenter
```
