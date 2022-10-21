# MonopolyWithCSharp

In Master Branch
  - BoardHandler
  - DoubleSideEffectHandler
  - JailHandler

Currently Working on
1. Waiting feedback
  - BankHandler
    : Handlers in master were refactored by myself as much as possible.
      This handler was refactored together, but I decided to keep it out of the master to ask for a feedback.
      All the handlers will have to be refactored again based on the feedback later.

2. Designing Event Flow.
  - Basic Concept
    : Event class has functions of all events such as Move(), RollDice(), LandOnTile() and so on.
      Event and Game classes share Delegator class with each other.
      The event passes an event function to the delegator, and the game runs the function using delegator instead of the event.
      A new event function is passed to the delegator when the previous function is called.
  - TestEvent1
    : In the previous design, Event classes behaved as if they are functions. The problem was mainly refactored with the TestEvent1 class.
  - TestEvent2
    : Some functions of TestEvent1 do more than their name indicate.
      Especially they not only run events, but also pass new events to the delegator.
      TestEvent2 class has the CallNextEvent() function, where all the logics to connect events are written.
      Every event does what their name indicates, and just call CallNextEvent().
3. Thinking about AI for trade and actuion...