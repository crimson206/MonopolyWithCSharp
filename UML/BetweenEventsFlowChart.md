This flow chart shows the order of events.

Events with semicolons should be refactored. 
1. MainEvent should be devided into more pieces.
2. Two different auction events can be derived from the current AuctionEvent

```mermaid
flowchart TB

    A[MainEvent: \n StartTurn]
    B[AuctionEvent \n FreePropertyAuctionEvent]
    C[MainEvent: \n LandEvent]
    D[SellItemEvent]
    E[MainEvent: \n EndTurn]
    F[TradeEvent]
    G[HouseBuildEvent]
    H[UnmortgageEvent]
    I[MainEvent: \n StartTurn]
    J[AuctionEvent \n OwnedPropertyAuctionEvent]

    a{didn't buy \n a property \n after landing it?}
    b{is \n a player's balancce \n minus \n after \n paying a rent?}
    c{is \n the turn \n rotated?}
    d{decided to \n auction \n a property?}

    A --> C
    C -- a player landed on \n a free property --> a
    a -- yes --> B
    a -- no --> E
    B --> E
    C -- a player landed \n on an ocupied property--> b
    b -- yes --> D
    b -- no --> E
    D --> d
    d -- yes --> J
    d -- no --> E
    J --> E
    E --> c
    c -- yes --> F
    c -- no --> G
    F --> G
    G --> H
    H --> I

```

