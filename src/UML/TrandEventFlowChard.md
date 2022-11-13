```mermaid
flowchart TB
    A[MainEvent] -- MainEvent called TradeEvent --> B[StartTrade : \n starts trades from the current player]
    B --> C[SelectTradeTarget : \n the player selects a target player]
    C --> D[SuggestTradeOwnerTradeCondition : \n 1. a property from the target \n 2. a property from me \n 3. additional money - positive  or negative]
    D --> E[MakeTargetDecisionOnTradeAgreement : \n the target agree or disagree]
    E --> F{agreed}
    F -- Yes --> G[DoTrade : \n exchange items]
    F -- No --> H{all player \n tried to trade}
    G --> H
    H -- No --> I[ChangeTradeOwner : \n another player can try to trade]
    I --> C
    H -- Yes --> J[EndTrade]
    J -- Call the MainEvent --> K[MainEvent]
```
